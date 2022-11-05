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
    return _vaultKeepRepo.CreateVaultKeep(vaultKeepData);
  }

  internal List<KeptKeep> GetAllKeptKeep(int vaultId)
  {
    Vault vault = _vaultsRepo.GetVaultById(vaultId);
    if (vault.IsPrivate == true)
    {
      throw new Exception("private vault");
    }
    List<KeptKeep> keeps = _vaultKeepRepo.GetAllKeepsByVault(vaultId);
    return keeps;
  }

  internal List<KeptKeep> GetAllKeptKeep(int vaultId, string userId)
  {
    Vault vault = _vaultsRepo.GetVaultById(vaultId);
    var notCreator = vault.CreatorId != userId;
    // var isPrivate = vault.IsPrivate
    if (vault.IsPrivate == true)
    {
      if (notCreator)
      {
        throw new Exception("sorry this is private and you are not the owner");
      }
    }
    List<KeptKeep> keeps = _vaultKeepRepo.GetAllKeepsByVault(vaultId);

    return keeps;
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