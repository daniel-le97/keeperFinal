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

  internal List<Keep> GetAllKeepsByProfileId(string profileId)
  {
      var sql = @"SELECT 
                keep.*,
                COUNT(vaultKeep.id) AS Kept,
                account.*
                FROM keeps keep
                JOIN accounts account ON account.id = keep.creatorId
                LEFT JOIN vaultKeeps vaultKeep on  vaultKeep.keepId = keep.id
                WHERE keep.creatorId = @profileId
                GROUP BY keep.id
                  ;";
          return _db.Query<Keep, Profile,Keep >(sql, (keep, profile) =>{
            keep.Creator = profile;
            return keep;
          }, new {profileId}).ToList();
    
  }

  internal List<Vault> GetAllVaultsByProfileId(string profileId)
  {
      var sql = @"SELECT 
                vault.*,
                account.*
                FROM vaults vault
                JOIN accounts account ON account.id = vault.creatorId
                where vault.creatorId = @profileId
                AND vault.isPrivate <> 1
                GROUP BY vault.id
             
                     ; ";
          return _db.Query<Vault, Profile, Vault>(sql,(vault, profile) =>{
              vault.Creator = profile;
              return vault;
          },new {profileId}).ToList();
    
  }
}