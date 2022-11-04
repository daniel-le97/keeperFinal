namespace keeperFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly VaultKeepsService _vaultKeepService;

  public VaultKeepsController(Auth0Provider auth0provider, VaultKeepsService vaultKeepService)
  {
    _auth0provider = auth0provider;
    _vaultKeepService = vaultKeepService;
  }
  
   [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        var userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
        VaultKeep vaultKeep = _vaultKeepService.CreateVaultKeep(vaultKeepData);
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  
  
  
}