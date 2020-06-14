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
    private Camera cam;
    private Vector3 touchStart;
    private float zoomOutMin = 2;
    private float zoomOutMax = 20;
    private float sensitivityScrollZoom = 10f;
    private float sensitivityTouchZoom = 0.025f;
    

   public void GetStarted(int numberOfCheckpoints, Dictionary<int, List<Hexagon>> checkpointTiles, Tiles tiles, Camera cam)
   {
       this.tiles = tiles;
       this.checkpointTiles = checkpointTiles;
       this.numberOfCheckpoints = numberOfCheckpoints;
       this.checkpointTilesColor = tiles.GetComponent<TileColorsPermanent>().GetCheckpointTilesColor();
       this.cam = cam;
       cam.orthographicSize = (cam.orthographicSize + cam.GetComponent<CameraFollow>().GetOffset().y) / 2.25f;
       Debug.Log("Choose " + numberOfCheckpoints + " path tiles as checkpoints now");
   }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            // Beginn of process of identifying and selecting or unselecting checkpoints
            Ray ray;
            if (Input.touchCount > 0)
            {
                ray = cam.ScreenPointToRay(Input.GetTouch(0).position);
            }
            else
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
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
                        clickedTile.SetIsCheckpointTile(-1);                                            // Tell the tile it is not a checkpoint anymore
                        chosenCheckpoints--;                                                            // Count down the chosen Checkpoints
                        Debug.Log((numberOfCheckpoints - chosenCheckpoints) + " more checkpoint(s) to choose");
                    }
                }
            }
            // End of process of identifying and selecting or unselecting checkpoints
            
            // Beginn of camera movement during checkpoint selection; thx to this tutorial --> https://pressstart.vip/tutorials/2018/07/12/44/pan--zoom.html
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position) .magnitude;

            float different = currentMagnitude - prevMagnitude;

            zoom(different * sensitivityTouchZoom);
        }
        
        else if(Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel") * sensitivityScrollZoom);
    }

    private void zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, zoomOutMin, zoomOutMax);
    } // End of camera movement during checkpoint selection

    public bool IsFinished()
    {
        return finished;
    }
}
