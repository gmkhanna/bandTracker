using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class VenueTest : IDisposable
    {
        public VenueTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_SaveAssignsIdToObject()
        {
            //Arrange
            Venue firstVenue = new Venue("Salmon");
            firstVenue.Save();

            //Act
            Venue savedVenue = Venue.GetAll()[0];

            int result = savedVenue.GetId();
            int testId = firstVenue.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
       public void Test_FindFindsVenueInDatabase()
       {
           //Arrange
           Venue firstVenue = new Venue("Salmon");
           firstVenue.Save();

           //Act
           Venue result = Venue.Find(firstVenue.GetId());

           //Assert
           Assert.Equal(firstVenue, result);
       }

       [Fact]
        public void Test_AddBand_AddsBandToVenue()
        {
            //Arrange
            Venue testVenue = new Venue("Cameo");
            testVenue.Save();

            Band testBand = new Band("Yoshis");
            testBand.Save();
            Band testBand2 = new Band("House of Blues");
            testBand2.Save();

            //Act
            testVenue.AddBand(testBand);
            testVenue.AddBand(testBand2);

            List<Band> result = testVenue.GetBands();
            List<Band> testList = new List<Band>{testBand, testBand2};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
         public void Test_GetBands_RetrievesBandFromVenue()
         {
             //Arrange
             Venue testVenue = new Venue("Salmon");
             testVenue.Save();

             Band testBand = new Band("Soup");
             testBand.Save();

             //Act
             testVenue.AddBand(testBand);

             List<Band> result = testVenue.GetBands();
             List<Band> testList = new List<Band>{testBand};

             //Assert
             Assert.Equal(testList, result);
         }


        public void Dispose()
        {
            Venue.DeleteAll();
            // Band.DeleteAll();
        }
    }
}
