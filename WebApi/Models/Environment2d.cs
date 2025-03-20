using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Environment2d
{

    public int Id { get; set; }
    public string Name{ get; set; }
    public int MaxLength { get; set; } 
    public int MaxHeight { get; set; }
    public string UserId { get; set; }
}
