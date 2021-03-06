using Media.DTOs;

namespace Media.Models;



public record Post
{
    /// <summary>
    /// Primary Key - NOT NULL, Unique, Index is Available
    /// </summary>
    public long PostId { get; set; }
    public string PostTitle { get; set; }
    public DateTimeOffset PostDate { get; set; }
    public long UserId { get; set; }
    public string Type { get; set; }


    /// <summary>
    /// Can be NULL
    /// </summary>


    public PostDTO asDto => new PostDTO
    {
        PostId = PostId,
        PostTitle = PostTitle,
        PostDate = PostDate,
        UserId = UserId,
        Type = Type


    };
}