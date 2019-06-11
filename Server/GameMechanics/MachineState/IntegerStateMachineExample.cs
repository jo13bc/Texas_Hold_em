//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Servidor.GameMechanics.MachineState
//{


//    public sealed class InnClecker : IChecker<int>
//    {
//        public bool check(int context)
//        {
//            return (1 == context);
//        }
//    }

//    sealed class IntState : IState<int>
//    {
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
