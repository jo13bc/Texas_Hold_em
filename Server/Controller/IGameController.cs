using Servidor.Configurations;
using Servidor.Jugador;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Controller
{
    public interface IGameController
    {
        void setSettings(Settings settings);
        bool addStrategy(IStrategy strategy);
        void start();
        void waitFinish();
    }
}
