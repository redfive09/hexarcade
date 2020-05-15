using System.Collections.Generic;
using UnityEngine;

/*  
*  Class purpose: Creating a new map and returning all its tiles
**/ 
public class MapGenerator : MonoBehaviour
{    
    [SerializeField] private GameObject hexTilePrefab; // For used hexagon tile


    // Offset are used for the gap between the tiles
    [SerializeField] private float tileXOffset = 1.8f;
    [SerializeField] private float tileZOffset = 1.565f;
    [SerializeField] private int mapWidth = 12;
    [SerializeField] private int mapHeight = 6;
    [SerializeField] private string folderName = "platform_1";


    // Saves all positions of all tiles
    // Not used yet, but maybe we need it later
    private List<Vector3> tilePos = new List<Vector3>();
    

    /*  This method checks first all the parameters, if something is wrong, then it prints a message to the console
     *  Returns: List of hexagon tiles of a new map with the entered values
    **/     
    public List<Hexagon> GenerateMap(int mapWidth, int mapHeight, float mapRadius, string folderName)
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

    public void GenerateMapWithEditor()
    {
        GenerateMap(mapWidth, mapHeight, GetComponent<SphereCollider>().radius, folderName);
    }


    /*  The method creates a new hexagon tile map, which can be found in a new folder in the hierachy
     *  Returns: List of hexagon tiles of a new map with the entered values 
     *  Made with the help of this tutorial: https://www.youtube.com/watch?v=BE54igXh5-Q
    **/ 
    List<Hexagon> NewHexTileMap(int mapWidth, int mapHeight, float mapRadius, string folderName)
    {
        List<Hexagon> allTiles = new List<Hexagon>(); // all the created tiles will be added here
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
                    // First, creating a tile object
                    string nameOfNewTile = x.ToString() + ", " + z.ToString();      // Naming the tile after it's map coordinates
                    GameObject newHexTile = CreateTile(xPos, zPos, nameOfNewTile);  // Creating a new tile
                    newHexTile.transform.parent = tilesFolder.transform;            // Putting tile into folder

                    // Second, setting up its script
                    Hexagon hexagonScript = newHexTile.GetComponent<Hexagon>();     // Getting the script of the tile
                    hexagonScript.SetMapPosition(x, z);                             // Saving the map coordinates inside the tile                  
                    allTiles.Add(hexagonScript);                                    // Adding tile to the list of all the created tiles of this map                    
                }
            }
        }        
        // PrintAllTileCoordinats();
        return allTiles;
    }
    
    /*  Indeed public, so individual tiles can easily be created here for specific level design purposes
     *  Returns: New Tile at the desired place in world coordinates
    **/ 
    public GameObject CreateTile(float xWorld, float zWorld, string name)
    {
        GameObject hexTile = Instantiate(hexTilePrefab);                   // Creating a new tile
        hexTile.name = name;                                            // Give it a name
        hexTile.transform.position = new Vector3(xWorld, 0, zWorld);    // Moving tile to it's calculated world coordinates
        return hexTile;
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
