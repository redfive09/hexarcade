using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsCheckpoint : MonoBehaviour
{
    private bool finished = false;
    private int numberOfRemainingCheckpoints;
    Dictionary<int, List<Hexagon>> checkpointTiles;

   public void GetStarted(int numberOfCheckpoints, Dictionary<int, List<Hexagon>> checkpointTiles)
   {
       this.checkpointTiles = checkpointTiles;
       numberOfRemainingCheckpoints = numberOfCheckpoints;
       Debug.Log("Choose " + numberOfCheckpoints + " path tiles as checkpoints now");
   }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus)) // Of course it should, player clicked on a 
        {
            numberOfRemainingCheckpoints--;
            Debug.Log(numberOfRemainingCheckpoints + " more checkpoint(s) to choose");
            // Colour checkpoint
        }

                // maybe this way: https://forum.unity.com/threads/detecting-mouse-click-on-object.19450/
        // for(int i = 0; i < numberOfCheckpoints; i++)
        // {
        //     checkpointTiles[i].Add(clickedHexagon);      // add all the choosen checkpoints to the list!
        // }
    }

    public bool IsFinished()
    {
        return numberOfRemainingCheckpoints == 0;
    }

    public Dictionary<int, List<Hexagon>> GetCheckpointTiles()
    {
        return checkpointTiles;
    }


}
