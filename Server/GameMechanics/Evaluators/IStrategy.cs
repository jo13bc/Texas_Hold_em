using Servidor.Cartas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Jugador
{
    public interface IStrategy
    {
        String getName();
        BetCommand GetCommand(GameInfo<PlayerInfo> state);
        void updateState(GameInfo<PlayerInfo> state);
        void check(List<Card> communityCards);
        void onPlayerCommand(String player, BetCommand betCommand);
    }
}
