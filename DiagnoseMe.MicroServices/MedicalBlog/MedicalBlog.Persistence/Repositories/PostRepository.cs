namespace MedicalBlog.Persistence.Repositories;
public class PostRepository : BaseRepo<Post>, IPostRepository
{
    public PostRepository(DbContext db) : base(db){}

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
        return posts;
    }
}
