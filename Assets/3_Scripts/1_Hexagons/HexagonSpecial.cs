using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonSpecial : MonoBehaviour
{    

    /* ------------------------------ SerializeFields ------------------------------  */
    // Teleporter values
    [SerializeField] private bool teleporterEntrance;
    [SerializeField] private int teleporterNumber;
    [SerializeField] private int teleporterConnectedWith;

    [SerializeField] private float velocity;


    /* ------------------------------ USEAGE OF SPECIAL-TILES ------------------------------  */  
    private const int TELEPORTER = 0;
    private const int VELOCITY = 1;


    /* ------------------------------ GENERAL INFORMATION FOR DIFFERENT OPERATIONS ------------------------------  */    
    private List<Ball> players = new List<Ball>();
    private int specialCase;
    private Dictionary<int, List<Hexagon>> specialTiles;
    private int getIndexNumberInList;
    private Hexagon thisHexagon;
    


    /* ------------------------------ MAIN METHODS FOR SPECIAL TILES ------------------------------  */
    public void GetStarted(Dictionary<int, List<Hexagon>> specialTiles)
    {
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();
        specialCase = thisHexagon.GetSpecialNumber();
        this.specialTiles = specialTiles;
    }


    public void SpecialTileTouched(Ball player)
    {
        players.Add(player);
        getIndexNumberInList = GetIndexNumberInList();

        if(getIndexNumberInList >= 0) // catch potential errors with the special tile list
        {        
            if(specialCase == TELEPORTER)
            {
                

                // 1. make it possible to go through (disable some stuff from the hexagon for that)
                // 2. when player is more than half in it, then beam it to the hexagon of specialCase 1 at the same index
                // 3. if "teleporterGoingBack", then switch the specialCase for both tiles

                
            }

            else if(specialCase == VELOCITY)
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                Vector3 currentVelocity = rb.velocity;
                currentVelocity *= velocity;
                rb.velocity = currentVelocity;
            }
        }
    }

    /* ------------------------------ GENERAL METHODS FOR SPECIAL TILES ------------------------------  */
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

    
    /* ------------------------------ SPECIFIC METHODS FOR TELEPORTERS ------------------------------  */

    private HexagonSpecial FindTeleporterExit(int teleporterConnectedWith)
    {
        List<Hexagon> teleporterList = specialTiles[TELEPORTER];

        for(int i = 0; i < teleporterList.Count; i++)
        {
            HexagonSpecial teleporter = teleporterList[i].GetComponent<HexagonSpecial>();

            if(teleporter.GetTeleporterNumber() == teleporterConnectedWith)
            {
                return teleporter;
            }
        }
        return null;
    }

    public int GetTeleporterNumber()
    {
        return teleporterNumber;
    }

    /* ------------------------------ SETTERS FOR SPECIAL TILES ------------------------------  */

    public void SetVelocity(float velocity)
    {
        this.velocity = velocity;
    }



    /* ------------------------------ EDITOR METHODS ------------------------------  */

    public string GetNameOfFunction()
    {
        if(getIndexNumberInList >= 0) // catch potential errors with the special tile list
        {
            string prefix = " -> ";
            if(specialCase == TELEPORTER) return prefix + nameof(TELEPORTER).ToLower();
            if(specialCase == VELOCITY) return prefix + nameof(VELOCITY).ToLower();

            
        }
        return "";
    }
}
