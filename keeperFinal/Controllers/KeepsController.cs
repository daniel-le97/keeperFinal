namespace keeperFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  
  private readonly KeepsService _keepsService;

  public KeepsController(Auth0Provider auth0provider, KeepsService keepsService)
  {
    _auth0provider = auth0provider;
    _keepsService = keepsService;
  }


   [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
      try
      {
        var userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
        keepData.CreatorId = userInfo.Id;
        Keep keep = _keepsService.CreateKeep(keepData);
        keep.Creator = userInfo;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

     [HttpPut("{keepId}")]
        [Authorize]
        public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData, int keepId)
        {
          try
          {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.EditKeep(keepData,keepId, userInfo?.Id);
            return Ok(keep);
          }
          catch (Exception e)
          {
            return BadRequest(e.Message);
          }
        }


        
          [HttpDelete("{keepId}")]
          [Authorize]
          public async Task<ActionResult<string>> DeleteKeep(int keepId)
          {
            try
            {
              Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
              _keepsService.DeleteKeep(keepId, userInfo.Id);
              return Ok("Keep deleted");
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
          }
        
    

    
      [HttpGet]
      public ActionResult<List<Keep>> GetAllKeeps([FromQuery] int offset)
      {
        try
        {
          List<Keep> example = _keepsService.GetAllKeeps(offset);
          return Ok(example);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
      }

        [HttpGet("{keepId}")]
      
        public async Task<ActionResult<Keep>> GetKeepById(int keepId)
        {
          try
          {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.GetKeepById(keepId, userInfo?.Id);
            return Ok(keep);
          }
          catch (Exception e)
          {
            return BadRequest(e.Message);
          }
        }
      
    
  
  
}
