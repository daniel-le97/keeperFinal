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

      if (userInfo == null)
      {
        throw new Exception("please login to make vaultKeeps");
      }

      vaultKeepData.CreatorId = userInfo.Id;
      VaultKeep vaultKeep = _vaultKeepService.CreateVaultKeep(vaultKeepData);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpDelete("{vaultKeepId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _vaultKeepService.DeleteVaultKeep(vaultKeepId, userInfo.Id);
      return Ok("VaultKeep deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }




}