using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.MachineState
{
    class BeforeStateDecorator<T> : IState<T>
    {
        private readonly IState<T> state;
        private readonly Thread thread;
        private bool executed = true;


        public BeforeStateDecorator(IState<T> state, Thread thread)
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
            if (executed)
            {
                thread.Start();
            }

            executed = state.execute(context);
            return executed;
        }
    }
}
