Console.WriteLine("Puzzle 01 result:");
await Puzzle01();

Console.WriteLine("Puzzle 02 result:");
await Puzzle02();

async Task<int> Puzzle01()
{
    var inputFilePath = "input.txt";
    int safeLevelsCounter = 0;

    await foreach (var line in File.ReadLinesAsync(inputFilePath))
    {
        var level = line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
        if (IsLevelSafe(level))
        {
            safeLevelsCounter++;
            continue;
        }
    }

    Console.WriteLine(safeLevelsCounter);
    return safeLevelsCounter;
}

async Task<int> Puzzle02()
{
    var inputFilePath = "input.txt";
    int safeLevelsCounter = 0;

    await foreach (var line in File.ReadLinesAsync(inputFilePath))
    {
        var level = line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
        if (IsLevelSafe(level))
        {
            safeLevelsCounter++;
            continue;
        }

        for (int i = 0; i < level.Count; i++)
        {
            var newLevel = level.ToList();
            newLevel.RemoveAt(i);
            if (IsLevelSafe(newLevel))
            {
                safeLevelsCounter++;
                break;
            }
        }
    }

    Console.WriteLine(safeLevelsCounter);
    return safeLevelsCounter;
}

bool IsLevelSafe(List<int> level)
{
    var diffs = new int[level.Count - 1];
    for (int i = 0; i < diffs.Length; i++)
    {
        diffs[i] = level[i + 1] - level[i];
    }

    return diffs.All(d => d >= 1 && d <= 3) || diffs.All(d => d >= -3 && d <= -1);
}