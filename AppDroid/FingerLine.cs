using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using System;
using System.Collections.Generic;
using static Android.Views.View;

namespace AppDroid
{
    public class FingerLine : View, IOnTouchListener
    {
        private Paint _paint = new Paint();
        private float _startX;
        private float _startY;
        private float _endX;
        private float _endY;
        public List<Line> _lines { get; set; } = new List<Line>();
        public Color _color { get; set; } = Color.Black;
        public float _width { get; set; } = 5f;
        public int id = 1;

        public FingerLine(Context context) : base(context)
        {
            _paint.StrokeCap = Paint.Cap.Round;
            _lines = new List<Line>();
            SetOnTouchListener(this);
        }

        #region ctors
        public FingerLine(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public FingerLine(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public FingerLine(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected FingerLine(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        #endregion

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            foreach (var item in _lines)
            {
                _paint.Color = item.LineColor;
                _paint.StrokeWidth = item.LineWidth;
                canvas.DrawLine(item.StartX, item.StartY, item.EndX, item.EndY, _paint);
            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _startX = e.GetX();
                    _startY = e.GetY();
                    _endX = _startX;
                    _endY = _startY;
                    break;
                case MotionEventActions.Move:
                    _startX = _endX;
                    _startY = _endY;
                    _endX = e.GetX();
                    _endY = e.GetY();

                    _lines.Add(new Line
                    {
                        Id = id,
                        StartX = _startX,
                        StartY = _startY,
                        EndX = _endX,
                        EndY = _endY,
                        LineColor = _color,
                        LineWidth = _width
                    });
                    break;
                case MotionEventActions.Up:
                    id++;
                    break;
            }
            Invalidate();
            return true;
        }
    }
}