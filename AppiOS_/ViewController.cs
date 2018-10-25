using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using System.Diagnostics;

namespace AppiOS_
{
    public partial class ViewController : UIViewController
    {
        DrawView drawView;
        int id = 1;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            View.BackgroundColor = UIColor.Cyan;

            var colorsBtn = new UIButton()
            {
                Frame = new CGRect((View.Frame.Width - 150) / 2, View.Frame.Bottom - 100, 150, 30),
                BackgroundColor = UIColor.Orange
            };
            colorsBtn.SetTitle("COLORS", UIControlState.Normal);
            colorsBtn.TouchUpInside += СolorsBtn_Click;

            var clearBtn = new UIButton()
            {
                Frame = new CGRect((View.Frame.Width - 150) / 2, View.Frame.Bottom - 150, 150, 30),
                BackgroundColor = UIColor.Orange
            };
            clearBtn.SetTitle("CLEAR", UIControlState.Normal);
            clearBtn.TouchUpInside += ClearBtn_Click;

            drawView = new DrawView
            {
                Frame = new CGRect(0, 70, View.Frame.Width, 500),
                BackgroundColor = UIColor.White,
            };
            clearBtn.Layer.BorderWidth = 2;
            clearBtn.Layer.BorderColor = UIColor.Black.CGColor;

            var label = new UILabel
            {
                Frame = new CGRect(View.Frame.Left + 30, View.Frame.Bottom - 50, 70, 30),
                Text = "Width:"
            };         

            var slider = new UISlider
            {
                MinValue = 1,
                MaxValue = 20,
                Continuous = true,
                Frame = new CGRect(View.Frame.Left + 90, View.Frame.Bottom - 50, 225, 30),
            };
            slider.AddTarget(SliderValueDidChange, UIControlEvent.AllEvents);
            var dict = new Dictionary<CGPoint, CGPoint>();

            var recon = new UIPanGestureRecognizer(r =>
            {
                switch (r.State)
                {
                    case UIGestureRecognizerState.Possible:
                        break;

                    case UIGestureRecognizerState.Began:
                        drawView.StartPoint = r.LocationInView(r.View);
                        drawView.EndPoint = r.LocationInView(r.View);
                        break;

                    case UIGestureRecognizerState.Changed:
                        drawView.StartPoint = drawView.EndPoint;
                        drawView.EndPoint = r.LocationInView(r.View);

                        drawView.DrawLines.Add(new DrawLine
                        {
                            Id = id,
                            StartPoint = drawView.StartPoint,
                            EndPoint = drawView.EndPoint,
                            LineColor = drawView.СurrentColor,
                            LineWidth = drawView.LineWidth
                        });
                        break;

                    case UIGestureRecognizerState.Ended:
                        id++;
                        break;

                    case UIGestureRecognizerState.Cancelled:
                        drawView.EndPoint = r.LocationInView(r.View);
                        break;

                    case UIGestureRecognizerState.Failed:
                        break;
                }
                drawView.SetNeedsDisplay();
            });
            drawView.AddGestureRecognizer(recon);
            View.AddSubviews(drawView, colorsBtn, slider, clearBtn, label);
        }

        void SliderValueDidChange(object sender, EventArgs eventArgs)
        {
            drawView.LineWidth = ((UISlider)sender).Value;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void СolorsBtn_Click(object sender, EventArgs e)
        {
            var alert = UIAlertController.Create("COLORS", "PLEASE SELECT A COLOR", UIAlertControllerStyle.ActionSheet);

            alert.AddAction(UIAlertAction.Create("Black", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                drawView.СurrentColor = UIColor.Black;
            }));
            alert.AddAction(UIAlertAction.Create("Green", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                drawView.СurrentColor = UIColor.Green;
            }));
            alert.AddAction(UIAlertAction.Create("Red", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                drawView.СurrentColor = UIColor.Red;
            }));
            alert.AddAction(UIAlertAction.Create("Orange", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                drawView.СurrentColor = UIColor.Orange;
            }));
            alert.AddAction(UIAlertAction.Create("Blue", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                drawView.СurrentColor = UIColor.Blue;
            }));
            alert.AddAction(UIAlertAction.Create("hide", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                DismissViewController(true, null);
            }));
            ShowViewController(alert, null);
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            var list = drawView.DrawLines.Where(i => i.Id == id - 1).ToList();

            Debug.Print(list.Count.ToString());
            foreach(var item in list)
            {
                drawView.DrawLines.Remove(item);
            }
            drawView.SetNeedsDisplay();
        }
    }
}
