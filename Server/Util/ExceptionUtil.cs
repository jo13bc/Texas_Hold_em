using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Exception
{
    class ExceptionUtil
    {
        public static readonly String NULL_ERR_MSG = "El argumento {0} no puede ser nulo.";
        public static readonly String LENGTH_ERR_MSG = "El argumento {0} no puede ser nulo y debe tener una longitud de {1}.";

        private ExceptionUtil(){}

        public static void checkNullArgument(Object o, String name)
        {
            if (o == null)
            {
                throw new ArgumentException(String.Format(NULL_ERR_MSG, name));
            }
        }

        public static  void checkArrayLengthArgument<T>(T[] a, String name, int l)
        {
            if(a == null || a.Length == 1)
            {
                throw new ArgumentException(String.Format(LENGTH_ERR_MSG, name,l));
            }
        }

        public static void checkArgument(Boolean throwEx, String msg, Object args)
        {
            if (throwEx)
            {
                throw new ArgumentException(String.Format(msg, args));
            }
        }
    }
}
