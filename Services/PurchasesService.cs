using System.Collections.Generic;
using DaVinki.Models;
using DaVinki.Repositories;

namespace DaVinki.Services
{
  public class PurchasesService
  {
    private readonly PurchasesRepository _repo;

    public PurchasesService(PurchasesRepository repo)
    {
      _repo = repo;
    }

    internal List<ArtPurchaseViewModel> GetAccountArtPurchase(string id)
    {
      return _repo.GetAccountArtPurchase(id);
    }
  }
}