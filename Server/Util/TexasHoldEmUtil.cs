using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Utility.TexasHoldem
{
    class TexasHoldEmUtil
    {
        public static readonly int MIN_PLAYER = 4;
        public static readonly int PLAYER_CARDS = 2;
        public static readonly int MAX_PLAYERS = 4;
        public static readonly int COMMUNITY_CARDS = 5;
        public static readonly Dictionary<BetCommandType, PlayerState> PLAYER_STATE_CONVERSOR = buildPlayerStateConversor();

        private static readonly bool[] PLAYER_STATE = { true, false, false, true, true, true, false };
        public enum BetCommandType
        {
            ERROR, TIMEOUT, FOLD, CHECK, CALL, RAISE, ALL_IN
        }

        public enum GameState
        {
            PRE_FLOP, FLOP, TURN, RIVER, SHOWDOWN, END
        }

        public enum PlayerState
        {
            READY, OUT, FOLD, CHECK, CALL, RAISE, ALL_IN
        }

        public static bool getPlayerState(PlayerState state)
        {
            return PLAYER_STATE[Array.IndexOf(Enum.GetValues(state.GetType()), state)];
        }

        class Player
        {
            private readonly bool active;

            private Player(bool isActive)
            {
                this.active = isActive;
            }

            public bool isActive()
            {
                return active;
            }
        }

        private TexasHoldEmUtil()
        {

        }

        public static PlayerState convert(BetCommandType betCommand)
        {
            return PLAYER_STATE_CONVERSOR[betCommand];
        }

        private static Dictionary<BetCommandType, PlayerState> buildPlayerStateConversor()
        {
            Dictionary<BetCommandType, PlayerState> result = new Dictionary<BetCommandType, PlayerState>();
            result.Add(BetCommandType.FOLD, PlayerState.FOLD);
            result.Add(BetCommandType.ALL_IN, PlayerState.ALL_IN);
            result.Add(BetCommandType.CALL, PlayerState.CALL);
            result.Add(BetCommandType.CHECK, PlayerState.CHECK);
            result.Add(BetCommandType.RAISE, PlayerState.RAISE);
            result.Add(BetCommandType.ERROR, PlayerState.FOLD);
            result.Add(BetCommandType.TIMEOUT, PlayerState.FOLD);
            return result;
        }
    }
}
