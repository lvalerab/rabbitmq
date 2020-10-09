using CoreDomain.Contracts;
using CoreDomain.Service;
using System;

namespace ConsumidorRabbit
{
    class Program
    {

        static void Main(string[] args)
        {
            string cola = args.Length>0?args[0]:"default";
            IEmitter<string> emisor = new EmiterService<string>();
            emisor.Receive((sender, t) =>
            {
                Console.WriteLine("Obtenido a las {0}, valor: {1}", DateTime.Now.ToString(), t);
            }, cola: cola);

            Console.WriteLine("Pulsa [Enter] para salir....");
            Console.ReadLine();
        }
    }
}
