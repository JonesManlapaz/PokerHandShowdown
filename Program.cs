using System;
using System.Collections.Generic;

namespace PokerHandShowdown
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Poker Hand Showdown");

            // initialize number of players
            int playerCount = 0;
            bool quit = false;

            while (!quit)
            {
                Console.Write("Please provide number of players (maximum 10): ");

                while (playerCount == 0)
                {
                    // get the number of players
                    string numberOfPlayers = Console.ReadLine();

                    // make sure it the input is a number
                    if (!int.TryParse(numberOfPlayers, out playerCount))
                    {
                        // inform that the input is invalid
                        PrintMessage(ConsoleColor.Red, "Please use an actual number between 1 to 10: ");
                    }
                    else if (playerCount == 0 || playerCount > 10)
                    {
                        // inform that the input is out of range
                        PrintMessage(ConsoleColor.Red, "Please supply a number between 1 to 10: ");
                        playerCount = 0;
                    }
                }

                // create an instance
                CardController cards = new CardController();
                
                cards.Create_Deck();

                cards.Shuffle_Deck();

                List<Card> dealtCards = cards.Deal_Deck();

                // check if the deck is shuffled
                /*foreach (Card item in dealtCards)
                {
                    Console.Write(item.CardValue.ToString("D") + item.CardSuit + " ");
                }
                Console.WriteLine();*/

                // initialize the array with number of players
                List<Player> playerInformation = new List<Player>(playerCount);

                // initialize length of longest name
                int nameLenght = 0;

                // loop to the number of players
                for (int i = 0; i < playerCount; i++)
                {
                    // initialize the player name
                    string playerName = "";

                    // ask for the names of players
                    Console.Write("Please provide Player " + (i + 1) + "'s name : ");

                    // loop until a name is provided
                    while (playerName == "")
                    {
                        // get the name
                        playerName = Console.ReadLine();

                        // checks if the input is not blank
                        if (playerName == "")
                        {
                            // inform that the input is invalid
                            PrintMessage(ConsoleColor.Yellow, "Please give a name for Player " + (i + 1) + ": ");
                        }
                        else
                        {
                            // checks length for longest name
                            if (nameLenght < playerName.Length)
                            {
                                nameLenght = playerName.Length;
                            }

                            // initialize the list
                            playerInformation.Add(new Player());

                            // add player name to the list
                            playerInformation[i].playerName = playerName;

                            // initialize the list of card the player will have
                            List<Card> playerCards = new List<Card>(5);

                            // initialize to check the first card
                            int card_Order = 0;

                            // deal the five card to the player and place it to their own list
                            for (int y = 5 * i; y < 5 * i + 5; y++)
                            {
                                playerCards.Add(new Card());
                                playerCards[card_Order].CardValue = dealtCards[y].CardValue;
                                playerCards[card_Order].CardSuit = dealtCards[y].CardSuit;
                                card_Order++;
                            }

                            // sorts the cards in players hand in descending order
                            playerCards.Sort();
                            
                            // add the sorted cards to the player
                            playerInformation[i].playerHand = playerCards;

                            // get what type of hand (Flush = 4, Three of a kind = 3, One pair = 2, High Card = 1)                       
                            playerInformation[i].pokerHandScore = PokerCombination.CheckCombination(playerCards).Item1;

                            // get the hight card value in the player's hand
                            playerInformation[i].playerHighestCardValue = PokerCombination.CheckCombination(playerCards).Item2;
                        }
                    }
                }

                // sorts the players by score in descending order
                playerInformation.Sort();

                Console.WriteLine();

                // display the player's information
                foreach (Player item in playerInformation)
                {
                    Console.Write(item.playerName + " ".PadRight(nameLenght - item.playerName.Length + 5));

                    

                    // display the value and suit of the cards
                    // change the value of 11-14 to J, Q, K, A
                    foreach (Card individual_Card in item.playerHand)
                    {
                        if (individual_Card.CardValue.ToString() == "JACK")
                        {
                            Console.Write("J" + individual_Card.CardSuit + " ");
                        }
                        else if (individual_Card.CardValue.ToString() == "QUEEN")
                        {
                            Console.Write("Q" + individual_Card.CardSuit + " ");
                        }
                        else if (individual_Card.CardValue.ToString() == "KING")
                        {
                            Console.Write("K" + individual_Card.CardSuit + " ");
                        }
                        else if (individual_Card.CardValue.ToString() == "ACE")
                        {
                            Console.Write("A" + individual_Card.CardSuit + " ");
                        }
                        else
                        {
                            Console.Write(individual_Card.CardValue.ToString("D") + individual_Card.CardSuit + " ");
                        }
                    }

                    // display what type of combination the player have
                    switch (item.pokerHandScore)
                    {
                        case 4:
                            Console.Write("\tFlush");
                            break;
                        case 3:
                            Console.Write("\tThree of a kind");
                            break;
                        case 2:
                            Console.Write("\tPair");
                            break;
                        default:
                            Console.Write("\tHigh card");
                            break;
                    }

                    Console.WriteLine();
                }

                List<Player> winnerPool = new List<Player>();
                int winnerOrder = 0;
                int temporaryScore = 0;
                int temporaryHighestCardScore = 0;
                List<Card> temporaryCard = new List<Card>();

                // determines the winner(s)
                for (int i = 0; i < playerCount; i++)
                {
                    if (temporaryScore < playerInformation[i].pokerHandScore)
                    {
                        winnerPool.Add(new Player());
                        winnerPool[winnerOrder] = playerInformation[i];
                        temporaryScore = playerInformation[i].pokerHandScore;
                        temporaryHighestCardScore = playerInformation[i].playerHighestCardValue;
                        temporaryCard = playerInformation[i].playerHand;
                        winnerOrder++;
                    }
                    else if (temporaryScore == playerInformation[i].pokerHandScore)
                    {
                        if (temporaryHighestCardScore == playerInformation[i].playerHighestCardValue)
                        {
                            // checks if players have the same hand
                            bool playerHandisSame = true;
                            for (int x = 0; x < playerInformation[i].playerHand.Count; x++)
                            {
                                if (playerInformation[i - 1].playerHand[x].CardValue > playerInformation[i].playerHand[x].CardValue)
                                {
                                    playerHandisSame = false;
                                }
                            }
                            if (playerHandisSame) // players with the same hand will be added to the winner pool
                            {
                                winnerPool.Add(new Player());
                                winnerPool[winnerOrder] = playerInformation[i];
                                winnerOrder++;
                            }
                        }
                    }
                }

                // determine if one or more player won and display the appropriate message
                if (winnerPool.Count > 1)
                {
                    Console.WriteLine("\nThe winners are : ");
                }
                else
                {
                    Console.WriteLine("\nThe winner is : ");
                }

                // display all winners
                foreach (Player winner in winnerPool)
                {
                    Console.WriteLine(winner.playerName);
                }

                // initilize answer as blank
                string answer = "";

                // will keep on asking if aswer does not match acceptable answer
                while (answer != "Y" ||  answer != "N")
                {
                    // display a meesage to ask if playing again
                    Console.WriteLine("\nPlay Again? [Y or N]");

                    // Get answer
                    answer = Console.ReadLine().ToUpper();

                    if (answer == "Y")
                    {
                        playerCount = 0;
                        break;
                    }
                    else if (answer == "N")
                    {
                        quit = true;
                        break;
                    }
                }

                Console.WriteLine();
            }
        }

        static void PrintMessage(ConsoleColor color, string message)
        {
            // change the color of the text
            Console.ForegroundColor = color;

            // print the message for the user
            Console.Write(message);

            // return the color of the text to white
            Console.ResetColor();
        }
    }
}
