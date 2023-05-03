using System;
using System.Collections.Generic;

class Solution
{
    static Queue<string> player1Deck = new Queue<string>(); // 1번 플레이어 카드 덱
    static Queue<string> player2Deck = new Queue<string>(); // 2번 플레이어 카드 덱
    static Queue<string> temporary_warDeck1 = new Queue<string>(); // 임시 전쟁 덱 1
    static Queue<string> temporary_warDeck2 = new Queue<string>(); // 임시 전쟁 덱 2
    static int gameturn = 0; // 게임 턴 수
    static void Main(string[] args)
    {
        // 입력 처리
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

        // 게임 진행
        while (player1Deck.Count > 0 && player2Deck.Count > 0)
        {
            ++gameturn;
            Fight();

            // 게임 진행 상황 출력
            #region Check the progress of the card game
            Console.Error.WriteLine($"{gameturn}번째 턴");
            Console.Error.Write($"player1Deck: ");
            foreach (string card in player1Deck)
            {
                Console.Error.Write($"{card} ");
            }
            Console.Error.Write($"\nplayer2Deck: ");
            foreach (string card in player2Deck)
            {
                Console.Error.Write($"{card} ");
            }
            Console.Error.Write($"\n\n\n");
            #endregion

        }

        // 결과 출력
        Console.WriteLine(player1Deck.Count > 0 ? $"1 {gameturn}" : $"2 {gameturn}");
    }

    static int Fight(string IsWarFun = "Fight")
    {
        // 각 플레이어가 카드 한 장씩 뽑음
        string player1Card = player1Deck.Dequeue();
        string player2Card = player2Deck.Dequeue();

        // CompareCards 함수를 사용하여 카드 랭크를 비교하고
        // player1이 이길 경우 player1Deck의 뒤에 두 카드를 추가하고 1을 반환합니다.
        if (CompareCards(player1Card, player2Card) > 0)
        {
            if (IsWarFun == "Fight")
            {
                player1Deck.Enqueue(player1Card);
                player1Deck.Enqueue(player2Card);
            }
            return 1;
        }
        // player2가 이길 경우 player2Deck의 뒤에 두 카드를 추가하고 2를 반환합니다.
        else if (CompareCards(player1Card, player2Card) < 0)
        {
            if (IsWarFun == "Fight")
            {
                player2Deck.Enqueue(player1Card);
                player2Deck.Enqueue(player2Card);
            }
            return 2;
        }
        else // 무승부일 경우
        {
            if (IsWarFun == "Fight")
            {
                // 임시 전쟁 덱에 카드 1장씩 추가
                temporary_warDeck1.Enqueue(player1Card);
                temporary_warDeck2.Enqueue(player2Card);

                // 전쟁 단계 실행
                War(player1Card, player2Card);
            }
            return 0;
        }
    }

    static void War(string player1Card, string player2Card)
    {
        // 3장의 카드를 뽑아 내려놓습니다.
        // (나중에 다시 이긴 사람의 덱에 들어가니 임시 전쟁덱에 넣습니다.)
        for (int i = 0; i < 3; i++)
        {
            // 플레이어 1 혹은 플레이어 2의 카드가 2장 이하로 남았으면 비깁니다.
            if (player1Deck.Count < 2 || player2Deck.Count < 2)
            {
                Console.WriteLine("PAT");
                Environment.Exit(0);
            }
            temporary_warDeck1.Enqueue(player1Deck.Dequeue());
            temporary_warDeck2.Enqueue(player2Deck.Dequeue());
        }

        // 결투를 위해 카드 한장을 더 뽑음(실제로 뽑는건 곁투에서 하니까 여긴 Peek())
        string card1 = player1Deck.Peek();
        string card2 = player2Deck.Peek();

        //뽑은 카드 정보를 임시 전쟁카드덱에 저장
        temporary_warDeck1.Enqueue(card1);
        temporary_warDeck2.Enqueue(card2);

        //결투를 벌입니다. 결과가 나오면 승자에게 사용한 카드를 몰아 줍니다.
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
        else // 다시 한 번 무승부가 되면 재귀적으로 War 함수를 호출합니다
        {
            War(card1, card2);
        }

    }

    //두 카드의 등급을 비교합니다.
    static int CompareCards(string card1, string card2)
    {
        int card1Rank = GetCardRank(card1);
        int card2Rank = GetCardRank(card2);

        return card1Rank - card2Rank;
    }

    //카드의 등급을 반환합니다.
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
