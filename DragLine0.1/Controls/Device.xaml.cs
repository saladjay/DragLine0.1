using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DragLine0._1.Controls
{
    /// <summary>
    /// Device.xaml 的互動邏輯
    /// </summary>
    public partial class Device : UserControl,IComPort
    {
        public Device()
        {
            InitializeComponent();
        }

        public ComPort[] ComArray
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ComPort LeftButtom
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Point LeftButtomPoint
        {
            get
            {
                return Parent != null ? this.TranslatePoint(new Point(0, this.ActualHeight), (UIElement)Parent) : new Point(0, 0);
            }
        }

        public ComPort LeftTop
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Point LeftTopPoint
        {
            get
            {
                return Parent != null ? this.TranslatePoint(new Point(0, 0), (UIElement)Parent) : new Point(0, 0);
            }
        }

        public ComPort RightButtom
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Point RightButtomPoint
        {
            get
            {
                return Parent != null ? this.TranslatePoint(new Point(this.ActualWidth, this.ActualHeight), (UIElement)Parent) : new Point(0, 0);
            }
        }

        public ComPort RightTop
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Point RightTopPoint
        {
            get
            {
                return Parent != null ? this.TranslatePoint(new Point(this.ActualWidth, 0), (UIElement)Parent) : new Point(0, 0);
            }
        }
    }
}
