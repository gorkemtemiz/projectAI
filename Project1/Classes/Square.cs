using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Square
    {
        public string Letter { get; set; }
        public int SquareValue { get; set; }

        public Square() { }

        public static Square CreateSquare(string newLetter,int newValue)
        {
            Square newSquare = new Square
            {
                Letter = newLetter,
                SquareValue = newValue
            };

            return newSquare;
        }
    }

}
