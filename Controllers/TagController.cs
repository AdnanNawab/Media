using Media.DTOs;
using Media.Models;
using Media.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Media.Controllers;

[ApiController]
[Route("api/tag")]
public class TagController : ControllerBase
{
    private readonly ILogger<TagController> _logger;
    private readonly ITagRepository _tag;
 private readonly IPostRepository _post;
    public TagController(ILogger<TagController> logger, ITagRepository tag,IPostRepository post)
    {
        _logger = logger;
        _tag = tag;
        _post = post;
    }

    // [HttpGet]
    // public async Task<ActionResult<List<tagDTO>>> GetAlltag()
    // {
    //     var tagList = await _tag.GetList();

    //     // User -> UserDTO
    //     var dtoList = tagList.Select(x => x.asDto);

    //     return Ok(dtotag);
    // }

    [HttpGet("{tag_id}")]
    public async Task<ActionResult<TagDTO>> GetUserById([FromRoute] long tag_id)
    {
        var tag = await _tag.GetById(tag_id);

        if (tag is null)
            return NotFound("No tag found with given  id");
            var dto = tag.asDto;
            dto.Post = (await _post.GetPostByTagId(tag_id)).Select(x => x.asDto).ToList();



        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<TagDTO>> Createtag([FromBody] TagCreateDTO Data)
    {

        var toCreatetag = new Tag
        {
            PostId = Data.PostId,
            TagName = Data.TagName.Trim(),


        };

        var createdTag = await _tag.Create(toCreatetag);

        return StatusCode(StatusCodes.Status201Created, createdTag.asDto);
    }

    [HttpPut("{tag_id}")]
    public async Task<ActionResult> Updatetag([FromRoute] long tag_id,
    [FromBody] TagUpdateDTO Data)
    {
        var existing = await _tag.GetById(tag_id);
        if (existing is null)
            return NotFound("No user found with given tag_id");

        var toUpdateLike = existing with
        {
            //tagName = Data.tagName?.Trim()?.ToLower() ?? existing.Name,
            TagName = Data.TagName?.Trim()?.ToLower() ?? existing.TagName,



        };

        var didUpdate = await _tag.Update(toUpdateLike);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update tag");

        return NoContent();
    }

    [HttpDelete("{tag_id}")]
    public async Task<ActionResult> Deletetag([FromRoute] long tag_id)
    {
        var existing = await _tag.GetById(tag_id);
        if (existing is null)
            return NotFound("No user found with given tag id");

        var didDelete = await _tag.Delete(tag_id);

        return NoContent();
    }
}

