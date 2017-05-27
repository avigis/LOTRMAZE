using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpfs.ViewModels;
using MazeLib;
using MazeGeneratorLib;

namespace Wpfs.Models
{
    public class SinglePlayerModel
    {

        public SinglePlayerModel(string name, int rows, int cols)
        {
            client = new MyTelnetClient();
            client.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            string generateCommand = "generate " + name + " " + rows + " " + cols;
            client.Write(generateCommand);
            string mazeString = client.Read();
            maze = Maze.FromJSON(mazeString);
            
            initialPos = maze.InitialPos;
            goalPos = maze.GoalPos;
            curPos = initialPos;
            MazeName = maze.Name;
            MazeCols = maze.Cols;
            MazeRows = maze.Rows;
            
        }

        private Maze maze;
        public Maze Maze
        {
            get { return maze; }
            set
            {
                maze = value;
                NotifyPropertyChanged("Maze");
            }
        }


        private MyTelnetClient client;
        public MyTelnetClient Client
        {
            get { return client; }
            set
            {
                client = value;
                NotifyPropertyChanged("Client");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            //this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string mazeName;
        public string MazeName {
            get { return mazeName; }
            set
            {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        private int mazeRows;
        public int MazeRows
        {
            get { return mazeRows; }
            set
            {
                mazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        private int mazeCols;
        public int MazeCols
        {
            get { return mazeCols; }
            set
            {
                mazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        private int searchAlgorithm;
        public int SearchAlgorithm
        {
            get { return searchAlgorithm; }
            set
            {
                searchAlgorithm = value;
                NotifyPropertyChanged("searchAlgorithm");
            }
        }

        private Position initialPos;
        public Position InitialPos
        {
            get { return this.initialPos; }
            set
            {
                initialPos = value;
                NotifyPropertyChanged("InitialPos");
            }
        }

        private Position goalPos;
        public Position GoalPos
        {
            get { return this.goalPos; }
            set
            {
                goalPos = value;
                NotifyPropertyChanged("GoalPos");
            }
        }

        private Position curPos;
        public Position CurPos
        {
            get { return this.curPos; }
            set
            {
                curPos = value;
                NotifyPropertyChanged("CurPos");
            }
        }

        public string Solve(string mazeName, int searcher)
        {
            //this.client.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            string solveCommand = "solve " + mazeName + " " + searcher;
            client.Write(solveCommand);
            string solution = client.Read();
            solution = SolutionDetailsToSolutionPath(solution);
            return solution;
        }

        public string SolutionDetailsToSolutionPath(string detailedSol)
        {
            int i = 0;
            string sol = "";
            while(detailedSol[i] != '0' && detailedSol[i] != '1' 
                && detailedSol[i] != '2' && detailedSol[i] != '3')
            {
                i++;
            }

            while (detailedSol[i] == '0' || detailedSol[i] == '1'
                || detailedSol[i] == '2' || detailedSol[i] == '3')
            {
                sol += detailedSol[i];
                i++;
            }
            return sol;
        }

        public bool ReachedDestination()
        {
            return curPos.Equals(goalPos);
        }

        public void SaveSettings()
        {
            //// 
        }
    }
}
