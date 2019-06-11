using Servidor.GameMechanics.MachineState;
using Servidor.Logic.Player;
using Servidor.Model;
using System;
using System.Collections.Generic;


namespace Servidor.GameMechanics.State
{
    class EndState : IState<ModelContext>
    {

        public static readonly String NAME = "EndHand";
        public bool execute(ModelContext context)
        {

            PlayerEntity dealearPlayer = context.getPlayer(context.getDealer());
            List<PlayerEntity> players = context.getPlayers();
            
            List<PlayerEntity> nextPlayers = new List<PlayerEntity>(players.Count);

            int dealerIndex = 0;
            int i = 0;

            foreach (PlayerEntity p in players){
                if (p.getChips()>0) {
                    nextPlayers.Add(p);
                    i++;
                }

                if (dealearPlayer==p)
                {
                    dealerIndex = i - 1;
                }


               
            }
            
            context.setDealer(dealerIndex);
            context.setPlayers(nextPlayers);
            return true;

        }

        public string getName()
        {
            return NAME;
        }
    }
}
