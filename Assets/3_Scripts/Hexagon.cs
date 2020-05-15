using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*
     *  Class purpose: Storing values of each individual tile and giving it behaviour, e. g. do something, when the ball stands on it
    **/
    public class Hexagon : MonoBehaviour
    {
        // Different booleans, not all them have setters or getters yet
        [SerializeField] private bool isPath = false;
        [SerializeField] private bool isCrackedTile = false;
        [SerializeField] private bool isStartingTile = false;
        [SerializeField] private bool isWinningTile = false;
        [SerializeField] private bool isMovingTile = false;
        [SerializeField] private bool isCurrentlyOccupied = false;

        // Start and End positions for moving tiles
        [SerializeField] private Vector3 movingTilePosA;
        [SerializeField] private Vector3 movingTilePosB;

        private int currentlyOccupiedCounter = 0; // Counts the number of players, who are currently on the tile

        // Map coordinates, not world coordinates!
        private float x;
        private float z;


        /*
         * Method is called at initiation of the game object.
         * Calls the method to set the positions of moving tiles and performs a coroutine that moves the tiles up and
         * down, as long as isMovingTile is set to true.
         */
        IEnumerator Start()
        {
            SetMovingTilePositions();
            while (isMovingTile) {
                yield return StartCoroutine(MoveObject(transform, movingTilePosA, movingTilePosB, 3));
                yield return StartCoroutine(MoveObject(transform, movingTilePosB, movingTilePosA, 3));
            }
        }

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

        // New
        public void SetIsMovingTile(bool status)
        {
            isMovingTile = status;
        }
        // New

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

        /*
         * Sets the initial positions of moving tiles
         */
        private void SetMovingTilePositions()
        {
            this.movingTilePosA = transform.position;
            Vector3 pointB = transform.position;
            pointB.y = -2;
            this.movingTilePosB = pointB;
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
        **/ // All colour settings and other values like "delay" gotta go to another place later
        public void GotOccupied()
        {
            isCurrentlyOccupied = true;
            currentlyOccupiedCounter++; // Count the number of players on the tile
            
            if(isWinningTile)
            {
                print("touched winning tile");
                // StateMachine.LevelUp();
            }
            else if(isPath & !isCrackedTile)
            {
                SetColor(Color.blue);
            }
            else if(isCrackedTile)
            {
                ActivateCrackedTile();
            }
            else if(!isPath)
            {
                SetColor(Color.red);
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
        }


        /*
        *  Method gets called to change the color of the cracked tile and destroy it after a delay.
        **/
        private void ActivateCrackedTile()
        {
            SetColor(Color.grey);
            float delay = 1f;
            Destroy (gameObject, delay);
        }


        /*
         *  Method gets called to move the tile up and down.
         */
        IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
        {
            float i = 0.0f;
            float rate = 1.0f / time;
            while (i < 1.0f) {
                i += Time.deltaTime * rate;
                thisTransform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }
        }

    } // CLASS END
