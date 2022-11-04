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
}