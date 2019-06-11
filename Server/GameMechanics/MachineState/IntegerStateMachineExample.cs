using Servidor.GameMechanics.MachineState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.MachineState
{
    class IntegerStateMachineExample
    {
        static void Main(string[] args)
        {
            StateMachine<int> sm = new StateMachine<int>();

            IntState state1 = new IntState("State 1");
            IntState state2 = new IntState("State 2");
            IntState state3 = new IntState("State 3");
            IntState state4 = new IntState("State 4");

            sm.setInitState(state1);

            sm.addTransition(state1, state2, (n) => (n % 2) == 0);
            sm.addTransition(state1, state3, (n) => (n % 3) == 0);
            sm.setDefaultTransition(state1, state4);

            StateMachineInstance<int> smi = sm.startInstance(6);
        }
    }

    class IntState : IState<int>
    {
        private string name;

        public IntState(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public bool execute(int context)
        {
            return true;
        }
    }
}
