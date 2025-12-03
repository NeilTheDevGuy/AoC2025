namespace AoC2025.Days;

public static class Day3
{
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(3);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //16973
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //168027167146027
    }

    private static async Task PartOne(string[] input)
    {
        var joltage =0 ;
        foreach (var line in input)
        {
            var firstNumber = 0;
            var firstNumberIdx = 0;
            var secondNumber = 0;
            //Start at 0 and goto end - 1 to find the highest initial number
            for (int i = 0; i < line.Length - 1; i++)
            {
                var thisNum = int.Parse(line[i].ToString());
                if (thisNum > firstNumber) {
                    firstNumber = thisNum;
                    firstNumberIdx = i;
                }
            }
            //Then again, starting after the location of the first number and going to the actual end.
            for (int i = firstNumberIdx + 1; i < line.Length; i++)
            {
                var thisNum = int.Parse(line[i].ToString());
                if (thisNum > secondNumber) secondNumber = thisNum;
            }
            var highest = int.Parse(firstNumber.ToString() + secondNumber.ToString());
            joltage += highest;
        }
        Console.WriteLine($"PartOne: {joltage}");
    }

    private static async Task PartTwo(string[] input)
    {
        long totalJoltage = 0;        
        foreach (var line in input)
        {
            var idx = 0;
            string joltage = "";
            var neededAtEnd = 12;

            for (int i = 0; i < 12; i++) {
                var (highestNumber, lowestIdx) = GetHighestNumberAndIndex(line, idx, neededAtEnd);
                joltage += highestNumber.ToString();
                idx = lowestIdx + 1;
                neededAtEnd--;
            }
            totalJoltage += long.Parse(joltage);
        }
        Console.WriteLine($"PartTwo: {totalJoltage}");
    }

    private static (int highestNumber, int lowestIdx) GetHighestNumberAndIndex(string line, int startingIdx, int neededAtEnd)
    {
        var amountToTrim = line.Length - neededAtEnd;
        var lowestIdx = line.Length;
        var highestNumber = 0;
        for (int i = 9; i >= 0; i--)
        {
            var idx = line.IndexOf(i.ToString(), startingIdx);
            if (idx > -1 && idx <= amountToTrim)
            {                
                lowestIdx = idx;
                highestNumber = i;
                break;
            }
        }
        return (highestNumber, lowestIdx);
    }
}
