﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Jacobs.Models.DAL;
using GoogleApi.Test.Maps.DistanceMatrix;
namespace Jacobs.Models
{
    public class FindingPaths
    {
        
       
      
        double[,] arrayDis;
        //array of distances
        int whichArea = 0;//help us to know which area selected
        string address;
        double lng;
        double lat;
        string fromCompany;
        string toCompany;
        string distributaionArea;
        string dateArrivel;
        int companyNum;
        string companyName;
      
        public List<FindingPaths> FindingPathslist;
        public List<FindingPaths> DistanceList;

        public FindingPaths() { }

        public FindingPaths(string address, double lng, double lat, string distributaionArea, string dateArrivel, int companyNum, string companyName)
        {
            this.address = address;
            this.lng = lng;
            this.lat = lat;
            this.distributaionArea = distributaionArea;
            this.dateArrivel = dateArrivel;
            this.companyNum = companyNum;
            this.companyName = companyName;


        }

        public FindingPaths(string fromCompany, string toCompany, int lat, int companyNum)
        {
            this.fromCompany = fromCompany;
            this.toCompany = toCompany;
            this.lat = lat;
            this.companyNum = companyNum;
        }

        public string Address { get => address; set => address = value; }
        public double Lng { get => lng; set => lng = value; }
        public double Lat { get => lat; set => lat = value; }
        public string DistributaionArea { get => distributaionArea; set => distributaionArea = value; }
        public string DateArrivel { get => dateArrivel; set => dateArrivel = value; }
        public int CompanyNum { get => companyNum; set => companyNum = value; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public string ToCompany { get => toCompany; set => toCompany = value; }
        public string FromCompany { get => fromCompany; set => fromCompany = value; }

        public List<FindingPaths> Read(string date)
        {
            FindingPathDataServices ds = new FindingPathDataServices();
              FindingPathslist = ds.Read(date);
            DistanceList = ds.ReadDistance(date);
            return Algo(FindingPathslist, DistanceList, date);
        }


        
        public List<FindingPaths> Algo(List<FindingPaths> list,List<FindingPaths> DistanceList, string date)
            //alogrithm of optimal path base on tsp
        {


            List<FindingPaths> selectedPath = new List<FindingPaths>();
            //lists with distrebiution area
            List<FindingPaths> north = new List<FindingPaths>();
            List<FindingPaths> south = new List<FindingPaths>();
            List<FindingPaths> center = new List<FindingPaths>();
            List<FindingPaths> jerusalem = new List<FindingPaths>();
            List<FindingPaths>[] areasArr = new List<FindingPaths>[4];
            //example of distances arrays
            double[,] arrayDis0 = new double[,] {  { 0, 54.6, 75.1, 53.9,129 },
                    {54.8, 0,25.5 , 7 ,101}, {  80.3, 25.6, 0, 27.3,53.6 }, { 54.2, 8.6,26.1, 0 ,98.1} , {127, 100,53 ,101,0}};
            double[,] arrayDis1 = new double[,] {  { 0, 50.3, 8.2, 17,15.2 },
                    {49, 0,56.8 , 43 ,37.2}, {  7.9, 58.2, 0, 20.6,22.1 }, { 14.7, 42.7, 17.5, 0 ,6.6} , {16.8, 38.3,20.9 ,7.1,0}};
            double[,] arrayDis2 = new double[,] {  { 0, 154, 125, 110 },
                    {154, 0,61.1 , 86}, { 95.1, 59.9, 0, 20.1 }, { 83.2, 78.6, 20.3, 0 } };
            double[,] arrayDis3 = new double[,] {  { 0, 114, 116, 115 },
                    {114, 0,5.1 ,4}, { 114, 5.1, 0, 2.8}, { 113, 4.4, 3.9, 0 } };
            areasArr[0] = north;
            areasArr[1] = center;
            areasArr[2] = south;
            areasArr[3] = jerusalem;

            foreach (FindingPaths obj in list)
            {
                //add address to lists

                if (obj.DistributaionArea == "צפון")
                {
                    areasArr[0].Add(obj);

                }

                else if (obj.DistributaionArea == "מרכז")
                {
                    areasArr[1].Add(obj);

                }

                else if (obj.DistributaionArea == "דרום")
                {
                    areasArr[2].Add(obj);
                }

                else //optin in jerusalem
                {
                    areasArr[3].Add(obj);

                }
            }

            for (int i = 0; i < areasArr.Length; i++)
            {
                if (areasArr[i].Count != 0)//if the area not empty from address
                {
                    fullfunc func = new fullfunc();
                    //function factorial calculate how much comabination has
                    int factRes = func.factorial(areasArr[i].Count);
                    //add +2 to save place to origin and destnation
                    string[,] result = new string[factRes, (areasArr[i].Count) + 2];
                 
                    List<String> listIncludeJacobs = new List<string>();
                    List<String> addressList = new List<string>();
                   

                    foreach (FindingPaths ans in areasArr[i])
                    {
                        addressList.Add(ans.Address);
                    }
                    listIncludeJacobs.Insert(0, "גשר העץ 27,עמק חפר");
                    listIncludeJacobs.AddRange(addressList);
                   string[] arrIncludeJacobs = listIncludeJacobs.ToArray();
                    string[] addressToArr = addressList.ToArray();
                    var resultDic = new Dictionary<string, int>();
                    if (whichArea == 0)
                    {
                        arrayDis = arrayDis0;
                        whichArea++;
                    }
                    //whichare include the number of select area
                    else if (whichArea == 1)
                    {
                        arrayDis = arrayDis1;
                        whichArea++;

                    }
                    else if (whichArea == 2)
                    {
                        arrayDis = arrayDis2;
                        whichArea++;
                    }
                    else
                        arrayDis = arrayDis3;
                    //function dic is a Dictionary for all the index in adress array
                    resultDic = func.Dic(arrIncludeJacobs);
                    //prnPermut functaion return all the combination of routes 
                    func.prnPermut(addressToArr, 0, addressToArr.Length - 1, result);
                    Console.Write("\n\n");



                    
                    var listAlgo = func.Dis(resultDic, result, list, arrayDis);
                    foreach (FindingPaths ans in listAlgo)
                    {
                        selectedPath.Add(ans);
                    }

                }

            }
            return selectedPath;
        }

    }
    class fullfunc
    {
     
