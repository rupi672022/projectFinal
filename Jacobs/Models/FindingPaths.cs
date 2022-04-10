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
        double[,] arrayDis;
        int whichArea = 0;
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
                if (areasArr[i].Count != 0)
                {
                    fullfunc func = new fullfunc();
                    //function factorial calculate how much comabination has
                    int factRes = func.factorial(areasArr[i].Count);
                    //add +2 to save place to origin and destnation
                    string[,] result = new string[factRes, (areasArr[i].Count) + 2];
                    int[,]combMatrixdis=new int[factRes, (areasArr[i].Count) + 2];
                    List<String> listIncludeJacobs = new List<string>();
                    List<String> addressList = new List<string>();
                    listIncludeJacobs.Insert(0, "גשר העץ 27,עמק חפר");

                    foreach (FindingPaths ans in areasArr[i])
                    {
                        addressList.Add(ans.Address);
                    }

                    listIncludeJacobs.AddRange(addressList);
                    string[] listToDicArr = listIncludeJacobs.ToArray();
                    string[] listToArr = addressList.ToArray();
                    var resultDic = new Dictionary<string, int>();
                    if (whichArea == 0)
                    {
                        arrayDis = arrayDis0;

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
                    resultDic = func.Dic(listToDicArr);
                    //prnPermut functaion return all the combination of routes 
                    func.prnPermut(listToArr, 0, listToArr.Length - 1, result);
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

            var capitals = new Dictionary<string, int>();
            for (int i = 0; i < areasArr.Length; i++)
            {
                capitals.Add(areasArr[i], i);
            }

            return capitals;
        }

        public List<FindingPaths> Dis(Dictionary<string, int> capitals, string[,] result, List<FindingPaths> list, double[,] arrayDis)///להפוך לרשימה
        {
            int[,] combonationMatrix = new int[result.GetLength(0), result.GetLength(1)];
            //dis function calculate for us the sum of all route between each 2 address
            List<FindingPaths> selectedCombintion = new List<FindingPaths>();
            int indexWinner = 0;
            double winner = 0;
            
            double sumline = 0;

            List<int> termsList = new List<int>();

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    foreach (var item in capitals)
                    {
                        if (result[i, j] ==item.Key )
                        {
                            combonationMatrix[i, j] = item.Value;
                        }
                    }

                }
            }
            for (int i = 0; i < combonationMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < combonationMatrix.GetLength(1); j++)
                {
                    if (j+1!= combonationMatrix.GetLength(1))
                    {
                        
                        sumline += arrayDis[combonationMatrix[i,j], combonationMatrix[i, j+1]];
                    }
                   
                    
                    //count++;
                }
                if (sumline < winner || winner == 0)
                {
                    //how is the shortes route between 2 combinations
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



            for (int j = 0; j < result.GetLength(1); j++)
            {


                //save the shortes combination in list object
                selectedCombintion.Add(new FindingPaths(result[indexWinner, j], 0, 0, "", "", 0));


            }
            return selectedCombintion;

        }



        //if we have 2 address we can calculate distance between them



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


