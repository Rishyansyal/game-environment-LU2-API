using System.ComponentModel.DataAnnotations;

namespace WebApi;

public class WorldCreator
{

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    

}