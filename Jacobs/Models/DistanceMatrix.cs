using System;
using System.Threading;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
//using NUnit.Framework;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geolocation.Response;
using Newtonsoft.Json.Linq;
//using Json.Net;
using Newtonsoft.Json;
using Jacobs.Models.DAL;
using Jacobs.Models;

namespace GoogleApi.Test.Maps.DistanceMatrix
{
    
    
    public class DistanceMatrix : BaseTest
    {
        
        
        int idFrom;
        int idTo;
        string from;
        string to;
        int distance;
        string area;

        public int IdFrom { get => idFrom; set => idFrom = value; }
        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public int Distance { get => distance; set => distance = value; }
        public int IdTo { get => idTo; set => idTo = value; }
        public string Area { get => area; set => area = value; }

        public DistanceMatrix() { }

        public DistanceMatrix(string from, string to, int distance, int idFrom,int idTo,string area)
        {
            this.IdFrom = idFrom;
            this.From = from;
            this.To = to;
            this.Distance = distance;
            this.IdTo = idTo;
            this.Area = area;
        }

        public int Insert(List<DistanceMatrix> final)//insert distance matrix to db
        {
            DistanceMatrixDataServices ds = new DistanceMatrixDataServices();
            int status = ds.Insert(this, final);
            return status;
        }

        public List<DistanceMatrix> Read(string area)
        {
            DistanceMatrixDataServices ds = new DistanceMatrixDataServices();
            List<DistanceMatrix> DistanceMatrixlist = ds.Read(area);
            return DistanceMatrixlist;
        }

       

        public List<DistanceMatrix> test(Dictionary<int,string> list,string area)
        {

            return GetDistMatrixFullPath(list,area);
        }


        public List<DistanceMatrix> GetDistMatrixFullPath(Dictionary<int, string> list,string area)
        {

            List<Address> companyAddresses = new List<Address>();
            List<LocationEx> companyLocations = new List<LocationEx>();
       
            list.Add(1, "גשר העץ 27,עמק חפר");

            foreach (KeyValuePair<int, string> ele1 in list)
            {
                Address address = new Address(ele1.Value);
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
                    int countFrom = 0;
                    int idFrom = 0;
                    int countTo = 0;
                    int idTo = 0;
                    foreach (KeyValuePair<int, string> ele1 in list)
                    {

                        if (countFrom == i )
                        {
                            idFrom = ele1.Key;
                        }
                        countFrom++;

                        if (countTo == j)
                        {
                            idTo = ele1.Key;
                        }
                        countTo++;

                        if (idFrom != 0 && idTo != 0)
                        {
                            int distance = firstLagC.rows[i].elements[j].distance.value;

                            alldistanceMatrixArea.Add(new DistanceMatrix(firstLagC.destination_addresses[i], firstLagC.destination_addresses[j], distance, idFrom,idTo,area));

                            int duration = firstLagC.rows[i].elements[j].duration.value;
                            //pathList.Add(companies[i], new Path(distance, duration))

                            idFrom = 0;
                            idTo = 0;
                        }
                    }
                  


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