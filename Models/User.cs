using Media.DTOs;

namespace Media.Models;



public record User
{
    
    public long UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string MailId { get; set; }
    public long Mobile { get; set; }


    


    public UserDTO asDto => new UserDTO
    {
        UserId = UserId,
        UserName = UserName,
        Password = Password,
        Mobile = Mobile,
        MailId = MailId,


    };
}