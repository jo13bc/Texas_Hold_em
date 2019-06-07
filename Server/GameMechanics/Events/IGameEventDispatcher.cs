using System.Threading;

namespace Servidor.Events
{
    interface IGameEventDispatcher
    {
        void dispatch(GameEvent gameEvent);
        void exit();
    }
}
