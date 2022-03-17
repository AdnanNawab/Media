using Media.Models;
using Dapper;
using Media.Utilities;
using Media.DTOs;

namespace Media.Repositories;

public interface ITagRepository
{
    Task<Tag> Create(Tag Item);
    Task<bool> Update(Tag Item);
    Task<bool> Delete(long EmployeeNumber);
    Task<Tag> GetById(long EmployeeNumber);
    Task<List<Tag>>GetTagByPostId(long PostId);




}
public class TagRepository : BaseRepository, ITagRepository
{
    public TagRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Tag> Create(Tag Item)
    {
        var query = $@"INSERT INTO ""{TableNames.tag}"" 
        (tag_id, tag_name) 
        VALUES (@TagId, @TagName) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Tag>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long TagId)
    {
        var query = $@"DELETE FROM ""{TableNames.tag}"" 
        WHERE tag_id = @TagId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { TagId });
            return res > 0;
        }
    }


    public async Task<List<TagDTO>> GetAllForPost(long PostId)
    {
        var query = $@"SELECT * FROM {TableNames.tag} 
        WHERE post_id = @PostId";

        using (var con = NewConnection)
            return (await con.QueryAsync<TagDTO>(query, new { PostId })).AsList();
    }


    public async Task<Tag> GetById(long TagId)
    {
        var query = $@"SELECT * FROM ""{TableNames.tag}"" 
        WHERE tag_id = @TagId";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Tag>(query, new { TagId });
    }

    

    public async Task<bool> Update(Tag Item)
    {
        var query = $@"UPDATE ""{TableNames.tag}"" SET tag_name = @tagName, 
          WHERE tag_id = @tagId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }

    public async Task<List<Tag>>GetTagByPostId(long PostId)
    {
        var query = $@"SELECT * FROM ""{TableNames.tag_post}"" tp
        LEFT JOIN {TableNames.tag} t ON t.tag_id = tp.tag_id
        WHERE tp.post_id = @post_id";

        using(var con = NewConnection){
            return (await con.QueryAsync<Tag>(query,new{PostId})).AsList();
        }
    }
}