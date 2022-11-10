namespace keeperFinal.Repositories;

public class KeepsRepository : BaseRepository
{
  public KeepsRepository(IDbConnection db) : base(db)
  {
  }

  internal Keep createKeep(Keep keepData)
  {
      var sql = @"INSERT INTO keeps
              (name, description, img, views, creatorId)
              VALUES (@Name, @Description, @Img, @views, @CreatorId);
              SELECT LAST_INSERT_ID()
                  ;";
    
       int keepId = _db.ExecuteScalar<int>(sql, keepData);
        return GetKeepById(keepId);
  }

  internal List<Keep> GetAllKeeps()
  {
      var sql = @"SELECT 
                keep.*,
                COUNT(vaultKeep.id) AS Kept,
                account.*
                FROM keeps keep
                JOIN accounts account ON account.id = keep.creatorId
                LEFT JOIN vaultKeeps vaultKeep ON vaultKeep.keepId = keep.id
                GROUP BY keep.id
         
      
             
                     ;";
          return _db.Query<Keep, Profile,Keep >(sql, (keep, profile) =>
           {
             keep.Creator = profile;
             
             return keep;
           }).ToList();
    
  }

  internal Keep GetKeepById(int keepId)
  {
    var sql = @"SELECT 
            keep.*,
            COUNT(vaultKeep.id) AS Kept,
            account.* 
          FROM keeps keep
          JOIN accounts account ON account.id = keep.creatorId
          LEFT JOIN vaultKeeps vaultKeep ON vaultKeep.keepId = keep.id
          WHERE keep.id =  @keepId 
          GROUP BY keep.id
          ;";
        return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
          keep.Creator = profile;
          return keep;
        }, new { keepId }).FirstOrDefault();
  }

  internal void DeleteKeep(int id)
  {
      var sql = @"DELETE FROM keeps WHERE id = @id;";
    
       var rows = _db.Execute(sql, new {id});
    if (rows !=1){throw new Exception("Data is bad or Id is Bad");}
    return;
    
  }
  internal Keep EditKeep(Keep original)
  {
     string sql = @"UPDATE keeps SET
                  name = @Name,
                  description = @Description,
                  img = @Img,
                  views = @Views
                  WHERE id = @Id 
                       ;";
        var rows = _db.Execute(sql, original);
        if (rows != 1)
        {
          throw new Exception("Unable to edit" );
        }
    
        return GetKeepById(original.Id);
  }
}