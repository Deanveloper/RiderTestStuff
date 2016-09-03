using System;
using System.Text;

namespace DeckTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            var deck = new Deck();
            Console.WriteLine(deck);
            deck.AlternatingShuffle();
            Console.WriteLine(deck);
        }
    }

    class Deck
    {
        private readonly Random _rand = new Random();
        public Card[] Cards { get; }

        public Deck()
        {
            Cards = new Card[52];
            for (int i = 0; i < 13; i++)
            {
                foreach (var suit in Enum.GetValues(typeof(Suit)))
                {
                    Console.WriteLine((int) suit * 13 + i);
                    Cards[(int) suit * 13 + i] = new Card((Suit) suit, i + 1);
                }
            }
        }

        /// <summary>
        /// Shuffles the deck by splitting it in two, and then
        /// continuously picking the top card from a randomly
        /// selected half</summary>
        /// <remarks>
        /// This should have similar behavior with how decks are
        /// most commonly shuffled in many card games</remarks>
        public void AlternatingShuffle()
        {
            var topHalf = new Card[26];
            var bottomHalf = new Card[26];
            Array.ConstrainedCopy(Cards, 0, topHalf, 0, 26);
            Array.ConstrainedCopy(Cards, 26, bottomHalf, 0, 26);
            var topIndex = 0;
            var bottomIndex = 0;

            for (var i = 0; i < 52; i++)
            {
                if (topIndex == 26)
                {
                    Cards[i] = bottomHalf[bottomIndex++];
                }
                else if (bottomIndex == 26)
                {
                    Cards[i] = topHalf[topIndex++];
                }
                else
                {
                    if (_rand.NextDouble() >= 0.5)
                    {
                        Cards[i] = bottomHalf[bottomIndex++];
                    }
                    else
                    {
                        Cards[i] = topHalf[topIndex++];
                    }
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder("Deck[");
            foreach (var card in Cards)
            {
                builder.Append(card).Append(",");
            }
            builder[builder.Length - 1] = ']';
            return builder.ToString();
        }
    }

    class Card
    {
        public Suit Suit { get; }

        public int Number { get; }

        public Card(Suit suit, int number)
        {
            Suit = suit;
            Number = number;
        }

        public override string ToString()
        {
            return $"Card[Suit={Suit},Number={Number}]";
        }
    }

    enum Suit
    {
        Clubs,
        Spades,
        Hearts,
        Diamonds
    }
}
