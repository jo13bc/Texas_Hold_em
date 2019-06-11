using Servidor.Cartas;
using Servidor.Configurations;
using Servidor.GameMechanics.MachineState;
using Servidor.Hand;
using Servidor.Jugador;
using Servidor.Logic.Player;
using Servidor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Servidor.Utility.TexasHoldem.TexasHoldEmUtil;

namespace Servidor.GameMechanics.State
{
    class ShowDownState : IState<ModelContext>
    {
        public static readonly String NAME = "ShowDown";

        public bool execute(ModelContext model)
        {
            List<PlayerEntity> players = calculateHandValue(model.getCommunityCars(), model.getPlayers());
            Dictionary<long, List<PlayerEntity>> indexByBet = new Dictionary<long, List<PlayerEntity>>();

            //List<PlayerEntity> lista = agrupar( players);


            //List<long> inverseSortBets = inverseSortBets, (10, 11), ->long

            return false;

        }

        public List<long, List<PlayerEntity>> agrupar(List<PlayerEntity> players)
        {
            List<PlayerEntity> result = new List<PlayerEntity>();

            //Metodo de agrupar
            return null;
        }

        public string getName()
        {
            return NAME;
        }

        private List<PlayerEntity> calculateHandValue(List<Card> cc, List<PlayerEntity> players)
        {
            Hand7Evaluator evaluator = new Hand7Evaluator(new HandEvaluator());
            evaluator.setCommunityCards(cc);
            foreach (PlayerEntity p in players)
            {
                p.setHandValue(evaluator.eval(p.getCard(0), p.getCard(1)));
            }
            return players;
        }
    }
}
