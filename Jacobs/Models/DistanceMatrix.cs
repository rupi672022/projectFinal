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
using Jacobs.Models.DAL;


namespace GoogleApi.Test.Maps.DistanceMatrix
{


    public class DistanceMatrix : BaseTest
    {
        //public int Insert()//insert new company
        //{
        //    TestDataServices ds = new TestDataServices();
        //    int status = ds.Insert(this);
        //    return status;
        //}

        public List<DistanceMatrix> Read( string Area)//get the all company
        {
            DistanceMatrixDataServices ds = new DistanceMatrixDataServices();
            List<DistanceMatrix> DistanceMatrixlist = ds.Read(Area);
            return DistanceMatrixlist;
        }

        public void test()
        {
           
            List<string> companies = new List<string> { "גשר העץ 27,עמק חפר","עיט 6 עין שריד", "ישראל גלילי 7 תל אביב" };
            double[,] northMatrixRes=GetDistMatrixFullPath(companies);
        }


        public double [,] GetDistMatrixFullPath(List<string> companies)
        { 

            List<Address> companyAddresses = new List<Address>();
            List<LocationEx> companyLocations = new List<LocationEx>();
            foreach (string company in companies)
            {
                Address address = new Address(company);
                companyLocations.Add(new LocationEx(address));
            }

            var request1 = new DistanceMatrixRequest
            {
                Key = this.ApiKey,
                Origins = companyLocations,
                Destinations = companyLocations
            };

            DistanceMatrixResponse firstLag = GoogleMaps.DistanceMatrix.Query(request1);
            Parent firstLagC = JsonConvert.DeserializeObject<Parent>(firstLag.RawJson);

            
            //Dictionary<string, Path> pathList = new Dictionary<string, Path>();
            double[,] matrixNorth = new double[firstLagC.rows.Length, firstLagC.rows.Length];
            int k = 0, m = 0;
            for (int i = 0; i < firstLagC.rows.Length; i++)
            {

                for (int j = 0; j < firstLagC.rows.Length; j++)
                {


                    int distance = firstLagC.rows[i].elements[j].distance.value;
                    int duration = firstLagC.rows[i].elements[j].duration.value;
                    //pathList.Add(companies[i], new Path(distance, duration));
                    matrixNorth[k, m] = distance;
                    m++;
                    //m=עמודה,K=שורה
                    if (m == firstLagC.origin_addresses.Length)
                    {
                        k++;
                        m = 0;
                    }


                }
            }

            return matrixNorth;

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