using System.Collections.Generic;

namespace PokerAnalyzer
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> cards;

        public Player(PlayerName name)
        {
            Name = name.ToString();
            cards = new List<Card>();
        }

        public void TakeCard(Card card)
        {
            cards.Add(card);
        }

        public override string ToString()
        {
            string result = Name + ": " + cards[0];
            for (int i = 1; i < cards.Count; i++)
            {
                result += ", " + cards[i];
            }
            return result;
        }
    }
}