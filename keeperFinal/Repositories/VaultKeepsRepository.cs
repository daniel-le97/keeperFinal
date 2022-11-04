namespace keeperFinal.Repositories;

public class VaultKeepsRepository : BaseRepository
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }

  

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    throw new NotImplementedException();
  }
}