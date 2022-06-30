using Assets.Scripts.Service.Assets;
using Assets.Scripts.Service.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.App
{
    public class DebugBehaviour : MonoBehaviour
    {
        public GameObject TileMap;

        public int Width;
        public int Height;

        private Tilemap map;

        private void Start()
        {
            map = TileMap.GetComponent<Tilemap>();

            WorldGeneratorService.Setup(Width, Height);
            MapGeneratorService.SetMap(Width, Height);

            MapGeneratorService.RandomFillMap();
            WorldGeneratorService.world = MapGeneratorService.GetMap();
            WorldGeneratorService.RenderWorld(map);
        }

        float timer = 0;
        bool timerReached = false;
        int stage = 0;  

        private void Update()
        {
            if (!timerReached)
                timer += Time.deltaTime;

            if (!timerReached && timer > 5 && stage == 0)
            {
                MapGeneratorService.SmoothMap(3);
                WorldGeneratorService.world = MapGeneratorService.GetMap();
                WorldGeneratorService.RenderWorld(map);
                stage++;
            }
            if (!timerReached && timer > 8 && stage == 1)
            {
                MapGeneratorService.SmoothMap(3);
                WorldGeneratorService.world = MapGeneratorService.GetMap();
                WorldGeneratorService.RenderWorld(map);
                stage++;
            }
            if (!timerReached && timer > 11 && stage == 2)
            {
                MapGeneratorService.SmoothWater();
                WorldGeneratorService.world = MapGeneratorService.GetMap();
                WorldGeneratorService.RenderWorld(map);
                stage++;
            }
            if (!timerReached && timer > 14 && stage == 3)
            {
                MapGeneratorService.SmoothWater();
                WorldGeneratorService.world = MapGeneratorService.GetMap();
                WorldGeneratorService.RenderWorld(map);
                stage++;
            }
            if (!timerReached && timer > 17 && stage == 4)
            {
                MapGeneratorService.SmoothMap(1);
                WorldGeneratorService.world = MapGeneratorService.GetMap();
                WorldGeneratorService.RenderWorld(map);
                stage++;
            }
            if (!timerReached && timer > 20 && stage == 5)
            {
                MapGeneratorService.SmoothMap(1);
                WorldGeneratorService.world = MapGeneratorService.GetMap();
                WorldGeneratorService.RenderWorld(map);
                stage++;
            }
            if (!timerReached && timer > 23 && stage == 6)
            {
                MapGeneratorService.SmoothMap(1);
                WorldGeneratorService.world = MapGeneratorService.GetMap();
                WorldGeneratorService.RenderWorld(map);
                stage++;
            }
            if (!timerReached && timer > 26 && stage == 7)
            {
                MapGeneratorService.SmoothMap(1);
                WorldGeneratorService.world = MapGeneratorService.GetMap();
                WorldGeneratorService.RenderWorld(map);
                stage++;
            }
            if (!timerReached && timer > 29 && stage == 8)
            {
                MapGeneratorService.SmoothMap(1);
                WorldGeneratorService.world = MapGeneratorService.GetMap();
                WorldGeneratorService.RenderWorld(map);
                stage++;
            }

            if (stage > 8) timerReached = true;
        }
    }
}
