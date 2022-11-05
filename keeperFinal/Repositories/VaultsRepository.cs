namespace keeperFinal.Repositories;
public class VaultsRepository : BaseRepository
{
  public VaultsRepository(IDbConnection db) : base(db)
  {
  }

  internal Vault CreateVault(Vault vaultData)
  {
    var sql = @"INSERT INTO vaults
                (name, description, coverImg, creatorId, isPrivate)
              VALUES (@Name, @Description, @CoverImg, @CreatorId, @IsPrivate);
              SELECT LAST_INSERT_ID()
                  ; ";

    int vaultId = _db.ExecuteScalar<int>(sql, vaultData);
    return GetVaultById(vaultId);
  }

  internal Vault GetVaultById(int vaultId)
  {
    var sql = @"SELECT vault.*,
          account.* 
          FROM vaults vault
          JOIN accounts account ON account.id = vault.creatorId
          WHERE vault.id =  @vaultId
          GROUP BY vault.id
          ;";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { vaultId }).FirstOrDefault();
  }

  internal Vault EditVault(Vault original)
  {
    string sql = @"UPDATE vaults SET
                  name = @Name,
                  description = @Description,
                  coverImg = @CoverImg,
                  isPrivate = @IsPrivate
                  WHERE id = @Id
                       ;";
    var rows = _db.Execute(sql, original);
    if (rows != 1)
    {
      throw new Exception("Unable to update");
    }

    return GetVaultById(original.Id);
  }

  internal List<Vault> GetVaultsByAccount(string userId)
  {
    var sql = @"SELECT 
                vault.*,
                account.*
                FROM vaults vault
                JOIN accounts account ON account.id = vault.creatorId
                where vault.creatorId = @userId
                GROUP BY vault.id ;";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { userId }).ToList();

  }

  internal void DeleteVault(int vaultId)
  {
    var sql = @"DELETE FROM vaults WHERE id = @vaultId;";

    var rows = _db.Execute(sql, new { vaultId });
    if (rows != 1) { throw new Exception("Data is bad or Id is Bad"); }
    return;

  }
}