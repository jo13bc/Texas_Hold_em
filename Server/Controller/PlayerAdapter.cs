using Servidor.Configurations;
using Servidor.Jugador;
using Servidor.Logic.Player;
using Servidor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Controller
{
    public sealed class PlayerAdapter
    {
        private PlayerAdapter() { }

        public static List<PlayerInfo> toPlayerInfo(List<PlayerEntity> players, string name)
        {
            //List<PlayerInfo> result = Collections.emptyList();
            List<PlayerInfo> result = new List<PlayerInfo>();
            if (players != null)
            {
                //result = new ArrayList<>(players.size());
                result = new List<PlayerInfo>(players.Count);
                foreach (PlayerEntity pe in players)
                {
                    result.Add(copy(pe, pe.showCards() || pe.getName().Equals(name)));
                }
            }
            return result;
        }

        public static GameInfo<PlayerInfo> toTableState(ModelContext model, String name)
        {
            GameInfo<PlayerInfo> result = new GameInfo<PlayerInfo>();
            result.setCommunityCards(model.getCommunityCars());
            result.setDealer(model.getDealer());
            result.setGameState(model.getGameState());
            result.setPlayerTurn(model.getPlayerTurn());
            result.setRound(model.getRound());
            if(model.getSettings() != null)
            {
                result.setSettings(new Settings(model.getSettings()));
            }
            result.setPlayers(toPlayerInfo(model.getPlayers(), name));
            return result;
        }

        public static PlayerInfo copy(PlayerEntity p, bool copyCards)
        {
            PlayerInfo result = new PlayerInfo();
            result.setName(p.getName());
            result.setChips(p.getChips());
            result.setBet(p.getBet());
            if (copyCards)
            {
                result.setCards(p.getCard(0), p.getCard(1));
            }

            result.setState(p.getState());
            result.setErrors(p.getErrors());
            return result;
        }
    }
}
