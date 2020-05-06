using System;
using System.Collections.Generic;
using UnityEngine;

    /*  
     *  Class purpose: Storing values of each individual tile
    **/ 
    public class Hexagon : MonoBehaviour
    {
        private bool isPath = false;

        // Map coordinates, not world coordinates!
        private float x; 
        private float z;

        public void SetIsPath()
        {
            isPath = true;
            SetColor(Color.yellow); // Should not be here, but for now it's okay
        }
        
        // Setting map coordinates, not world coordinates
        public void SetPosition(float x, float z)
        {
            this.x = x;
            this.z = z;
        }

        public void SetColor(Color color)
        {
            gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = color;
        }

        public float GetPositionX()
        {
            return x;
        }

        public float GetPositionZ() 
        {
            return z;
        }
    
    }


