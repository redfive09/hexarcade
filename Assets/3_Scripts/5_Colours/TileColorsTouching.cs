using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorsTouching : MonoBehaviour
{

    /* ------------------------------ COLOUR CHANGES BY TOUCHING ------------------------------  */

    // what color, when a player arrives at the tile
    
    [SerializeField] private Color arrivedCrackedTile; // 0
    [SerializeField] private Color arrivedPathTile; // 1
    [SerializeField] private Color arrivedDistractionTile; // 2
    [SerializeField] private Color arrivedCheckpointTile; // 3
    [SerializeField] private Color arrivedSpecialTile; // 4
    [SerializeField] private Color arrivedMovingTile; // 5
    [SerializeField] private Color arrivedStartingTile; // 6
    [SerializeField] private Color arrivedWinningTile; // 7
    [SerializeField] private Color arrivedStandardTile; // 8


    // what color, when a player leaves a tile
    [SerializeField] private Color leftCrackedTile; // 9        
    [SerializeField] private Color leftPathTile; // 10    
    [SerializeField] private Color leftDistractionTile; // 11
    [SerializeField] private Color leftCheckpointTile; // 12
    [SerializeField] private Color leftSpecialTile; // 13
    [SerializeField] private Color leftMovingTile; // 14
    [SerializeField] private Color leftStartingTile; // 15
    [SerializeField] private Color leftWinningTile; // 16
    [SerializeField] private Color leftStandardTile; // 17


    /* ------------------------------ EDITOR METHODS FOR PERSISTENT COLOUR CHANGES ------------------------------  */
    public Color[] GiveColorSet()
    {
        return new Color[] {
            arrivedCrackedTile,
            arrivedPathTile,
            arrivedDistractionTile,
            arrivedCheckpointTile,
            arrivedSpecialTile,
            arrivedMovingTile,
            arrivedStartingTile,
            arrivedWinningTile,
            arrivedStandardTile,
            leftCrackedTile,
            leftPathTile,
            leftDistractionTile,
            leftCheckpointTile,
            leftSpecialTile,
            leftMovingTile,
            leftStartingTile,
            leftWinningTile,
            leftStandardTile
        };
    }
}
