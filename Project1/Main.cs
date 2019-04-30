using Project1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Project1
{
    public class MainMethods : Table, IRack
    {
        public int PlayerNumber = 2;
        int IRack.PlayerNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> RackLetters = new List<string>();
        List<string> IRack.RackLetters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        ////////////////////////////////////////////////////////////////////////////////

        public static Square[,] ornekTable = new Square[5,5]
        {
            {CreateSquare("",0),CreateSquare("",0), CreateSquare("",0), CreateSquare("",0), CreateSquare("",0)},
            {CreateSquare("",0),CreateSquare("",0), CreateSquare("H",3),CreateSquare("",0), CreateSquare("",0)},
            {CreateSquare("",0),CreateSquare("P",0),CreateSquare("A",0),CreateSquare("S",0),CreateSquare("",0)},
            {CreateSquare("",0),CreateSquare("",0), CreateSquare("L",0),CreateSquare("I",0),CreateSquare("",0)},
            {CreateSquare("",0),CreateSquare("",0), CreateSquare("",0), CreateSquare("L",0),CreateSquare("S",3)}
        };

        public static List<String> GetLetters(Square[,] squares)
        {
            List<String> liste = new List<String>();

            for (int i = 0; i<5; i++)
            {
                string kelimeSoldanSaga = "";

                for (int j = 0; j < 5; j++)
                {
                    if (squares[i,j].Letter != "")
                    {
                        kelimeSoldanSaga += squares[i, j].Letter;
                    }
                }
                if (kelimeSoldanSaga != "") liste.Add(kelimeSoldanSaga);
            }

            for (int j = 0; j < 5; j++)
            {
                string kelimeYukaridanAsagiya = "";

                for (int i = 0; i < 5; i++)
                {
                    if (squares[i,j].Letter != "")
                    {
                        kelimeYukaridanAsagiya += squares[i, j].Letter;
                    }
                }
                if (kelimeYukaridanAsagiya != "") liste.Add(kelimeYukaridanAsagiya);
            }

            return liste;
        }

        ////////////////////////////////////////////////////////////////////////////////


        public List<string> GetRack(int playerNo)
        {
            if (playerNo == 2)
                return RackLetters;
            else
                return null;
        }

        public void PutToRack(string letter, int playerNo)
        {
            if (playerNo == 2)
                RackLetters.Add(letter);
        }



        ////////////////////////////////////////////////////////////////////////////////
        
        public static List<List<string>> GetPowerSetOfRackLetters(List<string> setOfLetters)
        {
            List<List<string>> subsets = new List<List<string>>();
            string[] tempArray = new string[3] {setOfLetters[0], setOfLetters[1], setOfLetters[2]};

            uint pow_set_size = (uint)Math.Pow(2, 3);
            int counter, j;

            for (counter = 0; counter < pow_set_size; counter++)
            {
                List<string> subset = new List<string>();
                for (j = 0; j < 3; j++)
                {
                    if ((counter & (1 << j)) > 0)
                    {
                        subset.Add( tempArray[j]);
                    }
                }
                subsets.Add(subset);
            }
            return subsets;
        }

        public static List<string> Swap(List<string> stringsToSwap, int i, int j)
        {
            string temp;

            temp = stringsToSwap[i];          
            stringsToSwap[i] = stringsToSwap[j];
            stringsToSwap[j] = temp;

            return stringsToSwap;
        }

        public static List<string> Permute(List<string> elementsToPermute, List<string> permutedWordList, int l, int r)
        {
            if (l == r)
            {
              string s = string.Join("", elementsToPermute.ToArray());
              permutedWordList.Add(s);
            }
            else
            {
              for (int i = l; i <= r; i++)
              {
                elementsToPermute = Swap(elementsToPermute, l, i);
                Permute(elementsToPermute, permutedWordList, l + 1, r);
                elementsToPermute = Swap(elementsToPermute, l, i);
              }
            }

            return permutedWordList;
        }

        public static List<string> GetRandomizedWords(List<string> lettersFromRack, List<string> wordsFromTable, List<string> permutedList)
        {
            List<string> randomizedWords = new List<string>();
            List<string> tempList = new List<string>();

            for (int i = 0; i < wordsFromTable.Count; i++)
            {
                tempList.AddRange(lettersFromRack);
                tempList.Add(wordsFromTable[i]);

                randomizedWords.AddRange(Permute(tempList, permutedList, 0, tempList.Count -1));

                tempList.Clear();
            }

            return randomizedWords;
        }

        ////////////////////////////////////////////////////////////////////////////////
      

        public static List<string> CheckRandomizedWords(List<string> wordsToCheck, List<string> checkedWords)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source = words.db;Version = 3;");
            connection.Open();

            SQLiteDataAdapter sQLiteDataAdapter;
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string commandText = "SELECT Word FROM AllWords";
            sQLiteDataAdapter = new SQLiteDataAdapter(commandText, connection);
            sQLiteDataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];

            for (int i = 0; i < wordsToCheck.Count; i++)
            {
                DataRow[] dataRows = dataTable.Select("Word = '" + wordsToCheck[i] + "'");

                foreach(DataRow row in dataRows)
                {
                    string st = row["Word"].ToString();

                    if (st == wordsToCheck[i])
                    {
                        checkedWords.Add(wordsToCheck[i]);
                    }
                else continue;

                }

            }
            connection.Close();

            return checkedWords;
        }

        ////////////////////////////////////////////////////////////////////////////////

        public static void Main(string[] args)
        {
            MainMethods mainMethods = new MainMethods();
            mainMethods.PutToRack("A", 2);
            mainMethods.PutToRack("L", 2);
            mainMethods.PutToRack("K", 2);

            List<string> permutedWords = new List<string>();
            List<string> checkedWords = new List<string>();
            List<List<string>> combinedRackLetters = new List<List<string>>();
            


            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;

            while(a < GetLetters(ornekTable).Count)
            {
                Console.WriteLine(GetLetters(ornekTable)[a]);
                a++;
            }


            Console.WriteLine("----------------------------------------------------------------");


            while(b < mainMethods.GetRack(2).Count)
            {
                Console.WriteLine(mainMethods.GetRack(2)[b]);
                b++;
            }
          

            Console.WriteLine("----------------------------------------------------------------");


            GetRandomizedWords(mainMethods.GetRack(2), GetLetters(ornekTable), permutedWords);
            while (c < permutedWords.Count)
            {
                Console.WriteLine(permutedWords[c]);
                c++;
            }


            Console.WriteLine("----------------------------------------------------------------");


            CheckRandomizedWords(permutedWords, checkedWords);
            while (d < checkedWords.Count)
            {
                Console.WriteLine(checkedWords[d]);
                d++;
            }

            Console.WriteLine("----------------------------------------------------------------");

            combinedRackLetters = GetPowerSetOfRackLetters(mainMethods.GetRack(2));
            for (int e = 0; e < 8; e++)
            {
                Console.WriteLine(string.Join("", combinedRackLetters[e].ToArray()));
            }

            Console.ReadLine();
        }
    }
}
