using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Domain
{
    public class HexTile
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool isLand { get; set; }
        public bool hasSettlement { get; set; }

        public string TileCode { get; set; }
    }
}
