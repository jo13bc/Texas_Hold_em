using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.MachineState
{
    class StateDecoratorBuilder<T> 
    {
        private IState<T> state;

        private StateDecoratorBuilder(IState<T> state)
        {
            this.state = state;
        }

        public static StateDecoratorBuilder<T> create(IState<T> state)
        {
            return new StateDecoratorBuilder<T>(state);
        }

        public StateDecoratorBuilder<T> after(Thread t)
        {
            this.state = new BeforeStateDecorator<T>(state, t);
            return this;
        }

        public StateDecoratorBuilder<T>before(Thread t)
        {
            this.state = new BeforeStateDecorator<T>(state, t);
            return this;
        }

        public IState<T> build()
        {
            return state;
        }

        public static IState<T> after(IState<T> state, Thread t)
        {
            return new AfterStateDecorator<T>(state, t);
        }

        public static IState<T> before(IState<T> state, Thread t)
        {
            return new BeforeStateDecorator<T>(state, t);
        }
    }
}
