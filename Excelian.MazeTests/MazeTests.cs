using System;
using Excelian.Maze;
using Excelian.Maze.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;


namespace Excelian.MazeTests
{
    [TestClass]
    public class MazeTests
    {

        private Maze.Maze _maze;
        private IMapResources _mapResources;


        [TestInitialize]
        public void TestInitialize()
        {
            _mapResources = Substitute.For<IMapResources>();
            var map = new Map()
            {
                Columns = 4,
                Rows = 8,
                MapDraw = "",
                MappedArray = new char[8, 4]{{'X', 'X','X','X' },
                                             { 'S', ' ','X','X' } ,
                                             { 'X', ' ','X','X' },
                                             { 'X', ' ',' ','X' },
                                             { 'X', 'X',' ','X' },
                                             { 'X', ' ',' ','X' },
                                             { 'X', ' ','X','X' },
                                             { 'X', 'F','X','X' }}


            };


            _mapResources.GetMap().Returns(map);
            _maze = new Maze.Maze(_mapResources);
        }


        [TestMethod]
        public void Should_return_true_when_locaotion_0_0()
        {
            //Act
            var result = _maze.CheckRange(0, 0);


            //Assert            
            Assert.IsTrue(result, "Its inside the range");
        }

        [TestMethod]
        public void Should_return_true_when_locaotion_is_on_maximum_points()
        {
            //Act
            var result = _maze.CheckRange(3, 2);


            //Assert            
            Assert.IsTrue(result, "Its inside the range");
        }


        [TestMethod]
        public void Should_return_true_when_locaotion_is_somewhere_in_middle()
        {
            //Act
            var result = _maze.CheckRange(2, 3);


            //Assert            
            Assert.IsTrue(result, "Its inside the range");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Should_throw_argument_exception_when_locaotion_is_below_minimum_range()
        {
            //Act
            var result = _maze.CheckRange(-1, 3);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Should_throw_argument_exception_when_locaotion_is_above_maximum_range()
        {
            //Act
            var result = _maze.CheckRange(15, 20);

        }

        [TestMethod]
        public void Should_return_true_when_locaotion_is_on_the_wall()
        {
            //Act
            var result = _maze.IsAWall(0, 0);

            //Assert            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_return_true_when_locaotion_is_on_the_Exit()
        {
            //Act
            var result = _maze.IsExit(1, 7);

            //Assert            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_get_starting_position_of_explorer()
        {
            //Act
            var result = _maze.GetStartingPositionOfExplorer(Symbol.Start);

            //Assert            
            Assert.IsTrue(result.PositionX==1);
            Assert.IsTrue(result.PositionY==0);
        }

        [TestMethod]
        public void Should_get_no_position_of_explorer_if_not_found()
        {
            //Act
            var result = _maze.GetStartingPositionOfExplorer("g");

            //Assert            
            Assert.IsTrue(result.PositionX == -1);
            Assert.IsTrue(result.PositionY == -1);
        }

       
    }
}
