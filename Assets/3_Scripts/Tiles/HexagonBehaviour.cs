using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBehaviour : MonoBehaviour
{

private List<Ball> balls = new List<Ball>(); // All the players who are setting on the tile get saved here        
private Hexagon thisHexagon;

    public void GetStarted(Hexagon hexagon)
    {
        thisHexagon = hexagon;
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
    *  This method gets called in order to destroy a hexagon. Use always this method for this purpose, never Destroy()!
    *  Parameters: "inEditor" should be "false" if the hexagon should be deleted during Game mode - only inEditor mode "true"!
    **/

    /*
    *  Method gets called to change the color of the cracked tile and destroy it after a delay.
    **/
    void ActivateCrackedTile()
    {
        // SetColor(Color.grey);
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

}
