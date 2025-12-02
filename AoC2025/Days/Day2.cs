using System.Text.RegularExpressions;

namespace AoC2025.Days;

public static class Day2
{
    public static async Task Run()
    {
        var input = await InputGetter.GetFromCsvAsString(2);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //19605500130
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //36862281418
    }

    private static async Task PartOne(string[] input)
    {
        var count = await Execute(input, false);
        Console.WriteLine($"Part 1: {count}");
    }

    private static async Task PartTwo(string[] input)
    {
        var count = await Execute(input, true);
        Console.WriteLine($"Part 2: {count}");
    }

    private static async Task<long> Execute(string[] input, bool useRegex) {
        long count = 0;
        foreach(var range in input)
        {
            long start = long.Parse(range.Split("-")[0]);
            long end = long.Parse(range.Split("-")[1]);
            
            for (long i = start; i <= end; i++)
            {

                if (ContainsDuplicate(i, useRegex))
                {
                    //Console.WriteLine($"Found Duplicate: {i}");
                    count += i;  
                } 
            }
        }
        return count;
    }

    private static bool ContainsDuplicate(long number, bool useRegex)
    {
        var numStr = number.ToString();
        if (useRegex)
        {
            var regex = new Regex(@"\b(\d+)\1+\b");
            return regex.IsMatch(numStr);
        }

        //All numbers are the same
        if (numStr.Length % 2 == 0 && numStr.ToCharArray().Distinct().Count() == 1)
        {
            return true;
        }
        //First half matches second half
        else if (numStr.Length % 2 == 0)
        {
            var left = numStr.Substring(0,numStr.Length / 2);
            var right = numStr.Substring(numStr.Length / 2);
            return left == right;
        }
        return false;
    }
}
