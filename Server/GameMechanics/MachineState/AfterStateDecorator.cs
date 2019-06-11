using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.MachineState
{
    class AfterStateDecorator<T> : IState<T> 
    {
        private readonly IState<T> state;
        private readonly Thread thread;


        public AfterStateDecorator(IState<T> state, Thread thread)
        {
            this.state = state;
            this.thread = thread;
        }


        public string getName()
        {
            return state.getName();
        }


        public bool execute(T context)
        {
            bool result = state.execute(context);
            if (result)
            {
                thread.Start();
            }
            return result;
        }
    }
}
