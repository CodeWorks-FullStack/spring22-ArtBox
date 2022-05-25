using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DaVinki.Models;

namespace DaVinki.Repositories
{
  public class ArtRepository
  {
    private readonly IDbConnection _db;

    public ArtRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Art> Get()
    {
      string sql = @"
      SELECT 
        a.*,
        act.* 
      FROM art a
      JOIN accounts act ON a.creatorId = act.id;
      ";
      return _db.Query<Art, Account, Art>(sql, (art, account) =>
      {
        art.Creator = account;
        return art;
      }).ToList();

    }

    internal Art Get(int id)
    {
      string sql = @"
      SELECT 
        a.*,
        act.* 
      FROM art a
      JOIN accounts act ON a.creatorId = act.Id
      WHERE a.id = @id";
      return _db.Query<Art, Account, Art>(sql, (art, account) =>
    {
      art.Creator = account;
      return art;
    }, new { id }).FirstOrDefault();
    }

    internal Art Create(Art artData)
    {
      string sql = @"
       INSERT INTO art
       (title, imgUrl, creatorId)
       VALUES
       (@Title, @ImgUrl, @CreatorId);
       SELECT LAST_INSERT_ID();
      ";
      artData.Id = _db.ExecuteScalar<int>(sql, artData);
      return artData;
    }

    internal void Edit(Art original)
    {
      string sql = @"
      UPDATE art
      SET
        title = @Title,
        imgUrl = @ImgUrl
      WHERE id = @Id;
      ";
      _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM art WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }


  }
}