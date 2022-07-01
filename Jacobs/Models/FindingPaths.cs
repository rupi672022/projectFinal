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
        int distanceCompany;
        int idFromCompany;
        int idToCompany;
      
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

        public FindingPaths(string fromCompany, string toCompany,int distanceCompany, string distributaionArea, int idFromCompany, int idToCompany)
        {
            this.fromCompany = fromCompany;
            this.toCompany = toCompany;
            this.distanceCompany = distanceCompany;
            this.distributaionArea = distributaionArea;
            this.idFromCompany = idFromCompany;
            this.idToCompany = idToCompany;
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
        public int DistanceCompany { get => distanceCompany; set => distanceCompany = value; }
        public int IdFromCompany { get => idFromCompany; set => idFromCompany = value; }
        public int IdToCompany { get => idToCompany; set => idToCompany = value; }

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
            List<FindingPaths> arrayDis = new List<FindingPaths>();

            List<FindingPaths> north = new List<FindingPaths>();
            List<FindingPaths> south = new List<FindingPaths>();
            List<FindingPaths> center = new List<FindingPaths>();
            List<FindingPaths> jerusalem = new List<FindingPaths>();
            
            List<FindingPaths>[] areasArr = new List<FindingPaths>[4];
            areasArr[0] = north;
            areasArr[1] = center;
            areasArr[2] = south;
            areasArr[3] = jerusalem;
            FindingPaths createSource = new FindingPaths("גשר העץ 27,עמק חפר", 34.895175832114731, 32.405410127264439, "",date,1, "משק יעקבס");
            areasArr[0].Add(createSource);
            areasArr[1].Add(createSource);
            areasArr[2].Add(createSource);
            areasArr[3].Add(createSource);

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

            List<FindingPaths> northDis = new List<FindingPaths>();
            List<FindingPaths> southDis = new List<FindingPaths>();
            List<FindingPaths> centerDis = new List<FindingPaths>();
            List<FindingPaths> jerusalemDis = new List<FindingPaths>();
            bool northbool = false;
            bool centerbool = false;
            bool southbool = false;

            bool jerusalembool = false;


            foreach (FindingPaths obj in DistanceList)
            {
                if (obj.DistributaionArea == "צפון"|| obj.DistributaionArea =="")
                {
                    foreach (FindingPaths ans in north)
                    {
                        if (ans.CompanyNum == obj.IdToCompany)
                        {
                            northDis.Add(obj);

                        }
                    }

                }
                if (obj.DistributaionArea == "מרכז"||obj.DistributaionArea == "")
                {
                    //countnorth
                    foreach (FindingPaths ans in center)
                    {
                        if (ans.CompanyNum == obj.IdToCompany )
                        {
                            centerDis.Add(obj);
                        }
                       
                    }
                }
                if (obj.DistributaionArea == "דרום" || obj.DistributaionArea == "")
                {
                    foreach (FindingPaths ans in south)
                    {
                        if (ans.CompanyNum == obj.IdToCompany )
                        {
                            southDis.Add(obj);
                        }
                    }
                }
                if (obj.DistributaionArea == "ירושלים" || obj.DistributaionArea == "")
                {
                    foreach (FindingPaths ans in jerusalem)
                    {
                        if (ans.CompanyNum == obj.IdToCompany)
                        {
                            jerusalemDis.Add(obj);
                        }
                    }
                }
            }
           
            List<FindingPaths> selectedPath = new List<FindingPaths>();
            //lists with distrebiution area
           
     

            

            for (int i = 0; i < areasArr.Length; i++)
            {
                if (areasArr[i].Count > 1)//if the area not empty from address
                {
                    fullfunc func = new fullfunc();
                    //function factorial calculate how much comabination has
                    int factRes = func.factorial(areasArr[i].Count-1);
                    //add +2 to save place to origin and destnation
                    string[,] result = new string[factRes, (areasArr[i].Count) + 1];
                 
                    List<String> listIncludeJacobs = new List<string>();
                    Dictionary<string,int> addressList = new Dictionary<string, int>();

                    List<string> addressToArr=new List<string>();
                    foreach (FindingPaths ans in areasArr[i])
                    {
                        addressList.Add(ans.Address,ans.CompanyNum);

                        addressToArr.Add(ans.Address);
                    }

                    string[] addressToArrN = addressToArr.ToArray();


                    var resultDic = new Dictionary<string, int>();
                   
                        if (northDis.Count > 4&&northbool!=true)
                        {
                            arrayDis = northDis;
                        northbool = true;
                        }
                    
                    //whichare include the number of select area
                    
                    else  if (centerDis.Count > 4&&centerbool != true )
                        {
                        
                        
                            arrayDis = centerDis;
                        centerbool = true;
                        }
                    
                    else  if (southDis.Count > 4&&southbool!=true)
                        {
                            arrayDis = southDis;

                        southbool = true;


                    }
                    else if (jerusalemDis.Count > 4&&jerusalembool!=true)
                    {
                      
                        
                            arrayDis = jerusalemDis;
                        jerusalembool = true;
                        }
                    
                    //function dic is a Dictionary for all the index in adress array
                    resultDic = func.Dic(addressList);
                    //prnPermut functaion return all the combination of routes 
                    int t = 0;
                    string[] arr=new string[addressToArrN.Length-1];

                    for (int z = 1; z < addressToArrN.Length;  z++)
                    {

                        arr[t]= addressToArrN[z];
                        t++;
                    }
                    func.prnPermut(arr, 0, addressToArrN.Length - 2, result);


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

        
        public Dictionary<string, int> Dic(Dictionary<string, int> areasArr)
        {
          

            //create Dictionary of index of address in original array
            var capitals = new Dictionary<string, int>();
          
            foreach (KeyValuePair< string,int > ele1 in areasArr)
            {
                capitals.Add(ele1.Key, ele1.Value);
            }
            return capitals;
        }

        public List<FindingPaths> Dis(Dictionary<string, int> capitals, string[,] result, List<FindingPaths> list, List<FindingPaths> arrayDis)///להפוך לרשימה
        {
            int resRowSize = result.GetLength(0);
            int resColSize = result.GetLength(1);
            //combonationMatrix size same like result size
            int[,] combonationMatrix = new int[resRowSize, resColSize];
            
            //dis function calculate for us the sum of all route between each 2 address
            List<FindingPaths> selectedCombintion = new List<FindingPaths>();
            int indexWinner = 0;
            double winner = 0;

            if(list.Exists(x => x.CompanyNum == 1))
            {

            }
            else
            {
                list.Add(new FindingPaths(result[0, 0], 34.895175832114731, 32.405410127264439, "", "", 1, ""));

            }
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
                    if (j + 1 != combMatrixCol)
                    {
                      
                        foreach(FindingPaths obj in arrayDis)
                        {
                            if(combonationMatrix[i, j]==obj.IdFromCompany&& combonationMatrix[i, j+1]== obj.IdToCompany)
                            {
                                sumline+=  obj.DistanceCompany;
                            }
                        }
                    }


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


