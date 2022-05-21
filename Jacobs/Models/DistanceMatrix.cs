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
        
        string addressMatrix;

        public DistanceMatrix()
        {

        }

        public int Insert()//insert new company
        {
            DistanceMatrixDataServices ds = new DistanceMatrixDataServices();
            int status = ds.Insert(this);
            return status;
        }

        public List<DistanceMatrix> Read(string area)
        {
            DistanceMatrixDataServices ds = new DistanceMatrixDataServices();
            List<DistanceMatrix> DistanceMatrixlist = ds.Read(area);
            return DistanceMatrixlist;
        }

        public string AddressMatrix { get => addressMatrix; set => addressMatrix = value; }

        public DistanceMatrix(string addressMatrix)
        {
            this.AddressMatrix = addressMatrix;
        }

        public void test(List<string> list)
        {

            // List<string> companies = new List<string> {  "גשר העץ 27,עמק חפר", "גשר העץ 27,עמק חפר", "גשר העץ 27,עמק חפר", "גשר העץ 27,עמק חפר", "גשר העץ 27,עמק חפר", "גשר העץ 27,עמק חפר", "עיט 6 עין*/ שריד", "ישראל גלילי 7 תל אביב", "ישראל גלילי 7 תל אביב", "ישראל גלילי 7 תל אביב" };
            List<string>[] northMatrixRes = GetDistMatrixFullPath(list);
        }


        public List<string>[] GetDistMatrixFullPath(List<string> list)
        {

            List<Address> companyAddresses = new List<Address>();
            List<LocationEx> companyLocations = new List<LocationEx>();
            foreach (string company in list)
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
           
            List<string> company1 = new List<string>();
            List<string> company2 = new List<string>();
            List<string> distances = new List<string>();
            List<string>[] fullTable = new List<string>[3];


            for (int i = 0; i < firstLagC.rows.Length; i++)
            {

                for (int j = 0; j < firstLagC.rows.Length; j++)
                {


                    string distance = firstLagC.rows[i].elements[j].distance.value.ToString();
                    company1.Add(firstLagC.destination_addresses[i]);
                    company2.Add(firstLagC.destination_addresses[j]);
                    distances.Add(distance);
                    fullTable[0] = company1;
                    fullTable[1] = company2;
                    fullTable[2] = distances;



                    int duration = firstLagC.rows[i].elements[j].duration.value;
                    //pathList.Add(companies[i], new Path(distance, duration));
                  


                }
            }

            return fullTable;

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