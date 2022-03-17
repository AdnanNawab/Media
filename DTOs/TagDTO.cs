using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Media.DTOs;

public record TagDTO
{
    [JsonPropertyName("tag_id")]
    public long TagId { get; set; }

    [JsonPropertyName("tag_name")]
    public string TagName { get; set; }

    [JsonPropertyName("post_id")]
    public long PostId { get; set; }

    [JsonPropertyName("post")]

    public List<PostDTO> Post { get; internal set; }

}

public record TagCreateDTO
{
    [JsonPropertyName("tag_id")]
    public long TagId { get; set; }

    [JsonPropertyName("tag_name")]
    public string TagName { get; set; }

    [JsonPropertyName("post_id")]
    public long PostId { get; set; }



}

public record TagUpdateDTO
{


    [JsonPropertyName("tag_name")]
    public string TagName { get; set; }


}