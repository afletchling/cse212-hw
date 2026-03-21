using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character
    /// words (lower case, no duplicates). Using sets, find an O(n)
    /// solution for returning all symmetric pairs of words.
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        HashSet<string> entries = words.ToHashSet();
        List<string> pairs = new();

        foreach (string word in entries)
        {
            string reverse = word[1].ToString() + word[0].ToString();

            if (entries.Contains(reverse) && word != reverse)
            {
                pairs.Add($"{word} & {reverse}");
                entries.Remove(reverse);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        Dictionary<string, int> degrees = new Dictionary<string, int>();

        foreach (string line in File.ReadLines(filename))
        {
            string[] fields = line.Split(",");
            if (degrees.ContainsKey(fields[3]))
            {
                degrees[fields[3]]++;
            }
            else
            {
                degrees[fields[3]] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a
    /// new word.  A dictionary is used to solve the problem.
    ///
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    ///
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For
    /// example, 'Ab' and 'Ba' should be considered anagrams
    ///
    /// Reminder: You can access a letter by index in a string by
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        Dictionary<string, int> word1Count = new();
        Dictionary<string, int> word2Count = new();

        for (int index = 0; index < word1.Length; index++)
        {
            string character = word1[index].ToString().ToLower();
            word1Count.TryGetValue(character, out int value);
            word1Count[character] = value + 1;
        }

        for (int index = 0; index < word2.Length; index++)
        {
            string character = word2[index].ToString().ToLower();
            word2Count.TryGetValue(character, out int value);
            word2Count[character] = value + 1;
        }

        for (int index = 0; index < word1.Length; index++)
        {
            string character = word1[index].ToString().ToLower();

            if (character == " ")
            {
                continue;
            }

            if (!word2Count.ContainsKey(character) || word1Count[character] != word2Count[character])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    ///
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found
    /// at this website:
    ///
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    ///
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using HttpClient client = new HttpClient();
        using HttpRequestMessage getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using Stream jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using StreamReader reader = new StreamReader(jsonStream);
        string json = reader.ReadToEnd();
        JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        FeatureCollection featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);
        string[] earthquakeSummary = new string[featureCollection.features.Length];

        for (int index = 0; index < earthquakeSummary.Length; index++)
        {
            Earthquake earthquake = featureCollection.features[index];
            earthquakeSummary[index] = $"{earthquake.properties.place} - Mag ${earthquake.properties.mag}";
        }

        return earthquakeSummary;
    }
}