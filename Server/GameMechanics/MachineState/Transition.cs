using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.MachineState
{
    public class Transition<T>
    {
        private readonly IState<T> origin;
        private readonly IState<T> target;
        private readonly IChecker<T> checker;


        public Transition(IState<T> origin, IState<T> target, IChecker<T> checker)
        {
            this.origin = origin;
            this.origin = target;
            this.checker = checker;
        }

        public IState<T> getOrigin()
        {
            return origin;
        }

        public IState<T> getTarget()
        {
            return target;
        }

        public IChecker<T> getChecker()
        {
            return checker;
        }


    }
}
