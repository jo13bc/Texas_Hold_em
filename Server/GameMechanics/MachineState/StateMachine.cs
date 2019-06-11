using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.MachineState
{
    public class StateMachine<T>
    {
        private IState<T> initState = null;
        private readonly Dictionary<string, IState<T>> defaultTransition = new Dictionary<string, IState<T>>();
        private readonly Dictionary<string, List<Transition<T>>> transitions = new Dictionary<string, List<Transition<T>>>();

        public StateMachine()
        {
        }
        public List<Transition<T>> getTransitionsByOrigin(IState<T> state)
        {
            List<Transition<T>> result = transitions[state.getName()];
            if (result == null)
            {
                result = new List<Transition<T>>();
            }
            return result;
        }
        public void setInitState(IState<T> initState)
        {
            this.initState = initState;
        }
        public IState<T> getDefaultTransition(IState<T> origin)
        {
            return defaultTransition[origin.getName()];
        }
        public void setDefaultTransition(IState<T> origin, IState<T> target)
        {
            this.defaultTransition[origin.getName()] = target;
        }
        public void addTransition(Transition<T> transition)
        {
            IState<T> origin = transition.getOrigin();
            List<Transition<T>> listTransitions = transitions[(origin.getName())];
            if (listTransitions == null)
            {
                listTransitions = new List<Transition<T>>();
                transitions.Add(origin.getName(), listTransitions);
            }
            listTransitions.Add(transition);
        }
        public void addTransition(IState<T> origin, IState<T> target, IChecker<T>.check checker)
        {
            addTransition(new Transition<T>(origin, target, checker));
        }
        public StateMachineInstance<T> startInstance(T data)
        {
            return new StateMachineInstance<T>(data, this, initState).execute();
        }

    }
}
