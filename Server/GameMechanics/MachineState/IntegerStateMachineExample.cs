//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Servidor.GameMechanics.MachineState
//{
//    class IntegerStateMachineExample
//    {
//        static void Main(string[] args)
//        {
//            StateMachine<int> sm = new StateMachine<int>();
//            IntState state1 = new IntState("State 1");
//            IntState state2 = new IntState("State 2");
//            IntState state3 = new IntState("State 3");
//            IntState state4 = new IntState("State 4");
//            sm.setInitState(state1);
//            var r1 = ((6 % 2) == 0) ? 1 : 0;
//            var r2 = ((6 % 3) == 0) ? 1 : 0;
//            sm.addTransition(state1, state2, new InnClecker());
//            sm.addTransition(state1, state3, new InnClecker());
//            sm.setDefaultTransition(state1, state4);

//            StateMachineInstance<int> smi = sm.startInstance(6);
//        }
//    }

//    public sealed class InnClecker : IChecker<int>
//    {
//        public bool check(int context)
//        {
//            return (1 == context);
//        }
//    }

//    sealed class IntState : IState<int> {
//        private string name;

//        public IntState(string name)
//        {
//            this.name = name;
//        }

//        public string getName()
//        {
//            return name;
//        }

//        public bool execute(int context)
//        {
//            return true;
//        }
//    }
//}
