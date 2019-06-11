using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.MachineState
{
    public class IChecker<T>
    {
        public delegate bool check(T context);
    }
}



