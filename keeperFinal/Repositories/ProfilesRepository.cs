namespace keeperFinal.Repositories;

public class ProfilesRepository : BaseRepository
{
  public ProfilesRepository(IDbConnection db) : base(db)
  {
  }
  internal Profile GetProfileById(string id)
  {
      string sql = @"SELECT * FROM accounts
                WHERE id = @id ;";
        return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
  }
}