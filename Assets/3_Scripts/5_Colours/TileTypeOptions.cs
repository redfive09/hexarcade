using System.Collections.Generic;
using UnityEngine;

public class TileTypeOptions
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
}