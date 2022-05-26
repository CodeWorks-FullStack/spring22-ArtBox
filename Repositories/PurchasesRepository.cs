using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DaVinki.Models;

namespace DaVinki.Repositories
{
  public class PurchasesRepository
  {
    private readonly IDbConnection _db;

    public PurchasesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<ArtPurchaseViewModel> GetAccountArtPurchase(string id)
    {
      string sql = @"
            SELECT
                act.*,
                a.*,
                p.price,
                p.delivered,
                p.id AS purchaseId
            FROM purchases p
            JOIN art a ON p.artId = a.id
            JOIN accounts act ON a.creatorId = act.id
            WHERE p.accountId = @id
        ";
      return _db.Query<Account, ArtPurchaseViewModel, ArtPurchaseViewModel>(sql, (act, ap) =>
      {
        ap.Creator = act;
        return ap;
      }, new { id }).ToList();




    }
  }
}