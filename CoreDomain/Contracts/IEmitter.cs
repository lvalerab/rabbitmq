
using System;
using System.Collections.Generic;

namespace CoreDomain.Contracts
{
    public interface IEmitter<T>
    {
        void Emit(T objeto, string cola = "default", string ruta = "default", bool duradero = false, bool exclusivo = false, bool autoBorrado = false, IDictionary<string, object> argumentos = null);
        void Receive(EventHandler<T> handler, string cola = "default", string ruta = "default", bool duradero = false, bool exclusivo = false, bool autoBorrado = false, IDictionary<string, object> argumentos = null);

    }
}
