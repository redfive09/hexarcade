using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColors : MonoBehaviour
{
    [SerializeField] private Color color;
    Tiles tiles;


    public void ChangeColorOfAllTiles()
    {
        SetTiles();

        List<Platform> platforms = tiles.GetPlatforms();

        for(int i = 0; i < platforms.Count; i++)
        {
            platforms[i].SetColor(color);
        }
    }

    public void ChangePathTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetPathTiles());
    }

    public void ChangeStartingTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetStartingTiles());
    }

    public void ChangeWinningTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetWinningTiles());
    }

    public void ChangeCheckpointTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetCheckpointTiles());
    }

    private void SetTiles()
    {
        tiles = GetComponent<Tiles>();
    }

    private void ChangeColor(Dictionary<int, List<Hexagon>> tiles)
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
}
