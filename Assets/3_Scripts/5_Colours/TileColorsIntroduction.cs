using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorsIntroduction : MonoBehaviour
{
    /* ------------------------------ COLOUR INDICATIONS OF LIST TILES AT MAP START ------------------------------  */
    [SerializeField] private float crackedTilesColorStartTime;
    [SerializeField] private float crackedTilesColorTime;
    [SerializeField] private float crackedTimeBeforeFading;
    [SerializeField] private float crackedTilesColorFading;
    [SerializeField] private Color crackedTilesColor;


    [SerializeField] private float pathTilesColorStartTime;
    [SerializeField] private float pathTilesColorTime;
    [SerializeField] private float pathTimeBeforeFading;
    [SerializeField] private float pathTilesColorFading;
    [SerializeField] private Color pathTilesColor;
    

    [SerializeField] private float distractionTilesColorStartTime;
    [SerializeField] private float distractionTilesColorTime;
    [SerializeField] private float distractionTimeBeforeFading;
    [SerializeField] private float distractionTilesColorFading;
    [SerializeField] private Color distractionTilesColor;


    [SerializeField] private float checkpointTilesColorStartTime;
    [SerializeField] private float checkpointTilesColorTime;
    [SerializeField] private float checkpointTimeBeforeFading;
    [SerializeField] private float checkpointTilesColorFading;
    [SerializeField] private Color checkpointTilesColor;


    [SerializeField] private float specialTilesColorStartTime;
    [SerializeField] private float specialTilesColorTime;
    [SerializeField] private float specialTimeBeforeFading;
    [SerializeField] private float specialTilesColorFading;
    [SerializeField] private Color specialTilesColor;


    [SerializeField] private float movingTilesColorStartTime;
    [SerializeField] private float movingTilesColorTime;
    [SerializeField] private float movingTimeBeforeFading;
    [SerializeField] private float movingTilesColorFading;
    [SerializeField] private Color movingTilesColor;


    [SerializeField] private float startingTilesColorStartTime;
    [SerializeField] private float startingTilesColorTime;
    [SerializeField] private float startingTimeBeforeFading;
    [SerializeField] private float startingTilesColorFading;
    [SerializeField] private Color startingTilesColor;


    [SerializeField] private float winningTilesColorStartTime;
    [SerializeField] private float winningTilesColorTime;
    [SerializeField] private float winningTimeBeforeFading;
    [SerializeField] private float winningTilesColorFading;
    [SerializeField] private Color winningTilesColor;


    private Tiles tiles;
    private TileTypeOptions[] colors;

    // Filling the array and setting up the tiles
    public void GetStarted()
    {
        tiles = GetComponent<Tiles>();

        colors = new TileTypeOptions[] {
        new TileTypeOptions(crackedTilesColorStartTime, crackedTilesColorTime, crackedTimeBeforeFading, crackedTilesColorFading, crackedTilesColor, tiles.GetCrackedTiles()),
        new TileTypeOptions(pathTilesColorStartTime, pathTilesColorTime, pathTimeBeforeFading, pathTilesColorFading, pathTilesColor, tiles.GetPathTiles()),
        new TileTypeOptions(distractionTilesColorStartTime, distractionTilesColorTime, distractionTimeBeforeFading, distractionTilesColorFading, distractionTilesColor, tiles.GetDistractionTiles()),
        new TileTypeOptions(checkpointTilesColorStartTime, checkpointTilesColorTime, checkpointTimeBeforeFading, checkpointTilesColorFading, checkpointTilesColor, tiles.GetCheckpointTiles()),
        new TileTypeOptions(specialTilesColorStartTime, specialTilesColorTime, specialTimeBeforeFading, specialTilesColorFading, specialTilesColor, tiles.GetSpecialTiles()),
        new TileTypeOptions(movingTilesColorStartTime, movingTilesColorTime, movingTimeBeforeFading, movingTilesColorFading, movingTilesColor, tiles.GetMovingTiles()),
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


    private class TileTypeOptions
    {
        private float startingTime;
        private float timeToNextTile;
        private float timeBeforeFadingStarts;
        private float timeForEachTileFading;
        private Color color;
        private Dictionary<int, List<Hexagon>> tiles;
        private bool finished;


        public TileTypeOptions (float startingTime, float timeToNextTile, float timeBeforeFadingStarts, 
                                float timeForEachTileFading, Color color, Dictionary<int, List<Hexagon>> tiles)
        {
            this.startingTime = startingTime;
            this.timeToNextTile = timeToNextTile;
            this.timeBeforeFadingStarts = timeBeforeFadingStarts;
            this.timeForEachTileFading = timeForEachTileFading;
            this.color = color; 
            this.tiles = tiles;
        }

        public float GetStartingTime()
        {
            return startingTime;
        }

        public float GetTimeToNextTile()
        {
            return timeToNextTile;
        }

        public float GetTimeBeforeFadingStarts()
        {
            return timeBeforeFadingStarts;
        }

        public float GetTimeForEachTileFading()
        {
            return timeForEachTileFading;
        }

        public Color GetColor()
        {
            return color;
        }

        public Dictionary<int, List<Hexagon>> GetTiles()
        {
            return tiles;
        }

        public bool IsFinished()
        {
            return finished;
        }

        public void SetFinished(bool status)
        {
            finished = status;
        }
    }
}