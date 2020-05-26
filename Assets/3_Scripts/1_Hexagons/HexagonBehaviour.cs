using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBehaviour : MonoBehaviour
{

    [SerializeField] private float crackedTileBreaksInSeconds;


    
    private Color standardColor; // Colour at start, 
    private Color[] colors; // all colours for the scenarios right below
    
    // colour codes for all different scenarios
    private int arrivedCrackedTile = 0, arrivedPathTile = 1, arrivedDistractionTile = 2, arrivedCheckpointTile = 3, arrivedSpecialTile = 4, arrivedMovingTile = 5, arrivedStartingTile = 6, arrivedWinningTile = 7, arrivedStandardTile = 8;
    
    private int leftCrackedTile = 9, leftPathTile = 10, leftDistractionTile = 11, leftCheckpointTile = 12, leftSpecialTile = 13, leftMovingTile = 14, leftStartingTile = 15, leftWinningTile = 16, leftStandardTile = 17;

    private List<Ball> balls = new List<Ball>(); // All the players who are setting on the tile get saved here
    private Hexagon thisHexagon;
    private bool markedForDestruction = false; // Make sure a hexagon doesn't try to get deleted twice (e. g. crackable tile)


    // Setup standard values for the editor mode
    public void Setup()
    {
        crackedTileBreaksInSeconds = 2f;        
    }

    void Start()
    {
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();
        standardColor = thisHexagon.GetColor();
    }
    

    /* Method gets called in order to tell the tile that a player stands on it
    *  Depending on its values, the tile knows what to do
    **/ // All colour settings and other values like "delay" gotta go to another place later
    public void GotOccupied(Ball player)
    {
        balls.Add(player);

        if(thisHexagon.IsCrackedTile())
        {
            thisHexagon.SetColor(colors[arrivedCrackedTile]);
            ActivateCrackedTile();
        }

        else if(thisHexagon.IsPathTile())
        {
            thisHexagon.SetColor(colors[arrivedPathTile]);
        }

        else if(thisHexagon.IsDistractionTile())
        {
            thisHexagon.SetColor(colors[arrivedDistractionTile]);
        }

        else if(thisHexagon.IsCheckpointTile())
        {
            thisHexagon.SetColor(colors[arrivedCheckpointTile]);
        }

        else if(thisHexagon.IsSpecialTile())
        {
            thisHexagon.SetColor(colors[arrivedSpecialTile]);
            this.transform.GetComponent<HexagonSpecial>().SpecialTileTouched(player, thisHexagon.GetSpecialTileNumber());
        }

        else if(thisHexagon.IsMovingTile())
        {
            thisHexagon.SetColor(colors[arrivedMovingTile]);
            this.transform.GetComponent<HexagonMovingTiles>().MovingTileTouched();
        }
        
        else if(thisHexagon.IsStartingTile())
        {
            thisHexagon.SetColor(colors[arrivedStartingTile]);            
        }

        else if(thisHexagon.IsWinningTile())
        {
            thisHexagon.SetColor(colors[arrivedWinningTile]);
        }

        else if(thisHexagon.IsStandardTile())
        {
            thisHexagon.SetColor(colors[arrivedStandardTile]);
        }
    }


    /* Method gets called in order to tell the tile that a player left the tile
    *  Depending on its values, the tile knows what to do
    **/
    public void GotUnoccupied(Ball player)
    {            
        balls.Remove(player);

        if(thisHexagon.IsCrackedTile())
        {
            thisHexagon.SetColor(colors[leftCrackedTile]);            
        }

        else if(thisHexagon.IsPathTile())
        {
            thisHexagon.SetColor(colors[leftPathTile]);
        }

        else if(thisHexagon.IsDistractionTile())
        {
            thisHexagon.SetColor(colors[leftDistractionTile]);
        }

        else if(thisHexagon.IsCheckpointTile())
        {
            thisHexagon.SetColor(colors[leftCheckpointTile]);
        }

        else if(thisHexagon.IsSpecialTile())
        {
            thisHexagon.SetColor(colors[leftSpecialTile]);
        }

        else if(thisHexagon.IsMovingTile())
        {
            thisHexagon.SetColor(colors[leftMovingTile]);
            this.transform.GetComponent<HexagonMovingTiles>().MovingTileLeft();
        }
        
        else if(thisHexagon.IsStartingTile())
        {
            thisHexagon.SetColor(colors[leftStartingTile]);            
        }

        else if(thisHexagon.IsWinningTile())
        {
            thisHexagon.SetColor(colors[leftWinningTile]);
        }

        else if(thisHexagon.IsStandardTile())
        {
            thisHexagon.SetColor(colors[leftStandardTile]);
        }
    }

  
    /* 
    *  Method gets called to change the color of the cracked tile and destroy it after a delay.
    **/
    void ActivateCrackedTile()
    {
        if(!markedForDestruction)
        {
            thisHexagon.DestroyHexagon(false, crackedTileBreaksInSeconds);
            markedForDestruction = true;            
        }        
    }

    /* ------------------------------ SETTER METHODS BEGINN ------------------------------  */
    public void SetColors(Color[] colors)
    {
        this.colors = colors;
    }

    public void SetCrackedTileBreaksInTime(float seconds)
    {
        crackedTileBreaksInSeconds = seconds;
    }
}
