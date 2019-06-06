using Servidor.Cartas;
using Servidor.Evaluador.Combinaciones;
using Servidor.Hand;
using Servidor.Utility.TexasHoldem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Jugador
{
    class Hand7Evaluator
    {
        public static readonly int TOTAL_CARDS = TexasHoldEmUtil.PLAYER_CARDS + TexasHoldEmUtil.COMMUNITY_CARDS;
        private readonly int[] combinatorialBuffer = new int[TexasHoldEmUtil.COMMUNITY_CARDS];
        private readonly Combination combinatorial = new Combination(TexasHoldEmUtil.COMMUNITY_CARDS, TOTAL_CARDS);
        private readonly IHandEvaluator evaluator;
        private readonly Card[] evalBuffer = new Card[TexasHoldEmUtil.COMMUNITY_CARDS];
        private readonly Card[] cards = new Card[TOTAL_CARDS];
        private int communityCardValue = 0;
        public Hand7Evaluator(IHandEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public void setCommunityCards(List<Card> communityCards)
        {
            int i = 0;
            foreach (Card card in communityCards)
            {
                evalBuffer[i] = card;
                cards[i++] = card;
            }
            communityCardValue = evaluator.eval(evalBuffer);
        }

        public int eval(Card card0, Card card1)
        {
            cards[TexasHoldEmUtil.COMMUNITY_CARDS] = card0;
            cards[TexasHoldEmUtil.COMMUNITY_CARDS + 1] = card1;
            return evalCards();
        }

        public static Card[] copy(Card[] src, Card[] target, int[] possitions)
        {
            int i = 0;
            foreach (int p in possitions)
            {
                target[i++] = src[p];
            }
            return target;
        }

        private int evalCards()
        {
            combinatorial.clear();
            combinatorial.next(combinatorialBuffer);
            int result = communityCardValue;
            while (combinatorial.hasNext())
            {
                result = Math.Max(result, evaluator.eval(copy(cards,evalBuffer,combinatorial.next(combinatorialBuffer))));
            }

            return result;
        }
    }
}
