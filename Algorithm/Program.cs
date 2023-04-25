using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
class Solution
{
    static void Main(string[] args)
    {
        Queue<string> player1Deck = new Queue<string>();
        Queue<string> player2Deck = new Queue<string>();

        int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
        for (int i = 0; i < n; i++)
        {
            string card = Console.ReadLine(); // the n cards of player 1
            player1Deck.Enqueue(card);
        }

        int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
        for (int i = 0; i < m; i++)
        {
            string card = Console.ReadLine(); // the m cards of player 2
            player2Deck.Enqueue(card);
        }

        // Play the game as long as both players have cards
        int turn = 0; // variable to count the number of turns that have passed
        while (player1Deck.Count > 0 && player2Deck.Count > 0)
        {
            turn++;
            //Console.Error.WriteLine($"player1Deck: {player1Deck.Count}, player2Deck: {player2Deck.Count}");

            Console.Error.Write($"{turn}번째 턴 player1Deck: ");
            foreach (string card in player1Deck)
            {
                Console.Error.Write($"{card} ");
            }
            Console.Error.Write($"\tplayer2Deck: ");
            foreach (string card in player2Deck)
            {
                Console.Error.Write($"{card}, ");
            }
            Console.Error.Write($"\n\n");
            Fight(ref player1Deck, ref player2Deck, "Fight");
        }

        // Print the result of the game
        Console.WriteLine(player1Deck.Count > 0 ? $"1 {turn}" : $"2 {turn}");
    }

    static int Fight(ref Queue<string> player1Deck, ref Queue<string> player2Deck, string IsWarFun)
    {
        string player1Card = player1Deck.Dequeue();
        string player2Card = player2Deck.Dequeue();

        //승자의 카드덱에 카드 게임의 규칙에서 정한대로 player1Card, player2Card 순으로 추가해야함
        //규칙 제대로 이해 못해서 이부분 때문에 계속 틀림 ㅠㅠ
        if (CompareCards(player1Card, player2Card) > 0)
        {
            //war에서 결투를 할 때 war함수에서 카드덱에 카드를 넣는 기능을 써야하는데 디버깅 했더니 결투 함수에서도 카드를 넣고있음
            //그래서 war함수에서는 안 섞이게 막음
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
        else // war
        {
            if (IsWarFun == "Fight")
            {
                War(ref player1Deck, ref player2Deck, player1Card, player2Card);
            }
            return 0;
        }
    }

    //player1Card와 player2Card도 전쟁이 끝난 이후에는 deck에 다시 들어가야 하므로 매개변수로 같이 넣어줍니다.
    static void War(ref Queue<string> player1Deck, ref Queue<string> player2Deck, string player1Card, string player2Card)
    {
        //전쟁에서 소모한 카드를 담을 배열을 선언
        Queue<string> used_warDeck1 = new Queue<string>();
        Queue<string> used_warDeck2 = new Queue<string>();

        //결투에 쓴 카드 한 장을 큐에 저장
        used_warDeck1.Enqueue(player1Card);
        used_warDeck2.Enqueue(player2Card);

        for (int i = 0; i < 3; i++)
        {
            //카드가 한장이 남아있으면 카드를 두번 뽑기 때문에 무승부가 나게 됨
            if (player1Deck.Count < 2 && player2Deck.Count < 2)
            {
                Console.WriteLine("PAT");
                return;
            }
            //카드 3장을 각가의 임시 전쟁카드덱에 저장
            used_warDeck1.Enqueue(player1Deck.Dequeue());
            used_warDeck2.Enqueue(player2Deck.Dequeue());
        }

        //결투를 위해 카드 한장을 더 뽑음
        string card1 = player1Deck.Dequeue();
        string card2 = player2Deck.Dequeue();
        //뽑은 카드 정보를 임시 전쟁카드덱에 저장
        used_warDeck1.Enqueue(card1);
        used_warDeck2.Enqueue(card2);

        //결투를 벌입니다. 결과가 나오면 승자에게 사용한 카드를 몰아 줍니다.
        int winPlayer = Fight(ref player1Deck, ref player2Deck, "War");
        if (winPlayer == 1) //플레이어1의 승리
        {
            foreach (var warcard in used_warDeck1)
            {
                player1Deck.Enqueue(warcard);
            }
            foreach (var warcard in used_warDeck2)
            {
                player1Deck.Enqueue(warcard);
            }
        }
        else if (winPlayer == 2) //플레이어1의 승리
        {
            foreach (var warcard in used_warDeck1)
            {
                player2Deck.Enqueue(warcard);
            }
            foreach (var warcard in used_warDeck2)
            {
                player2Deck.Enqueue(warcard);
            }
        }
        else
        {
            War(ref player1Deck, ref player2Deck, card1, card2);
        }

    }

    static int CompareCards(string card1, string card2)
    {
        string cardValues = "23456789" + "10" + "JQKA";
        int cardValueIndex1 = card1[0] == '1' && card1[1] == '0' ? 9 : cardValues.IndexOf(card1[0]);
        int cardValueIndex2 = card2[0] == '1' && card2[1] == '0' ? 9 : cardValues.IndexOf(card2[0]);

        return (cardValueIndex1 - cardValueIndex2);
    }
}
