namespace keeperFinal.Services;


public class KeepsService{
  private readonly KeepsRepository _keepsRepo;

  public KeepsService(KeepsRepository keepsRepo)
  {
    _keepsRepo = keepsRepo;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    return _keepsRepo.createKeep(keepData);
  }

  internal List<Keep> GetAllKeeps()
  {
    return _keepsRepo.GetAllKeeps();
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _keepsRepo.GetKeepById(keepId);
      if (keep == null)
          {
            throw new Exception("keep does not exist");
          }

        return keep;
        
    
    
  }

  internal Keep EditKeep(Keep keepData, int keepId, string userId)
  {
    Keep original = GetKeepById(keepId);
      if (original.CreatorId != userId)
          {
            throw new Exception("not your keep to edit");
          }

    original.Name = keepData.Name?? original.Name;
    original.Img = keepData.Img?? original.Img;
    original.Description = keepData.Description?? original.Description;
    original.views = keepData.views?? original.views;

    Keep keep = _keepsRepo.EditKeep(original);
    return keep;
    
    
  }

  internal void DeleteKeep(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("not your keep to delete");
    }
    _keepsRepo.DeleteKeep(keepId);
  }
}