using Newtonsoft.Json; //added JSON.NET with Nuget
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Jacobs.Models.DAL;

namespace GeoCoding_Art
{
    class Program
    {
        
        const string apiKey = "AIzaSyDf2uzM7Zqj26c-j3jxJLTH0UWG_8vtbk0"; //paste your API KEY HERE 
        static string baseUrlGC = "https://maps.googleapis.com/maps/api/geocode/json?address="; // part1 of the URL for GeoCoding
        static string baseUrlRGC = "https://maps.googleapis.com/maps/api/geocode/json?latlng="; // part1 of the URL for Reverse GeoCoding
        static string plusUrl = "&key=" + apiKey + "&sensor=false"; // part2 of the URL

        static public int DisplayMenu() // I add a menu for selecting between 1 - GeoCoding / 2 - Reverse Geocoding / 3 - Exit
        {
            
            var result = Console.ReadLine(); //waiting for an integer input for the menu; value can between 1-3
            return Convert.ToInt32(result); //converting result to an integer for the menu
        }

        
        

        public string GeoCoding(string address)
        {
            var json = new WebClient().DownloadString(baseUrlGC + address.Replace(" ", "+")
                + plusUrl);//concatenate URL with the input address and downloads the requested resource
            GoogleGeoCodeResponse jsonResult = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(json); //deserializing the result to GoogleGeoCodeResponse

            string status = jsonResult.status; // get status 

            string geoLocation = String.Empty;

            if (status == "OK") //check if status is OK
            {
                for (int i = 0; i < jsonResult.results.Length; i++) //loop throught the result for lat/lng
                {
                    geoLocation += "" + jsonResult.results[i].geometry.location.lat +
                    "," + jsonResult.results[i].geometry.location.lng ; //append the result addresses to every new line
                }
                return geoLocation; //return result
            }
            else
            {
                return status; //return status / error if not OK
            }
        }

       
    }
}