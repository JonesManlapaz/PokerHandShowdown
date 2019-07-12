using System;
using System.Collections.Generic;

namespace PokerHandShowdown
{
    class Player : IComparable <Player>
    {
        public string playerName = "";
        public int pokerHandScore = 0;
        public List<Card> playerHand = new List<Card>(5);
        public int playerHighestCardValue = 0;

        // use for sorting
        public int CompareTo(Player playerInformation)
        {
            int returnValue = 0;
            if ((playerInformation.pokerHandScore != this.pokerHandScore))
            {
                returnValue = (playerInformation.pokerHandScore - this.pokerHandScore);
            }
            else
            {
                if (playerInformation.playerHighestCardValue != this.playerHighestCardValue)
                {
                    returnValue = (playerInformation.playerHighestCardValue - this.playerHighestCardValue);
                }
                else
                {
                    for (int i = 0; i < playerInformation.playerHand.Count; i++)
                    {
                        if (playerInformation.playerHand[i].CardValue != this.playerHand[i].CardValue)
                        {
                            returnValue = ((int)playerInformation.playerHand[i].CardValue - (int)this.playerHand[i].CardValue);
                            break;
                        }
                    }
                }
            }
            return returnValue;
        }
    }
}
