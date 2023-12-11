

//Game 74: 12 green, 4 red, 4 blue; 3 red, 13 green; 1 red, 13 green, 1 blue; 1 red, 3 blue, 6 green; 6 blue, 5 red, 4 green; 7 blue, 5 green, 1 red

using System.Text.RegularExpressions;

var inputLines = File.ReadAllLines(@"C:\#Repositories\AdventOfCode\Day2\Day2\input.txt");
var maxRed = 12;
var maxGreen = 13;
var maxBlue = 14;

var games = new Dictionary<int, Game>();
var gameStillValid = false;
var result = 0;
var powerResult = 0;

foreach (var line in inputLines)
{
    gameStillValid = true;
    var sets = Regex.Replace(line,@"Game \d+: ", "").Split(';');
    var gameId = int.Parse(line.Split(':')[0].Replace("Game ", ""));

    games[gameId] = new Game();
    
    foreach (var set in sets)
    {
        var newSet = new Set();
        var setParts = set.Split(",");
        foreach (var part in setParts)
        {
            var numberAndColor = part.Trim().Split(' ');
            switch (numberAndColor[1])
            {
                case "blue":
                    newSet.Blue = int.Parse(numberAndColor[0]);
                    break;
                case "green":
                    newSet.Green = int.Parse(numberAndColor[0]);
                    break;
                case "red":
                    newSet.Red = int.Parse(numberAndColor[0]);
                    break;
                default:
                    break;
            }
        }
        games[gameId].Sets.Add(newSet);
        gameStillValid = gameStillValid && newSet.Blue<=maxBlue && newSet.Green<=maxGreen && newSet.Red<=maxRed;
    }
    if (gameStillValid)
    {
        result += gameId;
    }
    games[gameId].CalculatePower();
    powerResult += games[gameId].Power;
}

Console.WriteLine("Possible Game IDs summed up is: " + result);
Console.WriteLine("Power summed up is: " + powerResult);
Console.ReadLine();

internal class Game
{
    public List<Set> Sets = new List<Set>();
    public int Power { get; set;}

    public void CalculatePower()
    {
        var lowestBlue = 0;
        var lowestGreen = 0;
        var lowestRed = 0;

        foreach(var set in Sets)
        {
            if(set.Blue > lowestBlue)
                lowestBlue = set.Blue;
            if(set.Green > lowestGreen)
                lowestGreen = set.Green;
            if(set.Red > lowestRed)
                lowestRed = set.Red;
        }
        if(lowestBlue!= 0 &&  lowestGreen!= 0 &&lowestRed!= 0)
        {
            Power = lowestRed * lowestGreen * lowestBlue;
        }
        else
        {

        }
    }
}

internal class Set
{
    public int Blue { get; set; } = 0;
    public int Red { get; set; } = 0;
    public int Green { get; set; } = 0;
}