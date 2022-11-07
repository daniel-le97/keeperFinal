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

  public Vault GetVaultById(int vaultId, string userId)
  {
    Vault vault = _vaultsRepo.GetVaultById(vaultId);
    var notCreator = vault.CreatorId != userId;
    var IsPrivate = vault.IsPrivate == true;
   
    if (vault == null)
    {
      throw new Exception("unable to find vault");
    }
    // if (vault.IsPrivate == true)
    // {
    // }
      if (notCreator && IsPrivate)
      {
        throw new Exception("sorry this is private and you are not the owner");
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