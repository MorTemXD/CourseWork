public abstract class Entity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal PricePerYear { get; set; }
}

public class Game : Entity
{
    public string Developer { get; set; }
    public string Platform { get; set; }
    public LicenseType LicenseType { get; set; }
}

public class GraphicsPackage : Entity
{
    public string Manufacturer { get; set; }
    public GraphicsType GraphicsType { get; set; }
}

public enum LicenseType
{
    Дворічна,
    Трирічна
}

public enum GraphicsType
{
    Векторна,
    Растрова,
    Обидва
}