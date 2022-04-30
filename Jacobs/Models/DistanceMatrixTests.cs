using System;
using System.Threading;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
//using NUnit.Framework;
//using whatsapp.Models;
using System.Collections.Generic;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geolocation.Response;
using Newtonsoft.Json.Linq;
//using Json.Net;
using Newtonsoft.Json;
using System.Linq;
using Jacobs.Models;

namespace GoogleApi.Test.Maps.DistanceMatrix
{


    public class DistanceMatrixTests : BaseTest
    {

        public void test()
        {

            //List<string> homes = new List<string> { "עיט 6 עין שריד", "ישראל גלילי 7 תל אביב" };
            List<string>[] homes = getAddress( base.FindingPathslist);
            for (int i = 0; i < homes.GetLength(0); i++)
            {

                GetDistMatrixFullPath(homes[i]);



            }
        }

      
        public List<string>[] getAddress(List<FindingPaths> findingPathslist)

        {
            List<string>[] areasArr = new List<string>[4];
            List<string> north = new List<string>();
            List<string> south = new List<string>();
            List<string> center = new List<string>();
            List<string> jerusalem = new List<string>();
            areasArr[0] = north;
            areasArr[1] = center;
            areasArr[2] = south;
            areasArr[3] = jerusalem;
            foreach (FindingPaths obj in findingPathslist)
            {
                if (obj.DistributaionArea == "צפון")
                {
                    areasArr[0].Add(obj.Address);

                }

                else if (obj.DistributaionArea == "מרכז")
                {
                    areasArr[1].Add(obj.Address);

                }

                else if (obj.DistributaionArea == "דרום")
                {
                    areasArr[2].Add(obj.Address);
                }

                else //optin in jerusalem
                {
                    areasArr[3].Add(obj.Address);

                }

            }
            areasArr[0].Prepend("גשר העץ 27,עמק חפר");
            areasArr[1].Prepend("גשר העץ 27,עמק חפר");
            areasArr[2].Prepend("גשר העץ 27,עמק חפר");
            areasArr[3].Prepend("גשר העץ 27,עמק חפר");
            return areasArr;

        }

        Address destination;
        public Dictionary<string, Path> GetDistMatrixFullPath(List<string> homes)
        {
            Address origin;
            




            List<FindingPaths> homeAddresses = new List<FindingPaths>();
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

                Destinations = new[]
                {
                    new LocationEx(destination),
                }
            };

            DistanceMatrixResponse firstLag = GoogleMaps.DistanceMatrix.Query(request1);
            Parent firstLagC = JsonConvert.DeserializeObject<Parent>(firstLag.RawJson);





            Dictionary<string, Path> pathList = new Dictionary<string, Path>();
            //List<Path> pathList = new List<Path>();

            for (int i = 0; i < firstLagC.rows.Length; i++)
            {
                int distance = firstLagC.rows[i].elements[0].distance.value;
                int duration = firstLagC.rows[i].elements[0].duration.value;
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
        public string getDistance()
        {
            DistanceMatrixTests dm = new DistanceMatrixTests();
            dm.Setup();
            dm.test();

            return "ok";
        }
    }

    
}