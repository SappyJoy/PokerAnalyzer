using System;

namespace PokerAnalyzer
{
    public class Game
    {
        private Table table;
        private Deck deck;
        private int playersNumber;
        private Player[] players;

        public void start()
        {
            
            //playersNumber = howManyPlayers();
            playersNumber = 6;
            players = new Player[playersNumber];
            for (int i = 0; i < playersNumber; i++)
            {
                players[i] = new Player((PlayerName)i);
            }
            Player me = players[0];
            deck = new Deck();
            // начинается первый круг
            // выдаётся 2 карты каждому игроку
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < playersNumber; j++)
                {
                    players[j].TakeCard(deck.drawCard());
                }
            }
            Console.WriteLine(me);
            Deck otherCards = new Deck();
            foreach (var card in me.cards)
            {
                otherCards.cards.Remove(card);
            }
            Card[] possibleCards = new Card[5];
            Array.Copy(me.cards.ToArray(), 0, possibleCards, 0, me.cards.Count);
            check = new bool[otherCards.cards.Count];
            analyzePossibilities(possibleCards, otherCards.cards.ToArray(), 2);
            Console.WriteLine($"Pair: {(double)pair/totalCases*100:F3}%");
            Console.WriteLine($"Two Pairs: {(double)twoPair/totalCases*100:F3}%");
            Console.WriteLine($"Three Of A Kind: {(double)threeOfAKind/totalCases*100:F3}%");
            Console.WriteLine($"Straight: {(double)straight/totalCases*100:F3}%");
        }
        
        private int pair = 0;
        private int twoPair = 0;
        private int threeOfAKind = 0;
        private int straight = 0;
        private int flush = 0;
        private int fullHouse = 0;
        private int fourOfAKind = 0;
        private int straightFlush = 0;
        private int royalFlush = 0;
        private bool[] check;
        private int totalCases = 0;
        
        private void analyzePossibilities(Card[] cardsInHand, Card[] cardsNotInHand, int length)
        {
            if (length == 5)
            {
                totalCases++;
                if (checkPair(cardsInHand))
                    pair++;
                if (checkTwoPair(cardsInHand))
                    twoPair++;
                if (checkTreeOfAKnd(cardsInHand))
                    threeOfAKind++;
                if (checkStraight(cardsInHand))
                    straight++;
                return;
            }

            for (int i = 0; i < cardsNotInHand.Length; i++)
            {
                if (!check[i])
                {
                    check[i] = true;
                    cardsInHand[length] = cardsNotInHand[i];
                    analyzePossibilities(cardsInHand, cardsNotInHand, length + 1);
                    check[i] = false;
                }
            }
        }

        private bool checkPair(Card[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards.Length; j++)
                {
                    if (i != j && cards[i].Advantage == cards[j].Advantage)
                        return true;
                }
            }
            return false;
        }

        private bool checkTwoPair(Card[] cards)
        {
            int x = 0; int y = 0;
            bool firstPair = false;
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards.Length; j++)
                {
                    if (i != j && cards[i].Advantage == cards[j].Advantage)
                    {
                        firstPair = true;
                        x = i;
                        y = j;
                    }
                }
            }

            if (!firstPair)
                return false;
            Card[] newCards = new Card[cards.Length-2];
            int k = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (i != x && i != y)
                {
                    newCards[k] = cards[i];
                    k++;
                }
            }

            return checkPair(newCards);
        }

        public bool checkTreeOfAKnd(Card[] cards)
        {
            int x = 0; int y = 0;
            bool firstPair = false;
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards.Length; j++)
                {
                    if (i != j && cards[i].Advantage == cards[j].Advantage)
                    {
                        firstPair = true;
                        x = i;
                        y = j;
                    }
                }
            }
            if (!firstPair)
                return false;
            Card[] newCards = new Card[cards.Length-2];
            int k = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (i != x && i != y)
                {
                    newCards[k] = cards[i];
                    k++;
                }
            }
            for (int i = 0; i < newCards.Length; i++)
                if (cards[x].Advantage == newCards[i].Advantage)
                    return true;
            return false;
        }

        public bool checkStraight(Card[] cards)
        {
            Card[] newCards = new Card[cards.Length];
            Array.Copy(cards, 0, newCards, 0, cards.Length);
            Array.Sort(newCards);
            Suit suit = newCards[0].Suit;
            bool ch = true;
            for (int i = 1; i < newCards.Length; i++)
            {
                if (newCards[i].Advantage - newCards[i - 1].Advantage != 1)
                    return false;
                if (newCards[i].Suit != suit)
                    ch = false;
            }
            if (ch)
                return false;
            return true;
        }

        public void printCardsOnTable(Deck deck, Player[] players)
        {
            // Console.WriteLine($"Deck: {deck.Size} cards");
            // deck.writeCards();
            for (int i = 0; i < playersNumber; i++)
                Console.WriteLine(players[i]);
        }

        private static int howManyPlayers()
        {
            Console.WriteLine("How many players are at the table?\n");
            int playersNumber;
            while (true)
            {
                try
                {
                    playersNumber = Convert.ToInt32(Console.ReadLine());
                    if (playersNumber < 2 || playersNumber > 10)
                        throw new ArgumentException();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Enter the correct number of players (2-10)");
                }
            }

            return playersNumber;
        }
    }
}