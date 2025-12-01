namespace AoC2025.Days;

public static class Day1
{
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(1);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => Execute(input, false)); //1105
        await timedExecutor.ExecuteTimed(() => Execute(input, true)); //6599
    }

    private static async Task Execute(string[] input, bool countAtClick = false)
    {
        var location = 50;
        var zeroCount = 0;
        foreach (var line in input)
        {
            int move = int.Parse(line.Substring(1));
                        
            if (line[0].ToString() == "L")
            {
                while (move > 0)
                {
                    location--;
                    if (countAtClick && location == 0) zeroCount ++;
                    if (location == -1)
                    {                        
                        location = 99;
                    }
                    move--;
                }
            }
            else
            {
                while (move > 0)
                {
                    location++;
                    if (location == 100)
                    {
                        if (countAtClick) zeroCount ++;
                        location = 0;
                    }
                    move--;
                }
            }
            if (!countAtClick && location == 0) zeroCount ++;
        }
        Console.WriteLine($"ZeroCount : {zeroCount}");
    }
}
