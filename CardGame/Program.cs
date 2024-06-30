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

    public void Shuffle()
    {
        Random rng = new Random();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    public List<Card> GetCards()
    {
        return cards;
    }

    public List<int> FindAcePositions()
    {
        List<int> positions = new List<int>();
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Rank == Rank.Ace)
            {
                positions.Add(i);
            }
        }
        return positions;
    }

    public void MoveAllSpadesToTop()
    {
        cards = cards.OrderBy(card => card.Suit != Suit.Spades).ToList();
    }

    public void Sort()
    {
        cards.Sort((card1, card2) => card1.Rank.CompareTo(card2.Rank));
    }
}

public class Player
{
    public List<Card> Hand { get; set; }
    public int Points { get; set; }

    public Player()
    {
        Hand = new List<Card>();
        Points = 0;
    }
}

internal class Program
    {
        static void Main(string[] args) { }
    }