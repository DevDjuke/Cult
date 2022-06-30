using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.App
{
    public class CameraBehaviour : MonoBehaviour
    {
        public Camera cam;

        private bool isScrolling;

        public float panSpeed;
        public float scrollSpeed;

        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
        public float zMin;
        public float zMax;

        private int scrollDir = -1;

        void Start()
        {
            DebugBehaviour boot = GetComponent<DebugBehaviour>();

            panSpeed = 100;
            scrollSpeed = 100;

            int w = boot.Width;
            int h = boot.Height;

            xMin = 0;
            yMin = 0;
            zMin = -1000;

            xMax = w * 100;
            yMax = h * 10;

            zMax = -35;
        }

        private void Update()
        {
                if (0 != Input.GetAxis("Mouse ScrollWheel"))
                {
                    scrollDir = Input.GetAxis("Mouse ScrollWheel") < 0 ? 1 : -1;
                    isScrolling = true;
                }
                else if (Input.GetKey(KeyCode.KeypadPlus))
                {
                    scrollDir = -1;
                    isScrolling = true;
                }
                else if (Input.GetKey(KeyCode.KeypadMinus))
                {
                    scrollDir = +1;
                    isScrolling = true;
                }
                else
                {
                    isScrolling = false;
                }
        }

        void FixedUpdate()
        {
            Vector3 pos = transform.position;

            if (Input.GetKey("z") || Input.GetKey(KeyCode.UpArrow))
            {
                pos.y += panSpeed; 
            }else if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            {
                pos.y -= panSpeed;
            }

            if (Input.GetKey("q") || Input.GetKey(KeyCode.LeftArrow))
            {
                pos.x -= panSpeed;
            }else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            {
                pos.x += panSpeed;
            }

            if (isScrolling)
            {
                pos.z = scrollDir == 1 ? pos.z - scrollSpeed : pos.z + scrollSpeed;
                pos.z = Mathf.Clamp((int)pos.z, (int)(zMin), (int)(zMax));

                //pos.x = Input.mousePosition.x;
                //pos.y = Input.mousePosition.y;

                //transform.position = new Vector3(pos.x, pos.y, Mathf.Lerp(transform.position.z, pos.z, Time.deltaTime * 10));
            }

            pos.x = Mathf.Clamp(pos.x, xMin, xMax);
            pos.y = Mathf.Clamp(pos.y, yMin, yMax);

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, pos.x, Time.deltaTime), Mathf.Lerp(transform.position.y, pos.y, Time.deltaTime), Mathf.Lerp(transform.position.z, pos.z, Time.deltaTime * 10));
        }
    }
}
