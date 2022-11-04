namespace keeperFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  
  private readonly VaultsService _vaultsService;

  public VaultsController(Auth0Provider auth0provider, VaultsService vaultsService)
  {
    _auth0provider = auth0provider;
    _vaultsService = vaultsService;
  }

   [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
      try
      {
        var userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
        vaultData.CreatorId = userInfo.Id;
        Vault vault = _vaultsService.CreateVault(vaultData);
        vault.Creator = userInfo;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

     [HttpPut("{vaultId}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
        {
          try
          {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.EditVault(vaultData,vaultId, userInfo.Id);
            return Ok(vault);
          }
          catch (Exception e)
          {
            return BadRequest(e.Message);
          }
        }
    
      [HttpDelete("{vaultId}")]
      [Authorize]
      public async Task<ActionResult<string>> DeleteVault(int vaultId)
      {
        try
        {
          Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
          _vaultsService.DeleteVault(vaultId, userInfo.Id);
          return Ok("Vault deleted");
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
      }
    

      [HttpGet("{vaultId}")]
    
      public ActionResult<Vault> GetVaultById(int vaultId)
      {
        try
        {
          Vault vault = _vaultsService.GetVaultById(vaultId);
          return Ok(vault);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
      }
    
  
  

  
}