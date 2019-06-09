using Servidor.Jugador;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Logic.Player
{
    class PlayerEntity : PlayerInfo
    {
        private int _handValue = 0;
        private BetCommand _betCommand;
        private bool _showCards;
        public PlayerEntity() {}
        public bool showCards()
        {
            return _showCards;
        }
        public void showCards(bool _showCards)
        {
            this._showCards = _showCards;
        }
        public BetCommand getBetCommand()
        {
            return _betCommand;
        }
        public void setBetCommand(BetCommand _betCommand)
        {
            this._betCommand = _betCommand;
        }
        public  int getHandValue()
        {
            return _handValue;
        }
        public void setHandValue(int _handValue)
        {
            this._handValue = _handValue;
        }
    }
}
