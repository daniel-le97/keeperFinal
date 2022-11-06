namespace keeperFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;
  private readonly VaultKeepsService _vaultKeepsService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService, VaultKeepsService vaultKeepsService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
    _vaultKeepsService = vaultKeepsService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  
    [HttpGet("vaults")]
    public async Task<ActionResult<List<Vault>>> GetAllVaults()
    {
      try
      {
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        
        List<Vault> vault = _vaultsService.GetAllVaults(userInfo.Id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    
      [HttpGet("vaultKeeps")]
      public async Task<ActionResult<List<VaultKeep>>> GetAllVaultKeep()
      {
        try
        {
          Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
          
          List<VaultKeep> vaultKeeps = _vaultKeepsService.GetAllVaultKeep(userInfo.Id);
          return Ok(vaultKeeps);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
      }
    
  
}
