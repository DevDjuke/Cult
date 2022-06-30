using Assets.Scripts.Service.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Service.Assets
{
    public static class TileLoaderService
    {
        public enum TileKind
        {
            Water,
            Land
        }

        public enum TileSort
        {
            OpenWater,
            Shore,
            Grass,
            Settlement
        }

        public static string GetTilePath(TileKind kind, TileSort sort, string modelParam = null)
        {
            switch (kind)
            {               
                case TileKind.Land:
                    return GetLandTilePath(sort, modelParam);
                case TileKind.Water:
                default:
                    return GetWaterTilePath(sort, modelParam);
            }
        }

        private static string GetWaterTilePath(TileSort sort, string modelParam = null)
        {
            string path = TileConstants.tilesPath + TileConstants.waterTilesFolder;

            string model = TileConstants.typeAnotations["tiles"]
                            + TileConstants.kindAnotations["water"] + TileConstants.sortAnotations["water"];

            switch (sort)
            {
                case TileSort.Shore:
                    return path + "anim/" + model + "_" + modelParam + ".asset";
                case TileSort.OpenWater:
                default:
                    return path + model + "_0000.asset";
            }
        }

        private static string GetLandTilePath(TileSort sort, string modelParam = null)
        {
            string path = TileConstants.tilesPath + TileConstants.landTilesFolder + TileConstants.typeAnotations["tiles"]
                            + TileConstants.kindAnotations["land"];

            switch (sort)
            {
                case TileSort.Settlement:
                    return path + TileConstants.sortAnotations["settlement"] + "_0000.asset";
                case TileSort.Grass:
                default:
                    return path + TileConstants.sortAnotations["grass"] + "_0000.asset";
            }
        }
    }
}
