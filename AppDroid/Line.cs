using Android.Graphics;

namespace AppDroid
{
    public struct Line
    {
        public int Id { get; set; }
        public float StartX { get; set; }
        public float StartY { get; set; }
        public float EndX { get; set; }
        public float EndY { get; set; }
        public Color LineColor { get; set; }
        public float LineWidth { get; set; }
    }   
}