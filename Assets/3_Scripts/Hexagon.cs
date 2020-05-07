using System;
using System.Collections.Generic;
using UnityEngine;

    /*  Class purpose: Storing values of each individual tile
     *  ToFix: Timing logic for different scenarios, e. g. the ball goes over a crackedTile, it should disappear after a few seconds and maybe make some effects doing so
    **/ 
    public class Hexagon : MonoBehaviour
    {
        // Different booleans, not all them have setters or getters yet
        private bool isPath = false;
        private bool isCrackedTile = false;
        private bool isWinningTile = false;
        

        // Map coordinates, not world coordinates!
        private float x;
        private float z;

        public void SetIsPath(bool status)
        {
            isPath = status;            
        }

        public void SetIsCrackedTile(bool status)
        {
            isCrackedTile = status;
        }
        
        // Setting map coordinates, not world coordinates
        public void SetMapPosition(float x, float z)
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

        public void SetWorldPosition(Vector3 pos)
        {
            gameObject.transform.position = pos;
            // gameObject.transform.GetChild(0).transform.position = pos; // Probably child has to be moved as well, so this has to be tested if it is neccessary
        }
    }


