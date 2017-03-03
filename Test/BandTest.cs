using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class BandTest : IDisposable
    {
        public BandTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_SaveAssignsIdToObject()
        {
            //Arrange
            Band firstBand = new Band("Cameo");
            firstBand.Save();

            //Act
            Band savedBand = Band.GetAll()[0];

            int result = savedBand.GetId();
            int testId = firstBand.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
       public void Test_Find_FindsBandInDatabase()
       {
           //Arrange
           Band firstBand = new Band("Cameo");
           firstBand.Save();

           //Act
           Band result = Band.Find(firstBand.GetId());

           //Assert
           Assert.Equal(firstBand, result);
       }

       [Fact]
       public void Test_AddVenue_AddsVenueToBand()
       {
           Band testBand = new Band("Cameo");
           testBand.Save();

           Venue testVenue1 = new Venue("Yoshis");
           testVenue1.Save();

           testBand.AddVenue(testVenue1);

           List<Venue> result = testBand.GetVenues();
           List<Venue> testList = new List<Venue>{testVenue1};

           Assert.Equal(result, testList);

       }

    //    [Fact]
    //     public void Test_AddBand_AddsBandToBand()
    //     {
    //         //Arrange
    //         Band testBand = new Band("Cameo");
    //         testBand.Save();
       //
    //         Venue testVenue = new Venue("Yoshis");
    //         testVenue.Save();
       //
    //         //Act
    //         testBand.AddVenue(testVenue);
       //
    //         List<Venue> result = testBand.GetVenues();
    //         List<Venue> testList = new List<Venue>{testVenue};
       //
    //         //Assert
    //         Assert.Equal(testList, result);
    //     }

        public void Dispose()
        {
            Band.DeleteAll();
            // Venue.DeleteAll();
        }
    }
}
