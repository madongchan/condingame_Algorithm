using System;
using System.Collections.Generic;

class Solution
{
    static Queue<string> player1Deck = new Queue<string>();
    static Queue<string> player2Deck = new Queue<string>();
    static Queue<string> temporary_warDeck1 = new Queue<string>();
    static Queue<string> temporary_warDeck2 = new Queue<string>();
    static int turn = 0;
    static void Main(string[] args)
    {

        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string card = Console.ReadLine();
            player1Deck.Enqueue(card);
        }

        int m = int.Parse(Console.ReadLine());
        for (int i = 0; i < m; i++)
        {
            string card = Console.ReadLine();
            player2Deck.Enqueue(card);
        }

        while (player1Deck.Count > 0 && player2Deck.Count > 0)
        {
            ++turn;
            Fight();
            #region Check the progress of the card game
            //Console.Error.WriteLine($"player1Deck: {player1Deck.Count}, player2Deck: {player2Deck.Count}");
            Console.Error.Write($"{turn}번째 턴 player1Deck: ");
            foreach (string card in player1Deck)
            {
                Console.Error.Write($"{card} ");
            }
            Console.Error.Write($"\n\t      player2Deck: ");
            foreach (string card in player2Deck)
            {
                Console.Error.Write($"{card}, ");
            }
            Console.Error.Write($"\n\n\n");
            #endregion

        }
        Console.WriteLine(player1Deck.Count > 0 ? $"1 {turn}" : $"2 {turn}");
    }

    static int Fight(string IsWarFun = "Fight")
    {
        string player1Card = player1Deck.Dequeue();
        string player2Card = player2Deck.Dequeue();

        if (CompareCards(player1Card, player2Card) > 0)
        {
            if (IsWarFun == "Fight")
            {
                player1Deck.Enqueue(player1Card);
                player1Deck.Enqueue(player2Card);
            }
            return 1;
        }
        else if (CompareCards(player1Card, player2Card) < 0)
        {
            if (IsWarFun == "Fight")
            {
                player2Deck.Enqueue(player1Card);
                player2Deck.Enqueue(player2Card);
            }
            return 2;
        }
        else
        {
            if (IsWarFun == "Fight")
            {
                War(player1Card, player2Card);
            }
            return 0;
        }
    }

    static void War(string player1Card, string player2Card)
    {

        temporary_warDeck1.Enqueue(player1Card);
        temporary_warDeck2.Enqueue(player2Card);

        for (int i = 0; i < 3; i++)
        {
            if (player1Deck.Count < 2 || player2Deck.Count < 2)
            {
                Console.WriteLine("PAT");
                Environment.Exit(0);
            }
            temporary_warDeck1.Enqueue(player1Deck.Dequeue());
            temporary_warDeck2.Enqueue(player2Deck.Dequeue());
        }

        string card1 = player1Deck.Peek();
        string card2 = player2Deck.Peek();

        temporary_warDeck1.Enqueue(card1);
        temporary_warDeck2.Enqueue(card2);
        //Console.Error.WriteLine($"{temporary_warDeck1.Count}");

        int winPlayer = Fight("War");
        if (winPlayer == 1)
        {
            int tempCount = temporary_warDeck1.Count;
            for (int i = 0; i < tempCount; i++)
            {
                player1Deck.Enqueue(temporary_warDeck1.Dequeue());
            }

            tempCount = temporary_warDeck2.Count;
            for (int i = 0; i < tempCount; i++)
            {
                player1Deck.Enqueue(temporary_warDeck2.Dequeue());
            }
        }
        else if (winPlayer == 2)
        {
            int tempCount = temporary_warDeck1.Count;
            for (int i = 0; i < tempCount; i++)
            {
                player2Deck.Enqueue(temporary_warDeck1.Dequeue());
            }

            tempCount = temporary_warDeck2.Count;
            for (int i = 0; i < tempCount; i++)
            {
                player2Deck.Enqueue(temporary_warDeck2.Dequeue());
            }
        }
        else
        {
            War(card1, card2);
        }

    }

    static int CompareCards(string card1, string card2)
    {
        int card1Rank = GetCardRank(card1);
        int card2Rank = GetCardRank(card2);

        return card1Rank - card2Rank;
    }

    static int GetCardRank(string card)
    {
        card = card.Substring(0, card.Length - 1);
        switch (card)
        {
            case "J":
                return 11;
            case "Q":
                return 12;
            case "K":
                return 13;
            case "A":
                return 14;
            default:
                return int.Parse(card);
        }
    }

}
