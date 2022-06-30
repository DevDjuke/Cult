using Assets.Scripts.Domain;
using Assets.Scripts.Service.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Service.Generator
{
    public static class WorldGeneratorService
    {
        public static int Width;
        public static int Height;

        public static AssetBundle tileBundle;
        public static HexTile[,] world;

        public static void Setup(int width, int height)
        {
            Width = width;
            Height = height;
            tileBundle = AssetBundleLoadService.GetAssetBundle("tiles");
        }

        public static void Generate(Tilemap map, int width, int height)
        {
            Setup(width, height);
            bool pass = false;

            while (!pass)
            {
                world = GenerateWorld();
                pass = Validate();
            }
            
            RenderWorld(map);
        }

        private static bool Validate()
        {
            int landtreshhold = 25;
            int landCOUNT = 0;

            foreach(HexTile hex in world)
            {
                if (hex.isLand) landCOUNT++;
                if (landCOUNT >= landtreshhold) return true;
            }

            return false;
        }

        private static HexTile[,] GenerateWorld()
        {
            return MapGeneratorService.GenerateMap(Width, Height);
        }

        public static void RenderWorld(Tilemap map)
        {
            foreach(HexTile hex in world)
            {
                if(hex != null)
                {
                    if (hex.isLand)
                    {
                        if (hex.hasSettlement)
                        {
                            Tile grass = tileBundle.LoadAsset<Tile>(
                                TileLoaderService.GetTilePath(TileLoaderService.TileKind.Land, TileLoaderService.TileSort.Settlement));
                            map.SetTile(new Vector3Int(hex.X, hex.Y, 0), grass);
                        }
                        else
                        {
                            Tile grass = tileBundle.LoadAsset<Tile>(
                                TileLoaderService.GetTilePath(TileLoaderService.TileKind.Land, TileLoaderService.TileSort.Grass));
                            map.SetTile(new Vector3Int(hex.X, hex.Y, 0), grass);
                        }
                    }
                    else
                    {
                        TileBase water;

                        if(hex.TileCode == null || hex.TileCode.Equals("0000"))
                        {
                            water = tileBundle.LoadAsset<Tile>(
                            TileLoaderService.GetTilePath(TileLoaderService.TileKind.Water,
                                                            TileLoaderService.TileSort.OpenWater));
                        }
                        else
                        {
                            water = tileBundle.LoadAsset<AnimatedTile>(
                            TileLoaderService.GetTilePath(TileLoaderService.TileKind.Water,
                                                            TileLoaderService.TileSort.Shore, hex.TileCode));

                            
                        } 

                        map.SetTile(new Vector3Int(hex.X, hex.Y, 0), water);
                    }
                }
            }
        }
    }
}
