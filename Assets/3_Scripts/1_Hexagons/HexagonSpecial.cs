using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonSpecial : MonoBehaviour
{
    [SerializeField] private bool teleporterGoingBack;

    private List<Ball> players = new List<Ball>();
    private int specialCase;
    private Dictionary<int, List<Hexagon>> specialTiles;
    private Hexagon thisHexagon;

    public void GetStarted(Dictionary<int, List<Hexagon>> specialTiles)
    {
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();
        specialCase = thisHexagon.GetStartingNumber();
        this.specialTiles = specialTiles;
    }

    public void SpecialTileTouched(Ball player)
    {
        players.Add(player);

        if(specialCase == 0) // Teleporter entry
        {
            int getIndexNumberInList = GetIndexNumberInList();
            
            if(getIndexNumberInList >= 0)
            {
                // 1. make it possible to go through (disable some stuff from the hexagon for that)
                // 2. when player is more than half in it, then beam it to the hexagon of specialCase 1 at the same index
                // 3. if "teleporterGoingBack", then switch the specialCase for both tiles

            }
        }

        else if(specialCase == 1) // Teleporter exit
        {
            // not done yet
        }
    }


    public void SpecialTileLeft(Ball player)
    {
        players.Remove(player);

        if(specialCase == 0)
        {
            
        }
    }

    private int GetIndexNumberInList()
    {
        for(int i = 0; i < specialTiles[specialCase].Count; i++)
        {
            if(specialTiles[specialCase][i] == thisHexagon)
            {
                return i;
            }
        }
        return int.MinValue;
    }

}
