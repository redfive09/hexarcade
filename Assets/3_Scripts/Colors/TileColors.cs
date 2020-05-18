using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColors : MonoBehaviour
{

    // what color, when a player arrives at the tile
    [SerializeField] private Color arrivedStandardTile; // 0
    [SerializeField] private Color arrivedCrackedTile; // 1
    [SerializeField] private Color arrivedPatchTile; // 2
    [SerializeField] private Color arrivedDistractionTile; // 3
    [SerializeField] private Color arrivedSpecialTile; // 4


    // what color, when a player leaves a tile
    [SerializeField] private Color leftStandardTile; // 5
    [SerializeField] private Color leftPatchTile; // 6
    [SerializeField] private Color leftCrackedTile; // 7
    [SerializeField] private Color leftDistractionTile; // 8
    [SerializeField] private Color leftSpecialTile; // 9


    [SerializeField] private float pathTilesColorTime;
    [SerializeField] private float pathTimeBeforeFading;
    [SerializeField] private float pathTilesColorFading;
    [SerializeField] private Color pathTilesColor;
    

    [SerializeField] private float startingTilesColorTime;
    [SerializeField] private float startingTimeBeforeFading;
    [SerializeField] private float startingTilesColorFading;
    [SerializeField] private Color startingTilesColor;


    [SerializeField] private float winningTilesColorTime;
    [SerializeField] private float winningTimeBeforeFading;
    [SerializeField] private float winningTilesColorFading;
    [SerializeField] private Color winningTilesColor;


    [SerializeField] private float checkpointTilesColorTime;
    [SerializeField] private float checkpointTimeBeforeFading;
    [SerializeField] private float checkpointTilesColorFading;
    [SerializeField] private Color checkpointTilesColor;

    [SerializeField] private float distractionTilesColorTime;
    [SerializeField] private float distractionTimeBeforeFading;
    [SerializeField] private float distractionTilesColorFading;
    [SerializeField] private Color distractionTilesColor;

    [SerializeField] private float specialTilesColorTime;
    [SerializeField] private float specialTimeBeforeFading;
    [SerializeField] private float specialTilesColorFading;
    [SerializeField] private Color specialTilesColor;

    [SerializeField] private Color changeColorOfAllTiles;



    private Tiles tiles;

    /*  Prepares all the standard values of the [SerializeField] for the editor mode
     *  Positive numbers are the actual time in seconds, any other number doesn't make sense
    **/
    public void Setup()
    {
        pathTilesColorTime = 1f;
        pathTimeBeforeFading = 2f;
        pathTilesColorFading = 0.5f;

        startingTilesColorTime = 0f;
        startingTimeBeforeFading = 5f;
        startingTilesColorFading = 10f;

        winningTilesColorTime = 1f;
        winningTimeBeforeFading = 2f;
        winningTilesColorFading = 0.5f;

        checkpointTilesColorTime = 20f;
        checkpointTimeBeforeFading = 0f;
        checkpointTilesColorFading = 0f;

        distractionTilesColorTime = 0f;
        distractionTimeBeforeFading = 0f;
        distractionTilesColorFading = 0f;

        specialTilesColorTime = 0f;
        specialTimeBeforeFading = 0f;        
        specialTilesColorFading = 0f;

    }


    public void GetStarted()
    {
        SetTiles();        
        StartCoroutine(SetColor(pathTilesColor, tiles.GetPathTiles(), pathTilesColorTime, pathTimeBeforeFading, pathTilesColorFading));  // Since the method returns an IEnumerator it has to be calles with startCoroutine
        // StartCoroutine(SetColor(checkpointTilesColor, tiles.GetCheckpointTiles(), checkpointTilesColorTime));
        // StartCoroutine(SetColor(winningTilesColor, tiles.GetWinningTiles(), winningTilesColorTime));
    }


    /*  This method will make the colour of the path
     *  Works : tiles are colored in with a delay
    **/
    IEnumerator SetColor(Color color, Dictionary<int, List<Hexagon>> tiles, float start, float beforeFading, float fading)
    {        
        Dictionary<int, List<Color>> rememberColors = new Dictionary<int, List<Color>>();
        for(int i = 0; i < tiles.Count; i++)
        {               
            rememberColors[i] = new List<Color>();
            List<Hexagon> hexagons = tiles[i];
            
            for(int k = 0; k < hexagons.Count; k++)
            {
                rememberColors[i].Add(hexagons[k].GetColor());
                hexagons[k].SetColor(color);
                
                // Tutorial: https://answers.unity.com/questions/1604527/instantiate-an-array-of-gameobjects-with-a-time-de.html
            }
            yield return new WaitForSeconds(start); // wait seconds before continuing with the loop
        }
        yield return new WaitForSeconds(beforeFading); // wait the specified seconds in time after entire path has lit up
        StartCoroutine(MakePathDisappear(rememberColors, tiles, fading)); //start the disappering backwards
    }

    /*
    * Method goes trough the list of path tiles in the opposite ordner and colors them in the color of all other tiles,
    * in this case: cyan
    */
    IEnumerator MakePathDisappear(Dictionary<int, List<Color>> rememberColors, Dictionary<int, List<Hexagon>> tiles, float fading)
    {        
        for(int i = tiles.Count-1 ; i >= 0 ; i--)
        {
            for(int k = 0; k < tiles[i].Count; k++)
            {
                Color formerColer = rememberColors[i][k];
                tiles[i][k].SetColor(formerColer);
            }

            yield return new WaitForSeconds(fading); //wait before continuing with the loop
            // Tutorial: https://answers.unity.com/questions/1604527/instantiate-an-array-of-gameobjects-with-a-time-de.html
        }
    }


    public void ChangeColorOfAllTiles()
    {
        SetTiles();

        List<Platform> platforms = tiles.GetPlatforms();

        for(int i = 0; i < platforms.Count; i++)
        {
            platforms[i].SetColor(changeColorOfAllTiles);
        }
    }

    public void ChangePathTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetPathTiles(), pathTilesColor);
    }

    public void ChangeStartingTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetStartingTiles(), startingTilesColor);
    }

    public void ChangeWinningTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetWinningTiles(), winningTilesColor);
    }

    public void ChangeCheckpointTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetCheckpointTiles(), checkpointTilesColor);
    }

    public void ChangeDistractionTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetDistractionTiles(), distractionTilesColor);
    }

    public void ChangeSpecialTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetSpecialTiles(), specialTilesColor);
    }

    /*
     * Needs to be sperated and called at so many places for the editor
    */
    private void SetTiles()
    {
        tiles = GetComponent<Tiles>();
    }

    private void ChangeColor(Dictionary<int, List<Hexagon>> tiles, Color color)
    {           
        for(int i = 0; i < tiles.Count; i++)
        {
            List<Hexagon> tilesList = tiles[i];           
            
            for(int k = 0; k < tilesList.Count; k++)
            {
                tilesList[k].SetColor(color);
            }            
        }
    }

    public void GiveColors(HexagonBehaviour hexagon)
    {
        List<Color> colors = new List<Color>();
        colors.Add(arrivedStandardTile);
        colors.Add(arrivedCrackedTile);
        colors.Add(arrivedPatchTile);
        colors.Add(arrivedDistractionTile);
        colors.Add(arrivedSpecialTile);
        colors.Add(leftStandardTile);
        colors.Add(leftPatchTile);
        colors.Add(leftCrackedTile);
        colors.Add(leftDistractionTile);
        colors.Add(leftSpecialTile);
        hexagon.SetColors(colors);
    }
}