using System.Collections.Generic;

namespace PokerHandShowdown
{
    class PokerCombination
    {
        // accepts a the cards in player's hand a returns a score and the highest value card of the combination
        public static (int, int) CheckCombination(List<Card> cards)
        {

            int temporaryScore = 1;
            int temporaryHighestValue = 0;
            int otherHighestValue = 0;
            var temporarySuit = "";
            int sameSuitCount = 1;

            // check for a Flush combination
            foreach (Card item in cards)
            {
                if (temporarySuit == "")
                {
                    temporarySuit = item.CardSuit.ToString();
                }
                else
                {
                    if (temporarySuit == item.CardSuit.ToString())
                    {
                        sameSuitCount++;
                    }
                }

                if (sameSuitCount > 4)
                {
                    temporaryScore = 4;
                }
            }

            int sameValueCount = 0;
            int otherSameValueCount = 0;
            // Checks for a Three of a kind or a Pair
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (x != y)
                    {
                        if ((int)cards[x].CardValue == (int)cards[y].CardValue)
                        {
                            if (temporaryHighestValue <= (int)cards[y].CardValue)
                            {
                                temporaryHighestValue = (int)cards[y].CardValue;
                                sameValueCount++;
                            }
                            if (temporaryHighestValue > (int)cards[y].CardValue)
                            {
                                otherHighestValue = (int)cards[y].CardValue;
                                otherSameValueCount++;
                            }
                            
                        }
                    }
                }
            }

            if (sameValueCount < otherSameValueCount)
            {
                sameValueCount = otherSameValueCount;
                temporaryHighestValue = otherHighestValue;
            }

            if (sameValueCount >= 3) // check for three of a kind
            {
                temporaryScore = 3;
            }
            else if (sameValueCount == 2) // check for a pair
            {
                temporaryScore = 2;
            }
            else
            {
                temporaryHighestValue = (int)cards[0].CardValue; // High card
            }

            return (temporaryScore, temporaryHighestValue);
        }
    }
}
