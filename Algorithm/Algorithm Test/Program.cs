using System;

class Program
{
    static void Main()
    {
        int n = 50;
        Console.WriteLine($"Prime numbers up to {n} are: ");
        SieveOfEratosthenes(n);
    }

    static void SieveOfEratosthenes(int n)
    {
        bool[] prime = new bool[n + 1];
        for (int i = 0; i <= n; i++)
        {
            prime[i] = true;
        }

        for (int p = 2; p * p <= n; p++)
        {
            Console.WriteLine($"p: {p}");
            if (prime[p] == true)
            {
                for (int i = p * p; i <= n; i += p)
                {
                    prime[i] = false;
                }
            }
        }

        for (int p = 2; p <= n; p++)
        {
            if (prime[p] == true)
            {
                Console.Write(p + " ");
            }
        }
    }
}
