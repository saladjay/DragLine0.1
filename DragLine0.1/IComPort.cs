using DragLine0._1.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DragLine0._1
{
    public interface IComPort
    {
        ComPort LeftTop { get; set; }
        ComPort LeftButtom { get; set; }
        ComPort RightTop { get; set; }
        ComPort RightButtom { get; set; }
        ComPort[] ComArray { get; set; }

        Point LeftTopPoint { get; }
        Point LeftButtomPoint { get; }
        Point RightTopPoint { get; }
        Point RightButtomPoint { get; }
    }
}
