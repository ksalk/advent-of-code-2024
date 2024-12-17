await Puzzle01();
await Puzzle02();

async Task Puzzle01()
{
    var input = (await File.ReadAllTextAsync("input.txt")).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
    var rules = input[0].Split("\n", StringSplitOptions.RemoveEmptyEntries);
    var pages = input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);
    
    var orderingRules = new List<(int BeforeItem, int AfterItem)>();
    var bannedNumbers = new Dictionary<int, List<int>>();
    foreach (var rule in rules)
    {
        var parts = rule.Split("|", StringSplitOptions.RemoveEmptyEntries);
        orderingRules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
    }

    var middlesSum = 0;
    foreach (var page in pages)
    {
        var isBanned = false;
        var bannedNumbersForPage = new HashSet<int>();
        var pageNumbers = page.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        foreach (var orderingRule in orderingRules)
        {
            if (pageNumbers.Contains(orderingRule.BeforeItem) && pageNumbers.Contains(orderingRule.AfterItem) &&
                pageNumbers.IndexOf(orderingRule.BeforeItem) > pageNumbers.IndexOf(orderingRule.AfterItem))
            {
                isBanned = true;
                break;
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
    var rules = input[0].Split("\n", StringSplitOptions.RemoveEmptyEntries);
    var pages = input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);
    
    var orderingRules = new List<(int BeforeItem, int AfterItem)>();
    var bannedNumbers = new Dictionary<int, List<int>>();
    foreach (var rule in rules)
    {
        var parts = rule.Split("|", StringSplitOptions.RemoveEmptyEntries);
        orderingRules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
    }

    var middlesSum = 0;
    foreach (var page in pages)
    {
        var bannedNumbersForPage = new HashSet<int>();
        var pageNumbers = page.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        var brokenRules = GetBrokenRules(pageNumbers);

        if(brokenRules.Any())
        {
            do
            {
                var firstBrokenRule = brokenRules.First();

                var beforeItemIndex = pageNumbers.IndexOf(firstBrokenRule.BeforeItem);
                var afterItemIndex = pageNumbers.IndexOf(firstBrokenRule.AfterItem);

                pageNumbers[beforeItemIndex] = firstBrokenRule.AfterItem;
                pageNumbers[afterItemIndex] = firstBrokenRule.BeforeItem;

                brokenRules = GetBrokenRules(pageNumbers);
            }while(brokenRules.Any());

            middlesSum += pageNumbers[(pageNumbers.Count - 1) / 2];
        }
    }

    Console.WriteLine(middlesSum);

    List<(int BeforeItem, int AfterItem)> GetBrokenRules(List<int> pageNumbers)
    {
        var brokenRules = new List<(int BeforeItem, int AfterItem)>();
        foreach (var orderingRule in orderingRules)
        {
            if (pageNumbers.Contains(orderingRule.BeforeItem) && pageNumbers.Contains(orderingRule.AfterItem) &&
                pageNumbers.IndexOf(orderingRule.BeforeItem) > pageNumbers.IndexOf(orderingRule.AfterItem))
            {
                brokenRules.Add(orderingRule);
            }
        }

        return brokenRules;
    }
}