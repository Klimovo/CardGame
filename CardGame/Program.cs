using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum Rank
{
    Ace = 11,
    King = 4,
    Queen = 3,
    Jack = 2,
    Ten = 10,
    Nine = 9,
    Eight = 8,
    Seven = 7,
    Six = 6
}

public class Card
{
    public Suit Suit { get; set; }
    public Rank Rank { get; set; }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

public class Deck
{
    private List<Card> cards;

    public Deck()
    {
        InitializeDeck();
    }

    public void InitializeDeck()
    {
        cards = new List<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card { Suit = suit, Rank = rank });
            }
        }
    }

    internal class Program
{
    static void Main(string[] args)
    {
    }
}