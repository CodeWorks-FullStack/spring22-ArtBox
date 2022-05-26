namespace DaVinki.Models
{
  public class Purchase
  {
    public int Id { get; set; }
    public string AccountId { get; set; }
    public int ArtId { get; set; }
    public double Price { get; set; }
    public bool Delivered { get; set; }
  }
}