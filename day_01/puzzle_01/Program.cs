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