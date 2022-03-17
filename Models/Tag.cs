using Media.DTOs;

namespace Media.Models;



public record Tag
{
    /// <summary>
    /// Primary Key - NOT NULL, Unique, Index is Available
    /// </summary>
    public long TagId { get; set; }
    public string TagName { get; set; }

    public long PostId { get; set; }



    /// <summary>
    /// Can be NULL
    /// </summary>


    public TagDTO asDto => new TagDTO
    {
        TagId = TagId,
        TagName = TagName,

        PostId = PostId,



    };
}