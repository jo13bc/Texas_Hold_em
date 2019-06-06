using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Evaluador.Combinaciones
{
    interface ICombinatorial
    {
        long combinations();
        int size();
        void clear();
        int[] next(int[] items);
        bool hasNext();
    }
}
