namespace keeperFinal.Services;

public class VaultsService{
  private readonly VaultsRepository _vaultsRepo;

  public VaultsService(VaultsRepository vaultsRepo)
  {
    _vaultsRepo = vaultsRepo;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    return _vaultsRepo.CreateVault(vaultData);
  }

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _vaultsRepo.GetVaultById(vaultId);
      if (vault == null)
          {
            throw new Exception("unable to find vault");
          }
      if (vault.Id == 0)
      {
        throw new Exception("bad id");
      }
      return vault;
    
  }

  internal Vault EditVault(Vault vaultData, int vaultId, string userId)
  {
    Vault original = GetVaultById(vaultId);
      if (original.CreatorId != userId)
          {
            throw new Exception("not your vault to edit!");
          }

      original.CoverImg = vaultData.CoverImg?? original.CoverImg;
      original.Name = vaultData.Name?? original.Name;
      original.Description = vaultData.Description?? original.Description;
      original.IsPrivate = vaultData.IsPrivate?? original.IsPrivate;
    
    return _vaultsRepo.EditVault(original);
  }

  internal List<Vault> GetAllVaults(string userId)
  {
   return _vaultsRepo.GetVaultsByAccount(userId);
  }

  internal void DeleteVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId);
      if (vault.CreatorId != userId)
          {
            throw new Exception("not your vault to delete");
          }
          
          _vaultsRepo.DeleteVault(vaultId);
    
  }
}