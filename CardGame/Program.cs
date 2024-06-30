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

public class Game
{
    private Deck deck;
    private Player player;
    private Player computer;
    private bool playerTurn;

    public Game()
    {
        deck = new Deck();
        deck.Shuffle();

        player = new Player();
        computer = new Player();

        playerTurn = true;
    }

    public void Start()
    {
        // Initial deal
        DealCard(player);
        DealCard(computer);
        DealCard(player);
        DealCard(computer);

        while (true)
        {
            Console.Clear();
            DisplayGameStatus();

            if (playerTurn)
            {
                Console.WriteLine("Your turn.");
                if (AskPlayerForAction())
                {
                    DealCard(player);
                }
                else
                {
                    playerTurn = false;
                }
            }
            else
            {
                Console.WriteLine("Computer's turn.");
                if (computer.Points < 17) // Computer takes card if less than 17 points
                {
                    DealCard(computer);
                }
                else
                {
                    playerTurn = true;
                }
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }

            if (player.Points >= 21 || computer.Points >= 21)
            {
                EndGame();
                break;
            }
        }
    }
    private void DisplayGameStatus()
    {
        Console.WriteLine($"Your points: {player.Points}");
        Console.WriteLine("Your cards:");
        foreach (var card in player.Hand)
        {
            Console.WriteLine($"  {card}");
        }
        Console.WriteLine();

        Console.WriteLine($"Computer's points: {computer.Points}");
        Console.WriteLine("Computer's cards:");
        foreach (var card in computer.Hand)
        {
            Console.WriteLine($"  {card}");
        }
        Console.WriteLine();
    }

    private void DealCard(Player player)
    {
        List<Card> deckCards = deck.GetCards();
        Card dealtCard = deckCards.First();
        player.Hand.Add(dealtCard);
        player.Points += (int)dealtCard.Rank;
        deckCards.RemoveAt(0);
    }

    private bool AskPlayerForAction()
    {
        Console.WriteLine("Do you want to take another card? (y/n)");
        string response = Console.ReadLine().ToLower();
        return response == "y";
    }

    private void EndGame()
    {
        Console.Clear();
        DisplayGameStatus();

        if ((player.Points == 21 || player.Hand.Any(card => card.Rank == Rank.Ace)) &&
            (computer.Points != 21 && !computer.Hand.Any(card => card.Rank == Rank.Ace)))
        {
            Console.WriteLine("Congratulations! You win!");
        }
        else if ((computer.Points == 21 || computer.Hand.Any(card => card.Rank == Rank.Ace)) &&
            (player.Points != 21 && !player.Hand.Any(card => card.Rank == Rank.Ace)))
        {
            Console.WriteLine("Sorry, computer wins.");
        }
        else if (player.Points > 21 && computer.Points > 21)
        {
            Console.WriteLine("Both players exceeded 21 points. It's a draw.");
        }
        else if (player.Points > 21)
        {
            Console.WriteLine("You exceeded 21 points. Computer wins.");
        }
        else if (computer.Points > 21)
        {
            Console.WriteLine("Computer exceeded 21 points. You win.");
        }
        else
        {
            Console.WriteLine("Game ended.");
        }

        Console.WriteLine("Press Enter to play again or 'q' to quit.");
        string restart = Console.ReadLine().ToLower();
        if (restart != "q")
        {
            ResetGame();
            Start();
        }
    }

    internal class Program
    {
        static void Main(string[] args) { }
    }