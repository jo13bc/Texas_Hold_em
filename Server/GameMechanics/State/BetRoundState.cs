using Servidor.Cartas;
using Servidor.Configurations;
using Servidor.GameMechanics.MachineState;
using Servidor.Jugador;
using Servidor.Logic.Player;
using Servidor.Model;
using Servidor.Utility.TexasHoldem;
using System;
using System.Collections.Generic;
using System.Text;
using static Servidor.Utility.TexasHoldem.TexasHoldEmUtil;

namespace Servidor.GameMechanics.State
{
    class BetRoundState : IState<ModelContext>
    {

        delegate bool check(ModelContext m, PlayerEntity p, BetCommand c);

        public static readonly String NAME = "BetRound";

        private static readonly Dictionary<BetCommandType, check> CHECKERS = buildBetCommandChecker();

        private static Dictionary<BetCommandType, check> buildBetCommandChecker()
        {
            Dictionary<BetCommandType, check> result = new Dictionary<BetCommandType, check>();

            result.Add(BetCommandType.FOLD, (m, p, b) => true);
            result.Add(BetCommandType.TIMEOUT, (m, p, b) => false);
            result.Add(BetCommandType.ERROR, (m, p, b) => false);

            result.Add(BetCommandType.RAISE, (m, p, b) =>
                b.getChips() > (m.getHighBet() - p.getBet()) &&
                b.getChips() < p.getChips());


            result.Add(BetCommandType.ALL_IN, (m, p, b) =>
            {
                b.setChips(p.getChips());
                return p.getChips() > 0;
            });


            result.Add(BetCommandType.CALL, (c, p, b) =>
            {
                b.setChips(c.getHighBet() - p.getBet());
                return c.getHighBet() > c.getSettings().getBigBind();
            });


            result.Add(BetCommandType.CHECK, (c, p, b) =>
            {
                b.setChips(c.getHighBet() - p.getBet());
                return b.getChips() == 0 || c.getHighBet() == c.getSettings().getBigBind();
            });

            return result;
        }

        public bool execute(ModelContext model)
        {
            bool result = false;
            int playerTurn = model.getPlayerTurn();
            PlayerEntity player = model.getPlayer(playerTurn);
            BetCommand command = player.getBetCommand();
            if (command != null)
            {
                BetCommand resultCommand = command;
                player.setBetCommand(null);
                long betChips = 0;
                BetCommandType commandType = command.getType();
                if (CHECKERS[commandType](model, player, command))
                {
                    betChips = command.getChips();
                    player.setState(TexasHoldEmUtil.convert(command.getType()));
                }
                else
                {
                    commandType = BetCommandType.FOLD;
                    player.setState(PlayerState.FOLD);
                    if (command.getType() == BetCommandType.TIMEOUT)
                    {
                        resultCommand = new BetCommand(BetCommandType.TIMEOUT);
                    }
                    else
                    {
                        resultCommand = new BetCommand(BetCommandType.ERROR);
                    }
                    ModelUtil.incrementErrors(player, model.getSettings());
                }
                ModelUtil.playerBet(model, player, commandType, betChips);
                model.lastResultCommand(player, resultCommand);
                model.setPlayerTurn(ModelUtil.nextPalyer(model, playerTurn));
                result = true;
            }

            return result;
        }

        public string getName()
        {
            return NAME;
        }


    }
}
