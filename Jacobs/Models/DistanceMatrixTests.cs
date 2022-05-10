using System;
using System.Threading;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
//using NUnit.Framework;

using System.Collections.Generic;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geolocation.Response;
using Newtonsoft.Json.Linq;
//using Json.Net;
using Newtonsoft.Json;

namespace GoogleApi.Test.Maps.DistanceMatrix
{


    public class DistanceMatrixTests : BaseTest
    {

        public void test()
        {

            List<string> homes = new List<string> { "גשר העץ 27,עמק חפר","עיט 6 עין שריד", "ישראל גלילי 7 תל אביב" };

            string checkpoint = "מחסום תרקומיא";
            string hospital = "בית החולים תל השומר";
            GetDistMatrixFullPath(homes, checkpoint, hospital, true);
        }


        public Dictionary<string, Path> GetDistMatrixFullPath(List<string> homes, string checkpoint, string hospital, bool direction)
        {
            Address origin;
            Address destination;


            if (direction)
            {
                // home -> checkpoint -> hospital
                destination = new Address(checkpoint);
                origin = new Address(hospital);
            }
            else
            {
                // home - >hospital -> checkpoint
                destination = new Address(hospital);
                origin = new Address(checkpoint);
            }

            List<Address> homeAddresses = new List<Address>();
            List<LocationEx> homeLocations = new List<LocationEx>();
            foreach (string home in homes)
            {
                Address address = new Address(home);
                homeLocations.Add(new LocationEx(address));
            }

            var request1 = new DistanceMatrixRequest
            {
                Key = this.ApiKey,
                Origins = homeLocations,
                //Destinations = new[]
                //{
                //    new LocationEx(destination),
                //}
                Destinations = homeLocations
            };

            DistanceMatrixResponse firstLag = GoogleMaps.DistanceMatrix.Query(request1);
            Parent firstLagC = JsonConvert.DeserializeObject<Parent>(firstLag.RawJson);

            //var request2 = new DistanceMatrixRequest
            //{
            //    Key = this.ApiKey,
            //    Origins = new[]
            //    {
            //        new LocationEx(destination),
            //    },
            //    Destinations = new[]
            //    {
            //        new LocationEx(origin),
            //    }
            //};

            //DistanceMatrixResponse secondLag = GoogleMaps.DistanceMatrix.Query(request2);
            //Parent secondLagC = JsonConvert.DeserializeObject<Parent>(secondLag.RawJson);

            //var request3 = new DistanceMatrixRequest
            //{
            //    Key = this.ApiKey,
            //    Origins = new[] {
            //        new LocationEx(origin),
            //    },
            //    Destinations = homeLocations
            //};

            //DistanceMatrixResponse thirdLag = GoogleMaps.DistanceMatrix.Query(request3);
            //Parent thirdLagC = JsonConvert.DeserializeObject<Parent>(thirdLag.RawJson);


            //Distance secondLagDistance = secondLagC.rows[0].elements[0].distance;
            //Duration secondLagDuration = secondLagC.rows[0].elements[0].duration;



            Dictionary<string, Path> pathList = new Dictionary<string, Path>();
            //List<Path> pathList = new List<Path>();

            for (int i = 0; i < firstLagC.rows.Length; i++)
            {
                //int distance = firstLagC.rows[i].elements[0].distance.value + secondLagDistance.value + thirdLagC.rows[0].elements[i].distance.value;
                int distance = firstLagC.rows[i].elements[0].distance.value;
                int duration = firstLagC.rows[i].elements[0].duration.value;
                //int duration = firstLagC.rows[i].elements[0].duration.value + secondLagDuration.value + thirdLagC.rows[0].elements[i].duration.value;
                pathList.Add(homes[i], new Path(distance, duration));
            }

            return pathList;

        }

    }
    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public Element[] elements { get; set; }
    }

    public class Parent
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string status { get; set; }
    }
}