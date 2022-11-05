namespace keeperFinal.Repositories;

public class ProfilesRepository : BaseRepository
{
  public ProfilesRepository(IDbConnection db) : base(db)
  {
  }
  internal Profile GetProfileById(string id)
  {
      string sql = @"SELECT * FROM  accounts
                WHERE id = @id ;";
        return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
  }

  // internal Profile GetProfileById(int profileId)
  // {
  //     string sql = @"SELECT a.* FROM accounts a
  //                   WHERE a.id = @profileId
  //                    ;";
  //       return _db.Query<Profile, Account, Profile>(sql,(profile, account) =>{
  //         profile.CoverImg = account.CoverImg;
  //         profile.Id = account.Id;
  //         profile.Name = account.Name;
  //         profile.Picture = account.Picture;
  //         profile.UpdatedAt = account.UpdatedAt;
  //         profile.CreatedAt = account.CreatedAt;
  //         return profile;
  //       } ,new { profileId }).FirstOrDefault();
  // }
}