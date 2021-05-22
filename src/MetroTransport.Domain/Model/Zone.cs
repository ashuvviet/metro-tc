namespace MetroTransport.Domain
{
  public class Zone
  {
    public Zone(int id, string name) : this(id)
    {
      Name = name;
    }

    public Zone(int id)
    {
      Id = id;
    }

    public int Id { get;  }

    public string Name { get; }
  }
}