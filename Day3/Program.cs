

var result = 0;

var lines = File.ReadAllLines(@"C:\#Repositories\AdventOfCode\Day3\input.txt");
var lineArray = new List<string[]>();
for (int k = 0; k < lines.Length; k++)
{
    var foundDigit = false;
    var foundDigitCounter = 0;
    for (int i = 0; i < lines[k].Length; i++)
    {
        if (int.TryParse(lines[k][i].ToString(), out int digit))
        {
            foundDigit = true;
            foundDigitCounter++;

            if(i == lines[k].Length - 1)
            {
                if (k > 0 && !CheckLine(k, i, foundDigitCounter, lines[k - 1]))
                {
                    result += GetNumber(k, i+1, foundDigitCounter);
                    foundDigit = false;
                    foundDigitCounter = 0;
                    continue;
                }
                if (k + 1 < lines.Length && !CheckLine(k, i, foundDigitCounter, lines[k + 1]))
                {
                    result += GetNumber(k, i + 1, foundDigitCounter);
                    foundDigit = false;
                    foundDigitCounter = 0;
                    continue;
                }
                if (i - foundDigitCounter >= 0 && isSymbol(lines[k][i - foundDigitCounter]))
                {
                    result += GetNumber(k, i + 1, foundDigitCounter);
                    foundDigit = false;
                    foundDigitCounter = 0;
                    continue;
                }
                foundDigitCounter = 0;
                foundDigit = false;
            }
        }
        else
        {
            if (foundDigit)
            {
                if (k > 0 && !CheckLine(k, i-1, foundDigitCounter, lines[k-1])) {

                    result += GetNumber(k, i, foundDigitCounter);
                    foundDigit = false;
                    foundDigitCounter = 0;
                    continue;
                }
                if(k +1 < lines.Length && !CheckLine(k, i-1, foundDigitCounter, lines[k+1]))
                {
                    result += GetNumber(k, i, foundDigitCounter);
                    foundDigit = false;
                    foundDigitCounter = 0;
                    continue;
                }
                if (i - foundDigitCounter > 0 && isSymbol(lines[k][i - foundDigitCounter-1]) || isSymbol(lines[k][i]))
                {
                    result += GetNumber(k, i, foundDigitCounter);
                    foundDigit = false;
                    foundDigitCounter = 0;
                    continue;
                }
            }
            foundDigitCounter = 0;
            foundDigit = false;
        }
    }
}

int GetNumber(int k, int i, int foundDigitCounter)
{
    var numberString = "";
    for (int numberIndex = i - foundDigitCounter; numberIndex < i; numberIndex++)
    {
        numberString += lines[k][numberIndex].ToString();
    }
    return int.Parse(numberString);
}

bool CheckLine(int row, int column, int foundDigitCounter, string lines)
{
    for(int i = column +1; i > column - foundDigitCounter -1; i--)
    {
        if (i >=0 && i< lines.Length && !int.TryParse(lines[i].ToString(), out int digit) && lines[i].ToString() != ".")
        {
            return false;
        }
    }
    return true;
}

bool isSymbol(char v)
{
    if(!int.TryParse(v.ToString(), out int digit) && v.ToString() != ".")
    {
        return true;
    }
    return false;
}

Console.WriteLine("Result: " + result);



/////////////////////////////////////////////////// PART 2////////////////////////////////////////////////

long result2 = 0;
for (int i = 0; i < lines.Length; i++)
{
    for (int k = 0; k < lines[i].Length; k++) 
    {
        if (lines[i][k].ToString() == "*")
        { 
            var adjacentNumbers = new List<int>();
            CheckLineForNumber(k, lines[i-1], adjacentNumbers, false);
            CheckLineForNumber(k, lines[i+1], adjacentNumbers, false);
            CheckLineForNumber(k, lines[i], adjacentNumbers, true);
            //CheckBehindStar(k, lines[i], adjacentNumbers);

            if(adjacentNumbers.Count == 2) {
                result2 += adjacentNumbers[0] * adjacentNumbers[1];
            }
        }
    }
}


void CheckLineForNumber(int k, string line, List<int> adjacentNumbers, bool sameLine)
{
    var numberList = new List<FoundNumber>();
    var digitFound = false;
    var digitFoundCounter = 0;
    for(int i = 0;i< line.Length; i++)
    {
        if (int.TryParse(line[i].ToString(), out int digit))
        {
            digitFound = true;
            digitFoundCounter++;
            if (i + 1 == line.Length)
            {
                numberList.Add(GetFoundNumber(line, i+1, digitFoundCounter));

                digitFound = false;
                digitFoundCounter = 0;
            }
        }
        else
        {
            if (digitFound)
            {
                numberList.Add(GetFoundNumber(line, i, digitFoundCounter));
            }
            digitFound = false;
            digitFoundCounter = 0;
        }
    }
    foreach(FoundNumber number in numberList)
    {
        if (!sameLine && number.AdjacentIndexStart<= k && number.AdjacentIndexEnd>= k)
        {
            adjacentNumbers.Add(number.Number);
        }
        if (sameLine && (number.AdjacentIndexStart == k || number.AdjacentIndexEnd == k))
        {
            adjacentNumbers.Add(number.Number);
        }
    }
}

FoundNumber GetFoundNumber(string line, int i, int foundDigitCounter)
{
    var numberString = "";
    for (int numberIndex = i - foundDigitCounter; numberIndex < i; numberIndex++)
    {
        numberString += line[numberIndex].ToString();
    }
    var number = int.Parse(numberString);
    return new FoundNumber() { Number = number, AdjacentIndexStart = i-foundDigitCounter-1, AdjacentIndexEnd = i  };
}

Console.WriteLine("Result part 2 is: " + result2);
Console.ReadLine();

public class FoundNumber
{
    public int Number { get; set; }
    public int AdjacentIndexStart { get; set; }
    public int AdjacentIndexEnd { get; set; }
}
