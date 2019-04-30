using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1 
{
    public class Table : Square
    {
        protected Square[,] tableMatrix = new Square[15,15];

        public Square[,] GetTable()
        {
            return tableMatrix;
        }

        public void SetTable(Square[,] newTable)
        {
            tableMatrix = newTable;
        }
    }
}
