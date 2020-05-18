using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBehaviour : MonoBehaviour
{

[SerializeField] private float crackedTileBreaksInSeconds;

// Start and End positions for moving tiles
[SerializeField] private Vector3 movingTilePosA;
[SerializeField] private Vector3 movingTilePosB;
[SerializeField] private float speedOfMovingTiles;

private Color touchedCrackedTile; // when a player is on the tile
private Color detouchedCrackedTile; // when the player left the tile
private List<Ball> balls = new List<Ball>(); // All the players who are setting on the tile get saved here        
private Hexagon thisHexagon;


    // Setup standard values for the editor mode
    public void Setup()
    {
        crackedTileBreaksInSeconds = 2f;
        speedOfMovingTiles = 3f;
    }

    IEnumerator Start()
    {
        GetStarted();

        if(thisHexagon.IsMovingTile())
        {
            movingTilePosA = SetValuesForMovingHexagons(movingTilePosA);
            movingTilePosB = SetValuesForMovingHexagons(movingTilePosB);
            while (thisHexagon.IsMovingTile()) 
            {
                yield return StartCoroutine(MoveObject(this.transform, movingTilePosA, movingTilePosB, speedOfMovingTiles));
                yield return StartCoroutine(MoveObject(this.transform, movingTilePosB, movingTilePosA, speedOfMovingTiles));                        
            }
        }
    }

    void GetStarted()
    {
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();
        /* Set the colors here, like:
        touchedCrackedTile =
        detouchedCrackedTile =  */
    }



    /* Method gets called in order to tell the tile that a player stands on it
    *  Depending on its values, the tile knows what to do
    **/ // All colour settings and other values like "delay" gotta go to another place later
    public void GotOccupied(Ball player)
    {
        balls.Add(player);
        
        if(thisHexagon.IsWinningTile())
        {
            print("touched winning tile");
            // StateMachine.LevelUp();
        }
        else if(thisHexagon.IsPathTile() & !thisHexagon.IsCrackedTile())
        {
            // SetColor(Color.blue);
        }
        else if(thisHexagon.IsCrackedTile())
        {
            ActivateCrackedTile();
        }
        else if(!!thisHexagon.IsPathTile())
        {
            // SetColor(Color.red);
        }
    }


    /* Method gets called in order to tell the tile that a player left the tile
    *  Depending on its values, the tile knows what to do
    **/
    public void GotUnoccupied(Ball player)
    {            
        balls.Remove(player);
    }

  
    /* 
    *  Method gets called to change the color of the cracked tile and destroy it after a delay.
    **/
    void ActivateCrackedTile()
    {
        thisHexagon.DestroyHexagon(false, crackedTileBreaksInSeconds);
        thisHexagon.SetColor(touchedCrackedTile);
    }

    Vector3 SetValuesForMovingHexagons(Vector3 vector)
    {
        if(vector.x == 0)
        {
            vector.x = this.transform.position.x;
        }

        if(vector.y == 0)
        {
            vector.y = this.transform.position.y;
        }

        if(vector.z == 0)
        {
            vector.z = this.transform.position.z;
        }
        return vector;
    }

    /*
    *  Method gets called to move the tile up and down.
    */
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f * time/5;
        while (i < 1.0f) 
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
