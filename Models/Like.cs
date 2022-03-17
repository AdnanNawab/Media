using Media.DTOs;

namespace Media.Models;



public record Like
{
    /// <summary>
    /// Primary Key - NOT NULL, Unique, Index is Available
    /// </summary>
    public long LikeId { get; set; }
    public long PostId { get; set; }
    public long UserId { get; set; }



    /// <summary>
    /// Can be NULL
    /// </summary>


    public LikeDTO asDto => new LikeDTO
    {
        LikeId = LikeId,
        PostId = PostId,
        UserId = UserId,



    };
}