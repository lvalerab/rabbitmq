using System.IO;
using Newtonsoft.Json;

namespace CoreDomain.Utils.Json
{   public class JsonSerialize<T>
    {
        public byte[] GetJsonStr(T valor)
        {
            byte[] datos = null;
            JsonSerializer js = new JsonSerializer();
            MemoryStream ms = new MemoryStream();
            TextWriter tw = new StreamWriter(ms);
            js.Serialize(textWriter: tw, valor);
            datos = ms.ToArray();
            tw.Close();
            ms.Close();
            return datos;
        }

        public T GetObjectByt(byte[] datos)
        {
            T resultado;
            JsonSerializer js = new JsonSerializer();
            MemoryStream ms = new MemoryStream();
            ms.Write(datos, 0, datos.Length);
            TextReader tr = new StreamReader(ms);
            resultado = (T)js.Deserialize(reader: tr, typeof(T));
            return resultado;
        }
    }
}
