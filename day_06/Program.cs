Console.WriteLine("Puzzle 01 result:");
Console.WriteLine(await Puzzle01());

async Task<int> Puzzle01()
{
    var guardLocations = 0;
    var guards = new[] {'<', '^' ,'>', 'v'};
    var inputFilePath = "input.txt";
    var lines = await File.ReadAllLinesAsync(inputFilePath);

    var rowCount = lines.Count();
    var colCount = lines.First().Count();

    var grid = new char[rowCount, colCount];
    var visited = new bool[rowCount, colCount];

    var guardPosition = (-1, -1);
    for(int i = 0 ; i < rowCount ; i++)
    {
        for(int j = 0 ; j < colCount ; j++)
        {
            grid[i, j] = lines[i][j];
            if(guards.Contains(lines[i][j]))
            {
                guardPosition = (i, j);
            }
        }
    }
    var currentGuard = grid[guardPosition.Item1, guardPosition.Item2];

    do{
        if(!visited[guardPosition.Item1, guardPosition.Item2])
        {
            visited[guardPosition.Item1, guardPosition.Item2] = true;
            guardLocations++;
        }

        var nextPosition = GetNextPosition(currentGuard, guardPosition);
        if(!IsOutOfBounds(nextPosition.Item1, nextPosition.Item2) && grid[nextPosition.Item1, nextPosition.Item2] == '#')
        {
            currentGuard = GetNextGuard(currentGuard);
        }
        else
        {
            guardPosition = nextPosition;
        }
    }while(!IsOutOfBounds(guardPosition.Item1, guardPosition.Item2));    
    
    return guardLocations;

    bool IsOutOfBounds(int row, int column) => row < 0 || row >= rowCount || column < 0 || column >= colCount;
    (int, int) GetNextPosition(char guard, (int Row, int Col) currentPosition)
    {
        switch(guard)
        {
            case '<':
                return (currentPosition.Row, currentPosition.Col - 1);
            case '>':
                return (currentPosition.Row, currentPosition.Col + 1);
            case '^':
                return (currentPosition.Row - 1, currentPosition.Col);
            case 'v':
                return (currentPosition.Row + 1, currentPosition.Col);
            default:
                throw new InvalidOperationException();
        }
    }
    char GetNextGuard(char currentGuard) => guards[(Array.IndexOf(guards, currentGuard) + 1) % guards.Length];
}