using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class HexagonBehaviour : MonoBehaviour
{
    // colour codes for all different scenarios
    private Color[] colors; // all colours for the scenarios right below
    private int arrivedCrackedTile = 0, arrivedPathTile = 1, arrivedDistractionTile = 2, arrivedCheckpointTile = 3, arrivedSpecialTile = 4, arrivedMovingTile = 5, arrivedStartingTile = 6, arrivedWinningTile = 7, arrivedStandardTile = 8;

    private int leftCrackedTile = 9, leftPathTile = 10, leftDistractionTile = 11, leftCheckpointTile = 12, leftSpecialTile = 13, leftMovingTile = 14, leftStartingTile = 15, leftWinningTile = 16, leftStandardTile = 17;
    

    [SerializeField] private float crackedTileTrapsInSeconds;
    [SerializeField] private float fallDepth = 1.5f;
    private float destructionDelay;

    private List<Ball> balls = new List<Ball>(); // All the players who are setting on the tile get saved here
    private Hexagon thisHexagon;
    private bool markedForDestruction = false; // Make sure a hexagon doesn't try to get deleted twice (e. g. crackable tile)
    


    // Setup standard values for the editor mode
    public void Setup()
    {
        crackedTileTrapsInSeconds = 2f;
        fallDepth = 0f;
    }

    void Start()
    {
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();
        destructionDelay = crackedTileTrapsInSeconds * 3.0f;
    }


    /* Method gets called in order to tell the tile that a player stands on it
    *  Depending on its values, the tile knows what to do
    **/
    public void GotOccupied(Ball player)
    {
        balls.Add(player);
        thisHexagon.SetIsTouched(true);        

        if(thisHexagon.IsCrackedTile())
        {            
            thisHexagon.SetColor(colors[arrivedCrackedTile]);
            thisHexagon.GetAudioSource().Play();
            FallAndFade();
            ActivateCrackedTile();
        }

        if(thisHexagon.IsPathTile())
        {
            thisHexagon.SetColor(colors[arrivedPathTile]);
        }

        if(thisHexagon.IsDistractionTile())
        {
            thisHexagon.SetColor(colors[arrivedDistractionTile]);
            this.transform.GetComponent<HexagonDistraction>().DistractionTileTouched(player);
        }

        if(thisHexagon.IsCheckpointTile())
        {
            thisHexagon.SetColor(colors[arrivedCheckpointTile]);
        }

        if(thisHexagon.IsSpecialTile())
        {
            thisHexagon.SetColor(colors[arrivedSpecialTile]);
            this.transform.GetComponent<HexagonSpecial>().SpecialTileTouched(player);
        }

        if(thisHexagon.IsMovingTile())
        {
            thisHexagon.SetColor(colors[arrivedMovingTile]);
            this.transform.GetComponent<HexagonMovingTiles>().MovingTileTouched();
        }
        
        if(thisHexagon.IsStartingTile())
        {
            thisHexagon.SetColor(colors[arrivedStartingTile]);
            player.ArrviedStartingTile();
        }

        if(thisHexagon.IsWinningTile())
        {
            thisHexagon.SetColor(colors[arrivedWinningTile]);
            player.Won();
        }

        if(thisHexagon.IsStandardTile())
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

        if(thisHexagon.IsPathTile())
        {
            thisHexagon.SetColor(colors[leftPathTile]);
        }

        if(thisHexagon.IsDistractionTile())
        {
            thisHexagon.SetColor(colors[leftDistractionTile]);
            this.transform.GetComponent<HexagonDistraction>().DistractionTileLeft(player);
        }

        if(thisHexagon.IsCheckpointTile())
        {
            thisHexagon.SetColor(colors[leftCheckpointTile]);
        }

        if(thisHexagon.IsSpecialTile())
        {
            thisHexagon.SetColor(colors[leftSpecialTile]);
            this.transform.GetComponent<HexagonSpecial>().SpecialTileLeft(player);
        }

        if(thisHexagon.IsMovingTile())
        {
            thisHexagon.SetColor(colors[leftMovingTile]);
            this.transform.GetComponent<HexagonMovingTiles>().MovingTileLeft();
        }
        
        if(thisHexagon.IsStartingTile())
        {
            thisHexagon.SetColor(colors[leftStartingTile]);
            player.LeftStartingTile();
        }

        if(thisHexagon.IsWinningTile())
        {
            thisHexagon.SetColor(colors[leftWinningTile]);
        }

        if(thisHexagon.IsStandardTile())
        {
            thisHexagon.SetColor(colors[leftStandardTile]);
        }
    }


    /* ------------------------------ METHODS FOR CRACKED TILES ------------------------------  */
    /* 
    *  Method gets called to change the color of the cracked tile and destroy it after a delay.
    **/
    private void ActivateCrackedTile()
    {
        if(!markedForDestruction)
        {
            StartCoroutine(Disappear());

            markedForDestruction = true;            
        }        
    }

    private IEnumerator Disappear()
    {        
        float soundEndingTime = thisHexagon.GetAudioSource().clip.length;

        if(destructionDelay < soundEndingTime)
        {
            yield return new WaitForSeconds(destructionDelay);
            transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(soundEndingTime - destructionDelay);
            thisHexagon.DestroyHexagon(false, 0);
        }
        else
        {
            thisHexagon.DestroyHexagon(false, destructionDelay);
        }
    }

    private void FallAndFade()
    {

        LeanTween.moveY(gameObject, gameObject.transform.position.y - fallDepth, destructionDelay);
        LeanTween.alpha(thisHexagon.gameObject, 0.0f, destructionDelay);
        //gameObject.transform.GetChild(0).gameObject
        //LeanTween.alpha 
    }

    /* ------------------------------ SETTER METHODS BEGINN ------------------------------  */
    public void SetColors(Color[] colors)
    {
        this.colors = colors;
    }

    public void SetCrackedTileBreaksInTime(float seconds)
    {
        crackedTileTrapsInSeconds = seconds;
    }

    public void SetFallDepth(float fallDepth)
    {
        this.fallDepth = fallDepth;
    }


    /* ------------------------------ GETTER METHODS BEGINN ------------------------------  */
    public float GetCrackedTileBreaksInTime()
    {
        return crackedTileTrapsInSeconds;
    }

    public Hexagon GetHexagon()
    {
        return thisHexagon;
    }
}
