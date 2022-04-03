﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Models.DAL;

namespace System.Models
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

        }


        public List<FindingPaths> Algo(List<FindingPaths> list, string date)
        {

            List<String> north = new List<string>();
            List<String> south = new List<string>();
            List<String> center = new List<string>();
            List<String> jerusalem = new List<string>();

            List<String>[] areasArr = new List<string>[4];

            areasArr[0] = north;
            areasArr[1] = south;
            areasArr[2] = center;
            areasArr[3] = jerusalem;

            foreach (FindingPaths obj in list)
            {
                if (obj.DateArrivel == date)
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
            }

            for (int i = 0; i < areasArr.Length; i++)
            {
                if (areasArr[i].Count != 0)
                {
                    var func = new fullfunc();
                    func.factorial(areasArr[i].Count);
                    List<String> newarr = new List<string>();
                    newarr.Insert(0, "גשר העץ 27,עמק חפר");
                    newarr.AddRange(areasArr[i]);
                    List<String> dicArr = new List<string>();
                    string[] listToDicArr = newarr.ToArray();
                    string[] listToArr = areasArr[i].ToArray();
                    var resultDic = new Dictionary<string, int>();
                    resultDic = func.Dic(listToDicArr);
                    Console.Write("\nAll the combination are: \n", listToArr.Length);
                    func.prnPermut(listToArr, 0, listToArr.Length - 1);
                    Console.Write("\n\n");
                    func.Dis(resultDic);
                }
            }
        }


        class fullfunc
        {
            int currRow = 0;
            string[,] result = new string[120, 7];

            public Dictionary<string, int> Dic(string [] areasArr)
            {
              
                var capitals = new Dictionary<string, int>();
                for (int i = 0; i < areasArr.Length; i++)
                {
                    capitals.Add(areasArr[i], i);
                }

                return capitals;
            }

            public void Dis(Dictionary<string, int> capitals)///להפוך לרשימה
            {
                int indexWinner = 0;
                int winner = 0;
                int count = 0, sumline = 0;
                int[,] arrayDis = new int[,] { { 0, 4, 2, 3 }, { 3, 0, 4, 5 }, { 5, 4, 0, 2 }, { 4, 3, 5, 0 } };
                List<int> termsList = new List<int>();
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        termsList.Add(capitals[result[i, j]]);
                        if (count == 2)
                        {
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
                        winner = sumline;
                        sumline = 0;
                        count = 0;
                        termsList.Clear();
                        indexWinner = i;
                    }
                    else
                    {
                        sumline = 0;
                        count = 0;
                        termsList.Clear();
                    }
                }
                Console.WriteLine("The row number of the selected option: " + (indexWinner + 1));
                Console.Write("The selected path is: ");
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    if (j == result.GetLength(1) - 1)
                    {
                        Console.Write(result[indexWinner, j] + " ");

                    }
                    else
                    {
                        Console.Write(result[indexWinner, j] + " -> ");
                    }
                }
                Console.Write("And its length: " + winner + " km ");

            }
            public int factorial(int num)
            {
                int fact = 1;
                for (int i = 1; i <= num; i++)
                {
                    fact = fact * i;
                }
                return fact;
            }
            public void prnPermut(string [] list, int k, int m)
            {
                int i;
                int Z = 1;
                int count = 0;

                if (list[k] == list[m])
                {
                    for (int j = 0; j < Z; j++)
                    {
                        for (i = 0; i <= m;)
                        {
                            if (i == 0)
                            {
                                result[currRow, i] = "גשר העץ 27,עמק חפר";
                                Console.Write(result[currRow, i]);
                                result[currRow, i + 1] = list[i];
                                Console.Write(result[currRow, i + 1]);
                                i++;
                            }
                            else
                            {
                                result[currRow, i + 1] = list[i];
                                Console.Write(result[currRow, i + 1]);
                                i++;
                            }
                        }
                        i++;
                        result[currRow, i] = "גשר העץ 27,עמק חפר";
                        Console.WriteLine(result[currRow, i]);
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
                        prnPermut(list, k + 1, m);
                        SwapTwostring(ref listToArr[k], ref listToArr[i]);
                    }
                }
            }
            public void SwapTwostring(ref string a, ref string b)
            {
                string temp = a;
                a = b;
                b = temp;
            }

        }









    }








}
    