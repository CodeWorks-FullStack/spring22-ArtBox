using System.Data;
using System.Linq;
using Dapper;
using DaVinki.Models;

namespace DaVinki.Repositories
{
  public class CommentsRepository
  {
    private readonly IDbConnection _db;

    public CommentsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Comment Create(Comment commentData)
    {
      string sql = @"
        INSERT INTO comments
        (body, creatorId, artId)
        VALUES
        (@Body, @CreatorId, @ArtId);
        SELECT LAST_INSERT_ID();
        ";
      commentData.Id = _db.ExecuteScalar<int>(sql, commentData);
      return commentData;
    }

    internal Comment Get(int id)
    {
      string sql = @"
        SELECT
          a.*,
          c.*
        FROM comments c
        JOIN accounts a ON c.creatorId = a.id
        WHERE c.id = @id;
      ";
      return _db.Query<Account, Comment, Comment>(sql, (act, cmt) =>
      {
        cmt.Creator = act;
        return cmt;
      }, new { id }).FirstOrDefault();
    }
  }
}