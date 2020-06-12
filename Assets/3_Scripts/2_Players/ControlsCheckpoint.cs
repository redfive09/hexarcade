using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsCheckpoint : MonoBehaviour
{
    private bool finished = false;
    private int numberOfCheckpoints;
    private int chosenCheckpoints = 0;
    private Dictionary<int, List<Hexagon>> checkpointTiles;
    private Dictionary<Hexagon, Color> rememberOriginalColors = new Dictionary<Hexagon, Color>();
    private Color checkpointTilesColor;
    private Tiles tiles;
    

   public void GetStarted(int numberOfCheckpoints, Dictionary<int, List<Hexagon>> checkpointTiles, Tiles tiles, CameraFollow cameraFollow)
   {
       this.tiles = tiles;
       this.checkpointTiles = checkpointTiles;
       this.numberOfCheckpoints = numberOfCheckpoints;
       this.checkpointTilesColor = tiles.GetComponent<TileColorsPermanent>().GetCheckpointTilesColor();
       Debug.Log("Choose " + numberOfCheckpoints + " path tiles as checkpoints now");
       Debug.Log("Press C for confirming the choices");
   }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Ray ray;
            if (Input.touchCount > 0)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject tile = hit.transform.gameObject;

                if(tile.tag == "Tile")                                                                  // Make sure it's a hexagon
                {
                    Hexagon clickedTile = tile.GetComponent<Hexagon>();                    

                    if(clickedTile.IsPathTile() && !clickedTile.IsCheckpointTile()                      // User is selecting a pathTile for a checkpointTile
                        && numberOfCheckpoints - chosenCheckpoints > 0)                                 // and has still remaining checkpoints to choose
                    {
                        clickedTile.SetIsCheckpointTile(chosenCheckpoints);                             // Tell the tile it is a checkpoint now
                        tiles.SaveHexagonInList(checkpointTiles, clickedTile, chosenCheckpoints);       // Add it to the list of checkpoints
                        rememberOriginalColors.Add(clickedTile, clickedTile.GetColor());                // Remember the original colour in case user missclicked or decided otherwise
                        clickedTile.SetColor(checkpointTilesColor);                                     // Set the checkpoint colour
                        chosenCheckpoints++;                                                            // Count up the chosen Checkpoints                        
                        Debug.Log((numberOfCheckpoints - chosenCheckpoints) + " more checkpoint(s) to choose");
                    }
                    else if(clickedTile.IsPathTile() && clickedTile.IsCheckpointTile())                 // User is unselecting a checkpointTile on the path
                    {
                        tiles.DeleteHexagonInList(checkpointTiles, clickedTile);                        // Remove the hexagon from the checkpoint list
                        clickedTile.SetColor(rememberOriginalColors[clickedTile]);                      // Set the original colour
                        rememberOriginalColors.Remove(clickedTile);                                     // Remove it from the original colours
                        chosenCheckpoints--;                                                            // Count down the chosen Checkpoints
                        clickedTile.SetIsCheckpointTile(-1);                                            // Tell the tile it is not a checkpoint anymore
                        Debug.Log((numberOfCheckpoints - chosenCheckpoints) + " more checkpoint(s) to choose");
                    }
                }
            }
        }

        // Game continues when player is finishing up
        if(Input.GetKeyDown(KeyCode.C))
        {
            finished = true;
        }
    }

    public bool IsFinished()
    {
        return finished;
    }

    public Dictionary<int, List<Hexagon>> GetCheckpointTiles()
    {
        return checkpointTiles;
    }


}
