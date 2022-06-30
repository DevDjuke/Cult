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
    internal class BootBehaviour : MonoBehaviour
    {
        public GameObject TileMap;

        public int Width;
        public int Height;

        private Tilemap map;

        private void Start()
        {
            map = TileMap.GetComponent<Tilemap>();
            WorldGeneratorService.Generate(map, Width, Height);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                map.ClearAllTiles();
                WorldGeneratorService.Generate(map, Width, Height);
            }            
        }
    }
}
