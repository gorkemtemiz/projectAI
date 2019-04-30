using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Classes
{
    public interface IRack
    {
        int PlayerNumber { get; set; }
        List<string> RackLetters { get; set; }

        List<string> GetRack(int playerNo);
        void PutToRack(string letter, int playerNo);
    }
}
