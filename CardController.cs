using System;
using System.Collections.Generic;

namespace PokerHandShowdown
{
    class CardController
    {
        // variables
        const int MAX_NUMBER_OF_CARDS = 52;
        const int SHUFFLE_TIMES = 100;
        private Card[] cardDeck;

        public CardController()
        {
            cardDeck = new Card[MAX_NUMBER_OF_CARDS];
        }

        // create a 52 card deck
        // partner each value to each suit
        public void Create_Deck()
        {
            int card_Count = 0;
            foreach (Card.Card_Value card_Value in Enum.GetValues(typeof(Card.Card_Value)))
            {
                foreach (Card.Card_Suit card_Suit in Enum.GetValues(typeof(Card.Card_Suit)))
                {
                    cardDeck[card_Count] = new Card { CardValue = card_Value, CardSuit = card_Suit };
                    card_Count++;
                }
            }
        }

        // shuffling cards a 100 times
        public void Shuffle_Deck()
        {
            Random random = new Random();
            Card temporaryValue;
            
            for (int shuffleTimes = 0; shuffleTimes < SHUFFLE_TIMES; shuffleTimes++)
            {
                for (int i = 0; i < MAX_NUMBER_OF_CARDS; i++)
                {
                    int secondCardIndex = random.Next(13);
                    temporaryValue = cardDeck[i];
                    cardDeck[i] = cardDeck[secondCardIndex];
                    cardDeck[secondCardIndex] = temporaryValue;
                }
            }
        }

        // provide the deck
        public List<Card> Deal_Deck()
        {

            List<Card> dealtCards = new List<Card>();

            int array_Count = 0;
            foreach (Card item in cardDeck)
            {
                dealtCards.Add(new Card());
                dealtCards[array_Count].CardValue = item.CardValue;
                dealtCards[array_Count].CardSuit = item.CardSuit;
                array_Count++;
            }

            return dealtCards;
        }
    }
}
