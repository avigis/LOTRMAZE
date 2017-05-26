﻿using MazeLib;
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

namespace Wpfs.Views
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        public MazeBoard()
        {
            InitializeComponent();
        }

        public string MazeName
        {
            get { return (string)GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeBoard));


        public int MazeRows
        {
            get { return (int)GetValue(MazeRowsProperty); }
            set { SetValue(MazeRowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeRows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeRowsProperty =
            DependencyProperty.Register("MazeRows", typeof(int), typeof(MazeBoard));



        public int MazeCols
        {
            get { return (int)GetValue(MazeColsProperty); }
            set { SetValue(MazeColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeCols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeColsProperty =
            DependencyProperty.Register("MazeCols", typeof(int), typeof(MazeBoard));


        public string InitialPos
        {
            get { return (string)GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialPosProperty =
            DependencyProperty.Register("InitialPos", typeof(string), typeof(MazeBoard));


        public string GoalPos
        {
            get { return (string)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeBoard));



        public string CurPos
        {
            get { return (string)GetValue(CurPosProperty); }
            set { SetValue(CurPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurPosProperty =
            DependencyProperty.Register("CurPos", typeof(string), typeof(MazeBoard));





        public void Draw(string recsStr, int rows, int cols, Position curPos, Position goalPos)
        {
            int recindex= 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Rectangle rec = new Rectangle()
                    {
                        Height = (this.ActualHeight / rows),/* 50,*/
                        Width = (this.ActualWidth / cols),/* 50,*/
                        Fill = Brushes.White,
                        //Stroke = Brushes.Black,
                        //StrokeThickness = 2
                    };

                    while (recsStr[recindex] != '1' && recsStr[recindex] != '0'
                        && recsStr[recindex] != '#' && recsStr[recindex] != '*')
                    {
                        recindex++;
                    }

                    if (recsStr[recindex] == '1')
                            rec.Fill = Brushes.Black;
                        else if (recsStr[recindex] == '0')
                            rec.Fill = Brushes.White;
                        //else if (recsStr[recindex] == '*')
                        //    rec.Fill = new ImageBrush(new BitmapImage(new Uri(@"resources\homer.jpg", UriKind.Relative)));
                        //else if (recsStr[recindex] == '#')
                        //    rec.Fill = new ImageBrush(new BitmapImage(new Uri(@"resources\donut.jpg", UriKind.Relative)));

                        Canvas.SetTop(rec, i * rec.Height);
                        Canvas.SetLeft(rec, j * rec.Width);
                        myCanvas.Children.Add(rec);
                        recindex++;
                }
            }

            Rectangle recInit = new Rectangle()
            {
                Height = (this.ActualHeight / rows),
                Width = (this.ActualWidth / cols),
                Fill = new ImageBrush(new BitmapImage(new Uri(@"resources\homer.jpg", UriKind.Relative))),
                //Stroke = Brushes.Black,
                //StrokeThickness = 2
            };

            Canvas.SetTop(recInit, curPos.Row * recInit.Height);
            Canvas.SetLeft(recInit, curPos.Col * recInit.Width);
            myCanvas.Children.Add(recInit);

            Rectangle recGoal = new Rectangle()
            {
                Height = (this.ActualHeight / rows),
                Width = (this.ActualWidth / cols),
                Fill = new ImageBrush(new BitmapImage(new Uri(@"resources\donut.jpg", UriKind.Relative))),
                //Stroke = Brushes.Black,
                //StrokeThickness = 2
            };

            Canvas.SetTop(recGoal, goalPos.Row * recGoal.Height);
            Canvas.SetLeft(recGoal, goalPos.Col * recGoal.Width);
            myCanvas.Children.Add(recGoal);

        }
    }
}
