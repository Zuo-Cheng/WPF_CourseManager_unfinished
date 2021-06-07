using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyControls
{
    /// <summary>
    /// InstrumentControl.xaml 的交互逻辑
    /// </summary>
    public partial class InstrumentControl : UserControl
    {
            

        /// <summary>
        /// 仪表器的背景颜色
        /// </summary>
        public Brush PlateBackground
        {
            get { return (Brush)GetValue(PlateBackgroundProperty); }
            set { SetValue(PlateBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlateBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlateBackgroundProperty =
            DependencyProperty.Register("PlateBackground", typeof(Brush), typeof(InstrumentControl), new PropertyMetadata(default(Brush)));


        /// <summary>
        /// 仪表器的字体大小
        /// </summary>
        public int ScaleTextSize
        {
            get { return (int)GetValue(ScaleTextSizeProperty); }
            set { SetValue(ScaleTextSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleTextSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleTextSizeProperty =
            DependencyProperty.Register("ScaleTextSize", typeof(int), typeof(InstrumentControl),
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnValuePropertyChanged)));



        /// <summary>
        /// 刻度字体颜色
        /// </summary>
        public Brush ScaleFontBrush
        {
            get { return (Brush)GetValue(ScaleFontBrushProperty); }
            set { SetValue(ScaleFontBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleFontBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleFontBrushProperty =
            DependencyProperty.Register("ScaleFontBrush", typeof(Brush), typeof(InstrumentControl),
                new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnValuePropertyChanged)));




        /// <summary>
        /// 仪表器的值得依赖属性
        /// </summary>
        public int InstrumentValue
        {
            get { return (int)this.GetValue(InstrumentValueProerty); }
            set { this.SetValue(InstrumentValueProerty, value); }
        }
        /// <summary>
        /// 注册仪表器的值得依赖属性
        /// </summary>
        public static readonly DependencyProperty InstrumentValueProerty =
            DependencyProperty.Register("InstrumentValue", typeof(int), typeof(InstrumentControl),
                new PropertyMetadata(default(int),new PropertyChangedCallback(OnValuePropertyChanged)));


        /// <summary>
        /// 注册仪表器的最小值
        /// </summary>
        public int Minimun
        {
            get { return (int)GetValue(MinimunProperty); }
            set { SetValue(MinimunProperty, value); }
        }
            
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimunProperty =
            DependencyProperty.Register("MyProperty", typeof(int), typeof(InstrumentControl), 
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnValuePropertyChanged)));


        /// <summary>
        /// 最大值
        /// </summary>
        public int Maxmun
        {
            get { return (int)GetValue(MaxmunProperty); }
            set { SetValue(MaxmunProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maxmun.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxmunProperty =
            DependencyProperty.Register("Maxmun", typeof(int), typeof(InstrumentControl),
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnValuePropertyChanged)));



        /// <summary>
        /// 刻度区间
        /// </summary>
        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), typeof(InstrumentControl),
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnValuePropertyChanged)));





        /// <summary>
        /// 当仪表器的值发生改变时触发这个依赖属性的委托方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //刷新一下仪表器
            (d as InstrumentControl).Refresh();
        }

        public InstrumentControl()
        {
            InitializeComponent();

            //当前用户控件大小发生变化时触发
            this.SizeChanged += InstrumentControl_SizeChanged;
        }

        /// <summary>
        /// 当前用户控件大小发生变化时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstrumentControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double minSize = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
            this.backEllipse.Width = minSize;
            this.backEllipse.Height = minSize;
        }


        /// <summary>
        /// 仪表器的值发生改变时刷新
        /// </summary>
        private void Refresh()
        {
            //获得仪表器的半径，用于围绕中心点进行画尺度
            double radius = this.backEllipse.Width / 2;
            if (double.IsNaN(radius)) return;

            this.mainCanvas.Children.Clear();


            //仪表器中各多少个尺度为一个长的尺度
            //double scaleAreaCount = 10;

            double step = 270.0 / (this.Maxmun - this.Minimun);

            //画仪表器的短尺度
            for(int i = 0; i < this.Maxmun - this.Minimun; i++)
            {
                Line lineScale = new Line();
                lineScale.X1 = radius - (radius - 13) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 13) * Math.Sin((i * step - 45) * Math.PI / 180);
                lineScale.X2 = radius - (radius - 8) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y2 = radius - (radius - 8) * Math.Sin((i * step - 45) * Math.PI / 180);

                lineScale.Stroke = this.ScaleFontBrush;
                lineScale.StrokeThickness = 1;

                this.mainCanvas.Children.Add(lineScale);
            }
            //画仪表器的长尺度
            step = 270.0 / Interval;
            int scaleText = this.Minimun;
            for (int i = 0; i <= Interval; i++)
            {
                Line lineScale = new Line();
                lineScale.X1 = radius - (radius - 20) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 20) * Math.Sin((i * step - 45) * Math.PI / 180);
                lineScale.X2 = radius - (radius - 8) * Math.Cos((i * step - 45) * Math.PI / 180);
                lineScale.Y2 = radius - (radius - 8) * Math.Sin((i * step - 45) * Math.PI / 180);

                lineScale.Stroke = this.ScaleFontBrush;
                lineScale.StrokeThickness = 1;

                this.mainCanvas.Children.Add(lineScale);

                //在刻度上面添加文字
                TextBlock textScale = new TextBlock();
                textScale.Width = 34;
                textScale.TextAlignment = TextAlignment.Center;
                textScale.FontSize = this.ScaleTextSize;
                textScale.Text = (scaleText + (this.Maxmun - this.Minimun) / Interval * i).ToString();

                textScale.Foreground = this.ScaleFontBrush;

                Canvas.SetLeft(textScale, radius - (radius - 36) * Math.Cos((i * step - 45) * Math.PI / 180)-17);
                Canvas.SetTop(textScale, radius - (radius - 36) * Math.Sin((i * step - 45) * Math.PI / 180)-10);
                this.mainCanvas.Children.Add(textScale);
            }

            //画指针下面的白色半圆
            string sData = "M{0} {1} A{0} {0} 0 1 1 {1} {2}";
            sData = string.Format(sData, radius / 2,radius,radius*1.5);
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            this.circle.Data = (Geometry)converter.ConvertFrom(sData);

            step = 270.0 / (this.Maxmun - this.Minimun);
            //this.rtPointer.Angle = this.InstrumentValue * step - 45;

            //double value = double.IsNaN(this.InstrumentValue) ? 0 : this.InstrumentValue;
            //设置指针移动时是动画效果
            DoubleAnimation da = new DoubleAnimation((int)((this.InstrumentValue-this.Minimun) *step)-45,new 
                Duration(TimeSpan.FromMilliseconds(200)));
            this.rtPointer.BeginAnimation(RotateTransform.AngleProperty, da);

            //画指针
            sData = "M{0} {1} ,{1} {2} ,{1} {3}";
            sData = string.Format(sData, radius * 0.3, radius, radius - 5, radius + 5);
            this.pointer.Data = (Geometry)converter.ConvertFrom(sData);
        }
    }
}
