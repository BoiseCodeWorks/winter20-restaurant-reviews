using System.ComponentModel.DataAnnotations;

namespace restaurant_server.Models
{
  public class Review
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string OwnerId { get; set; }
    public bool Published { get; set; }
    public int RestaurantId { get; set; }
    [Range(0, 5)]
    public int Rating { get; set; }
    public Profile Owner { get; set; }
  }
}