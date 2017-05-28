using MazeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;

namespace Wpfs.Models
{
    public class MultiPlayerModel
    {
        public MultiPlayerModel(/*string name, int rows, int cols*/)
        {
            client1 = new MyTelnetClient();
            //client1.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            //string startCommand = "start " + name + " " + rows + " " + cols;
            //client1.Write(startCommand);
            //string mazeString = client1.Read();
            //maze = Maze.FromJSON(mazeString);

            //initialPos = maze.InitialPos;
            //goalPos = maze.GoalPos;
            //curPos1 = initialPos;
            //MazeName = maze.Name;
            //MazeCols = maze.Cols;
            //MazeRows = maze.Rows;
            client1.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            GetGameList(client1);

            //new Task(ListenToServer).Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
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

        private MyTelnetClient client1;
        public MyTelnetClient Client1
        {
            get { return client1; }
            set
            {
                client1 = value;
                NotifyPropertyChanged("Client1");
            }
        }

        //private MyTelnetClient client2;
        //public MyTelnetClient Client2
        //{
        //    get { return client2; }
        //    set
        //    {
        //        client2 = value;
        //        NotifyPropertyChanged("Client2");
        //    }
        //}

        private string mazeName;
        public string MazeName
        {
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

        
        private Position curPos1;
        public Position CurPos1
        {
            get { return this.curPos1; }
            set
            {
                if (MazeLoc(value) == 1)
                {
                    curPos1 = value;
                    NotifyPropertyChanged("CurPos1");
                }
            }
        }

        private Position curPos2;
        public Position CurPos2
        {
            get { return this.curPos2; }
            set
            {
                if (MazeLoc(value) == 1)
                {
                    curPos2 = value;
                    NotifyPropertyChanged("CurPos2");
                }
            }
        }

        private bool secondClientClosed;
        public bool SecondClientClosed
        {
            get { return this.secondClientClosed; }
            set
            {
                secondClientClosed = value;
                NotifyPropertyChanged("SecondClientClosed");
            }
        }

        private ObservableCollection<string> gamesList;  
        public ObservableCollection<string> GamesList
        {
            get { return this.gamesList; }
            set
            {
                gamesList = value;
                NotifyPropertyChanged("GamesList");
            }
        }

        public void MoveLeft()
        {
            this.CurPos1 = new Position(curPos1.Row, curPos1.Col - 1);
            client1.Write("play left");
        }

        public void MoveRight()
        {
            this.CurPos1 = new Position(curPos1.Row, curPos1.Col + 1);
            client1.Write("play right");
        }

        public void MoveUp()
        {
            this.CurPos1 = new Position(curPos1.Row - 1, curPos1.Col);
            client1.Write("play up");
        }

        public void MoveDown()
        {
            this.CurPos1 = new Position(curPos1.Row + 1, curPos1.Col);
            client1.Write("play down");
        }

        public int MazeLoc(Position pos)
        {
            if (pos.Row < 0 || pos.Col < 0 || pos.Row >= maze.Cols || pos.Row >= maze.Rows )
                return 0;
            return 1;
        }

        public void ListenToServer()
        {
            while (client1.IsConnected)
            {
                string msgFromServer = client1.Read();
                Client2Moved(msgFromServer);
            }
        }

        public void Client2Moved(string msg)
        {
            if (msg.Contains("up"))
            {
                this.CurPos2 = new Position(curPos2.Row - 1, curPos2.Col);
            }
            else if (msg.Contains("down"))
            {
                this.CurPos2 = new Position(curPos2.Row + 1, curPos2.Col);
            }
            else if (msg.Contains("left"))
            {
                this.CurPos2 = new Position(curPos2.Row, curPos2.Col - 1);
            }
            else if (msg.Contains("right"))
            {
                this.CurPos2 = new Position(curPos2.Row, curPos2.Col + 1);
            }
            else {
                //char[] separators = { ',', '[', ']', '"', '\r', '\n', ' ', };
                //foreach (char c in separators)
                //{
                //    msg = msg.Replace(c, '\0');
                //}
                    if (msg.Equals("{}\r\n"))
                    {
                        this.SecondClientClosed = true;
                    }
        }

            //vm.OtherPlayerMoved();
        }

        public void Start()
        {
            string startCommand = "start " + this.MazeName + " " + this.MazeRows + " " + this.MazeCols;
            this.client1.Write(startCommand);

            string mazeString = client1.Read();
            maze = Maze.FromJSON(mazeString);

            initialPos = maze.InitialPos;
            goalPos = maze.GoalPos;
            curPos1 = initialPos;
            curPos2 = initialPos;
            MazeName = maze.Name;
            MazeCols = maze.Cols;
            MazeRows = maze.Rows;
            secondClientClosed = false;

            var thread = new Thread(new ThreadStart(ListenToServer));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        public void Join(string mymazeName)
        {
            client1 = new MyTelnetClient();
            client1.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            string joinCommand = "join " + mymazeName;
            client1.Write(joinCommand);
            string mazeString = client1.Read();
            maze = Maze.FromJSON(mazeString);

            initialPos = maze.InitialPos;
            goalPos = maze.GoalPos;
            curPos1 = initialPos;
            curPos2 = initialPos;
            MazeName = maze.Name;
            MazeCols = maze.Cols;
            MazeRows = maze.Rows;
            secondClientClosed = false;


            var thread = new Thread(new ThreadStart(ListenToServer));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        public void GetGameList(MyTelnetClient client)
        {
            char[] separators = { ',', '[', ']','"','\r','\n',' ',};
            client.Write("list");
            string strList = client.Read();
            string[] tokens = strList.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            GamesList = new ObservableCollection<string>(tokens);
        }
    }
}
