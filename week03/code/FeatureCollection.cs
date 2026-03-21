public class FeatureCollection
{
    public Earthquake[] features { get; set; }
}

public class Earthquake
{
    public EarthquakeProperties properties { get; set; }
}

public class EarthquakeProperties
{
    public double ?mag { get; set; }
    public string place { get; set; }
}