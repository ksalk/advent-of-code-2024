Console.WriteLine("Puzzle 01 result:");
await Puzzle01();

Console.WriteLine("Puzzle 02 result:");
await Puzzle02();

async Task<int> Puzzle01()
{
    var inputFilePath = "input.txt";

    var list1 = new List<int>();
    var list2 = new List<int>();

    await foreach (var line in File.ReadLinesAsync(inputFilePath))
    {
        var pair = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        list1.Add(int.Parse(pair[0]));
        list2.Add(int.Parse(pair[1]));
    }

    list1 = list1.Order().ToList();
    list2 = list2.Order().ToList();

    var result = list1
        .Zip(list2, (x, y) => Math.Abs(x - y))
        .Sum();

    Console.WriteLine(result);
    return result;
}

async Task<int> Puzzle02()
{
    var inputFilePath = "input.txt";

    var hashset = new HashSet<int>();
    var list2 = new List<int>();

    await foreach (var line in File.ReadLinesAsync(inputFilePath))
    {
        var pair = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        hashset.Add(int.Parse(pair[0]));
        list2.Add(int.Parse(pair[1]));
    }

    var result = list2
        .Where(x => hashset.Contains(x))
        .Sum();

    Console.WriteLine(result);
    return result;
}