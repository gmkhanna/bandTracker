using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
    public class Band
    {
        private int _id;
        private string _name;

        public Band(string Name, int Id = 0)
        {
            _id = Id;
            _name = Name;
        }
        public override bool Equals(System.Object otherBand)
        {
            if(!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band) otherBand;
                bool idEquality = this.GetId() == newBand.GetId();
                bool nameEquality = this.GetName() == newBand.GetName();
                return (idEquality && nameEquality);
            }
        }

        public static List<Band> GetAll()
        {
            List<Band> AllCategories = new List<Band> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int bandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);

                Band newBand = new Band(bandName, bandId);
                AllCategories.Add(newBand);
            }

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return AllCategories;
        }
        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@Name)", conn);

            SqlParameter nameParameter = new SqlParameter("@Name", this.GetName());
            cmd.Parameters.Add(nameParameter);

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

        public static Band Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId", conn);

            SqlParameter bandIdParameter = new SqlParameter("@BandId", id.ToString());
            cmd.Parameters.Add(bandIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundBandId = 0;
            string foundBandName = null;

            while(rdr.Read())
            {
                foundBandId = rdr.GetInt32(0);
                foundBandName = rdr.GetString(1);
            }

            Band foundBand = new Band(foundBandName, foundBandId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundBand;
        }

        public void AddVenue(Venue newVenue)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (venue_id, band_id) VALUES (@VenueId, @BandId);", conn);

            SqlParameter venueIdParam = new SqlParameter("@VenueId", newVenue.GetId());
            SqlParameter bandIdParam = new SqlParameter("@BandId", this.GetId());

            cmd.Parameters.Add(venueIdParam);
            cmd.Parameters.Add(bandIdParam);

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public List<Venue> GetVenues()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands.id = @BandId;", conn);

            SqlParameter bandIdParam = new SqlParameter("@BandId", this.GetId().ToString());
            cmd.Parameters.Add(bandIdParam);

            List<Venue> venueList = new List<Venue> {};

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int venueId = rdr.GetInt32(0);
                string venueName = rdr.GetString(1);
                Venue newVenue = new Venue(venueName, venueId);
                venueList.Add(newVenue);
            }

                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
                return venueList;
        }

        //     public List<Venue> GetVenues()
        //     {
        //         SqlConnection conn = DB.Connection();
        //         conn.Open();
        //
        //         SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands.id = @BandId;", conn);
        //
        //         SqlParameter idBandParam = new SqlParameter("@BandId", this.GetId().ToString());
        //
        //         cmd.Parameters.Add(idBandParam);
        //
        //         List<Venue> venueList = new List<Venue> {};
        //
        //         SqlDataReader rdr = cmd.ExecuteReader();
        //
        //         while (rdr.Read())
        //         {
        //             int venueId = rdr.GetInt32(0);
        //             string venueName = rdr.GetString(1);
        //             Venue newVenue = new Venue(venueName, venueId);
        //             venueList.Add(newVenue);
        //         }
        //
        //         if (rdr != null)
        //         {
        //             rdr.Close();
        //         }
        //         if (conn != null)
        //         {
        //             conn.Close();
        //         }
        //         return venueList;
        //     }

            public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id=@BandId;", conn);

            SqlParameter idParameter = new SqlParameter("@BandId", this.GetId());
            cmd.Parameters.Add(idParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public void Update(string newName)
      {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @NewName OUTPUT INSERTED.name WHERE id = @BandId;", conn);

          SqlParameter newNameParameter = new SqlParameter();
          newNameParameter.ParameterName = "@NewName";
          newNameParameter.Value = newName;
          cmd.Parameters.Add(newNameParameter);

          SqlParameter bandIdParameter = new SqlParameter("@BandId", this.GetId());
          cmd.Parameters.Add(bandIdParameter);
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


        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
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
