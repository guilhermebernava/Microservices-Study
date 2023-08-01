namespace ItensService.Models;

public class ItemModel
{
    public ItemModel(string name, double value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }
    public double Value { get; set; }
}
