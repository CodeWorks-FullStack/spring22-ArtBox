using System;
using System.ComponentModel.DataAnnotations;

namespace DaVinki.Models
{
  public class Art
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [Required]
    [MinLength(3)]
    public string Title { get; set; }

    [Url]
    public string ImgUrl { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }

  }
}