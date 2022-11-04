namespace keeperFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly ProfilesService _profilesService;

  public ProfilesController(Auth0Provider auth0provider, ProfilesService profilesService)
  {
    _auth0provider = auth0provider;
    _profilesService = profilesService;
  }

  [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(int profileId)
    {
      try
      {
        Profile profile = _profilesService.GetProfileById(profileId);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  
}
