namespace keeperFinal.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepRepo;
  private readonly VaultsRepository _vaultsRepo;

  public VaultKeepsService(VaultKeepsRepository vaultKeepRepo, VaultsRepository vaultsRepo)
  {
    _vaultKeepRepo = vaultKeepRepo;
    _vaultsRepo = vaultsRepo;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
     
    VaultKeep vaultKeep = _vaultKeepRepo.CreateVaultKeep(vaultKeepData);
      if (vaultKeep.CreatorId == null)
          {
            throw new Exception("bad");
          }

          if (vaultKeep == null)
          {
            throw new Exception("no");
          }
    
    return vaultKeep;

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