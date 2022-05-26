using System;
using System.Collections.Generic;
using DaVinki.Models;
using DaVinki.Repositories;

namespace DaVinki.Services
{
  public class CommentsService
  {
    private readonly CommentsRepository _repo;

    public CommentsService(CommentsRepository repo)
    {
      _repo = repo;
    }

    internal Comment Create(Comment commentData)
    {
      return _repo.Create(commentData);
    }

    internal Comment Get(int id)
    {
      Comment comment = _repo.Get(id);
      if (comment == null)
      {
        throw new Exception("Invaild Comment Id");
      }
      return comment;
    }

    internal void Delete(int id, string userId)
    {
      Comment comment = Get(id);
      if (comment.CreatorId != userId)
      {
        throw new Exception("Cannot delete other comment");
      }
      _repo.Delete(id);
    }

    internal List<Comment> GetCommentsForArt(int id)
    {
      return _repo.GetCommentsForArt(id);
    }
  }
}