using System;

namespace PokerAnalyzer
{
    public class Card : IComparable
    {
        public Suit Suit { get; set; }
        public Advantage Advantage { get; set; }

        public Card(Suit suit, Advantage advantage)
        {
            Suit = suit;
            Advantage = advantage;
        }

        public override string ToString()
        {
            return Suit + " " + Advantage;
        }

        public override bool Equals(object obj)
        {
            Card other = (Card) obj;
            if (other == null) return false;
            return Suit.Equals(other.Suit) && Advantage.Equals(other.Advantage);
        }

        public override int GetHashCode()
        {
            return Suit.GetHashCode() + Advantage.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            Card otherCard = (Card) obj;
            if (this.Advantage < otherCard.Advantage)
            {
                return -1;
            }

            return 1;
        }
    }
}