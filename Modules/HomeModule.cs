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



            // // BAND --- Adds --- Band to Band Added Page (popUp)
            // Post["/band-added"] = _ => {
            //     return View["band_added_page.cshtml"];
            // };
            //
            // // VENUE --- Adds --- Venue to Venue Added Page (popUp)
            // Post["/venue-added"] = _ => {
            //     return View["venue_added_page.cshtml"];
            // };
            //
            //
            //
            // //BANDS --- Shows --- Entire Band List Page
            // Get["/bands"] = _ => {
            //     return View["all_bands.cshtml"];
            // };
            //
            // //BAND --- Shows --- Specific Band Details
            // Get["/band/{id}"] = parameters => {
            //     Request.Form[""]
            //     return View["specific_band.cshtml"];
            // };
            //
            //
            //
            //
            // //VENUE --- Shows --- Entire Venue List Page
            // Get["/venues"] = _ => {
            //     return View["all_venues.cshtml"];
            // };
            //
            // //VENUE --- Shows --- Specific Venue Details
            // Get["/venue/{id}"] = parameters => {
            //     Request.Form[""]
            //     return View["specific_band.cshtml"];
            // };
            //
            //
            //
            //
            // // BAND --- Adds --- Band to Band Added Page (popUp)
            // Post["/band-added"] = _ => {
            //     return View["band_added_page.cshtml"];
            // };
            //
            // // VENUE --- Adds --- Venue to Venue Added Page (popUp)
            // Post["/venue-added"] = _ => {
            //     return View["venue_added_page.cshtml"];
            // };

        }
    }
}
