namespace AoC2025.Days;

public static class Day5
{
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(5);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //868
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //354143734113772
    }

    private static async Task PartOne(string[] input) 
    {
        var ranges = new List<(long, long)>();
        var ids = new List<long>();
        var separator = input.ToList().IndexOf("");
        var freshCount = 0;
        foreach (var line in input.Take(separator))
        {
            var splitRange = line.Split("-");
            long rangeStart = long.Parse(splitRange[0]);
            long rangeEnd = long.Parse(splitRange[1]);
            ranges.Add((rangeStart, rangeEnd));
        }
        foreach (var line in input.Skip(separator + 1)) {
            var id = long.Parse(line);
            ids.Add(id);
        }

        foreach (var id in ids)
        {
            foreach (var range in ranges) {
                if (id >= range.Item1 && id <= range.Item2) {            
                    freshCount++;
                    break;
                }
            }
        }
        Console.WriteLine($"Part One: {freshCount}");
    }

    private static async Task PartTwo(string[] input)
    {
        var ranges = new List<(long, long)>();
        var separator = input.ToList().IndexOf("");        
        foreach (var line in input.Take(separator))
        {
            var splitRange = line.Split("-");

            long rangeStart = long.Parse(splitRange[0]);
            long rangeEnd = long.Parse(splitRange[1]);
            ranges.Add((rangeStart, rangeEnd));
        }
        
        //Have to order so can compare the ranges as we go along
        ranges = ranges.OrderBy(r => r.Item1).ThenBy(r => r.Item2).ToList();

        //Start with the first one.
        long totalIngredientCount = ranges[0].Item2 - ranges[0].Item1 + 1;        
        long prevEnd = ranges[0].Item2;

        foreach (var range in ranges)
        {
            if (range.Item2 <= prevEnd) continue; //Falls in the previous range so ignore
            if (range.Item1 > prevEnd) //Not in the range so add
            {
                totalIngredientCount += range.Item2 - range.Item1 + 1;
            }
            else //Add what's not in the prev range
            {
                totalIngredientCount += range.Item2 - prevEnd;
            }
            prevEnd = range.Item2;
        }

        Console.WriteLine($"Part Two: {totalIngredientCount}");
    }
}
