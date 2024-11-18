namespace bicycle2;

class Program
{
    static void Main(string[] args)
    {
        //task 1
        Task1();
        
        //task2 
        // Task2();
        
        //task3
        // Task3();
        
        //task4
        // Task4();
    }
    
    public static int RandomValue()
    {
        Random random = new Random();
        
        return random.Next(9) + 1;
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
    
    //task2
    public static void Task2()
    {
        // 153 == 1^3 + 5^3 + 3^3
        int n;
        do
        {
            Console.WriteLine("Enter a number greater than 0");
            n = int.Parse(Console.ReadLine().Trim());
        } while (n < 1);

        for (int i = 1; i <= n; i++)
        {
            if(IsArmstrong(i))
                Console.WriteLine($"{i} is Armstrong");
        }
    }

    public static bool IsArmstrong(int number)
    {
        int newNumber = number;
        int sum = 0;
        int digitsCount = newNumber.ToString().Length;

        for (int i = 0; i < digitsCount; i++)
        {
            int digit = newNumber % 10; // last digit
            sum += (int)Math.Pow(digit * 1.0, digitsCount * 1.0); // not safe, but for my task it's ok, cuz I need number without decimal part 
            newNumber /= 10; // decrease number to continue operations
        }

        return number == sum;
    }
    
    //task3
    public static void Task3()
    {
        int password = GeneratePassword();

        while (true)
        {
            if (GuessThePassword(password, false))
            {
                break;                
            }
        }
    }

    public static int GeneratePassword()
    {
        return ((RandomValue() * 10 + RandomValue()) * 10 + RandomValue()) * 10 + RandomValue();
    }

    public static bool GuessThePassword(int password, bool hack = false)
    {
        Console.WriteLine("What's password?");
        // Console.WriteLine($"my password is: {password}"); // for debug
        
        string userCorrect = AskUserInput(password, hack);
        
        if (!userCorrect.Contains("X"))
        {
            Console.WriteLine("You correct!");
            return true;
        }
        else
        {
            Console.WriteLine(userCorrect);
            gameLastOutput = userCorrect;
            return false;
        }
    }

    public static string AskUserInput(int password, bool hack = false)
    {
        int userInput;

        if (hack)
        {
            userInput = HackNumber(password);
        }
        else
        {
            userInput = UserNumber();
        }
        
        return FindNumber(userInput, password);
    }

    public static int UserNumber()
    {
        return int.Parse(Console.ReadLine().Trim());
    }

    // yeah yeah, naming is not good. My brain just leaked out
    public static string FindNumber(int whatFind, int whereFind)
    {
        string result = "";

        string strWhatFind = whatFind.ToString();
        string strWhereFind = whereFind.ToString();
        
        for (int i = 0; i < strWhatFind.Length; i++)
        {
            if (strWhatFind[i] == strWhereFind[i])
            {
                result += strWhatFind[i];
            }
            else
            {
                result += "X";
            }
        }
        
        return result;
    }

    public static void Task4()
    {
        // let's hack this sh*t!
        int password = GeneratePassword();

        HackTheGame(password);
    }

    public static string gameLastOutput = "XXXX";
    public static int hackNumberGuess = 1111;
    
    public static void HackTheGame(int password)
    {
        int steps = 0;
        
        while (true)
        {
            steps += 1;
            Console.WriteLine($"Hacker: \n\tStep: {steps}, guessing: {hackNumberGuess}");
            if (GuessThePassword(password, true))
            {
                Console.WriteLine($"Hacker: \n\tHacked!. For {steps} steps");
                break;                
            }

            string hackNumberGuessAsString = hackNumberGuess.ToString();

            for (int i = 0; i < gameLastOutput.Length; i++)
            {
                if (gameLastOutput[i] == 'X')
                {
                    int currentDigit = int.Parse(hackNumberGuessAsString[i].ToString());

                    currentDigit++;
                    
                    hackNumberGuessAsString = hackNumberGuessAsString.Remove(i, 1).Insert(i, currentDigit.ToString());
                    
                    hackNumberGuess = int.Parse(hackNumberGuessAsString);
                }
            }
        }
    }

    public static int HackNumber(int password)
    {
        return hackNumberGuess;
    }
}