using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsCheckpoint : MonoBehaviour
{
    // Settings for CONSTANTS
    private const float ZOOM_OUT_MIN = 2;
    private const float ZOOM_OUT_MAX = 20;
    private const float SENSITIVITY_SCROLL_ZOOM = 10f;
    private const float SENSITIVITY_TOUCH_ZOOM = 0.025f;
    private const float MIN_TIME_BETWEEN_PAN_AND_ZOOM = 0.15f;
    private const float MIN_DISTANCE_FOR_PANNING_RECOGNITION = 0.05f;


    // Fields for checkpoint (de)selection    
    private Dictionary<int, List<Hexagon>> checkpointTiles;
    private Dictionary<Hexagon, Color> rememberOriginalColors = new Dictionary<Hexagon, Color>();
    private Color checkpointTilesColor;
    private Tiles tiles;
    private Ray ray;
    private int numberOfCheckpoints;
    private int chosenCheckpoints = 0;


    // Fields for controls
    private Camera cam;    
    private Touch touch;
    private Vector3 touchStart;
    private float lastZoomTime;
    private bool touchPhaseEnded = true;
    private bool startedPanning = false;

 

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
        if(Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
        }

        if(Input.GetMouseButtonDown(0))
        {
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            ray = cam.ScreenPointToRay(Input.mousePosition);
            touchPhaseEnded = false;
        }

        // Beginn of camera movement during checkpoint selection; thx to this tutorial --> https://pressstart.vip/tutorials/2018/07/12/44/pan--zoom.html
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position) .magnitude;

            float different = currentMagnitude - prevMagnitude;

            zoom(different * SENSITIVITY_TOUCH_ZOOM);
            lastZoomTime = Time.fixedTime;
        }
        
        else if(Input.GetMouseButton(0) && MIN_TIME_BETWEEN_PAN_AND_ZOOM < Time.fixedTime - lastZoomTime)
        {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            if(direction.sqrMagnitude > MIN_DISTANCE_FOR_PANNING_RECOGNITION)
            {                
                cam.transform.position += direction;                
                startedPanning = true;
            }
        } // End of camera movement during checkpoint selection

        if(Input.GetMouseButtonUp(0) && !touchPhaseEnded)
        {
            if(startedPanning)
            {
                startedPanning = false;
            }
            else
            {                
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    GameObject tile = hit.transform.gameObject;
                    if(tile.tag == "Tile")                                                                  // Make sure it's a hexagon
                    {
                        Hexagon clickedTile = tile.GetComponent<Hexagon>();

                        if(clickedTile.IsPathTile() && !clickedTile.IsCheckpointTile()                      // User clicked on a pathTile, which is not a checkpointTile
                            && numberOfCheckpoints - chosenCheckpoints > 0)                                 // and has still remaining checkpoints to choose
                        {
                            SelectCheckpoint(clickedTile);
                        }
                        else if(clickedTile.IsPathTile() && clickedTile.IsCheckpointTile())                 // User clicks on a checkpointTile on the path
                        {
                            DeselectCheckpoint(clickedTile);
                        }                        
                    }
                }                
            }
            touchPhaseEnded = true;           
        }

        zoom(Input.GetAxis("Mouse ScrollWheel") * SENSITIVITY_SCROLL_ZOOM);
    }

    private void zoom(float increment)
    {        
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, ZOOM_OUT_MIN, ZOOM_OUT_MAX);
    }

    private void SelectCheckpoint(Hexagon clickedTile)
    {
        clickedTile.SetIsCheckpointTile(chosenCheckpoints);                             // Tell the tile it is a checkpoint now
        tiles.SaveHexagonInList(checkpointTiles, clickedTile, chosenCheckpoints);       // Add it to the list of checkpoints
        rememberOriginalColors.Add(clickedTile, clickedTile.GetColor());                // Remember the original colour in case user missclicked or decided otherwise
        clickedTile.SetColor(checkpointTilesColor);                                     // Set the checkpoint colour
        chosenCheckpoints++;                                                            // Count up the chosen Checkpoints
        Debug.Log((numberOfCheckpoints - chosenCheckpoints) + " more checkpoint(s) to choose");
    }

    private void DeselectCheckpoint(Hexagon clickedTile)
    {
        tiles.DeleteHexagonInList(checkpointTiles, clickedTile);                        // Remove the hexagon from the checkpoint list
        clickedTile.SetColor(rememberOriginalColors[clickedTile]);                      // Set the original colour
        rememberOriginalColors.Remove(clickedTile);                                     // Remove it from the original colours                        
        clickedTile.SetIsCheckpointTile(-1);                                            // Tell the tile it is not a checkpoint anymore
        chosenCheckpoints--;                                                            // Count down the chosen Checkpoints
        Debug.Log((numberOfCheckpoints - chosenCheckpoints) + " more checkpoint(s) to choose");
    }
}
