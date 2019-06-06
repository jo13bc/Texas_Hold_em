using Servidor.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Jugador
{
    interface IGameController
    {
        void setSettings(Settings settings);
        bool addStrategy(IStrategy strategy);
        void start();
        void waitFinish();
    }
}
