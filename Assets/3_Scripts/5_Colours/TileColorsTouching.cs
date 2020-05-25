using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorsTouching : MonoBehaviour
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



    /* ------------------------------ EDITOR METHODS FOR PERSISTENT COLOUR CHANGES ------------------------------  */
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
