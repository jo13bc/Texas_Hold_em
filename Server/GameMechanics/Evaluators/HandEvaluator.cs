using Servidor.Cartas;
using Servidor.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Hand
{
    sealed class HandEvaluator: IHandEvaluator
    {
        private static readonly int ENCODE_BASE = Card.indexRank(Card.Rank.ACE) + 1;
        private static readonly int INDEXES_LENGTH = 2;
        private static readonly int RANK_INDEX = 0;
        private static readonly int REPEATS_INDEX = 1;
        private static readonly Hands.Type[][] MATRIX_TYPE = generarMatrizType();
        private readonly int[][] indexes = generarMatrizIndex(Hands.CARDS, INDEXES_LENGTH);
        private readonly int[] ranks = new int[ENCODE_BASE];
        private readonly int[] suits = new int[Enum.GetValues(typeof(Card.Suit)).Length];
        private bool isStraight = false;
        private bool isFlush = false;

        private static int[][] generarMatrizIndex(int i, int j)
        {

            int[][] matriz = new int[i][];
            for (int k = 0; k < i; k++)
            {
                matriz[k] = new int[j];
            }
            return matriz;
        }

        private static Hands.Type[][] generarMatrizType()
        {
            Hands.Type[][] matriz = new Hands.Type[4][];
            matriz[0] = new Hands.Type[1];
            matriz[1] = new Hands.Type[2];
            matriz[2] = new Hands.Type[2];
            matriz[3] = new Hands.Type[1];
            matriz[0][0] = Hands.Type.HIGH_CARD;
            matriz[1][0] = Hands.Type.ONE_PAIR;
            matriz[1][1] = Hands.Type.TWO_PAIR;
            matriz[2][0] = Hands.Type.THREE_OF_A_KIND;
            matriz[2][1] = Hands.Type.FULL_HOUSE;
            matriz[3][0] = Hands.Type.FOUR_OF_A_KIND;
            return matriz;
        }

        public int eval(Card[] cards)
        {
            ExceptionUtil.checkArrayLengthArgument<Card>(cards, "cards",Hands.CARDS);
            isFlush = false;
            Array.Fill(suits,0);
            Array.Fill(ranks, 0);
            int index = 0;
            HashSet<Card> previosCard = new HashSet<Card>(Hands.CARDS);
            foreach(Card card in cards)
            {
                ExceptionUtil.checkNullArgument(card,"card[" + index++ + "]");
                ExceptionUtil.checkArgument(previosCard.Contains(card),"La carta {0} esta repetida.",card);
                previosCard.Add(card);
                ranks[Card.indexRank(card.getRank())]++;
                suits[Card.indexSuit(card.getSuit())]++;
            }
            isFlush = suits[Card.indexSuit(cards[0].getSuit())] == Hands.CARDS;
            isStraight = false;
            int straightCounter = 0;
            int j = 0;
            for (int i = ranks.Length - 1; i >= 0; i--)
            {
                if(ranks[i] > 0)
                {
                    straightCounter++;
                    isStraight = straightCounter == Hands.CARDS;
                    indexes[j][RANK_INDEX] = i;
                    indexes[j][REPEATS_INDEX] = ranks[i];
                    upIndex(j++);
                }
                else
                {
                    straightCounter = 0;
                }
            }
            isStraight = isStraight || checkStraight5toAce(straightCounter);
            return calculateHandValue();
        }
        //Actualiza el orden de los pares en los índices
        private void upIndex(int i)
        {
            int k = i;
            while(k>0 && indexes[k - 1][REPEATS_INDEX] < indexes[k][REPEATS_INDEX]){
                int[] temp = indexes[k - 1];
                indexes[k - 1] = indexes[k];
                indexes[k] = temp;
                k--;
            }
        }

        private bool checkStraight5toAce(int straightCntr)
        {
            bool straight5toAce = false;
            //Evalúa si se trata del caso especial
            if(ranks[Card.indexRank(Card.Rank.ACE)] == 1 && straightCntr == Hands.CARDS - 1)
            {
                //Si es el caso especial abria que reorganizar los índices
                straight5toAce = true;
                for(int i = 1; i< indexes.Length; i++)
                {
                    indexes[i - 1][RANK_INDEX] = indexes[i][RANK_INDEX];
                }
                indexes[indexes.Length - 1][RANK_INDEX] = Card.indexRank(Card.Rank.ACE);
            }
            return straight5toAce;
        }

        private int calculateHandValue()
        {
            Hands.Type type;
            if (isStraight)
            {
                type = isFlush ? Hands.Type.STRAIGHT_FLUSH : Hands.Type.STRAIGHT;
            }
            else if(isFlush)
            {
                type = Hands.Type.FLUSH;
            }
            else
            {
                type = MATRIX_TYPE[indexes[0][REPEATS_INDEX] - 1][indexes[1][REPEATS_INDEX] - 1];
            }
            return encodeValue(type,indexes);
        }

        private static int encodeValue(Hands.Type type, int[][] indexes)
        {
            int result = Array.IndexOf(Enum.GetValues(type.GetType()), type);
            int i = 0, j = 0;
            while (j < Hands.CARDS)
            {
                for (int k = 0; k < indexes[i][REPEATS_INDEX]; k++)
                {
                    result = result * ENCODE_BASE + indexes[i][RANK_INDEX];
                    j++;
                }
                i++;
            }
            return result;
        }
    }
}
