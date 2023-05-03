using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static Dictionary<char, int> letterPoints = new Dictionary<char, int>()
{
    {'e', 1}, {'a', 1}, {'i', 1}, {'o', 1}, {'n', 1}, {'r', 1}, {'t', 1}, {'l', 1}, {'s', 1}, {'u', 1},
    {'d', 2}, {'g', 2},
    {'b', 3}, {'c', 3}, {'m', 3}, {'p', 3},
    {'f', 4}, {'h', 4}, {'v', 4}, {'w', 4}, {'y', 4},
    {'k', 5},
    {'j', 8}, {'x', 8},
    {'q', 10}, {'z', 10}
};

    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        string[] dictionary = new string[N];
        for (int i = 0; i < N; i++)
        {
            dictionary[i] = Console.ReadLine();
            
        }
        string letters = Console.ReadLine();

        int max_score = 0; // 최고 점수
        string max_score_world = null; // 최고 점수에 해당하는 단어

        foreach (string word in dictionary)
        {
            if(is_word_feasible(word, letters))
            {
                int score = get_word_score(word);
                if (score > max_score)
                {
                    max_score = score;
                    max_score_world = word;
                }
            }
        }

        Console.WriteLine(max_score_world);
    }

    //반복문을 이용하여 사전의 모든 단어를 확인하고, 각 단어가 주어진 알파벳 꾸러미로 조합 가능한지 확인하는 함수
    static bool is_word_feasible(string word, string letters)
    {
        bool feasible = true;
        // 단어가 조합 가능한지 확인하기 위해서는 다음 2가지 조건이 필요하다.
        // 1. 단어에 쓰인 모든 글자가 알파벳 꾸러미에 있는지 확인합니다.
        // 2. 단어에 쓰인 글자의 개수만큼 알파벳 꾸러미에 있는지 확인합니다.


        return feasible;
    }

    //주어진 알파벳으로 글자로 조합 가능한 경우, 단어의 점수를 계산하는 함수
    static int get_word_score(string word)
    {
        int score = 0;
        foreach(var word_char in word)
        {
            score += letterPoints[word_char];
        }
        return 0;
    }
}