namespace keeperFinal.Repositories;

public class ProfilesRepository : BaseRepository
{
  public ProfilesRepository(IDbConnection db) : base(db)
  {
  }

  internal Profile GetProfileById(int profileId)
  {
      string sql = @"SELECT a.* FROM accounts a
                    WHERE id = @profileId
                     ;";
        return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
  }
}