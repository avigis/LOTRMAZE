using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wpfs.ViewModels;

namespace Wpfs.Views
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private MultiPlayerViewModel vm;

        private Thread t;
        CancellationTokenSource cts;
        CancellationToken token;
        private Task task;
        private bool gameEnded;

        public MultiPlayerWindow(MultiPlayerViewModel vm)
        {
            ////////////////////////////////////////////////////////////////////////////////
            InitializeComponent();
            this.vm = vm;
            this.DataContext = vm;
            gameEnded = false;

            //t = new Thread(() => RepeatDraw(this));
            //t.SetApartmentState(ApartmentState.STA);

            //t.Start();
            //cts = new CancellationTokenSource();
            //token = cts.Token;
            //task = Task.Factory.StartNew(RepeatDraw, token);
            //task = new Task(RepeatDraw);
            //task.Start();
            //BackToMainAfterGameEnded();
            vm.PropertyChanged += MyPropertyChangedEventHandler;
            //vm.NotifyPropertyChanged();
        }
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////
        /// </summary>

        private void MyPropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => {
                myCanvasPlayer1.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos1, vm.VM_GoalPos, 1);
            myCanvasPlayer2.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos2, vm.VM_GoalPos, 2);

            if (vm.VM_CurPos1.Equals(vm.VM_GoalPos) || vm.VM_CurPos2.Equals(vm.VM_GoalPos))
            {
                if (vm.VM_CurPos1.Equals(vm.VM_GoalPos))
                {
                    MessageBoxResult result = MessageBox.Show("You Won! (:", "Reached destination");
                }
                else if (vm.VM_CurPos2.Equals(vm.VM_GoalPos))
                {
                    MessageBoxResult result = MessageBox.Show("You Lost :'(", "Reached destination");
                }

                MainWindow mainWin = new MainWindow();
                mainWin.Show();
                this.Hide();
            }
            }

                ));
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to leave?", "Main menu", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MainWindow mainWin = new MainWindow();
                    mainWin.Show();
                    this.Hide();
                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

        private void MyCanvasMulti_Loaded(object sender, RoutedEventArgs e)
        {
            myCanvasPlayer1.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos1, vm.VM_GoalPos, 1);
            myCanvasPlayer2.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos2, vm.VM_GoalPos, 2);
        }


        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            int curRow = vm.VM_CurPos1.Row;
            int curCol = vm.VM_CurPos1.Col;

            if (e.Key == Key.Up && curRow > 0 && vm.VM_Maze[curRow - 1, curCol] == CellType.Free)
            {
                vm.Model.MoveUp();
            }
            else if (e.Key == Key.Down && curRow < vm.VM_MazeRows - 1 && vm.VM_Maze[curRow + 1, curCol] == CellType.Free)
            {
                vm.Model.MoveDown();
            }
            else if (e.Key == Key.Left && curCol > 0 && vm.VM_Maze[curRow, curCol - 1] == CellType.Free)
            {
                vm.Model.MoveLeft();
            }
            else if (e.Key == Key.Right && curCol < vm.VM_MazeCols - 1 && vm.VM_Maze[curRow, curCol + 1] == CellType.Free)
            {
                vm.Model.MoveRight();
            }

            //myCanvasPlayer1.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos1, vm.VM_GoalPos, 1);
            //myCanvasPlayer2.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos2, vm.VM_GoalPos, 2);
        }

        public void RepeatDraw(MultiPlayerWindow parentWin)
        {
            while (!(vm.VM_CurPos1.Equals(vm.VM_GoalPos)) && !(vm.VM_CurPos2.Equals(vm.VM_GoalPos)))
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => {

                    myCanvasPlayer1.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos1, vm.VM_GoalPos, 1);
                    myCanvasPlayer2.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos2, vm.VM_GoalPos, 2);
                    Thread.Sleep(50);
                }

                ));

            }
            if (vm.VM_CurPos1.Equals(vm.VM_GoalPos)) {
                MessageBoxResult result = MessageBox.Show("You Won! (:", "Reached destination");
            } else if (vm.VM_CurPos2.Equals(vm.VM_GoalPos))
            {
                MessageBoxResult result = MessageBox.Show("You Lost :'(", "Reached destination");
            }

            //task.Dispose();
            //cts.Dispose();
            //t.Abort();
            gameEnded = true;
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            parentWin.Hide();
        }
    }
}
