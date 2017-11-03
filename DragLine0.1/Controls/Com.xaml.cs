using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExtendedString;
using System.Diagnostics;

namespace DragLine0._1.Controls
{
    /// <summary>
    /// Com.xaml 的互動邏輯
    /// </summary>
    public partial class ComPort : UserControl
    {
        public Canvas canvas { get; set; }
        public Control parent { get; set; }
        private ComType _comType;
        public ComType comType
        {
            get { return _comType; }
            set { _comType = value; }
        }
        private LineHelper _LineHelper = SingleTon<LineHelper>.GetInstance();
        public ComPort()
        {
            InitializeComponent();
            comPort.PreviewMouseLeftButtonDown += Com_MouseDown;
            comPort.PreviewMouseLeftButtonUp += Com_MouseUp;
        }

        private void Com_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GetCanvas();
            _LineHelper.EndControl = parent;
            _LineHelper.EndCom = this;
            _LineHelper.EndPoint = GetPoint();
            _LineHelper.DrawLine();
        }

        private void Com_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            GetCanvas();
            _LineHelper.canvas = canvas;
            _LineHelper.StartCom = this;
            _LineHelper.StartControl = parent;
            _LineHelper.StartPoint = GetPoint();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }

        private void GetCanvas()
        {
            if (canvas == null)
            {
                var p1 = LogicalTreeHelper.GetParent(this) as Grid;
                parent= LogicalTreeHelper.GetParent(p1) as Control;
                canvas = parent.Parent as Canvas;
                if(parent==null||canvas==null)
                {
                    Debug.WriteLine("parent is " + (parent == null) + " canvas is " + (canvas == null));
                    throw new Exception();
                }
            }
        }

        private Point GetPoint()
        {
            switch (comType)
            {
                case ComType.empty: return this.TranslatePoint(new Point(0, 0), canvas);
                case ComType.Left:return this.TranslatePoint(new Point(0, this.ActualHeight / 2), canvas);
                case ComType.Top:return this.TranslatePoint(new Point(this.ActualWidth / 2, 0), canvas);
                case ComType.Right:return this.TranslatePoint(new Point(this.ActualWidth, this.ActualHeight / 2), canvas);
                case ComType.Buttom:return this.TranslatePoint(new Point(this.ActualWidth / 2, this.ActualHeight), canvas);
                default:return this.TranslatePoint(new Point(0, 0), canvas);
            }
        }
    }
}
