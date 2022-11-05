namespace keeperFinal.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultsRepo;
  private readonly VaultKeepsRepository _vaultKeepRepo;

  public VaultsService(VaultsRepository vaultsRepo, VaultKeepsRepository vaultKeepRepo)
  {
    _vaultsRepo = vaultsRepo;
    _vaultKeepRepo = vaultKeepRepo;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    return _vaultsRepo.CreateVault(vaultData);
  }

  internal Vault GetVaultById(int vaultId, string userId)
  {
    Vault vault = _vaultsRepo.GetVaultById(vaultId);
   
    if (vault == null)
    {
      throw new Exception("unable to find vault");
    }
    var notCreator = vault.CreatorId != userId;
    if (vault.IsPrivate == true)
    {
      if (notCreator)
      {
        throw new Exception("sorry this is private and you are not the owner");
      }
    }
    return vault;

  }
  internal Vault EditVault(Vault vaultData, int vaultId, string userId)
  {
    Vault original = GetVaultById(vaultId, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("not your vault to edit!");
    }

    original.CoverImg = vaultData.CoverImg ?? original.CoverImg;
    original.Name = vaultData.Name ?? original.Name;
    original.Description = vaultData.Description ?? original.Description;
    original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;

    return _vaultsRepo.EditVault(original);
  }

  internal List<Vault> GetAllVaults(string userId)
  {


    return _vaultsRepo.GetVaultsByAccount(userId);
  }

  internal void DeleteVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId,userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("not your vault to delete");
    }

    _vaultsRepo.DeleteVault(vaultId);

  }

  internal List<KeptKeep> GetAllKeptKeep(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId, userId);
    List<KeptKeep> keeps = _vaultKeepRepo.GetAllKeepsByVault(vaultId);
    return keeps;
  }
}