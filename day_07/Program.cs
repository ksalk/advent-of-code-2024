Console.WriteLine("Puzzle 01 result:");
Console.WriteLine(await Puzzle01());

Console.WriteLine("Puzzle 02 result:");
Console.WriteLine(await Puzzle02());

async Task<long> Puzzle01()
{
    var inputFilePath = "input.txt";
    var lines = await File.ReadAllLinesAsync(inputFilePath);
    long result = 0;

    foreach (var line in lines)
    {
        var parts = line
            .Split(':', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        var expectedValue = long.Parse(parts.First());
        var numbers = parts.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        var numbersCount = numbers.Count;
        var allPossibilites = Math.Pow(2, numbersCount - 1);

        for(int i = 0 ; i < allPossibilites ; i++)
        {
            var possibility = i;
            var actualValue = numbers[0];
            for(int j = 1 ; j < numbersCount ; j++)
            {
                // 0 is addition
                // 1 is multiplication
                if((possibility & 1) == 0)
                {
                    actualValue += numbers[j];
                }
                else
                {
                    actualValue *= numbers[j];
                }
                possibility >>= 1;
            }

            if(actualValue == expectedValue)
            {
                result += expectedValue;
                break;
            }
        }
    }

    return result;
}

async Task<long> Puzzle02()
{
    var inputFilePath = "input.txt";
    var lines = await File.ReadAllLinesAsync(inputFilePath);
    long result = 0;

    foreach (var line in lines)
    {
        var parts = line
            .Split(':', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        var expectedValue = long.Parse(parts.First());
        var numbers = parts.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        var numbersCount = numbers.Count;
        var allPossibilites = Math.Pow(3, numbersCount - 1);

        for(int i = 0 ; i < allPossibilites ; i++)
        {
            var possibility = i;
            var actualValue = numbers[0];
            for(int j = 1 ; j < numbersCount ; j++)
            {
                // 0 is addition
                // 1 is multiplication
                // 2 is concat
                var rem = possibility % 3;
                if(rem == 0)
                {
                    actualValue += numbers[j];
                }
                else if(rem == 1)
                {
                    actualValue *= numbers[j];
                }
                else
                {
                    var numberOfDigits = Math.Floor(Math.Log10(numbers[j]) + 1);
                    actualValue = actualValue * (long)Math.Pow(10, numberOfDigits) + numbers[j];
                }
                possibility /= 3;
            }

            if(actualValue == expectedValue)
            {
                result += expectedValue;
                break;
            }
        }
    }

    return result;
}