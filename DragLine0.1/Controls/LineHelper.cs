using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DragLine0._1.Controls
{
    public class LineHelper
    {
        public Canvas canvas { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public ComPort StartCom { get; set; }
        public ComPort EndCom { get; set; }

        public Control StartControl { get; set; }
        public Control EndControl { get; set; }

        private Point MinPoint, MaxPoint;
        public void DrawLine()
        {
            if (canvas==null)
            {
                return;
            }
            Line NewLine = new Line() { StrokeThickness = 3, Stroke = new SolidColorBrush(Colors.Black), Visibility = Visibility.Visible };
            Path NewPath = new Path() { StrokeThickness = 2, Stroke = new SolidColorBrush(Colors.Black) };
            //NewLine.X1 = StartPoint.X;
            //NewLine.Y1 = StartPoint.Y;
            //NewLine.X2 = EndPoint.X;            
            //NewLine.Y2 = EndPoint.Y;
            MinPoint = new Point(StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X, StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y);
            MaxPoint = new Point(StartPoint.X < EndPoint.X ? EndPoint.X : StartPoint.X, StartPoint.Y < EndPoint.Y ? EndPoint.Y : StartPoint.Y);
            canvas.Children.Add(NewLine);
            DrawLine2();
            Debug.WriteLine(canvas.Children.Count);
        }

        private void DrawLine2()
        {
            Point A = new Point() { X = Canvas.GetLeft(StartControl), Y = Canvas.GetTop(StartControl) };
            bool IsIntersectA = IsRectIntersect(MinPoint, MaxPoint, A, new Point() { X = A.X + StartControl.ActualWidth, Y = A.Y + StartControl.ActualHeight });
            Point B = new Point() { X = Canvas.GetLeft(EndControl), Y = Canvas.GetTop(EndControl) };
            bool IsIntersectB = IsRectIntersect(MinPoint, MaxPoint, B, new Point() { X = B.X + EndControl.ActualWidth, Y = B.Y + EndControl.ActualHeight });
            if(!IsIntersectA&&!IsIntersectB)
            {
                Debug.Write("no intersect");
                if (IsFaceToFace(StartCom, EndCom))
                {
                    if (StartCom.comType == ComType.Left || StartCom.comType == ComType.Right)
                    {
                        //Path NewPath = new Path() { StrokeThickness = 2, Stroke = new SolidColorBrush(Colors.Black) };
                        //PathGeometry pathGeometry = new PathGeometry();
                        //PathFigure pathFigure = new PathFigure() { StartPoint = StartPoint };
                        //LineSegment FirstLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(MinPoint.X / 2 + MaxPoint.X / 2, StartPoint.Y) };
                        //LineSegment SecondLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(MinPoint.X / 2 + MaxPoint.X / 2, EndPoint.Y) };
                        //LineSegment ThirdLine = new LineSegment() { IsSmoothJoin = true, Point = EndPoint };
                        //pathFigure.Segments.Add(FirstLine);
                        //pathFigure.Segments.Add(SecondLine);
                        //pathFigure.Segments.Add(ThirdLine);
                        //pathGeometry.Figures.Add(pathFigure);
                        //NewPath.Data = pathGeometry;
                        //canvas.Children.Add(NewPath);
                        canvas.Children.Add(DrawPathRightToLeft(StartPoint, EndPoint));
                    }
                    else
                    {
                        //Path NewPath = new Path() { StrokeThickness = 2, Stroke = new SolidColorBrush(Colors.Black) };
                        //PathGeometry pathGeometry = new PathGeometry();
                        //PathFigure pathFigure = new PathFigure() { StartPoint = StartPoint };
                        //LineSegment FirstLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(StartPoint.X, MinPoint.Y / 2 + MaxPoint.Y / 2) };
                        //LineSegment SecondLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(EndPoint.X, MinPoint.Y / 2 + MaxPoint.Y / 2) };
                        //LineSegment ThirdLine = new LineSegment() { IsSmoothJoin = true, Point = EndPoint };
                        //pathFigure.Segments.Add(FirstLine);
                        //pathFigure.Segments.Add(SecondLine);
                        //pathFigure.Segments.Add(ThirdLine);
                        //pathGeometry.Figures.Add(pathFigure);
                        //NewPath.Data = pathGeometry;
                        //canvas.Children.Add(NewPath);
                        canvas.Children.Add(DrawPathTopToButtom(StartPoint, EndPoint));
                    }
                }
                else
                {
                    Point PointA = StartCom.comType == ComType.Left || StartCom.comType == ComType.Right ? EndPoint : StartPoint;
                    Point PointB = StartCom.comType == ComType.Left || StartCom.comType == ComType.Right ? StartPoint : EndPoint;
                    Path NewPath = new Path() { StrokeThickness = 2, Stroke = new SolidColorBrush(Colors.Black) };
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigure pathFigure = new PathFigure() { StartPoint = StartPoint };
                    LineSegment FirstLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(PointA.X, PointB.Y) };
                    LineSegment SecondLine = new LineSegment() { IsSmoothJoin = true, Point = EndPoint };
                    pathFigure.Segments.Add(FirstLine);
                    pathFigure.Segments.Add(SecondLine);
                    pathGeometry.Figures.Add(pathFigure);
                    NewPath.Data = pathGeometry;
                    canvas.Children.Add(NewPath);
                }
            }
            else if (IsIntersectA&&!IsIntersectB)
            {
                Debug.WriteLine("intersect with a");

            }
            else if (!IsIntersectA&&IsIntersectB)
            {
                Debug.WriteLine("intersect with b");
            }
            else
            {
                Debug.WriteLine("intersect with a and b");
            }
        }

        private Path DrawPathRightToLeft(Point StartPoint, Point EndPoint, ComPort StartCom = null, ComPort EndCom = null)
        {
            Path NewPath = new Path() { StrokeThickness = 2, Stroke = new SolidColorBrush(Colors.Black) };
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure() { StartPoint = StartPoint };
            LineSegment FirstLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(StartPoint.X / 2 + EndPoint.X / 2, StartPoint.Y) };
            LineSegment SecondLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(StartPoint.X / 2 + EndPoint.X / 2, EndPoint.Y) };
            LineSegment ThirdLine = new LineSegment() { IsSmoothJoin = true, Point = EndPoint };
            pathFigure.Segments.Add(FirstLine);
            pathFigure.Segments.Add(SecondLine);
            pathFigure.Segments.Add(ThirdLine);
            pathGeometry.Figures.Add(pathFigure);
            NewPath.Data = pathGeometry;
            return NewPath;
        }

        private Path DrawPathTopToButtom(Point StartPoint, Point EndPoint, ComPort StartCom = null, ComPort EndCom = null)
        {
            Path NewPath = new Path() { StrokeThickness = 2, Stroke = new SolidColorBrush(Colors.Black) };
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure() { StartPoint = StartPoint };
            LineSegment FirstLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(StartPoint.X, StartPoint.Y / 2 + EndPoint.Y / 2) };
            LineSegment SecondLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(EndPoint.X, StartPoint.Y / 2 + EndPoint.Y / 2) };
            LineSegment ThirdLine = new LineSegment() { IsSmoothJoin = true, Point = EndPoint };
            pathFigure.Segments.Add(FirstLine);
            pathFigure.Segments.Add(SecondLine);
            pathFigure.Segments.Add(ThirdLine);
            pathGeometry.Figures.Add(pathFigure);
            NewPath.Data = pathGeometry;
            return NewPath;
        }

        private Path DrawPathRightToButtom(Point StartPoint, Point EndPoint)
        {
            Path NewPath = new Path() { StrokeThickness = 2, Stroke = new SolidColorBrush(Colors.Black) };
            Point PointA = StartCom.comType == ComType.Left || StartCom.comType == ComType.Right ? EndPoint : StartPoint;
            Point PointB = StartCom.comType == ComType.Left || StartCom.comType == ComType.Right ? StartPoint : EndPoint;
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure() { StartPoint = StartPoint };
            LineSegment FirstLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(PointA.X, PointB.Y) };
            LineSegment SecondLine = new LineSegment() { IsSmoothJoin = true, Point = EndPoint };
            pathFigure.Segments.Add(FirstLine);
            pathFigure.Segments.Add(SecondLine);
            pathGeometry.Figures.Add(pathFigure);
            NewPath.Data = pathGeometry;
            return NewPath;
        }

        private Path DrawPathTopToLeft(Point StartPoint, Point EndPoint,ComPort StartCom,ComPort EndCom)
        {
            Path NewPath = new Path() { StrokeThickness = 2, Stroke = new SolidColorBrush(Colors.Black) };
            Point PointA = StartCom.comType == ComType.Left || StartCom.comType == ComType.Right ? EndPoint : StartPoint;
            Point PointB = StartCom.comType == ComType.Left || StartCom.comType == ComType.Right ? StartPoint : EndPoint;
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure() { StartPoint = StartPoint };
            LineSegment FirstLine = new LineSegment() { IsSmoothJoin = true, Point = new Point(PointA.X, PointB.Y) };
            LineSegment SecondLine = new LineSegment() { IsSmoothJoin = true, Point = EndPoint };
            pathFigure.Segments.Add(FirstLine);
            pathFigure.Segments.Add(SecondLine);
            pathGeometry.Figures.Add(pathFigure);
            NewPath.Data = pathGeometry;
            return NewPath;
        }

        private bool IsRectIntersect(Point A,Point B,Point C,Point D)//A&B belong to the same rectangle, C&D belong to the same rectangle
        {
            return !((C.X <= A.X && D.X <= A.X) || (C.X >= B.X && D.X >= B.X) || (C.Y <= A.Y && D.Y <= A.Y) || (C.Y >= B.Y && D.Y >= B.Y));
        }

        private bool IsFaceToFace(ComPort A,ComPort B)
        {
            return ((int)A.comType + (int)B.comType == 5);
        }

        private Point FindNonIntersectPoint(IComPort IntersectControl,ComPort IntersectComPort, Point AnotherPoint,IComPort AnotherControl)
        {
            Point[] PointArray = new Point[2];
            switch(IntersectComPort.comType)
            {
                case ComType.Buttom:PointArray[0] = IntersectControl.LeftButtomPoint;PointArray[1] = IntersectControl.RightButtomPoint;break;
                case ComType.Left:PointArray[0] = IntersectControl.LeftButtomPoint;PointArray[1] = IntersectControl.LeftTopPoint; break;
                case ComType.Right:PointArray[0] = IntersectControl.RightButtomPoint;PointArray[1] = IntersectControl.RightTopPoint;break;
                case ComType.Top:PointArray[0] = IntersectControl.LeftTopPoint;PointArray[1] = IntersectControl.RightTopPoint;break;
            }
            foreach (Point item in PointArray)
            {
                if (IsRectIntersect(IntersectControl.LeftTopPoint, IntersectControl.RightButtomPoint, item, AnotherPoint))
                    return item;
            }
            switch(IntersectComPort.comType)
            {
                case ComType.Buttom:PointArray[0] = IntersectControl.LeftTopPoint;PointArray[1] = IntersectControl.RightTopPoint;break;
                case ComType.Left: PointArray[0] = IntersectControl.RightButtomPoint; PointArray[1] = IntersectControl.RightTopPoint; break;
                case ComType.Right: PointArray[0] = IntersectControl.LeftButtomPoint; PointArray[1] = IntersectControl.LeftTopPoint; break;
                case ComType.Top: PointArray[0] = IntersectControl.LeftButtomPoint; PointArray[1] = IntersectControl.RightButtomPoint; break;
            }
            foreach (Point item in PointArray)
            {
                if (IsRectIntersect(IntersectControl.LeftTopPoint, IntersectControl.RightButtomPoint, item, AnotherPoint))
                    return item;
            }
            return new Point(-1, -1);
        }
    }

    public enum ComType
    {
        empty,
        Left,
        Buttom,
        Top,
        Right
    }
}
