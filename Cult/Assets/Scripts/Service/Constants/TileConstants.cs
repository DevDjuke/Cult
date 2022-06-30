using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Service.Constants
{
    public static class TileConstants
    {
        public static readonly string tilesPath = "assets/tiles/";
        public static readonly string waterTilesFolder = "water/";
        public static readonly string landTilesFolder = "land/";

        public static readonly Dictionary<string, string> typeAnotations =
            new Dictionary<string, string> { { "tiles", "a" } };

        public static readonly Dictionary<string, string> kindAnotations =
            new Dictionary<string, string> { { "water", "a" }, { "land", "b" } };

        public static readonly Dictionary<string, string> sortAnotations =
            new Dictionary<string, string> { { "water", "a" }, { "grass", "b" }, { "settlement", "c" } };
    }
}
