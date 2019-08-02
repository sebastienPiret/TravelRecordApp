using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelRecordApp.Helpers;
using Xamarin.Forms;

namespace TravelRecordApp.Model
{
    /**
     * Classes generated from the requested foursquare json from VenueLogic classe.
     * The json text are translated into c# classes with the help of https://www.jsonutils.com/
     */
    public class Location
    {
        public string address { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int distance { get; set; }
        
        public string cc { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public IList<string> formattedAddress { get; set; }
        public string postalCode { get; set; }
        public string crossStreet { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }
        public bool primary { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public IList<Category> categories { get; set; }

        /**
         * async task to make http request to foursquare, in order to get list venues
         * @ venues the list of venue
         */
        public static async Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            List<Venue> venues = new List<Venue>();

            var url = VenueRoot.GenerateUrl(latitude, longitude);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
                venues = venueRoot.response.venues as List<Venue>;
            }

            return venues;
        }
    }

    public class Response
    {
        public IList<Venue> venues { get; set; }
    }

    /**
     * Main class to use in the application
     */
    public class VenueRoot
    {
        /**
         * The principal parameters that we want from the original json gotten from foursquare.
         * from this parameter come all the above classes
         */
        public Response response { get; set; }

        /**
         *  simple class to generate url from gps coord and credential from foursquare API
         */
        public static string GenerateUrl(double latitude, double longitude)
        {
            return string.Format(Constants.VENUE_SEARCH, latitude, longitude, Constants.CLIENT_ID,
                Constants.CLIENT_SECRET,DateTime.Now.ToString("yyyyMMdd"));
        }
        
        
    }
}
