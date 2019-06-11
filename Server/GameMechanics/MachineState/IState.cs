using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.MachineState
{
    public interface IState<T>
    {
        string getName();
        bool execute(T context);
    }
}
