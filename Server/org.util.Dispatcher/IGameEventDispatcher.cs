using System.Threading;

namespace Servidor.org.util.Dispatcher
{
    interface IGameEventDispatcher
    {
        void dispatch(GameEvent gameEvent);
        void exit();
    }
}
