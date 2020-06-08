using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonDistraction : MonoBehaviour
{
    [SerializeField] private bool onlyThisPlatformIsBlinking;
    [SerializeField] private bool randomBlinkingColour;
    [SerializeField] private float blinkingTime;
    

    private int distractionNumber;
    private Hexagon[] platformTiles;
    private Hexagon[] allTiles;

    public void GetStarted(int distractionNumber, Hexagon[] platformTiles, Hexagon[] allTiles)
    {
        this.distractionNumber = distractionNumber;
        this.platformTiles = platformTiles;
        this.allTiles = allTiles;
    }

    public void DistractionTileTouched(Ball player)
    {
        switch(distractionNumber)
        {
            case 0:
                if(onlyThisPlatformIsBlinking)
                {
                    StartBlinking(platformTiles);
                }
                else
                {
                    StartBlinking(allTiles);
                }
            break;
        }
    }

    public void DistractionTileLeft(Ball player)
    {

    }

    private void StartBlinking(Hexagon[] hexagons)
    {
        for(int i = 0; i < hexagons.Length; i++)
        {
            if(hexagons[i] == null)
            {
                Debug.Log("hexagon dead at " + i); // catch nulls!!!
            }
        }
    }
}