﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*
     *  Class purpose: Storing values of each individual tile
    **/
    public class Hexagon : MonoBehaviour
    {
        /*
        *  Negative numbers means false, but these are not booleans anymore
        *  Positive numbers get an additional meaning, for example:
        *  "isPath = 0" is the first tile of the path, "isPath = 1" the second and so on
        *  There can be more tiles which have the same number, which can make sense in the Multiplayer
        *  e.g.: Imagine a map for several players and each player has a different path,
        *  then "isPath = 0" will be the first tiles for the players, and
        *  "isStartingTile = 0" could be the starting tile for the first player,
        *  "isStartingTile = 1" for the second player, etc.
        **/
        [SerializeField] private int isCrackedTile;
        [SerializeField] private int isPath;
        [SerializeField] private int isDistractionTile; // At a different int could be a different distraction
        [SerializeField] private int isCheckpointTile;
        [SerializeField] private int isSpecialTile; // Could be anything, just another option for your creativity :)
        [SerializeField] private int isMovingTile;
        [SerializeField] private int isStartingTile;
        [SerializeField] private int isWinningTile;
        [SerializeField] private Color color;
                
        private List<Ball> balls = new List<Ball>(); // All the players who are setting on the tile get saved here        

        private bool isStandardTile = true; // = no special function at all

        // Platform coordinates, not world coordinates!
        private float x;
        private float z;
        private string originalName;

        // Prepares all the standard values of the [SerializeField] for the editor mode
        public void Setup()
        {
            isCrackedTile = -1;
            isPath = -1;
            isDistractionTile = -1;
            isCheckpointTile = -1;
            isSpecialTile = -1;            
            isMovingTile = -1;
            isStartingTile = -1;
            isWinningTile = -1;
            color = this.transform.GetChild(0).GetComponent<Renderer>().material.color;
            this.GetComponent<HexagonBehaviour>().Setup();
            this.GetComponent<HexagonMovingTiles>().Setup();            
        }


        /*
         * Method is called at initiation of the game object.
         * Calls the method to set the positions of moving tiles and performs a coroutine that moves the tiles up and
         * down, as long as isMovingTile is set to true.
         */
        void Start()
        {
            SetColor();
        }

        /* ------------------------------ SETTER METHODS BEGINN ------------------------------  */
        
        public void SetIsPathTile(int status)
        {
            isPath = status; // negative numbers mean, it is not part of the path
        }
        
        // 0 for player 0;   1 for player 1,    2 for player 2,     etc.
        // Alternative: different starting tiles for each map
        public void SetIsStartingTile(int status)
        {
            isStartingTile = status; // negative numbers mean, it is not part of the path
        }

        // Same like starting tile
        public void SetIsWinningTile(int status)
        {
            isWinningTile = status; // negative numbers mean, it is not a winningTile
        }

        // Same like starting tile
        public void SetIsCheckpointTile(int status)
        {
            isCheckpointTile = status; // negative numbers mean, it is not a winningTile
        }

        // At a different int could be a different distraction
        public void SetIsDistractionTile(int status)
        {
            isDistractionTile = status; // negative numbers mean, it is not a winningTile
        }

        // Could be anything, just another option for your creativity :)
        public void SetIsSpecialTile(int status)
        {
            isSpecialTile = status; // negative numbers mean, it is not a winningTile
        }

        public void SetIsCrackedTile(int status)
        {
            isCrackedTile = status;
        }
     
        public void SetIsMovingTile(int status)
        {
            isMovingTile = status; 
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

        // The first SetColor() method is made for the editor mode, to use the SerializeField color
        public void SetColor()
        {
            SetColor(this.color);
        }

        public void SetColor(Color color)
        {
            this.color = color;
            // this.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.color = color;
            this.transform.GetChild(0).GetComponent<Renderer>().material.color = color;
            
        }

        public void SetStandardTile(bool status)
        {
            isStandardTile = status;
        }

        public void SetOriginalName(string newName)
        {
            originalName = newName;
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

        public Color GetColor()
        {
            return color;
        }

        public bool IsCrackedTile()
        {
            if(isCrackedTile < 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        public int GetCrackedNumber()
        {
            return isCrackedTile;
        }

        public bool IsMovingTile()
        {
            if(isMovingTile < 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        public int GetMovingNumber()
        {
            return isMovingTile;
        }



        public bool IsPathTile()
        {
            if(isPath < 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        public int GetPathNumber()
        {
            return isPath;
        }

        public bool IsStartingTile()
        {
            if(isStartingTile < 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        public int GetStartingNumber()
        {
            return isStartingTile;
        }

        public bool IsWinningTile()
        {
            if(isWinningTile < 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        public int GetWinningNumber()
        {
            return isWinningTile;
        }

        public bool IsCheckpointTile()
        {
            if(isCheckpointTile < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetCheckpointNumber()
        {
            return isCheckpointTile;
        }

        public bool IsDistractionTile()
        {
            if(isDistractionTile < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetDistractionNumber()
        {
            return isDistractionTile;
        }

        public bool IsSpecialTile()
        {
            if(isSpecialTile < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetSpecialNumber()
        {
            return isSpecialTile;
        }

        public bool IsStandardTile()
        {
            return isStandardTile;
        }

        public string GetOriginalName()
        {
            return originalName;
        }


        /* ------------------------------ DELETION METHOD ------------------------------  */
        public void DestroyHexagon(bool inEditor, float seconds)
        {
            /* <-- here needs to be code added, to remove it from all lists in the tiles-Script, 
             *     in case it is a pathTile, startingTile, etc.!!! --> */

            Platform platform = GetComponentInParent<Platform>(); // get the platform the tile is in
            int numberOfHexagonsInPlatform = platform.GetNumberOfHexagons(); // ask it now, platforms dies after last tile got deleted

            platform.RemoveHexagon(this, inEditor); // first tell the platform to remove the tile from the list!
            
            // if it wasn't the last tile in the platform, then destroy it
            if(numberOfHexagonsInPlatform > 1 || this != null)
            {
                if(inEditor)
                {
                    DestroyImmediate(gameObject);
                }
                else
                {
                    Destroy(gameObject, seconds);
                }
            }
        }
    } // CLASS END
