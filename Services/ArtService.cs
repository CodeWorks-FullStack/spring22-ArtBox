using System;
using System.Collections.Generic;
using DaVinki.Models;
using DaVinki.Repositories;

namespace DaVinki.Services
{
  public class ArtService
  {
    private readonly ArtRepository _repo;

    public ArtService(ArtRepository repo)
    {
      _repo = repo;
    }

    internal List<Art> Get()
    {
      return _repo.Get();
    }

    /// <summary>
    ///Gets... by the ID!!!
    ///</summary>
    internal Art Get(int id)
    {
      Art art = _repo.Get(id);
      if (art == null)
      {
        throw new Exception("Invalid Art id");
      }
      return art;
    }

    internal Art Create(Art artData)
    {
      return _repo.Create(artData);
    }

    internal Art Edit(Art artData)
    {
      Art original = Get(artData.Id);
      if (original.CreatorId != artData.CreatorId)
      {
        throw new Exception("You do not own this art");
      }
      original.Title = artData.Title ?? original.Title;
      original.ImgUrl = artData.ImgUrl ?? original.ImgUrl;

      _repo.Edit(original);

      return Get(original.Id);
    }

    internal void Delete(int id, string userId)
    {
      Art art = Get(id);
      if (art.CreatorId != userId)
      {
        throw new Exception("You do not own this art");
      }
      _repo.Delete(id);
    }
  }
}