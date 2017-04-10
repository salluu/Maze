using System;
using Excelian.Maze;
using Excelian.Maze.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Excelian.MazeTests
{
    [TestClass]
    public class ExplorerTests
    {
        private Explorer _explorer;
        private Maze.IMaze _maze;
        private Position position;
        private IMazeConsole _console;
        private IMapResources _mapResources;

        private readonly char[,] _mappedArray = new char[8, 4]
        {
            {'X', 'X', 'X', 'X'},
            {'S', ' ', 'X', 'X'},
            {'X', ' ', 'X', 'X'},
            {'X', ' ', ' ', 'X'},
            {'X', 'X', ' ', 'X'},
            {'X', ' ', ' ', 'X'},
            {'X', ' ', 'X', 'X'},
            {'X', 'F', 'X', 'X'}
        };


            


    [TestInitialize]
        public void TestInitialize()
        {
            _maze = Substitute.For<IMaze>();
            _console = Substitute.For<IMazeConsole>();
           
            position = new Position() { PositionX = 1, PositionY = 0};
           
            _maze.GetStartingPositionOfExplorer(Symbol.Start).Returns(position);
            _maze.CheckRange(0,0).ReturnsForAnyArgs(true);
            _maze.IsExit(2,1).ReturnsForAnyArgs(true);
            _maze.IsAWall(0,0).ReturnsForAnyArgs(false);
            _explorer = new Explorer(_maze, _console);
        }

        [TestMethod]
        public void Should_move_explorer_toars_right()
        {
            //Act
            _explorer.MoveTowards(Direction.Right);

            //Assert            
            Assert.AreEqual(_explorer.CurrentPosition.PositionX, 2);
            Assert.AreEqual(_explorer.CurrentPosition.PositionY, 0);

        }

        [TestMethod]
        public void Should_move_explorer_toars_left()
        {
            //Act
            _explorer.MoveTowards(Direction.Left);

            //Assert            
            Assert.AreEqual(_explorer.CurrentPosition.PositionX, 0);
            Assert.AreEqual(_explorer.CurrentPosition.PositionY, 0);

        }

        [TestMethod]
        public void Should_move_explorer_toars_Up()
        {
            //Act
            _explorer.MoveTowards(Direction.Down);
            _explorer.MoveTowards(Direction.Up);

            //Assert            
            Assert.AreEqual(_explorer.CurrentPosition.PositionX, 1);
            Assert.AreEqual(_explorer.CurrentPosition.PositionY, 0);

        }

        [TestMethod]
        public void Should_move_explorer_toars_Down()
        {
            //Act
            _explorer.MoveTowards(Direction.Down);

            //Assert            
            Assert.AreEqual(_explorer.CurrentPosition.PositionX, 1);
            Assert.AreEqual(_explorer.CurrentPosition.PositionY, 1);

        }

        [TestMethod]
        public void Should_set_finished_true_when_exit()
        {
            //Arrange
            _maze.IsExit(2, 1).Returns(true);
            _maze.IsAWall(0, 0).ReturnsForAnyArgs(false);
            _explorer = new Explorer(_maze, _console);


            //Act
            _explorer.MoveTowards(Direction.Right);
            _explorer.MoveTowards(Direction.Down);

            //Assert            
            Assert.IsTrue(_explorer.Finished);

        }

        [TestMethod]
        public void Should_explore_maze_automatically_returns_true()
        {
            //Arrnge
            var alreadySearched = new bool[8, 4];
            _maze.IsExit(2, 1).ReturnsForAnyArgs(false);
            _maze.IsAWall(2, 1).ReturnsForAnyArgs(true);
            _maze.IsExit(7, 1).Returns(true);
            _maze.IsAWall(1, 0).Returns(false);
            _maze.IsAWall(1, 1).Returns(false);
            _maze.IsAWall(2, 1).Returns(false);
            _maze.IsAWall(3, 1).Returns(false);
            _maze.IsAWall(3, 2).Returns(false);
            _maze.IsAWall(4, 2).Returns(false);
            _maze.IsAWall(5, 2).Returns(false);
            _maze.IsAWall(5, 1).Returns(false);
            _maze.IsAWall(6, 1).Returns(false);
            _maze.NumberOfColumns.Returns(4);
            _maze.NumberOfRows.Returns(8);

           
            //Act
            var result = _explorer.ExploreMaze(_explorer.CurrentPosition.PositionX, _explorer.CurrentPosition.PositionY, alreadySearched);

            //Assert            
            Assert.IsTrue(result );
         
        }
    }
}
