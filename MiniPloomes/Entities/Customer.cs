namespace MiniPloomes.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}
