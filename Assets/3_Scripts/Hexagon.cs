using System;
using System.Collections.Generic;
using UnityEngine;

    /*  
     *  Class purpose: Storing values of each individual tile and giving it behaviour, e. g. do something, when the ball stands on it
    **/ 
    public class Hexagon : MonoBehaviour
    {
        // Different booleans, not all them have setters or getters yet
        private bool isPath = false;
        private bool isCrackedTile = true;
        private bool isWinningTile = false;
        private bool isCurrentlyOccupied = false;
        private int currentlyOccupiedCounter = 0;
        

        // Map coordinates, not world coordinates!
        private float x;
        private float z;


        /* ------------------------------ SETTER METHODS BEGINN ------------------------------  */
        public void SetIsPath(bool status)
        {
            isPath = status;            
        }

        public void SetIsCrackedTile(bool status)
        {
            isCrackedTile = status;
        }

        public void SetIsWinningTile(bool status)
        {
            isWinningTile = status;
        }
        
        // Setting map coordinates, not world coordinates
        public void SetMapPosition(float x, float z)
        {
            this.x = x;
            this.z = z;
        }

        public void SetWorldPosition(Vector3 pos)
        {
            transform.position = pos;
            // gameObject.transform.GetChild(0).transform.position = pos; // Probably child has to be moved as well, so this has to be tested if it is neccessary
        }

        public void SetColor(Color color)
        {
            gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = color;
        }

        public void SetIsCurrentlyOccupied(bool status)
        {
            isCurrentlyOccupied = status;
        }


        /* ------------------------------ GETTER METHODS BEGINN ------------------------------  */

        public float GetPositionX()
        {
            return x;
        }

        public float GetPositionZ() 
        {
            return z;
        }

        public bool GetIsCurrentlyOccupied()
        {
            return isCurrentlyOccupied;
        }

        public bool GetIsWinningTile()
        {
            return isWinningTile;
        }


        /* ------------------------------ BEHAVIOUR METHODS BEGINN ------------------------------  */

        /* Method gets called in order to tell the tile that a player stands on it 
        *  Depending on its values, the tile knows what to do
        **/
        public void GotOccupied()
        {
            isCurrentlyOccupied = true;
            currentlyOccupiedCounter++; // Count the number of players on the tile

            
            // All colour settings and other values like "delay" gotta go to another place later
            if(isPath)
            {
                SetColor(Color.red);
            }
            else if(isCrackedTile)
            {
                SetColor(Color.black);
                float delay = 2f;
                Destroy (gameObject, delay);
            }
        }


        /* Method gets called in order to tell the tile that a player left the tile
        *  Depending on its values, the tile knows what to do
        **/
        public void GotUnoccupied()
        {            

            // First check, if no more player is on the field
            currentlyOccupiedCounter--;
            if (currentlyOccupiedCounter == 0)
            {
                isCurrentlyOccupied = false;
            }

            if(!isCurrentlyOccupied)
            {
                SetColor(Color.blue);
            }

            // Maybe do something else
        }


        void Start()
        {
            gameObject.AddComponent<WinScenario>();
        }
    } // CLASS END


