using System.Text.RegularExpressions;

var result = 0;
var pattern = @"\d|zero|one|two|three|four|five|six|seven|eight|nine";

Dictionary<string, int> numberTable =
    new Dictionary<string, int>
        {{"zero",0},{"one",1},{"two",2},{"three",3},{"four",4},
        {"five",5},{"six",6},{"seven",7},{"eight",8},{"nine",9} };

if (File.Exists(@"C:\#Repositories\AdventOfCode\Day1\static\input.txt"))
{
    var inputLines = File.ReadAllLines(@"C:\#Repositories\AdventOfCode\Day1\static\input.txt");


    foreach (var line in inputLines)
    {
        var matchFirstDigit = Regex.Match(line, pattern);
        var matchLastDigit = Regex.Match(line, pattern, RegexOptions.RightToLeft);
        if (!string.IsNullOrEmpty(matchFirstDigit.Value))
        {
            if (!int.TryParse(matchFirstDigit.Value, out int firstDigit))
            {
                firstDigit = numberTable[matchFirstDigit.Value];
            }

            if (matchLastDigit.Value != null)
            {
                if (!int.TryParse(matchLastDigit.Value, out int lastDigit))
                {
                    lastDigit = numberTable[matchLastDigit.Value];
                }
                Console.WriteLine($"{line} translates to {firstDigit} and {lastDigit} which results in number {int.Parse($"{firstDigit}{lastDigit}")}");

                result += int.Parse($"{firstDigit}{lastDigit}");
            }
            else
            {
                result += int.Parse($"{firstDigit}{firstDigit}");
            }
        }
    }
}

Console.WriteLine($"Result is {result}");
Console.ReadLine();
