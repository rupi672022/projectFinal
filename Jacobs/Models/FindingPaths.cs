using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Jacobs.Models.DAL;

namespace Jacobs.Models
{
    public class FindingPaths
    {
        string address;
        double lng;
        double lat;
        string distributaionArea;
        string dateArrivel;
        int companyNum;



        public FindingPaths() { }

        public FindingPaths(string address, double lng, double lat, string distributaionArea, string dateArrivel, int companyNum)
        {
            this.address = address;
            this.lng = lng;
            this.lat = lat;
            this.distributaionArea = distributaionArea;
            this.dateArrivel = dateArrivel;
            this.companyNum = companyNum;


        }

        public string Address { get => address; set => address = value; }
        public double Lng { get => lng; set => lng = value; }
        public double Lat { get => lat; set => lat = value; }
        public string DistributaionArea { get => distributaionArea; set => distributaionArea = value; }
        public string DateArrivel { get => dateArrivel; set => dateArrivel = value; }
        public int CompanyNum { get => companyNum; set => companyNum = value; }

        public List<FindingPaths> Read(string date)
        {
            FindingPathDataServices ds = new FindingPathDataServices();
            List<FindingPaths> FindingPathslist = ds.Read(date);
            return Algo(FindingPathslist, date);
            //return FindingPathslist;
        }


        public List<FindingPaths> Algo(List<FindingPaths> list, string date)
        {
            
            List<FindingPaths> selectedPath = new List<FindingPaths>();
            //lists with distrebiution area
            List<FindingPaths> north = new List<FindingPaths>();
            List<FindingPaths> south = new List<FindingPaths>();
            List<FindingPaths> center = new List<FindingPaths>();
            List<FindingPaths> jerusalem = new List<FindingPaths>();

            List<FindingPaths>[] areasArr = new List<FindingPaths>[4];

            areasArr[0] = north;
            areasArr[1] = south;
            areasArr[2] = center;
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
                if (areasArr[i].Count == 3)
                {
                    fullfunc func = new fullfunc();
                   //function factorial calculate how much comabination has
                    int factRes = func.factorial(areasArr[i].Count);
                    //add +2 to save place to origin and destnation
                    string[,] result = new string[factRes, (areasArr[i].Count) + 2];

                    List<String> listIncludeJacobs = new List<string>();
                    List<String> addressList = new List<string>();
                    listIncludeJacobs.Insert(0, "גשר העץ 27,עמק חפר");

                    foreach(FindingPaths ans in areasArr[i])
                    {
                        addressList.Add(ans.Address);
                    }

                    listIncludeJacobs.AddRange(addressList);
                    string[] listToDicArr = listIncludeJacobs.ToArray();
                    string[] listToArr = addressList.ToArray();
                    var resultDic = new Dictionary<string, int>();
                    //function dic is a Dictionary for all the index in adress array
                    resultDic = func.Dic(listToDicArr);
                    //prnPermut functaion return all the combination of routes 
                    func.prnPermut(listToArr, 0, listToArr.Length - 1, result);
                    Console.Write("\n\n");


                    var listAlgo = func.Dis(resultDic, result, list);
                    foreach (FindingPaths ans in listAlgo)
                    {
                        selectedPath.Add(ans);
                    }
 
                }
               
            }
            return selectedPath;
        }


        class fullfunc
        {
            int currRow = 0;


            public Dictionary<string, int> Dic(string[] areasArr)
            {

                var capitals = new Dictionary<string, int>();
                for (int i = 0; i < areasArr.Length; i++)
                {
                    capitals.Add(areasArr[i], i);
                }

                return capitals;
            }

            public List<FindingPaths> Dis(Dictionary<string, int> capitals, string[,] result, List<FindingPaths> list)///להפוך לרשימה
            {
                //dis function calculate for us the sum of all route between each 2 address
                List<FindingPaths> selectedCombintion = new List<FindingPaths>();
                int indexWinner = 0;
                int winner = 0;
                int count = 0, sumline = 0;
                int[,] arrayDis = new int[,] {  { 0, 2, 9, 4 }, {3, 0,1 , 5 }, {  5, 5, 0, 5 }, { 5, 3, 3, 0 } };
                List<int> termsList = new List<int>();
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        termsList.Add(capitals[result[i, j]]);
                        //if we have 2 address we can calculate distance between them

                        if (count == 2)
                        {
                            //we need to sum from adress in place 0 to 1
                            //and from place 1 to 2
                            sumline += arrayDis[termsList[j - 2], termsList[j - 1]];
                            sumline += arrayDis[termsList[j - 1], termsList[j]];
                        }

                        if (count > 2 || (result.Length == j + 1))
                        {
                            sumline += arrayDis[termsList[j - 1], termsList[j]];


                        }
                        count++;
                    }
                    if (sumline < winner || winner == 0)
                    {
                        //how is the shortes route between 2 combinations
                        winner = sumline;
                        sumline = 0;
                        count = 0;
                        termsList.Clear();
                        indexWinner = i;
                        //save the index of the winner
                    }
                    else
                    {
                        sumline = 0;
                        count = 0;
                        termsList.Clear();
                    }
                }
           
                for (int j = 0; j < result.GetLength(1); j++)
                {


                    //save the shortes combination in list object
                    selectedCombintion.Add(new FindingPaths(result[indexWinner, j], 0, 0, "", "", 0));

                    
                }
                return selectedCombintion;

            }
            public int factorial(int num)//ATZERT
            {
                int fact = 1;
                for (int i = 1; i <= num; i++)
                {
                    fact = fact * i;
                }
                return fact;
            }
            public void prnPermut(string[] list, int k, int m, string[,] result)
            {
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
                                result[currRow, i] = "גשר העץ 27,עמק חפר";
                            
                                result[currRow, i + 1] = list[i];
                               
                                i++;
                            }
                            else
                            {
                                //insert the next address to list

                                result[currRow, i + 1] = list[i];
                               
                                i++;
                            }
                        }
                        i++;
                        //add the our company for make a circle
                        result[currRow, i] = "גשר העץ 27,עמק חפר";
                        count++;
                        currRow++;
                    }
                }
                else
                {
                    string[] listToArr = list.ToArray();
                    for (i = k; i <= m; i++)
                    {

                        SwapTwostring(ref listToArr[k], ref listToArr[i]);
                        prnPermut(list, k + 1, m, result);
                        SwapTwostring(ref listToArr[k], ref listToArr[i]);
                    }
                }
            }
            public void SwapTwostring(ref string a, ref string b)
            {
                //swap between 2 address to make combination
                string temp = a;
                a = b;
                b = temp;
            }

        }









    }








}

