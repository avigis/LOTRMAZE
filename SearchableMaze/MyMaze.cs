using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace SearchableMaze
{   /// <summary>
    /// Adapter of maze
    /// </summary>
    public class MyMaze : ISearchable<Position>
    {
        private Maze maze;

        /// <summary>
        /// constructor of adapter
        /// </summary>
        /// <param name="omaze"></param> adptee maze
        public MyMaze(Maze omaze)
        {
            this.maze = omaze;
        }
        /// <summary>
        /// check if the params is in the maze
        /// </summary>
        /// <param name="x">rows index</param>
        /// <param name="y">colums index</param>
        /// <returns> true if the params is in the maze</returns>
        delegate bool isInsideMaze(int x, int y);

        /// <summary>
        /// returnrs list of neighbors of the states.
        /// </summary>
        /// <param name="s"> current state</param>
        /// <returns>list of states - neighbors of the current state</returns>
        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            List<Position> neighbors = new List<Position>();
            int currentRow = s.state.Row;
            int currentCol = s.state.Col;

            neighbors.Add(new Position(currentRow, currentCol + 1));
            neighbors.Add(new Position(currentRow + 1, currentCol));
            neighbors.Add(new Position(currentRow, currentCol - 1));
            neighbors.Add(new Position(currentRow - 1, currentCol));

            List<Position> filteredNeighbors = new List<Position>();
            filteredNeighbors = neighbors.Where(n => n.Col >= 0 && n.Row >= 0
            && n.Row < maze.Rows && n.Col < maze.Cols && maze[n.Row, n.Col] == CellType.Free).ToList();

            List <State<Position>> returnedPossibilities = new List<State<Position>>();
            foreach (Position pos in filteredNeighbors)
            {
                State<Position> state = new State<Position>(pos);
                state.cost = s.cost + 1;
                state.cameFrom = s;

                returnedPossibilities.Add(state);
            }

            return returnedPossibilities;
        }
        /// <summary>
        /// return the initial state
        /// </summary>
        /// <returns>the initial state</returns>
        public State<Position> getGoalState()
        {
            State<Position> goalPos = new State<Position>(maze.GoalPos);
            return goalPos;
        }
        /// <summary>
        /// return the goal state
        /// </summary>
        /// <returns>the goal state</returns>
        public State<Position> getInitialState()
        {
            State<Position> initPos = new State<Position>(maze.InitialPos);
            return initPos;
        }
    }
}
