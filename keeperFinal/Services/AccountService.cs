namespace keeperFinal.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userId)
  {
    Account original = _repo.GetById(editData.Id);
      if (original.Id != userId)
          {
            throw new Exception("you are a very very bad user");
          }
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    original.CoverImg = editData.CoverImg.Length > 0 ? editData.CoverImg : original.CoverImg; 
    return _repo.Edit(original);
  }

 
}
