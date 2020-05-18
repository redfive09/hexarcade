using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*
     *  Class purpose: Storing values of each individual tile and giving it behaviour, e. g. do something, when the ball stands on it
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
        [SerializeField] private int isPath;
        [SerializeField] private int isStartingTile;
        [SerializeField] private int isWinningTile;
        [SerializeField] private int isCheckpointTile;


        // Different booleans, not all them have setters or getters yet
        [SerializeField] private bool isCrackedTile;
        [SerializeField] private bool isMovingTile;
        

   
        
        [SerializeField] private Color color;

        private List<Ball> balls = new List<Ball>(); // All the players who are setting on the tile get saved here        
        
        // Map coordinates, not world coordinates!
        private float x;
        private float z;

        // Prepares all the standard values of the [SerializeField] for the editor mode
        public void Setup()
        {
            isPath = -1;
            isStartingTile = -1;
            isWinningTile = -1;
            isCheckpointTile = -1;
            isCrackedTile = false;
            isMovingTile = false;
            color = this.transform.GetChild(0).GetComponent<Renderer>().material.color;
            this.GetComponent<HexagonBehaviour>().Setup();
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
        
        public void SetIsPath(int status)
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

        public void SetIsCrackedTile(bool status)
        {
            isCrackedTile = status;
        }
     
        public void SetIsMovingTile(bool status)
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

        /*
         * Sets the initial positions of moving tiles
         */
        // private void SetMovingTilePositions()
        // {
        //     this.movingTilePosA = transform.position;
        //     Vector3 pointB = transform.position;
        //     pointB.y = -2;
        //     this.movingTilePosB = pointB;
        // }


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
            return isCrackedTile;
        }

        public bool IsMovingTile()
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


        /* ------------------------------ METHODS FOR EDITOR MODE ------------------------------  */
        public void PrintCurrentWorldPosition()
        {
            Debug.Log(this.transform.parent.transform.position - this.transform.position);
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
            if(numberOfHexagonsInPlatform > 1)
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
