using System.ComponentModel.DataAnnotations;

namespace WebApi;

public class EnvironmentCreator
{

    public Guid Id { get; set; }
    public DateOnly Date { get; set; }


    [Range(-25,50)]
    public int TemperatureC { get; set; }


    public string? Summary { get; set; }
}
