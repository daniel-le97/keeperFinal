namespace keeperFinal.Services;

public class ProfilesService{
  private readonly ProfilesRepository _ProfilesRepository;

  public ProfilesService(ProfilesRepository profilesRepository)
  {
    _ProfilesRepository = profilesRepository;
  }

  internal Profile GetProfileById(int profileId)
  {
   
  Profile profile =  _ProfilesRepository.GetProfileById(profileId);
    if (profile == null)
        {
          throw new Exception("bad id");
        }

          if (profile.Id == "0")
              {
                throw new Exception("bad");
              }
        
        

        
        
  
  return profile;
  
  }
}