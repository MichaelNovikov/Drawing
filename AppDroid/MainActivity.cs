using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.Linq;
using System;
using static Android.Widget.SeekBar;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace AppDroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private FingerLine _drawView;
        private Button _colorsBtn;
        private Button _clearBtn;
        private SeekBar _seekBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _clearBtn = FindViewById<Button>(Resource.Id.button1);
            _clearBtn.Click += ClearBtn_Click;

            _colorsBtn = FindViewById<Button>(Resource.Id.button2);
            _colorsBtn.Click += ColorsBtn_Click;

            _drawView = new FingerLine(this);
            _drawView.SetBackgroundColor(Color.White);

            _seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            _seekBar.ProgressChanged += SeekBar_Changed;

            var lauout = FindViewById<LinearLayout>(Resource.Id.linear_layout);
            lauout.AddView(_drawView);
        }

        private void ColorsBtn_Click(object sender, EventArgs e)
        {
            var colors = new string[] { "Black", "Green", "Red", "Orange", "Blue", "hide" };
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
            alertDialog.SetTitle("PLEASE SELECT A COLOR");   
            alertDialog.SetItems(colors, OnColorClick);
            alertDialog.Show();
        }

        private void OnColorClick(object sender, DialogClickEventArgs args)
        {  
            switch (args.Which)
            {
                case 0:
                    _drawView._color = Color.Black;
                    break;
                case 1:
                    _drawView._color = Color.Green;
                    break;
                case 2:
                    _drawView._color = Color.Red;
                    break;
                case 3:
                    _drawView._color = Color.Orange;
                    break;
                case 4:
                    _drawView._color = Color.Blue;
                    break;
                case 5:
                    break;
            }
        }

        private void SeekBar_Changed(object s, ProgressChangedEventArgs e)
        {
              _drawView._width = e.Progress;
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            var list = _drawView._lines.Where(i => i.Id == _drawView.id - 1).ToList();

            foreach (var item in list)
            {
                _drawView._lines.Remove(item);
            }
            _drawView.Invalidate();
        }
    }
}