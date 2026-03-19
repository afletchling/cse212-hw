/*
 * CSE 212 Lesson 6C
 *
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 *
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 *
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        Dictionary<string, int> players = new Dictionary<string, int>();

        using TextFieldParser reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData) {
            string[] fields = reader.ReadFields()!;
            string playerId = fields[0];
            int points = int.Parse(fields[8]);

            if (players.ContainsKey(playerId))
            {
                players[playerId] += points;
            }
            else
            {
                players[playerId] = points;
            }
        }

        Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");

        KeyValuePair<string, int>[] playerArray = players.ToArray();
        playerArray.Sort((origin, compare) =>
        {
            return compare.Value - origin.Value;
        });

        for (int index = 0; index < Math.Min(playerArray.Length, 10); index++)
        {
            Console.WriteLine(playerArray[index]);
        }
    }
}