using System;
using System.Collections.Generic;
using UnityEngine;


    public class Hexagon : MonoBehaviour
    {
        private bool isPath = false;
        private float x;
        private float z;
        
        void Start()
        {
            // if (isPartOfPath())
            // {
            //     gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.yellow;
            //     isPath = true;
            // }
        }

        private void Update()
        {
 
        }

        public void setIsPath()
        {
            isPath = true;
            gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.yellow;
        }
        
        public void setPosition(float x, float z) 
        {
            this.x = x;
            this.z = z;
        }

        public float getPositionX() 
        {
            return x;
        }

        public float getPositionZ() 
        {
            return z;
        }
    
    }


