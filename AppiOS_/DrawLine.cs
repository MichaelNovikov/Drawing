using CoreGraphics;
using UIKit;

namespace AppiOS_
{
    public struct DrawLine
    {
        internal int Id { get; set; }
        internal CGPoint StartPoint { get; set; }
        internal CGPoint EndPoint { get; set; }
        internal UIColor LineColor { get; set; }
        internal float LineWidth { get; set; }
    }
}