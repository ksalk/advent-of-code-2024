await Puzzle01();

async Task Puzzle01()
{
    var input = (await File.ReadAllTextAsync("input.txt")).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
    var rules = input[0].Split("\n", StringSplitOptions.RemoveEmptyEntries);
    var pages = input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);
    
    var bannedNumbers = new Dictionary<int, List<int>>();
    foreach (var rule in rules)
    {
        var parts = rule.Split("|", StringSplitOptions.RemoveEmptyEntries);
        if(bannedNumbers.ContainsKey(int.Parse(parts[1])))
            bannedNumbers[int.Parse(parts[1])].Add(int.Parse(parts[0]));
        else
            bannedNumbers.Add(int.Parse(parts[1]), new List<int> { int.Parse(parts[0]) });
    }

    var middlesSum = 0;
    foreach (var page in pages)
    {
        var isBanned = false;
        var bannedNumbersForPage = new HashSet<int>();
        var pageNumbers = page.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        foreach (var pageNumber in pageNumbers)
        {
            if (bannedNumbersForPage.Contains(pageNumber))
            {
                isBanned = true;
                break;
            }

            foreach (var bannedNumber in bannedNumbers[pageNumber])
            {
                bannedNumbersForPage.Add(bannedNumber);
            }
        }

        if(!isBanned)
            middlesSum += pageNumbers[(pageNumbers.Count - 1) / 2];
    }

    Console.WriteLine(middlesSum);
}

async Task Puzzle02()
{
    var input = (await File.ReadAllTextAsync("input.txt")).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
    var rulesStrings = input[0].Split("\n", StringSplitOptions.RemoveEmptyEntries);
    var pages = input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);
    
    var bannedNumbers = new Dictionary<int, List<int>>();
    foreach (var rule in rulesStrings)
    {
        var parts = rule.Split("|", StringSplitOptions.RemoveEmptyEntries);
        if(bannedNumbers.ContainsKey(int.Parse(parts[1])))
            bannedNumbers[int.Parse(parts[1])].Add(int.Parse(parts[0]));
        else
            bannedNumbers.Add(int.Parse(parts[1]), new List<int> { int.Parse(parts[0]) });
    }
    
    var rules = rulesStrings.Select(r => r.Split("|", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();

    var middlesSum = 0;
    foreach (var page in pages)
    {
        var isBanned = false;
        var bannedNumbersForPage = new HashSet<int>();
        var pageNumbers = page.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        foreach (var pageNumber in pageNumbers)
        {
            if (bannedNumbersForPage.Contains(pageNumber))
            {
                isBanned = true;
                break;
            }

            foreach (var bannedNumber in bannedNumbers[pageNumber])
            {
                bannedNumbersForPage.Add(bannedNumber);
            }
        }

        if(isBanned)
        {
            // attempt to fix
            bool isFixed;
            do
            {
                isFixed = true;
                bannedNumbersForPage = new HashSet<int>();
                foreach (var pageNumber in pageNumbers)
                {
                    if (bannedNumbersForPage.Contains(pageNumber))
                    {
                        isFixed = false;
                        break;
                    }

                    foreach (var bannedNumber in bannedNumbers[pageNumber])
                    {
                        bannedNumbersForPage.Add(bannedNumber);
                    }
                }
            }while(!isFixed);

            middlesSum += pageNumbers[(pageNumbers.Count - 1) / 2];
        }
    }

    Console.WriteLine(middlesSum);
}