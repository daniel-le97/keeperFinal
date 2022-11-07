namespace keeperFinal.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepRepo;
  private readonly VaultsRepository _vaultsRepo;
  private readonly VaultsService _vs;

  public VaultKeepsService(VaultKeepsRepository vaultKeepRepo, VaultsRepository vaultsRepo, VaultsService vs)
  {
    _vaultKeepRepo = vaultKeepRepo;
    _vaultsRepo = vaultsRepo;
    _vs = vs;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    Vault vault = _vs.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
    if (vaultKeepData.CreatorId != vault.CreatorId)
    {
      throw new Exception("save to your own vaults");
    }
    VaultKeep vaultKeep = _vaultKeepRepo.CreateVaultKeep(vaultKeepData);
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

  internal List<VaultKeep> GetAllVaultKeep(string userId)
  {
    List<VaultKeep> vaultKeep = _vaultKeepRepo.getAllVaultKeep(userId);
    return vaultKeep;
  }
}