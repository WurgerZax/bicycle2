namespace bicycle2;

class Program
{
    static void Main(string[] args)
    {
        //task 1
        Task1();
    }

    public static void Task1()
    {
        Console.WriteLine("Nice to meet you");
        Console.WriteLine("Make a bet $5-$100");

        int bet = int.Parse(Console.ReadLine().Trim());
        
        while (bet % 5 != 0 || bet > 100 || bet < 5)
        {
            Console.WriteLine("You must enter a number which divisions entirely on 5 and in diapason 5-100");
            bet = int.Parse(Console.ReadLine().Trim());
        }

        // int bet = 5; // for tests

        Console.WriteLine("----------");
        
        int combination = (RandomValue() * 10 + RandomValue()) * 10 + RandomValue();

        // int combination = 777; // for tests

        Console.WriteLine("Dropped combination: " + combination);
        
        double coefficient = GetCoefficient(combination/100, combination % 100 / 10, combination % 10);

        double winFormula = bet * coefficient;

        if (coefficient == 1.0)
        {
            Console.WriteLine("You lost");

            AskToRepeat();
        }
        else
        {
            string jackpot = "";
            
            if (combination % 10 == 7)
            {
                jackpot = combination / 100 == 7 && combination % 10 == 7 ? "" : "mini" + " Jackpot";   
            }
            Console.WriteLine("Your win: " + winFormula + $" {jackpot}");
            
            AskToRepeat();
        }
    }

    public static int RandomValue()
    {
        Random random = new Random();
        
        return random.Next(9) + 1;
    }

    public static double GetCoefficient(int num1, int num2, int num3)
    {
        double coefficient; // num1 * 10 * 1.5 || num1 * 1 * 1.25
        // for 7 -> 150 * 1.5 || 15 * 1.25
        if (num1 == num2) 
        {
            int firstDigit = num1 == 7 ? 15 : num1;
            coefficient = num1 == num3 ? firstDigit * 10 * 1.5 : firstDigit * 1.25;
        }
        // в условиях задачи не сказано, что делать если в комбинации есть и 7 и 9. Так что было принято решение, что коэфициент 7-рки имеет высший приоритет
        else if (num1 == 7 || num2 == 7 || num3 == 7)
        {
            coefficient = 1.6;
        }
        else if (num1 == 9 || num2 == 9 || num3 == 9)
        {
            coefficient = 1.35;
        }
        else
        {
            coefficient = 1.0;
        }
        return coefficient;
    }

    public static void AskToRepeat()
    {
        Console.WriteLine("Wanna try again? (Press [y] if you agree)");
        string input = Console.ReadLine().Trim().ToLower();
        if (input == "y")
        {
            Console.Clear();
            Task1();
        }
    }
}