        int currRow = 0;

        
        public Dictionary<string, int> Dic(string[] areasArr)
        {
            //create Dictionary of index of address in original array

            var capitals = new Dictionary<string, int>();
            for (int i = 0; i < areasArr.Length; i++)
            {
                capitals.Add(areasArr[i], i);
            }

            return capitals;
        }

        public List<FindingPaths> Dis(Dictionary<string, int> capitals, string[,] result, List<FindingPaths> list, double[,] arrayDis)///להפוך לרשימה
        {
            int resRowSize = result.GetLength(0);
            int resColSize = result.GetLength(1);
            //combonationMatrix size same like result size
            int[,] combonationMatrix = new int[resRowSize, resColSize];
            
            //dis function calculate for us the sum of all route between each 2 address
            List<FindingPaths> selectedCombintion = new List<FindingPaths>();
            int indexWinner = 0;
            double winner = 0;


            list.Add(new FindingPaths(result[0, 0], 0, 0, "", "", 0, ""));
            double sumline = 0;

            List<int> termsList = new List<int>();
           
            for (int i = 0; i < resRowSize; i++)
                {
                //create matrix of all combination index 

                    for (int j = 0; j < resColSize; j++)
                    {
                    
                        if (capitals.ContainsKey(result[i, j]))
                        {

                            combonationMatrix[i, j] = capitals[result[i, j]];

                        }


                    }

                
            }
           int combMatrixRow = combonationMatrix.GetLength(0);
            int combMatrixCol = combonationMatrix.GetLength(1);

            //from distance array sum every row(every combination)
            for (int i = 0; i < combMatrixRow; i++)
            {
                for (int j = 0; j < combMatrixCol; j++)
                {
                    if (j+1!= combMatrixCol)
                    {
                        
                        sumline += arrayDis[combonationMatrix[i,j], combonationMatrix[i, j+1]];
                    }
                   
                    
                    //count++;
                }
                if (sumline < winner || winner == 0)
                {
                    //how is the shortest route between 2 combinations
                    winner = sumline;
                    sumline = 0;
                    termsList.Clear();
                    indexWinner = i;
                    //save the index of the winner
                }
                else
                {
                    sumline = 0;
                    termsList.Clear();
                }
            }



            for (int j = 0; j < resColSize; j++)
            {
                for (int x = 0; x < list.Count; x++)
                {
                    if (result[indexWinner, j] == list[x].Address)
                        //save the shortes combination in list object
                        selectedCombintion.Add(new FindingPaths(result[indexWinner, j], list[x].Lng, list[x].Lat, list[x].DistributaionArea, list[x].DateArrivel, list[x].CompanyNum, list[x].CompanyName));
                }


            }

            return selectedCombintion;

        }



        //if we have 2 address we can calculate distance between them



        public int factorial(int num)//ATZERT
        {
            //check how much combination have
            int fact = 1;
            for (int i = 1; i <= num; i++)
            {
                fact = fact * i;
            }
            return fact;
        }
        public void prnPermut(string[] list, int k, int m, string[,]  resultComb)
        {
            //this function create matrix of combination
            int i;
            int Z = 1;
            int count = 0;
            //if the address is the same,we find the combination
            if (list[k] == list[m])
            {
                for (int j = 0; j < Z; j++)
                {
                    for (i = 0; i <= m;)
                    {
                        if (i == 0)
                        {
                            // in the first place add our origin to start
                            //push the other address in the next cell
                            resultComb[currRow, i] = "גשר העץ 27,עמק חפר";


                            resultComb[currRow, i + 1] = list[i];

                            i++;
                        }
                        else
                        {
                            //insert the next address to list

                            resultComb[currRow, i + 1] = list[i];

                            i++;
                        }
                    }
                    i++;
                    //add the our company for make a circle
                    resultComb[currRow, i] = "גשר העץ 27,עמק חפר";
                    count++;
                    currRow++;
                }
            }
            else
            {
               
                for (i = k; i <= m; i++)
                {

                    SwapTwostring(ref list[k], ref list[i]);
                    prnPermut(list, k + 1, m, resultComb);
                    SwapTwostring(ref list[k], ref list[i]);
                }

            } 
        }
        public void SwapTwostring(ref string a, ref string b)
        {
            //swap between two address
            string temp = a;
            a = b;
            b = temp;
        }
    }
}


