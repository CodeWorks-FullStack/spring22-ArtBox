using System;
using System.ComponentModel.DataAnnotations;

namespace DaVinki.Models
{
  public class Comment
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    [MinLength(20)]
    public string Body { get; set; }
    public int ArtId { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
}