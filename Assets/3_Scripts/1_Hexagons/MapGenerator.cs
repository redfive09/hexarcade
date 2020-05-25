using System.Collections.Generic;
using UnityEngine;

/*  
*  Class purpose: Creating a new map and returning all its tiles
**/ 
public class MapGenerator : MonoBehaviour
{    
    [SerializeField] private GameObject hexTilePrefab; // For used hexagon tile
    [SerializeField] private GameObject mapPrefab;     


    // Offset are used for the gap between the tiles
    [SerializeField] private float tileXOffset = 1.8f;
    [SerializeField] private float tileZOffset = 1.565f;
    [SerializeField] private int mapWidth = 12;
    [SerializeField] private int mapHeight = 6;
    [SerializeField] private string platformName;

    private Tiles tiles; // Script of actual map    

    /*  This method checks first all the parameters, if something is wrong, then it prints a message to the console
     *  Returns: List of hexagon tiles of a new map with the entered values
    **/     
    public List<Hexagon> GenerateMap(int mapWidth, int mapHeight, float mapRadius, string platformName)
    {
        if(mapWidth > 1 && mapHeight > 1 && mapRadius >= 2)
        {
            SetupMap();
            return NewHexagonPlatform(mapWidth, mapHeight, mapRadius, platformName);
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
        GenerateMap(mapWidth, mapHeight, GetComponent<SphereCollider>().radius, platformName);
    }

    public void GenerateTileWithEditor(string hexagonName, string platformName)
    {
        SetupMap();

        Platform platform = tiles.GetPlatform(platformName);
        if(platform == null)
        {
            platform = CreatePlatform(platformName);
        }

        Hexagon hexagon = CreateTile(0, 0, hexagonName, platform);
    }


    /*  The method creates a new hexagon tile map, which can be found in a new folder in the hierachy
     *  Returns: List of hexagon tiles of a new map with the entered values 
     *  Made with the help of this tutorial: https://www.youtube.com/watch?v=BE54igXh5-Q
    **/ 
    List<Hexagon> NewHexagonPlatform(int mapWidth, int mapHeight, float mapRadius, string platformName)
    {        
        Platform platform = CreatePlatform(platformName); // all the created tiles will be added here
        
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
                
                float distanceToCenter = Mathf.Sqrt(Mathf.Pow(xPos, 2) + Mathf.Pow(zPos, 2)); // Using positions of world coordinates
 
                // Making sure, if the potential new hexagon position is still within the desired radius
                if(distanceToCenter < mapRadius)
                {                       
                    string hexagonName = x.ToString() + ", " + z.ToString();            // Naming the tile after it's map coordinates
                    Hexagon hexagon = CreateTile(xPos, zPos, hexagonName, platform);    // Creating a new tile and add it to the platform
                    hexagon.SetMapPosition(x, z);                                       // Saving the map coordinates inside the tile for now, later not relevant anymore 
                }
            }
        }
        return platform.GetTilesList();
    }


    /*  This method checks if a platform or individual tile are generated for the first time
     *  If true, it creates the GameObjects "Map"
     *  "Map" holds all elements, which are on the map, like player, tiles, distractions
     *  "Tiles", e. g. will hold all tiles of the map, "Players" all players and so on
    **/ 
        void SetupMap()
    {     
        if(tiles == null)
        {
            // In case we hit play and then we wanna add something again, we have to set "tiles" again
            GameObject mapInScene = GameObject.Find("/Map");

            if(mapInScene != null)
            {                
                tiles = mapInScene.GetComponentInChildren<Tiles>();
            }
            else // there's no map and no tiles, everything has to be created first
            {                
                GameObject newMap = Instantiate(mapPrefab);             // Create a new map
                newMap.name = "Map";                                    // Name it
                tiles = newMap.GetComponentInChildren<Tiles>();         // Save the script in the fields
                Map map = newMap.GetComponent<Map>();                   // Get the map script
                map.AddTiles(tiles);                                    // Add the "tiles" script to the map                
            }
        }
    }


    /*  This method gets created when a platform or individual tile gets generated for the first time
     *  It created a GameObject called "Map", which holds all platforms and individual tiles
    **/ 
    Platform CreatePlatform(string platformName)
    {   
        if(platformName == null || platformName == "")                // Check if the name is set, otherwise give it one
        {
            platformName = "Platform" + tiles.GetNumberOfPlatforms();
        }

        var platformObject = new GameObject();                        // new platform
        platformObject.name = platformName;                           // name it
        platformObject.transform.parent = tiles.transform;            // make the map to its parent
        platformObject.AddComponent<Platform>();                      // add the script to the platform        
        Platform platform = platformObject.GetComponent<Platform>();  // get the platform script
        platform.Setup();                                             // Tell the platform to setup itself
        tiles.AddPlatform(platform);                                  // add it to the tilesFolder        
        return platform;
    }

    
    /*  Indeed public, so individual tiles can easily be created here for specific level design purposes
     *  Returns: New Tile at the desired place in world coordinates
    **/ 
    public Hexagon CreateTile(float xWorld, float zWorld, string hexagonName, Platform platform)
    {
        if(hexagonName == null || hexagonName == "")                    // Check if the name is set, otherwise give it one
        {
            hexagonName = "Hexagon" + platform.GetNumberOfHexagons();
        }

        GameObject hexTile = Instantiate(hexTilePrefab);                // Creating a new tile
        hexTile.name = hexagonName;                                     // Give it a name
        hexTile.transform.position = new Vector3(xWorld, 0, zWorld);    // Moving tile to it's calculated world coordinates
        Hexagon hexagon = hexTile.GetComponent<Hexagon>();              // Get the hexagon script
        hexagon.Setup();                                                // Tell the hexagon to setup itself
        hexagon.transform.parent = platform.transform;                  // Putting hexagon into folder
        platform.AddHexagon(hexagon);                                   // Adding tile to the list of all the created tiles of this map        
        return hexagon;
    }
} // END OF CLASS
