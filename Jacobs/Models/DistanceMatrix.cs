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
        int id;
        string from;
        string to;
        int distance;
        public int Id { get => id; set => id = value; }

        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public int Distance { get => distance; set => distance = value; }

        public DistanceMatrix() { }

        public DistanceMatrix(string from, string to, int distance,int id)
        {
            this.Id = id;
            this.From = from;
            this.To = to;
            this.Distance = distance;
        }

        public int Insert(List<DistanceMatrix> list)//insert distance matrix to db
        {
            DistanceMatrixDataServices ds = new DistanceMatrixDataServices();
            int status = ds.Insert(list);
            return status;
        }

        public List<DistanceMatrix> Read(string area)
        {
            DistanceMatrixDataServices ds = new DistanceMatrixDataServices();
            List<DistanceMatrix> DistanceMatrixlist = ds.Read(area);
            return DistanceMatrixlist;
        }

       

        public List<DistanceMatrix> test(List<string> list)
        {

            return GetDistMatrixFullPath(list);
        }


        public List<DistanceMatrix> GetDistMatrixFullPath(List<string> list)
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
           
         
            List<DistanceMatrix> alldistanceMatrixArea = new List<DistanceMatrix>();
            DistanceMatrix dm = new DistanceMatrix();


            for (int i = 0; i < firstLagC.rows.Length; i++)
            {

                for (int j = 0; j < firstLagC.rows.Length; j++)
                {
                    int distance = firstLagC.rows[i].elements[j].distance.value;

                    alldistanceMatrixArea.Add(new DistanceMatrix(firstLagC.destination_addresses[i], firstLagC.destination_addresses[j], distance,dm.Id ));

                    int duration = firstLagC.rows[i].elements[j].duration.value;
                    //pathList.Add(companies[i], new Path(distance, duration));
                  


                }
            }

            return alldistanceMatrixArea;

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