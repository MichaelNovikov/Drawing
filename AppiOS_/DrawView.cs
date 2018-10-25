using CoreGraphics;
using System.Collections.Generic;
using System.Diagnostics;
using UIKit;

namespace AppiOS_
{
    public class DrawView : UIView
    {
        internal CGPoint StartPoint { get; set; }
        internal CGPoint EndPoint { get; set; }
        internal UIColor СurrentColor { get; set; } = UIColor.Black;
        internal float LineWidth { get; set; } = 1f;
        internal List<DrawLine> DrawLines { get; set; } = new List<DrawLine>();

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            var context = UIGraphics.GetCurrentContext();

            foreach (var item in DrawLines)
            {
                item.LineColor.SetStroke();
                context.SetLineWidth(item.LineWidth);
                context.MoveTo(item.StartPoint.X, item.StartPoint.Y);
                context.AddLineToPoint(item.EndPoint.X, item.EndPoint.Y);
                context.SetLineCap(CGLineCap.Round);
                context.StrokePath();
            }

            UIColor.Green.SetFill();
            context.DrawPath(CGPathDrawingMode.FillStroke);
        }
    }
}
