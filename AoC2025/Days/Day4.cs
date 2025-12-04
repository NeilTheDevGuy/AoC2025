using System.Text;

namespace AoC2025.Days;

public static class Day4
{
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(4);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //1451
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //8701
    }

    private static async Task PartOne(string[] input) 
    {
        var rollCount = 0;
        var y = 0;        
        foreach (var line in input)
        {
            for (int x = 0; x < line.Length; x++)
            {
                if (IsRoll(input, x, y)) {
                    var count = CountAdjacent(input, x, y);
                    if (count < 4) {
                        rollCount++;
                    }
                }
            }            
            y++;
        }
        Console.WriteLine($"Part One: {rollCount}");
    }

    private static async Task PartTwo(string[] input)
    {
        var rollCount = 0;
        var prevRollCount = 0;
                
        while (true) {
            var y = 0;
            foreach (var line in input)
            {
                var lineSb = new StringBuilder();
                lineSb.Append(line);
                for (int x = 0; x < line.Length; x++)
                {
                    if (IsRoll(input, x, y)) {
                        var count = CountAdjacent(input, x, y);
                        if (count < 4) {
                            rollCount++;
                            lineSb[x] = 'x';                            
                        }
                    }
                }
                input[y] = lineSb.ToString();
                y++;
            }
            if (rollCount == prevRollCount) break;
            prevRollCount = rollCount;
            y = 0;
        }
        Console.WriteLine($"Part Two: {rollCount}");
    }

    private static int CountAdjacent(string[] input, int x, int y)
    {
        var count = 0;
        
        //Above
        if (y > 0 && IsRoll(input, x, y - 1))        
            count++;
        
        //AboveRight
        if (y > 0 && x + 1 < input[y].Length && IsRoll(input, x + 1, y - 1))
            count++;
        
        //Right
        if (x + 1 < input[y].Length && IsRoll(input, x + 1, y))
            count ++;
        
        //BelowRight
        if (x + 1 < input[y].Length && y + 1 < input.Length && IsRoll(input, x + 1, y + 1))
            count++;

        //Below
        if (y + 1 < input.Length && IsRoll(input, x, y + 1))
            count++;

        //BelowLeft
        if (x > 0 && y + 1 < input.Length && IsRoll(input, x - 1 , y + 1))
            count++;

        //Left
        if (x > 0 && IsRoll(input, x - 1, y))
            count++;

        //AboveLeft
        if (x > 0 && y > 0 && IsRoll(input, x - 1, y - 1))
            count++;

        return count;
    }

    private static bool IsRoll(string[] input, int x, int y)
    {
        return input[y][x] == '@';
    }
}
