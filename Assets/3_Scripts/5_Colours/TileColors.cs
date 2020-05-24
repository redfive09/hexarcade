using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColors : MonoBehaviour
{

    /* ------------------------------ COLOUR CHANGES BY TOUCHING ------------------------------  */

    // what color, when a player arrives at the tile
    [SerializeField] private Color arrivedStandardTile; // 0
    [SerializeField] private Color arrivedCrackedTile; // 1
    [SerializeField] private Color arrivedPathTile; // 2
    [SerializeField] private Color arrivedDistractionTile; // 3
    [SerializeField] private Color arrivedSpecialTile; // 4


    // what color, when a player leaves a tile
    [SerializeField] private Color leftStandardTile; // 5
    [SerializeField] private Color leftPathTile; // 6
    [SerializeField] private Color leftCrackedTile; // 7
    [SerializeField] private Color leftDistractionTile; // 8
    [SerializeField] private Color leftSpecialTile; // 9



    /* ------------------------------ COLOUR INDICATIONS OF LIST TILES AT MAP START ------------------------------  */
    [SerializeField] private float pathTilesColorStartTime;
    [SerializeField] private float pathTilesColorTime;
    [SerializeField] private float pathTimeBeforeFading;
    [SerializeField] private float pathTilesColorFading;
    [SerializeField] private Color pathTilesColor;
    private bool pathTilesColorFinished = false;

    

    [SerializeField] private float startingTilesColorStartTime;
    [SerializeField] private float startingTilesColorTime;
    [SerializeField] private float startingTimeBeforeFading;
    [SerializeField] private float startingTilesColorFading;
    [SerializeField] private Color startingTilesColor;
    private bool startingTilesColorFinished = false;


    [SerializeField] private float winningTilesColorStartTime;
    [SerializeField] private float winningTilesColorTime;
    [SerializeField] private float winningTimeBeforeFading;
    [SerializeField] private float winningTilesColorFading;
    [SerializeField] private Color winningTilesColor;
    private bool winningTilesColorFinished = false;


    [SerializeField] private float checkpointTilesColorStartTime;
    [SerializeField] private float checkpointTilesColorTime;
    [SerializeField] private float checkpointTimeBeforeFading;
    [SerializeField] private float checkpointTilesColorFading;
    [SerializeField] private Color checkpointTilesColor;
    private bool checkpointTilesColorFinished = false;


    [SerializeField] private float distractionTilesColorStartTime;
    [SerializeField] private float distractionTilesColorTime;
    [SerializeField] private float distractionTimeBeforeFading;
    [SerializeField] private float distractionTilesColorFading;
    [SerializeField] private Color distractionTilesColor;
    private bool distractionTilesColorFinished = false;


    [SerializeField] private float specialTilesColorStartTime;
    [SerializeField] private float specialTilesColorTime;
    [SerializeField] private float specialTimeBeforeFading;
    [SerializeField] private float specialTilesColorFading;
    [SerializeField] private Color specialTilesColor;
    private bool specialTilesColorFinished = false;

    [SerializeField] private Color changeColorOfAllTiles;


    private Tiles tiles;
    private TileTypeOptions[] colors;

    // Filling the array and setting up the tiles    
    public void GetStarted()
    {
        SetTiles();

        colors = new TileTypeOptions[] {
        new TileTypeOptions(pathTilesColorStartTime, pathTilesColorTime, pathTimeBeforeFading, pathTilesColorFading, pathTilesColor, tiles.GetPathTiles()),
        new TileTypeOptions(distractionTilesColorStartTime, distractionTilesColorTime, distractionTimeBeforeFading, distractionTilesColorFading, distractionTilesColor, tiles.GetDistractionTiles()),
        new TileTypeOptions(checkpointTilesColorStartTime, checkpointTilesColorTime, checkpointTimeBeforeFading, checkpointTilesColorFading, checkpointTilesColor, tiles.GetCheckpointTiles()),
        new TileTypeOptions(specialTilesColorStartTime, specialTilesColorTime, specialTimeBeforeFading, specialTilesColorFading, specialTilesColor, tiles.GetSpecialTiles()),
        new TileTypeOptions(startingTilesColorStartTime, startingTilesColorTime, startingTimeBeforeFading, startingTilesColorFading, startingTilesColor, tiles.GetStartingTiles()),
        new TileTypeOptions(winningTilesColorStartTime, winningTilesColorTime, winningTimeBeforeFading, winningTilesColorFading, winningTilesColor, tiles.GetWinningTiles())
        };
    }

    public void DisplayTiles()
    {
        for(int i = 0; i < colors.Length; i++)
        {
            StartCoroutine(SetColor(colors[i]));
        }
    }

    /*  This method will make the colour of the path
     *  Works : tiles are colored in with a delay
    **/
    IEnumerator SetColor(TileTypeOptions tileOptions)
    {
        yield return new WaitForSeconds(tileOptions.GetStartingTime()); // wait seconds before continuing with the loop

        Dictionary<int, List<Hexagon>> tiles = tileOptions.GetTiles();
        Dictionary<int, List<Color>> rememberColors = new Dictionary<int, List<Color>>();
        for(int i = 0; i < tiles.Count; i++)
        {               
            rememberColors[i] = new List<Color>();
            List<Hexagon> hexagons = tiles[i];
            
            for(int k = 0; k < hexagons.Count; k++)
            {
                rememberColors[i].Add(hexagons[k].GetColor());
                hexagons[k].SetColor(tileOptions.GetColor());
                
                // Tutorial: https://answers.unity.com/questions/1604527/instantiate-an-array-of-gameobjects-with-a-time-de.html
            }
            yield return new WaitForSeconds(tileOptions.GetTimeToNextTile()); // wait seconds before continuing with the loop
        }
        StartCoroutine(MakePathDisappear(rememberColors, tiles, tileOptions)); //start the disappering backwards
    }

    /*
    * Method goes trough the list of path tiles in the opposite ordner and colors them in the color of all other tiles,
    * in this case: cyan
    */
    IEnumerator MakePathDisappear(Dictionary<int, List<Color>> rememberColors, Dictionary<int, List<Hexagon>> tiles, TileTypeOptions tileOptions)
    {
        yield return new WaitForSeconds(tileOptions.GetTimeBeforeFadingStarts()); // wait the specified seconds in time after entire path has lit up
        for(int i = tiles.Count-1 ; i >= 0 ; i--)
        {
            for(int k = 0; k < tiles[i].Count; k++)
            {
                Color formerColer = rememberColors[i][k];
                tiles[i][k].SetColor(formerColer);
            }

            yield return new WaitForSeconds(tileOptions.GetTimeForEachTileFading()); //wait before continuing with the loop
            // Tutorial: https://answers.unity.com/questions/1604527/instantiate-an-array-of-gameobjects-with-a-time-de.html
        }
        tileOptions.SetFinished(true);
    }

    public bool IsFinished()
    {
        for(int i = 0; i < colors.Length; i++)
        {
            if(!colors[i].IsFinished())
            {
                return false;
            }
        }
        return true;
    }


    /* ------------------------------ EDITOR METHODS FOR PERSISTENT COLOUR CHANGES ------------------------------  */
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

    /*
     * Needs to be sperated and called at so many places for the editor
    */
    private void SetTiles()
    {
        tiles = GetComponent<Tiles>();
    }


    public void GiveColors(HexagonBehaviour hexagon)
    {
        List<Color> colors = new List<Color>();
        colors.Add(arrivedStandardTile);
        colors.Add(arrivedCrackedTile);
        colors.Add(arrivedPathTile);
        colors.Add(arrivedDistractionTile);
        colors.Add(arrivedSpecialTile);
        colors.Add(leftStandardTile);
        colors.Add(leftPathTile);
        colors.Add(leftCrackedTile);
        colors.Add(leftDistractionTile);
        colors.Add(leftSpecialTile);
        hexagon.SetColors(colors);
    }
}