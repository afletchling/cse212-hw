public class Person
{
    public readonly string Name;
    public int Turns { get; set; }
    public bool Done { get; set; }

    internal Person(string name, int turns)
    {
        Name = name;
        Turns = turns;
        Done = false;
    }

    public override string ToString()
    {
        return Turns <= 0 ? $"({Name}:Forever)" : $"({Name}:{Turns})";
    }
}