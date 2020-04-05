using System;
using System.Collections;
using System.Collections.Generic;

namespace PokerAnalyzer
{
    public class Deck
    {
        public List<Card> cards;

        public int Size
        {
            get
            {
                return cards.Count;
            }
            private set { }
        }

        public Deck()
        {
            var suits = Enum.GetNames(typeof(Suit));
            var advantages = Enum.GetNames(typeof(Advantage));
            var cardsCount = suits.Length * advantages.Length;
            cards = new List<Card>();
            int i = 0;
            foreach (var suit in suits)
            {
                foreach (var advantage in advantages)
                {
                    cards.Add(new Card((Suit)Enum.Parse(typeof(Suit), suit),
                        (Advantage)Enum.Parse(typeof(Advantage), advantage)));
                    i++;
                }
            }
            shuffleDeck(cards);
        }

        private static void shuffleDeck(List<Card> cards)
        {
            int deckSize = cards.Count;
            int swapsCount = 500;
            Random random = new Random();
            
            for (int i = 0; i < swapsCount; i++)
            {
                swapCards(cards, random.Next(0, deckSize), random.Next(0, deckSize));
            }
        }

        private static void swapCards(List<Card> arr, int a, int b)
        {
            Card tmp = arr[a];
            arr[a] = arr[b];
            arr[b] = tmp;
        }

        public Card drawCard()
        {
            Card card = cards[0];
            cards.Remove(card);
            return card;
        }
        
        public void writeCards()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                string suit = cards[i].Suit.ToString();
                string advantage = cards[i].Advantage.ToString();
                Console.WriteLine(advantage + " " + suit);
            }
        }
    }
}