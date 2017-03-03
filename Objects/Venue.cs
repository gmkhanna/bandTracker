using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }
    public override bool Equals(System.Object otherVenue)
    {
      if(!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = this.GetId() == newVenue.GetId();
        bool nameEquality = this.GetName() == newVenue.GetName();
        return (idEquality && nameEquality);
      }
    }

    public static List<Venue> GetAll()
    {
      List<Venue> AllVenues = new List<Venue> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);

        Venue newVenue = new Venue(venueName, venueId);
        AllVenues.Add(newVenue);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return AllVenues;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName)", conn);

      SqlParameter nameParam = new SqlParameter("@VenueName", this.GetName());
      cmd.Parameters.Add(nameParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId", conn);

      SqlParameter venueIdParameter = new SqlParameter("@VenueId", id.ToString());

      cmd.Parameters.Add(venueIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundVenue;
    }

    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();


      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (venue_id, band_id) VALUES (@VenueId, @BandId)", conn);

      SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetId());
      SqlParameter bandIdParameter = new SqlParameter("@BandId", newBand.GetId());

      cmd.Parameters.Add(venueIdParameter);
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null);
      {
        conn.Close();
      }
    }

    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @venueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetId().ToString());

      cmd.Parameters.Add(venueIdParameter);

      List<Band> bandList = new List<Band> {};

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
          int bandId = rdr.GetInt32(0);
          string bandName = rdr.GetString(1);
          Band newBand = new Band(bandName, bandId);
          bandList.Add(newBand);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return bandList;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName OUTPUT INSERTED.name WHERE id = @VenueId;", conn);

      SqlParameter newNameParameter = new SqlParameter("@NewName", newName);
      cmd.Parameters.Add(newNameParameter);

      SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetId());
      cmd.Parameters.Add(venueIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    // public static List<Venue> SearchedIngredient(string queryIngredient)
    // {
    //   List<Venue> MatchedVenues = new List<Venue> {};
    //   string[] stringArray;
    //
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   while(rdr.Read())
    //   {
    //     int venueId = rdr.GetInt32(0);
    //     string venueName = rdr.GetString(1);
    //     string venueIngredients = rdr.GetString(2);
    //     string venueInstructions = rdr.GetString(3);
    //     string rating = rdr.GetString(4);
    //
    //     stringArray = venueIngredients.Split();
    //
    //     for (int i = 0; i < stringArray.Length; i++)
    //     {
    //       if (stringArray[i] == queryIngredient)
    //       {
    //       Venue searchedIngredient = new Venue(venueName, venueIngredients, venueInstructions, rating, venueId);
    //       MatchedVenues.Add(searchedIngredient);
    //       }
    //     }
    //   }
    //
    //   if(rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if(conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return MatchedVenues;
    // }


    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd= new SqlCommand("DELETE FROM venues WHERE id=@VenueId", conn);
      SqlParameter idParameter = new SqlParameter("@VenueId", this.GetId());
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
  }
}
