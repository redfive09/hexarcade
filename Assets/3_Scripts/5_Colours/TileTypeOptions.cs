using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTypeOptions : MonoBehaviour
{
private float startingTime;
private float timeToNextTile;
private float timeBeforeFadingStarts;
private float timeForEachTileFading;
private Color color;
private Dictionary<int, List<Hexagon>> tiles;
private bool finished;


    public TileTypeOptions(float startingTime, float timeToNextTile, float timeBeforeFadingStarts, 
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

    // public void StartColoring()
    // {
    //     StartCoroutine(SetColor());
    //     Debug.Log("colors");
    // }

    // IEnumerator SetColor()
    // {
    //     yield return new WaitForSeconds(startingTime); // wait seconds before continuing with the loop
    //     Dictionary<int, List<Color>> rememberColors = new Dictionary<int, List<Color>>();
    //     for(int i = 0; i < tiles.Count; i++)
    //     {               
    //         rememberColors[i] = new List<Color>();
    //         List<Hexagon> hexagons = tiles[i];
            
    //         for(int k = 0; k < hexagons.Count; k++)
    //         {
    //             rememberColors[i].Add(hexagons[k].GetColor());
    //             hexagons[k].SetColor(color);
                
    //             // Tutorial: https://answers.unity.com/questions/1604527/instantiate-an-array-of-gameobjects-with-a-time-de.html
    //         }
    //         yield return new WaitForSeconds(timeToNextTile); // wait seconds before continuing with the loop
    //     }
    //     yield return new WaitForSeconds(timeBeforeFadingStarts); // wait the specified seconds in time after entire path has lit up
    //     StartCoroutine(MakePathDisappear(rememberColors)); //start the disappering backwards
    // }

    //     IEnumerator MakePathDisappear(Dictionary<int, List<Color>> rememberColors)
    // {        
    //     for(int i = tiles.Count-1 ; i >= 0 ; i--)
    //     {
    //         for(int k = 0; k < tiles[i].Count; k++)
    //         {
    //             Color formerColer = rememberColors[i][k];
    //             tiles[i][k].SetColor(formerColer);
    //         }

    //         yield return new WaitForSeconds(timeForEachTileFading); //wait before continuing with the loop
    //         // Tutorial: https://answers.unity.com/questions/1604527/instantiate-an-array-of-gameobjects-with-a-time-de.html
    //     }
    //     finished = true;
    // }

}