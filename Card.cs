using System;

namespace PokerHandShowdown
{
    class Card : IComparable <Card>
    {
        // C for club, D for diamond, H for heart, s for spade
        public enum Card_Suit
        {
            C,
            D,
            H,
            S
        }

        // each suit is consist of 13 cards from 2 to ace
        public enum Card_Value
        {
            TWO = 2,
            THREE,
            FOUR,
            FIVE,
            SIX,
            SEVEN,
            EIGHT,
            NINE,
            TEN,
            JACK,
            QUEEN,
            KING,
            ACE
        }

        // properties
        public Card_Suit CardSuit
        {
            get;
            set;
        }

        public Card_Value CardValue
        {
            get;
            set;
        }

        // use for sorting
        public int CompareTo(Card cards)
        {
            return ((int)cards.CardValue - (int)this.CardValue);
        }
    }
}
