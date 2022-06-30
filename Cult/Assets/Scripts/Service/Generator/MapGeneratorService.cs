using Assets.Scripts.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Service.Generator
{
    public static class MapGeneratorService
    {
        public static int RandomFillPercent = 45;
        private static string seed;

        private static Random random;

        private static int Width;
        private static int Height;
        private static HexTile[,] Map;

        public static HexTile[,] GenerateMap(int width, int height)
        {
            SetMap(width, height);
            RandomFillMap();

            for (var i = 0; i < 2; i++)
            {
                SmoothMap(3);
            }
            for (var i = 0; i < 2; i++)
            {
                SmoothWater();
            }
            for (var i = 0; i < 5; i++)
            {
                SmoothMap(1);
            }

            //SetShores();

            SetSettlements();

            return Map;
        }

        public static void SetMap(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new HexTile[Width, Height];
        }

        public static HexTile[,] GetMap()
        {
            return Map;
        }

        public static void RandomFillMap()
        {
            seed = System.DateTime.UtcNow.ToString();
            random = new Random(seed.GetHashCode());

            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    HexTile hex = new HexTile()
                    {
                        X = x,
                        Y = y
                    };

                    if (x <= 3 || y <= 3 || x >= Width - 4 || y >= Height - 4)
                    {
                        hex.isLand = false;
                    }

                    else
                    {
                        hex.isLand = random.Next(0,100) < RandomFillPercent;
                    }

                    Map[x, y] = hex;
                }
            }
        }

        public static void SmoothMap(int treshold)
        {
            List<HexTile> temp = Map.Cast<HexTile>().ToList();
            temp = temp.OrderBy(x => Guid.NewGuid()).ToList();
            
            while(temp.Count !=0)
            {
                var index = random.Next(0, temp.Count - 1);
                HexTile tile = temp.ElementAt(index);
                int neighbours = GetSurroundings(tile.X, tile.Y);

                if (neighbours > treshold)
                {
                    Map[tile.X, tile.Y].isLand = true;
                }
                else
                {
                    Map[tile.X, tile.Y].isLand = false;
                }

                temp.RemoveAt(index);
            }
        }

        private static void SmoothMapEquals(int treshold)
        {
            List<HexTile> temp = Map.Cast<HexTile>().ToList();
            temp = temp.OrderBy(x => Guid.NewGuid()).ToList();

            while (temp.Count != 0)
            {
                var index = random.Next(0, temp.Count - 1);
                HexTile tile = temp.ElementAt(index);
                int neighbours = GetSurroundings(tile.X, tile.Y);

                if (tile.isLand)
                {
                    if (neighbours == treshold)
                    {
                        if (random.Next(0, 100) < 60)
                        {
                            Map[tile.X, tile.Y].isLand = false;
                        }
                    }
                }

                temp.RemoveAt(index);
            }
        }

        public static void SmoothWater()
        {
            foreach (HexTile tile in Map)
            {
                if (tile.isLand)
                {
                    int neighbours = GetSurroundings(tile.X, tile.Y);

                    if (neighbours < 2)
                    {
                        Map[tile.X, tile.Y].isLand = false;
                    }
                }
            }
        }

        private static int GetSurroundings(int x, int y)
        {
            int count = 0;

            if (x <= 3 || x >= Width-3 || y <= 3 || y >= Height-3) return count;

            if (y % 2 == 0)
            {
                count += Map[x - 1, y+1].isLand ? 1 : 0;
                count += Map[x, y+1].isLand ? 1 : 0;
                count += Map[x-1, y].isLand ? 1 : 0;
                count += Map[x+1, y].isLand ? 1 : 0;
                count += Map[x-1, y-1].isLand ? 1 : 0;
                count += Map[x, y-1].isLand ? 1 : 0;
            }
            else
            {
                count += Map[x, y + 1].isLand ? 1 : 0;
                count += Map[x+1, y + 1].isLand ? 1 : 0;
                count += Map[x - 1, y].isLand ? 1 : 0;
                count += Map[x + 1, y].isLand ? 1 : 0;
                count += Map[x, y - 1].isLand ? 1 : 0;
                count += Map[x+1, y - 1].isLand ? 1 : 0;
            }    
            return count;
        }

        public static void GrowMap()
        {
            GrowSquare(25, 40, 75, 110);
            GrowSquare(100, 40, 125, 110);
        }

        private static void GrowSquare(int X, int Y, int width, int height)
        {
            HexTile leftMost = null;
            HexTile rightMost = null;
            HexTile topMost = null;
            HexTile bottomMost = null;

            for (int c = X; c < X + width; c++)
            {
                for (int r = Y; r < Y + height; r++)
                {
                    if(c < Width -1 && r < Height - 1)
                    {
                        HexTile tile = Map[c, r];
                        if (tile.isLand)
                        {
                            if (leftMost == null)
                            {
                                leftMost = tile;
                            }
                            else if (tile.X <= leftMost.X)
                            {
                                if (tile.X < leftMost.X || random.Next(100) < 60)
                                {
                                    leftMost = tile;
                                }
                            }

                            if (topMost == null)
                            {
                                topMost = tile;
                            }
                            else if (tile.Y >= topMost.Y)
                            {
                                if (tile.Y > topMost.Y || random.Next(100) < 60)
                                {
                                    topMost = tile;
                                }
                            }

                            if (rightMost == null)
                            {
                                rightMost = tile;
                            }
                            else if (tile.X >= rightMost.Y)
                            {
                                if (tile.X > rightMost.Y || random.Next(100) < 60)
                                {
                                    rightMost = tile;
                                }
                            }

                            if (bottomMost == null)
                            {
                                bottomMost = tile;
                            }
                            else if (tile.Y <= bottomMost.Y)
                            {
                                if (tile.Y < bottomMost.Y || random.Next(100) < 60)
                                {
                                    bottomMost = tile;
                                }
                            }
                        }
                    }
                }
            }

            if (leftMost != null)
            {
                DrunkWalk(leftMost.X, leftMost.Y, topMost.X, topMost.Y, topMost.X);
                DrunkWalk(topMost.X, topMost.Y, rightMost.X, rightMost.Y, topMost.X);
                DrunkWalk(rightMost.X, rightMost.Y, bottomMost.X, bottomMost.Y, bottomMost.X);
                DrunkWalk(bottomMost.X, bottomMost.Y, leftMost.X, leftMost.Y, bottomMost.X);
            }
        }

        private static void DrunkWalk(int startX, int startY, int endX, int endY, int fillX)
        {
            int modX = startX > endX ? -1 : 1;
            int modY = startY > endY ? -1 : 1;

            int yWalk = startY;
            int xWalk = startX;

            bool arrived = false;

            while(!arrived)
            {
                int xRand = random.Next(100) < 60 ? 1 : 0;
                int yRand = random.Next(100) < 60 ? 1 : 0;

                var xStep = modX * xRand;
                var yStep = modY * yRand;

                yWalk += yWalk == endY ? 0 : yStep;
                xWalk += xWalk == endX ? 0 : xStep;

                if(xWalk < Width-3 && yWalk < Height-3 && xWalk > 3 && yWalk > 3)
                {
                    Map[xWalk, yWalk].isLand = true;

                    FillTriangle(Math.Min(xWalk, fillX), Math.Max(xWalk, fillX), yWalk);
                }

                arrived = (yWalk == endY) && (xWalk == endX);
            }
        }

        private static void FillTriangle(int startX, int endX, int y)
        {
            for(var i = startX; i < endX; i++)
            {
                Map[i, y].isLand = true;
            }
        }

        private static void SetShores()
        {
            foreach(HexTile tile in Map)
            {
                int x = tile.X;
                int y = tile.Y;

                string code = "0000";

                if (x > 0 && y > 0 && x < Width - 1 && y < Height - 1)
                {
                    if (!tile.isLand)
                    {
                        if(tile.Y % 2 == 0)
                        {
                            code = "";
                            code += Map[x - 1, y].isLand ? "1" : "0";
                            code += Map[x - 1, y + 1].isLand ? "1" : "0";
                            code += Map[x, y + 1].isLand ? "1" : "0";
                            code += Map[x + 1, y].isLand ? "1" : "0";
                        }
                        else
                        {
                            code = "";
                            code += Map[x - 1, y].isLand ? "1" : "0";
                            code += Map[x, y + 1].isLand ? "1" : "0";
                            code += Map[x + 1, y + 1].isLand ? "1" : "0";
                            code += Map[x + 1, y].isLand ? "1" : "0";
                        }
                    }
                }

                tile.TileCode = code;
            }
        }

        private static void SetSettlements()
        {
            int settlementTreshold = 10;
            int settlementCount = 0;
            bool reached = false;

            List<HexTile> temp = Map.Cast<HexTile>().ToList();
            temp = temp.OrderBy(x => Guid.NewGuid()).ToList();

            while (!reached)
            {
                var index = random.Next(0, temp.Count - 1);
                HexTile tile = temp.ElementAt(index);

                if (tile.isLand)
                {
                    Map[tile.X, tile.Y].hasSettlement = true;
                    settlementCount++;
                    reached = settlementCount >= settlementTreshold;
                }

                temp.RemoveAt(index);
            }
        }
    }
}
