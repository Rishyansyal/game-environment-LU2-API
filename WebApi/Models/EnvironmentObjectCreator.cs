namespace WebApi.Models
{
    public class EnvironmentObject
    {
        public int Id { get; set; }
        public int WorldId { get; set; }
        public string ObjectType { get; set; }
        public float X_Position { get; set; }
        public float Y_Position { get; set; }
        public float Rotation { get; set; }
    }

}
