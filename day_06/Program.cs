Console.WriteLine("Puzzle 01 result:");
Console.WriteLine(await Puzzle01());

Console.WriteLine("Puzzle 02 result:");
Console.WriteLine(await Puzzle02());

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

async Task<int> Puzzle02()
{
    var guards = new[] {'<', '^' ,'>', 'v'};
    var inputFilePath = "input.txt";
    var lines = await File.ReadAllLinesAsync(inputFilePath);

    var rowCount = lines.Count();
    var colCount = lines.First().Count();

    var grid = new char[rowCount, colCount];

    var guardStartingPosition = (-1, -1);
    var startingGuard = ' ';
    for(int i = 0 ; i < rowCount ; i++)
    {
        for(int j = 0 ; j < colCount ; j++)
        {
            grid[i, j] = lines[i][j];
            if(guards.Contains(lines[i][j]))
            {
                guardStartingPosition = (i, j);
                startingGuard = lines[i][j];
            }
        }
    }
    
    var countLoops = 0;
    for(int i = 0 ; i < rowCount ; i++)
    {
        for(int j = 0 ; j < colCount ; j++)
        {
            if(grid[i, j] != '.')
            {
                continue;
            }
            
            grid[i, j] = '#';

            var guardPosition = guardStartingPosition;
            var currentGuard = grid[guardPosition.Item1, guardPosition.Item2];
            var guardsTouchedObstacle = new List<(char, int, int)>();

            do{
                var nextPosition = GetNextPosition(currentGuard, guardPosition);
                if(!IsOutOfBounds(nextPosition.Item1, nextPosition.Item2) && grid[nextPosition.Item1, nextPosition.Item2] == '#')
                {
                    if(guardsTouchedObstacle.Any(x => x.Item1 == currentGuard && x.Item2 == nextPosition.Item1 && x.Item3 == nextPosition.Item2))
                    {
                        countLoops++;
                        break;
                    }
                    guardsTouchedObstacle.Add((currentGuard, nextPosition.Item1, nextPosition.Item2));
                    currentGuard = GetNextGuard(currentGuard);
                }
                else
                {
                    guardPosition = nextPosition;
                }
            }while(!IsOutOfBounds(guardPosition.Item1, guardPosition.Item2));    

            grid[i, j] = '.';
        }
    }

    return countLoops;

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