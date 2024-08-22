namespace CadastroWebApi.Models;

public class Product
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float Price { get; set; }
    public float Amount { get; set; }
    public DateTime Date { get; set; }

}
