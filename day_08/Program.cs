Console.WriteLine("Puzzle 01 result:");
Console.WriteLine(await Puzzle01());

Console.WriteLine("Puzzle 02 result:");
Console.WriteLine(await Puzzle02());

async Task<long> Puzzle01()
{
    var inputFilePath = "input.txt";
    var lines = await File.ReadAllLinesAsync(inputFilePath);
    var width = lines.First().Length;
    var height = lines.Count();
    var antennasByFreq = new Dictionary<char, List<(int Row, int Col)>>();
    long result = 0;

    var antinodes = new bool[height, width];
    
    for(int i = 0 ; i < height ; i++)
    {
        for(int j = 0 ; j < width ; j++)
        {
            if(lines[i][j] != '.')
            {
                var isFreqInDict = antennasByFreq.ContainsKey(lines[i][j]);
                if(isFreqInDict)
                {
                    antennasByFreq[lines[i][j]].Add((i, j));
                }
                else
                {
                    antennasByFreq[lines[i][j]] = new List<(int Row, int Col)>{(i, j)};
                }
            }
        }
    }

    foreach(var frequency in antennasByFreq)
    {
        var allAntennasPairs = new List<((int Row, int Col) FirstAntenna, (int Row, int Col) SecondAntenna)>();
        var antennasCount = frequency.Value.Count;
        for(int i = 0 ; i < antennasCount ; i++)
        {
            for(int j = i + 1 ; j < antennasCount ; j++)
            {
                allAntennasPairs.Add((frequency.Value[i], frequency.Value[j]));
            }
        }

        foreach (var antennaPair in allAntennasPairs)
        {
            var xDiff = antennaPair.SecondAntenna.Col - antennaPair.FirstAntenna.Col;
            var yDiff = antennaPair.SecondAntenna.Row - antennaPair.FirstAntenna.Row;

            var firstAntinode = (antennaPair.FirstAntenna.Row - yDiff, antennaPair.FirstAntenna.Col - xDiff);
            var secondAntinode = (antennaPair.SecondAntenna.Row + yDiff, antennaPair.SecondAntenna.Col + xDiff);

            if(IsInBounds(firstAntinode.Item1, firstAntinode.Item2))
            {
                antinodes[firstAntinode.Item1, firstAntinode.Item2] = true;
            }

            if(IsInBounds(secondAntinode.Item1, secondAntinode.Item2))
            {
                antinodes[secondAntinode.Item1, secondAntinode.Item2] = true;
            }
        }
    }

    for(int i = 0 ; i < height ; i++)
    {
        for(int j = 0 ; j < width ; j++)
        {
            if(antinodes[i, j])
            {
                result++;
            }
        }
    }
    
    return result;

    bool IsInBounds(int row, int col)
    {
        return row >= 0 && row < height && col >= 0 && col < width;
    }
}

async Task<long> Puzzle02()
{
    var inputFilePath = "input.txt";
    var lines = await File.ReadAllLinesAsync(inputFilePath);
    var width = lines.First().Length;
    var height = lines.Count();
    var antennasByFreq = new Dictionary<char, List<(int Row, int Col)>>();
    long result = 0;

    var antinodes = new bool[height, width];
    
    for(int i = 0 ; i < height ; i++)
    {
        for(int j = 0 ; j < width ; j++)
        {
            if(lines[i][j] != '.')
            {
                var isFreqInDict = antennasByFreq.ContainsKey(lines[i][j]);
                if(isFreqInDict)
                {
                    antennasByFreq[lines[i][j]].Add((i, j));
                }
                else
                {
                    antennasByFreq[lines[i][j]] = new List<(int Row, int Col)>{(i, j)};
                }
            }
        }
    }

    foreach(var frequency in antennasByFreq)
    {
        var allAntennasPairs = new List<((int Row, int Col) FirstAntenna, (int Row, int Col) SecondAntenna)>();
        var antennasCount = frequency.Value.Count;
        for(int i = 0 ; i < antennasCount ; i++)
        {
            for(int j = i + 1 ; j < antennasCount ; j++)
            {
                allAntennasPairs.Add((frequency.Value[i], frequency.Value[j]));
            }
        }

        foreach (var antennaPair in allAntennasPairs)
        {
            var xDiff = antennaPair.SecondAntenna.Col - antennaPair.FirstAntenna.Col;
            var yDiff = antennaPair.SecondAntenna.Row - antennaPair.FirstAntenna.Row;

            antinodes[antennaPair.FirstAntenna.Col, antennaPair.FirstAntenna.Row] = true;
            antinodes[antennaPair.SecondAntenna.Col, antennaPair.SecondAntenna.Row] = true;

            var q = 1;
            while (true)
            {
                var antinode = (antennaPair.FirstAntenna.Row - q * yDiff, antennaPair.FirstAntenna.Col - q * xDiff);
                if(!IsInBounds(antinode.Item1, antinode.Item2))
                {
                    break;
                }

                antinodes[antinode.Item1, antinode.Item2] = true;
                q++;
            }

            q = 1;
            while (true)
            {
                var antinode = (antennaPair.SecondAntenna.Row + q * yDiff, antennaPair.SecondAntenna.Col + q * xDiff);
                if(!IsInBounds(antinode.Item1, antinode.Item2))
                {
                    break;
                }

                antinodes[antinode.Item1, antinode.Item2] = true;
                q++;
            }
        }
    }

    for(int i = 0 ; i < height ; i++)
    {
        for(int j = 0 ; j < width ; j++)
        {
            if(antinodes[i, j])
            {
                result++;
            }
        }
    }
    
    return result;

    bool IsInBounds(int row, int col)
    {
        return row >= 0 && row < height && col >= 0 && col < width;
    }
}