Console.WriteLine("Puzzle 01 result"); 
Console.WriteLine(await Puzzle01());

// implementation without regex's
async Task<int> Puzzle01()
{
    var input = await File.ReadAllTextAsync("input.txt");

    var n = input.Length;
    var sum = 0;

    for (var i = 0 ; i < n - 8; )
    {
        if (input[i] == 'm' && input[i+1] == 'u' && input[i+2] == 'l' && input[i+3] == '(')
        {
            var subindex = i + 4;

            // found 'mul(', get 
            var firstNumberLength = 0;
            var firstNumber = 0;
            do
            {
                var currentChar = input[subindex + firstNumberLength];
                if (input[subindex + firstNumberLength] >= '0' && input[subindex + firstNumberLength] <= '9')
                {   
                    firstNumber = firstNumber * 10 + (input[subindex + firstNumberLength] - '0');
                    firstNumberLength++;
                }
                else if(input[subindex + firstNumberLength] == ',')
                {
                    break;
                }
                else
                {
                    firstNumberLength = -1;
                    break;
                }
            } while(firstNumberLength <= 4);

            // found sth outer than digit or coma or number is 4 digits long
            if(firstNumberLength == -1 || firstNumberLength == 4)
            {
                i++;
                continue;
            }

            var secondNumberLength = 0;
            var secondNumber = 0;
            do
            {
                if(input[subindex + firstNumberLength + 1 + secondNumberLength] >= '0' && input[subindex + firstNumberLength + 1 + secondNumberLength] <= '9')
                {   
                    secondNumber = secondNumber * 10 + (input[subindex + firstNumberLength + 1 + secondNumberLength] - '0');
                    secondNumberLength++;
                }
                else if(input[subindex + firstNumberLength + 1 + secondNumberLength] == ')')
                {
                    break;
                }
                else
                {
                    secondNumberLength = -1;
                    break;
                }
            } while(secondNumberLength <= 4);

            // found sth outer than digit or ) or number is 4 digits long
            if(secondNumberLength == -1 || secondNumberLength == 4)
            {
                i++;
                continue;
            }

            // all good
            sum += firstNumber * secondNumber;
            i += 4 + firstNumberLength + 1 + secondNumberLength + 1;
        }
        else
        {
            i++;
        }
    };


    return sum;
}