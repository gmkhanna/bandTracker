using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            //Shows --- Index Page
            Get["/"] = _ => {
                return View["index.cshtml"];
            };


            //BANDS --- Shows --- Entire Band List Page
            Get["/bands"] = _ => {
                List<Band> BandList = Band.GetAll();
                return View["bands_all.cshtml", BandList];
            };
            // BAND --- Adds --- Band to Band Added Page (popUp)
            Post["/bands"] = _ => {
                Band newBand = new Band(Request.Form["band-add"]);
                newBand.Save();
                List<Band> BandList = Band.GetAll();
                return View["bands_all.cshtml", BandList];
            };


            //VENUES --- Shows --- Entire Venue List Page
            Get["/venues"] = _ => {
                List<Venue> VenueList = Venue.GetAll();
                return View["venues_all.cshtml", VenueList];
            };
            // VENUE --- Adds --- Venue to Venue Added Page (popUp)
            Post["/venues"] = _ => {
                Venue newVenue = new Venue(Request.Form["venue-add"]);
                newVenue.Save();
                List<Venue> VenueList = Venue.GetAll();
                return View["venues_all.cshtml", VenueList];
            };
            // VENUE MANAGEMENT HERE -------------------------
            //VENUES --- Shows --- Venue Add Band / Edit/Delete Page
            Get["/venue/{id}/manage"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue SelectedVenue = Venue.Find(parameters.id);
                List<Band> VenueWBands = SelectedVenue.GetBands();
                List<Band> BandList = Band.GetAll();
                model.Add("venue", SelectedVenue);
                model.Add("venuesWBands", VenueWBands);
                model.Add("bandList", BandList);
                return View["venue_manage.cshtml", model];
            };

            // Venue - Add band to Venue
            Post["/venues/band-added"] = _ => {
                  Band band = Band.Find(Request.Form["band-id"]);
                  Venue venue = Venue.Find(Request.Form["venue-id"]);
                  venue.AddBand(band);
                  return View["success.cshtml"];
                };

                // Venue - edit venue
            Patch["/venue/{id}/edited"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                selectedVenue.Update(Request.Form["venue-name"]);
                return View["success.cshtml", selectedVenue];
            };

            // Venue - delete Venue
            Delete["/venue/{id}/deleted"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                selectedVenue.Delete();
                List<Venue> AllVenues = Venue.GetAll();
                return View["success.cshtml", AllVenues];
            };
        }
    }
}
