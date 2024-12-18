Console.WriteLine("Puzzle 01 result:");
Console.WriteLine(await Puzzle01());

async Task<long> Puzzle01()
{
    var inputFilePath = "input.txt";
    var input = (await File.ReadAllTextAsync(inputFilePath)).Where(char.IsDigit).ToArray();
    var length = input.Length;
    long result = 0;

    var diskMap = input.Select(x => x - '0').ToArray();
    var diskMapLength = diskMap.Sum();
    var disk = new int[diskMapLength];

    var isFile = true;
    var fileIdCounter = 0;
    var iterator = 0;
    for(int i = 0 ; i < diskMap.Length ; i++)
    {
        for (int j = 0 ; j < diskMap[i] ; j++)
        {
            disk[iterator] = isFile ? fileIdCounter : -1;
            iterator++;
        }
        isFile = !isFile;
        if(isFile)
        {
            fileIdCounter++;
        }
    }

    var nextEmptySlot = 0;
    var nextFileSlot = diskMapLength - 1;

    while(nextEmptySlot < nextFileSlot)
    {
        if(disk[nextEmptySlot] != -1)
        {
            nextEmptySlot++;
            continue;
        }
        
        if(disk[nextFileSlot] == -1)
        {
            nextFileSlot--;
            continue;
        }

        var temp = disk[nextEmptySlot];
        disk[nextEmptySlot] = disk[nextFileSlot];
        disk[nextFileSlot] = temp;

        nextEmptySlot++;
        nextFileSlot--;
    }

    for(int i = 0 ; i < diskMapLength ; i++)
    {
        if(disk[i] != -1)
        {
            result += disk[i] * i;
        }
    }
    
    return result;
}