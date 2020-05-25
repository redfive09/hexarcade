using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorsPermanent : MonoBehaviour
{
    [SerializeField] private Color crackedTilesColor;
    [SerializeField] private Color pathTilesColor;
    [SerializeField] private Color distractionTilesColor;
    [SerializeField] private Color checkpointTilesColor;
    [SerializeField] private Color specialTilesColor;
    [SerializeField] private Color movingTilesColor;
    [SerializeField] private Color startingTilesColor;
    [SerializeField] private Color winningTilesColor;
    [SerializeField] private Color standardTilesColor;
    [SerializeField] private Color changeColorOfAllTiles;
    private Tiles tiles;


    /*
     * Needs to be sperated and called at so many places for the editor
    */
    private void SetTiles()
    {
        tiles = GetComponent<Tiles>();
    }

    
    public void ChangeColorOfStandardTiles()
    {
        SetTiles();

        List<Hexagon> standardTiles = tiles.GetStandardTiles();

        for(int i = 0; i < standardTiles.Count; i++)
        {
            standardTiles[i].SetColor(standardTilesColor);
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

    public void ChangeCrackedTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetCrackedTiles(), crackedTilesColor);
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

    public void ChangeMovingTilesColor()
    {
        SetTiles();
        ChangeColor(tiles.GetMovingTiles(), movingTilesColor);
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
}
