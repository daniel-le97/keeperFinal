namespace keeperFinal.Services;

public class ProfilesService{
  private readonly ProfilesRepository _ProfilesRepository;

  public ProfilesService(ProfilesRepository profilesRepository)
  {
    _ProfilesRepository = profilesRepository;
  }

  internal Profile GetProfileById(string profileId)
  {
   
  Profile profile =  _ProfilesRepository.GetProfileById(profileId);
  
  return profile;
  
  }

  internal List<Keep> GetAllKeepsByProfile(string profileId)
  {
   return _ProfilesRepository.GetAllKeepsByProfileId(profileId);
  }

  internal List<Vault> GetAllVaultsByProfile(string profileId)
  {
    return _ProfilesRepository.GetAllVaultsByProfileId(profileId);
  }
}