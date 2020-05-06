using System.Collections.Generic;
using UnityEngine;

/*  
*  Class purpose: Creating a new map and returning all its tiles
**/ 
public class MapGenerator : MonoBehaviour
{    
    [SerializeField] GameObject hexTilePrefab; // For used hexagon tile


    // Offset are used for the gap between the tiles
    [SerializeField] float tileXOffset = 1.8f;
    [SerializeField] float tileZOffset = 1.565f;


    // Saves all positions of all tiles
    // Not used yet, but maybe we need it later
    private List<Vector3> tilePos = new List<Vector3>();



    /*  This method checks first all the parameters, if something is wrong, then it prints a message to the console
     *  Returns: List of hexagon tiles of a new map with the entered values
    **/     
    public List<GameObject> GenerateMap(int mapWidth, int mapHeight, float mapRadius, string folderName)
    {
        if(mapWidth > 1 && mapHeight > 1 && mapRadius >= 2)
        {
            return NewHexTileMap(mapWidth, mapHeight, mapRadius, folderName);
        }
        else
        {
            Debug.Log(
            "Couldn't generate a map, since there's a problem with at least one of the map values:" + "\n" +
            "mapWidth: " + mapWidth + " || " + "mapHeight: " + mapHeight + " || " + "mapRadius: " + mapRadius
            );
            return null;
        }
    }


    /*  The method creates a new hexagon tile map, which can be found in a new folder in the hierachy
     *  Returns: List of hexagon tiles of a new map with the entered values 
     *  Made with the help of this tutorial: https://www.youtube.com/watch?v=BE54igXh5-Q
    **/ 
    List<GameObject> NewHexTileMap(int mapWidth, int mapHeight, float mapRadius, string folderName)
    {
        List<GameObject> allTiles = new List<GameObject>(); // all the created tiles will be added here
        var tilesFolder = new GameObject(); // this will contain all the tile objects in the hierachy
        tilesFolder.name = folderName;
        
        // The following calculations prepare, that one tile will be in the centre of the generated map (x/z at 0/0)
        // All the other tiles, will be around the centre tile
        float mapXMin = -mapWidth/2;
        float mapXMax = mapWidth/2;
 
        float mapZMin = -mapHeight/2;
        float mapZMax = mapHeight/2;
 
        // 2D-For-Loops for the tiles map coordinates
        for(float x = mapXMin; x < mapXMax; x++)
        {
            // Pre-calculation for the tiles world coordinates
            float xPosEven = x * tileXOffset;
            float xPosOdd = xPosEven + tileXOffset/2;
 
            for(float z = mapZMin; z < mapZMax; z++)
            {
                float xPos;
                float zPos = z * tileZOffset;
                
                if(z % 2 == 0)
                {
                    xPos = xPosEven;
                }
                else
                {
                    xPos = xPosOdd;
                }
                
                float distanceToCenter = Mathf.Sqrt(Mathf.Pow(xPos, 2) + Mathf.Pow(zPos, 2)); // Using numbers of world coordinates
 
                // Making sure, if the potential new hexagon position is still within the desired radius
                if(distanceToCenter < mapRadius)
                {
                    GameObject HexTile = Instantiate(hexTilePrefab);        // Creating a new tile
                    HexTile.transform.parent = tilesFolder.transform;       // Putting tile into folder
                    HexTile.name = x.ToString() + ", " + z.ToString();      // Naming the tile after it's map coordinates
                    HexTile.GetComponent<Hexagon>().SetPosition(x, z);      // Saving the map coordinates inside the tile         
                    HexTile.transform.position = new Vector3(xPos, 0, zPos);// Moving tile to it's calculated world coordinates                    
                    allTiles.Add(HexTile);                                  // Adding tile to the list of all the created tiles of this map
                }
            }
        }        
        // PrintAllTileCoordinats();
        return allTiles;
    }
    
    /*  
     *  Prints one console log of the total number of tiles and its individual positions
    **/ 
    void PrintAllTileCoordinats()
    {
        string coord = "Number of tiles: " + tilePos.Count + " || ";
        for (int i = 0; i < tilePos.Count; i++)
        {
            coord += tilePos[i].x + ", " + tilePos[i].z + " || ";
        }
        Debug.Log(coord);
    }
}
