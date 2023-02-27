using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_PP2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // declarations
            string uInput = ""; // user input
            int cardNum = 0; // number of cards
            int[][,] bCards = new int[][,] { }; // holder of the bingo cards
            Random rnd = new Random(); // randomizer
            int tempNum = 0; // holder of the temporary generated number
            bool duplicate = false; // flag if the number is duplicate
            int dCard = 0; // index of card to be displayed
            string unformated = ""; // for display
            bool loop = true; // the flag that will tell the program to keep looping.

            // user input initialize
            while (cardNum <= 0)
            {
                cardNum = 0; // just a reset
                Console.WriteLine("How many cards would you like me to generate? " +
                    "Please input a number greater than 0. . .");
                uInput = Console.ReadLine(); // takes the user input
                try
                {
                    cardNum = int.Parse(uInput);
                }
                catch(Exception e)
                {
                    cardNum = 0;
                }
                #region or you can just use tryparse
                //bool isNum = false;
                //isNum = int.TryParse(uInput, out cardNum);
                #endregion
            }

            // re-initializing bCards
            bCards = new int[cardNum][,];
            for (int x = 0; x < bCards.Length; x++)
                bCards[x] = new int[5, 5];
            Console.WriteLine(cardNum + " card/s initialized.\n");
            // 3d jagged (assume bCard is a pure jagged array)
            // bCards = new int[cardNum][][];
            // for(int x = 0; x < bCards.Length; x++)
            // {
            //      bCards[x] = new int[5][];
            //      for(int y = 0; y < bCards[x].Length; y++)
            //      {
            //          bCards[x][y] = new int[5];
            //      }
            // }
            // 3d multidimension (assume bCard is a pure multidimension array)
            // bCards = new int[cardNum, 5,5];


            // logic
            // generate the bingo cards
            for (int x = 0; x < bCards.Length; x++) // card
            {
                for(int y = 0; y < bCards[x].GetLength(0); y++) // row
                {
                    for(int z = 0; z < bCards[x].GetLength(1); z++) // column
                    {
                        tempNum = rnd.Next(1, 16);
                        tempNum += z * 15;

                        // duplicate check
                        duplicate = false;

                        for (int a = 0; a < y; a++)
                        {
                            if (bCards[x][a,z] == tempNum)
                            {
                                duplicate = true;
                                break;
                            }
                        }

                        if (!duplicate)
                            bCards[x][y, z] = tempNum;
                        else
                            z--;
                    }
                }
            }
            Console.WriteLine(cardNum + " card/s generated.\n");

            // display
            while (loop)
            {
                Console.WriteLine("Displaying card " + (dCard + 1) + ".");
                Console.WriteLine(" B \t I \t N \t G \t O ");
                for(int x = 0; x < bCards[dCard].GetLength(0); x++)
                {
                    for (int y = 0; y < bCards[dCard].GetLength(1); y++)
                    {
                        unformated = bCards[dCard][x, y] + "";
                        while (unformated.Length < 3)
                            unformated = "0" + unformated;
                        Console.Write(unformated + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Please enter <, > or e to proceed: ");
                uInput = Console.ReadLine().ToLower();
                switch(uInput[0])
                {
                    case '<':
                        dCard--;
                        if (dCard < 0)
                            dCard = bCards.Length - 1;
                        break;
                    case '>':
                        dCard++;
                        if (dCard >= bCards.Length)
                            dCard = 0;
                        break;
                    case 'e':
                        Console.WriteLine("Thank you for using the application.");
                        loop = false;
                        break;
                }
                Console.Clear();
            }
        }
    }
}
