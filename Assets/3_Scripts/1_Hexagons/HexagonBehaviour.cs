using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBehaviour : MonoBehaviour
{

    [SerializeField] private float crackedTileBreaksInSeconds;


    // Color list with its codes
    private List<Color> colors;
    private Color standardColor;
    
    private int arrivedStandardTile = 0; 
    private int arrivedCrackedTile = 1;
    private int arrivedPathTile = 2;
    private int arrivedDistractionTile = 3;
    private int arrivedSpecialTile = 4;

    private int leftStandardTile = 5;
    private int leftPathTile = 6;
    private int leftCrackedTile = 7;
    private int leftDistractionTile = 8;
    private int leftSpecialTile = 9;
    

    private List<Ball> balls = new List<Ball>(); // All the players who are setting on the tile get saved here        
    private Hexagon thisHexagon;


    // Setup standard values for the editor mode
    public void Setup()
    {
        crackedTileBreaksInSeconds = 2f;        
    }

    void Start()
    {
        GetStarted();
    }

    void GetStarted()
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
        
        if(thisHexagon.IsWinningTile())
        {
            print("touched winning tile");
            // StateMachine.LevelUp();
        }
        else if(thisHexagon.IsPathTile())
        {
            thisHexagon.SetColor(colors[arrivedPathTile]);
        }
        else if(thisHexagon.IsCrackedTile())
        {
            thisHexagon.SetColor(colors[arrivedCrackedTile]);
            ActivateCrackedTile();
        }
        else if(thisHexagon.IsDistractionTile())
        {
            thisHexagon.SetColor(colors[arrivedDistractionTile]);
        }
        else if(thisHexagon.IsSpecialTile())
        {
            thisHexagon.SetColor(colors[arrivedSpecialTile]);
        }
        else
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

        if(thisHexagon.IsPathTile())
        {
            thisHexagon.SetColor(colors[leftPathTile]);
        }
        else if(thisHexagon.IsCrackedTile())
        {
            thisHexagon.SetColor(colors[leftCrackedTile]);            
        }
        else if(thisHexagon.IsDistractionTile())
        {
            thisHexagon.SetColor(colors[leftDistractionTile]);
        }
        else if(thisHexagon.IsSpecialTile())
        {
            thisHexagon.SetColor(colors[leftSpecialTile]);
        }
        else
        {
            thisHexagon.SetColor(colors[leftStandardTile]);
        }
    }

  
    /* 
    *  Method gets called to change the color of the cracked tile and destroy it after a delay.
    **/
    void ActivateCrackedTile()
    {
        thisHexagon.DestroyHexagon(false, crackedTileBreaksInSeconds);
        thisHexagon.SetColor(colors[arrivedCrackedTile]);
    }

    /* ------------------------------ SETTER METHODS BEGINN ------------------------------  */
    public void SetColors(List<Color> colors)
    {
        this.colors = colors;
    }
}
