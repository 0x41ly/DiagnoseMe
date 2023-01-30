namespace MedicalBlog.Persistence.Repositories;
public class PostRepository : BaseRepo<Post>, IPostRepository
{
    public PostRepository(DbContext db) : base(db){}

    public async override Task<List<Post>> GetAllAsync()
    {
        return await table
            .Include(x => x.Author)
            .Include(x => x.Comments)
            .ThenInclude(c => c.Author)
            .ToListAsync();
    }
    public async override Task<Post> GetByIdAsync(object id)
    {
        return (await table
            .Where(x => x.Id == (string) id)
            .Include(x => x.Author)
            .Include(x => x.Comments)
            .ThenInclude(c => c.Author)
            .FirstOrDefaultAsync())!;
    }

    public async Task<List<Post>> GetByDocterIdAsync(string authorId)
    {
        return (await GetAllAsync())
            .Where(x => x.AuthorId == authorId)
            .ToList(); 
    }

    public async Task<List<Post>> GetByTagsAsync(List<string> tags)
    {
        var posts = new List<Post>();
        var allPosts = await GetAllAsync();
        foreach(var tag in tags){
            posts.AddRange(allPosts.Where(x => x.Tags.Contains(tag)));
        }
        return posts.Distinct().ToList();
    }
}
