namespace keeperFinal.Repositories;

public class VaultKeepsRepository : BaseRepository
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }

  

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
      var sql = @"INSERT INTO vaultKeeps (vaultId, keepId, creatorId)
              VALUES (@VaultId, @KeepId, @CreatorId);
              SELECT LAST_INSERT_ID() ;";
       int vaultKeepId = _db.ExecuteScalar<int>(sql, vaultKeepData);
        return GetVaultKeepById(vaultKeepId);
  }

  internal List<KeptKeep> GetAllKeepsByVault(int vaultId)
  {
      var sql = @"SELECT 
                vaultKeep.*,
                vaultKeep.id AS VaultKeepId,
               keep.*,
                account.*
                FROM vaultKeeps vaultKeep
                JOIN accounts account ON account.id = vaultKeep.creatorId
                JOIN keeps keep ON keep.id = vaultKeep.keepId
                where vaultKeep.vaultId = @vaultId
                GROUP BY vaultKeep.id
             
                     ; ";
          return _db.Query<KeptKeep, Profile,KeptKeep>(sql, (keep, profile) =>
           {
             keep.Creator = profile;
             
             return keep;
           }, new {vaultId}).ToList();
    
  }

  internal void DeleteVaultKeep(int id)
  {
      var sql = @"DELETE FROM vaultKeeps WHERE id = @id ;";
    
       var rows = _db.Execute(sql, new {id});
    if (rows != 1){throw new Exception("Data is bad or Id is Bad");}
    return;
    
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
      string sql = @"SELECT 
                *
                FROM  vaultKeeps
                WHERE id = @vaultKeepId
                     ;";
        return _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
  }
}