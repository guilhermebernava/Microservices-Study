namespace ItensService.Models;

public class ItemUpdateModel
{
    public ItemUpdateModel(ItemModel itemModel, int id)
    {
        ItemModel = itemModel;
        Id = id;
    }

    public ItemModel ItemModel { get; set; }
    public int Id { get; set; }
}
