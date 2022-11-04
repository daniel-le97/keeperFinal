namespace keeperFinal.Services;

public class VaultKeepsService{
  private readonly VaultKeepsRepository _vaultKeepRepo;

  public VaultKeepsService(VaultKeepsRepository vaultKeepRepo)
  {
    _vaultKeepRepo = vaultKeepRepo;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {

    return _vaultKeepRepo.CreateVaultKeep(vaultKeepData);
  }

  internal List<KeptKeep> GetAllKeptKeep(int vaultId)
  {
    return _vaultKeepRepo.GetAllKeepsByVault(vaultId);
  }

  internal void DeleteVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
      if (vaultKeep.CreatorId != userId)
          {
            throw new Exception("not your vaultKeep to delete!");
          }

          _vaultKeepRepo.DeleteVaultKeep(vaultKeepId);
    
    
  }
  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _vaultKeepRepo.GetVaultKeepById(vaultKeepId);
      if (vaultKeep == null)
          {
            throw new Exception("no vaultKeep found");
          }
  
    
    return vaultKeep;
    
  }
}