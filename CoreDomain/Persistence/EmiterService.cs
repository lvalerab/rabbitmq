using CoreDomain.Contracts;
using CoreDomain.Utils.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;

namespace CoreDomain.Service
{
    public class EmiterService<T>:IEmitter<T>
    {

        private const string _URI_CONEXION = "amqp://guest:guest@localhost:5672";

        public void Emit(T objeto, string cola="default", string ruta="default", bool duradero=false, bool exclusivo=false, bool autoBorrado=false, IDictionary<string, object> argumentos=null)
        {
            ConnectionFactory conn = new ConnectionFactory();
            conn.Uri = new Uri(_URI_CONEXION);
            using(IConnection cn =conn.CreateConnection())
            {
                using(IModel ch=cn.CreateModel())
                {
                    JsonSerialize<T> jser = new JsonSerialize<T>();
                    byte[] datos = jser.GetJsonStr(objeto);

                    //Enviamos
                    ch.QueueDeclare(queue: cola,
                                    durable: duradero,
                                    exclusive: exclusivo,
                                    autoDelete: autoBorrado,
                                    arguments: argumentos);

                    ch.BasicPublish(exchange: "",
                                    routingKey: ruta,
                                    basicProperties: null,
                                    body: datos);
                }
            }
        }

        public void Receive(EventHandler<T> handler, string cola = "default", string ruta = "default", bool duradero = false, bool exclusivo = false, bool autoBorrado = false, IDictionary<string, object> argumentos = null)
        {
            
            IConnectionFactory cf = new ConnectionFactory();
            cf.Uri = new Uri(_URI_CONEXION);
            using(IConnection cn=cf.CreateConnection())
            {
                using (IModel ch = cn.CreateModel())
                {
                    ch.QueueDeclare(queue: cola,
                                    durable: duradero,
                                    exclusive: exclusivo,
                                    autoDelete: autoBorrado,
                                    arguments: argumentos);
                    var consumidor = new EventingBasicConsumer(ch);
                    consumidor.Received += (model, ea) =>
                    {
                        byte[] datos = ea.Body.ToArray();
                        JsonSerialize<T> js = new JsonSerialize<T>();
                        T valor;
                        valor = js.GetObjectByt(datos);
                        handler(this,valor);
                    };
                }
            }
        }
    }
}
