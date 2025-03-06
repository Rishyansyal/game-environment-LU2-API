namespace WebApi.Models
{
    public class Object2d
    {
        public int Id { get; set; }
        public int EnvironmentId { get; set; }
        public string PrefabId { get; set; }
        public float X_Position { get; set; }
        public float Y_Position { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float RotationZ { get; set; }
        public int SortingLayer { get; set; }
    }
}