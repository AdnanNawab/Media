using Media.DTOs;
using Media.Models;
using Media.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Media.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostRepository _post;

    private readonly IUserRepository _user;
    private readonly ILikeRepository _like;
    private readonly ITagRepository _tag;
    public PostController(ILogger<PostController> logger, IPostRepository post,IUserRepository user,ILikeRepository like,ITagRepository tag)
    {
        _logger = logger;
        _post = post;
        _user = user;
        _like = like;
        _tag = tag;
    }

    [HttpGet]
    public async Task<ActionResult<List<PostDTO>>> GetAllPost()
    {
        var postList = await _post.GetList();

        // User -> UserDTO
        var dtoList = postList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{post_id}")]
    public async Task<ActionResult<PostDTO>> GetUserById([FromRoute] long post_id)
    {
        var post = await _post.GetById(post_id);

        if (post is null)
            return NotFound("No user found with given  id");

            var dto = post.asDto;
            dto.Tag = (await _tag.GetTagByPostId(post_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<PostDTO>> CreatePost([FromBody] PostCreateDTO Data)
    {

        var toCreatePost = new Post
        {
            PostTitle = Data.PostTitle.Trim(),
            PostDate = Data.PostDate.UtcDateTime,
            UserId = Data.UserId,
            Type = Data.Type.Trim(),

        };

        var createdPost = await _post.Create(toCreatePost);

        return StatusCode(StatusCodes.Status201Created, createdPost.asDto);
    }

   

    [HttpDelete("{post_id}")]
    public async Task<ActionResult> DeletePost([FromRoute] long post_id)
    {
        var existing = await _post.GetById(post_id);
        if (existing is null)
            return NotFound("No user found with given post id");

        var didDelete = await _post.Delete(post_id);

        return NoContent();
    }
}

