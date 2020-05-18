using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBehaviour : MonoBehaviour
{

[SerializeField] private Color UntouchedCrackedTile;
[SerializeField] private Color TouchedCrackedTile;

// Start and End positions for moving tiles
[SerializeField] private Vector3 movingTilePosA;
[SerializeField] private Vector3 movingTilePosB;

private List<Ball> balls = new List<Ball>(); // All the players who are setting on the tile get saved here        
private Hexagon thisHexagon;


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
        // SetColor(Color.grey);
        float delay = 1f;
        Destroy (gameObject, delay);
    }

    IEnumerator Start()
    {
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();

        if(thisHexagon.IsMovingTile())
        {
            movingTilePosA = SetValuesForMovingHexagons(movingTilePosA);
            movingTilePosB = SetValuesForMovingHexagons(movingTilePosB);
            while (thisHexagon.IsMovingTile()) 
            {
                yield return StartCoroutine(MoveObject(this.transform, movingTilePosA, movingTilePosB, 3));
                yield return StartCoroutine(MoveObject(this.transform, movingTilePosB, movingTilePosA, 3));                        
            }
        }
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
    IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f) 
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
