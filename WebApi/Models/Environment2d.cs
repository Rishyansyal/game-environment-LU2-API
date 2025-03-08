using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Environment2d
{

    [Range(0, 4)]
    public int Id { get; set; }
    public string Name{ get; set; }
    public int MaxLength { get; set; } 
    public int MaxHeight { get; set; }
}
