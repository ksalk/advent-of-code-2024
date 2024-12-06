var input = await File.ReadAllLinesAsync("input.txt");

var width = input[0].Length;
var height = input.Length;

var xmasCounter = 0;

string xmas = "XMAS";

for(int i = 0; i < height; i++)
{
    // row forward
    for(int j = 0 ; j < width - xmas.Length; j++)
    {
        var foundXmas = true;
        var q = 0;
        do
        {
            if (GetCharAt(i, j + q) == xmas[q])
                q++;
            else
            {
                foundXmas = false;
                break;
            }
        } while (q < xmas.Length);

        if (foundXmas)
        {
            xmasCounter++;
            j += xmas.Length - 1;
        }
    }

    // row backward
    for (int j = width - 1; j >= xmas.Length - 1; j--)
    {
        var foundXmas = true;
        var q = 0;
        do
        {
            if (GetCharAt(i, j - q) == xmas[q])
                q++;
            else
            {
                foundXmas = false;
                break;
            }
        } while (q < xmas.Length);

        if (foundXmas)
        {
            xmasCounter++;
            j -= xmas.Length - 1;
        }
    }
}

for(int i = 0; i < width; i++)
{
    // column forward
    for (int j = 0; j < height - xmas.Length; j++)
    {
        var foundXmas = true;
        var q = 0;
        do
        {
            if (GetCharAt(j + q, i) == xmas[q])
                q++;
            else
            {
                foundXmas = false;
                break;
            }
        } while (q < xmas.Length);

        if (foundXmas)
        {
            xmasCounter++;
            j += xmas.Length - 1;
        }
    }

    // column backward
    for (int j = height - 1; j >= xmas.Length - 1; j--)
    {
        var foundXmas = true;
        var q = 0;
        do
        {
            if (GetCharAt(j - q, i) == xmas[q])
                q++;
            else
            {
                foundXmas = false;
                break;
            }
        } while (q < xmas.Length);

        if (foundXmas)
        {
            xmasCounter++;
            j -= xmas.Length - 1;
        }
    }
}

for(int i = 0; i < width + height; i++)
{
    for(int j = 0; j < i; j++)
    {
        if(!ElementExists(j, i - j))
            continue;

        var foundXmas = true;
        var q = 0;
        do
        {
            if (ElementExists(j + q, i - j - q) && GetCharAt(j + q, i - j - q) == xmas[q])
                q++;
            else
            {
                foundXmas = false;
                break;
            }
        } while (q < xmas.Length);

        if (foundXmas)
        {
            xmasCounter++;
            j += xmas.Length - 1;
        }
    }
}

Console.WriteLine(xmasCounter);

char GetCharAt(int row, int column) => input[row][column];
bool ElementExists(int row, int column) => row >= 0 && row < height && column >= 0 && column < width;