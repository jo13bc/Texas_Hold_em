using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.MachineState
{
    public class StateMachineInstance<T>
    {
        private readonly T context;
        private readonly StateMachine<T> parent;
        private IState<T> state;
        private bool finish;
        private bool pause;
        

        public StateMachineInstance(T context, StateMachine<T> parent, IState<T> state)
        {
            this.context = context;
            this.parent = parent;
            this.state = state;
            this.finish = false;
        }

        public bool isFinish()
        {
            return finish;
        }

        public StateMachineInstance<T> execute()
        {
            this.pause = false;
            while (state !=null && !pause)
            {
                state = executeState();
            }

            finish = state == null;
            return this;
        }

        public T getContext()
        {
            return context;
        }

        private IState<T> executeState()
        {
            pause = !state.execute(context);
            IState<T> result = state;
            if (!pause)
            {
                foreach(Transition<T> transition in parent.getTransitionsByOrigin(state))
                {
                    if (transition.getChecker().check(context))
                    {
                        return transition.getTarget();
                    }
                }
                result = parent.getDefaultTransition(state);
            }
            return result;
        }


    }
}
