using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Cartas
{
    public sealed class Card
    {
        private readonly String CARD_RANK = "23456789TJQKA";
        private readonly String CARD_SUIT = "♠♥♦♣";
        public enum Suit
        {
            SPADE, HEART, DIAMOND, CLUB
        }
        public enum Rank
        {
            TWO, TRHEE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE
        }

        private readonly Suit suit;
        private readonly Rank rank;

        public Card(Suit suit, Rank rank)
        {
            this.suit = suit;
            this.rank = rank;
        }

        public Suit getSuit()
        {
            return suit;
        }

        public Rank getRank()
        {
            return rank;
        }

        public static int indexRank(Rank rank)
        {
            return Array.IndexOf(Enum.GetValues(rank.GetType()), rank);
        }

        public static int indexSuit(Suit suit)
        {
            return Array.IndexOf(Enum.GetValues(suit.GetType()), suit);
        }

        public override string ToString()
        {
            return CARD_RANK.Substring(indexRank(rank), 1) + CARD_SUIT.Substring(indexSuit(suit), 1);
        }

        public override int GetHashCode()
        {
            return indexRank(rank) * Enum.GetValues(typeof(Suit)).Length + indexSuit(suit);
        }

        public override bool Equals(Object obj)
        {
            bool result = true;
            if (this != obj)
            {
                result = false;
                if(obj != null &&  this.GetType() == obj.GetType())
                {
                    result = GetHashCode() == ((Card)obj).GetHashCode();
                }
            }
            return result;
        }
    }
}
