using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Cartas
{
    class Deck
    {
        private readonly List<Card> cards;
        private int index = 0;

        public Deck()
        {
            this.cards = getAllCards();
        }

        public Card obtainCard()
        {
            Card result = null;
            if(index < cards.Count)
            {
                result = cards[index];
                index++;
            }
            return result;
        }

        public void shuffle()
        {
            //Metodo para barajar el mazo.
            index = 0;
            System.Random _random = new System.Random();
            Card aux;
            int n = cards.Count;
            for (int i = 0; i < n; i++) {
                int j = i + (int)(_random.NextDouble() * (n - i));
                aux = cards[j];
                cards[j] = cards[i];
                cards[i] = aux;
            }
        }

        public static List<Card> getAllCards()
        {
            int numCards = Enum.GetValues(typeof(Card.Suit)).Length * Enum.GetValues(typeof(Card.Rank)).Length;
            List<Card> result = new List<Card>(numCards);
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit))) {
                foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank))) {
                    result.Add(new Card(suit,rank));
                }
            }
            return result;
        }
    }
}
