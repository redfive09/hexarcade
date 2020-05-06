using System.Collections;
using System.Collections.Generic;
using _3_Scripts;
using UnityEngine;

public class HexTileMapGenerator : MonoBehaviour
{
    [SerializeField]  GameObject hexTilePrefab;
    [SerializeField]  Transform holder;
    [SerializeField] int mapWidth = 12;
    [SerializeField] int mapHeight = 6;
    [SerializeField] float tileXOffset = 1.8f;
    [SerializeField]  float tileZOffset = 1.565f;
    private SphereCollider SphereCollider;
    private List<Vector3> tilePos = new List<Vector3>();
    private int numberOfTiles = 0;


    void Start()
    {
        this.SphereCollider = GetComponent<SphereCollider>();
        CreateHexTileMap(SphereCollider.radius);
    }

    // Make sure for UI later, that the minimal map values are met!
    public void GenerateMap(int mapWidth, int mapHeight, float mapRadius)
    {
        if(mapWidth > 1 && mapHeight > 1 && mapRadius >= 2)
        {            
            SphereCollider.radius = mapRadius;
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            CreateHexTileMap(mapRadius);
        }
        else
        {
            Debug.Log(
            "Couldn't generate a map, since there's a problem with at least one of the map values:" + "\n" +
            "mapWidth: " + mapWidth + " || " + "mapHeight: " + mapHeight + " || " + "mapRadius: " + mapRadius
            );
        }       
    }

    // Made with the help of this tutorial: https://www.youtube.com/watch?v=BE54igXh5-Q
    void CreateHexTileMap(float mapRadius)
    {
        float mapXMin = -mapWidth/2;
        float mapXMax = mapWidth/2;
 
        float mapZMin = -mapHeight/2;
        float mapZMax = mapHeight/2;
 
        for(float x = mapXMin; x < mapXMax; x++)
        {
        float xPosEven = x * tileXOffset;
        float xPosOdd = xPosEven + tileXOffset/2;
 
            for(float z = mapZMin; z < mapZMax; z++)
            {                
                Vector3 pos;                
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

                float distanceToCenter = Mathf.Sqrt(Mathf.Pow(xPos, 2) + Mathf.Pow(zPos, 2));
 
                if(distanceToCenter < mapRadius)
                {                    
                    GameObject TempGO = Instantiate(hexTilePrefab);  
                    pos = new Vector3(xPos, 0, zPos);
                    StartCoroutine(SetTileInfo(TempGO, x, z, pos));

                    tilePos.Add(pos);
                    numberOfTiles++;
                }
            }
        }
        // PrintAllTileCoordinats();
    }

    IEnumerator SetTileInfo(GameObject GO, float x, float z, Vector3 pos)
    {
        yield return new WaitForSeconds(0.00001f);
        GO.transform.parent = holder;
        GO.name = x.ToString() + ", " + z.ToString();
        GO.transform.position = pos;
        GO.AddComponent<Hexagon>();
    }

    void PrintAllTileCoordinats()
    {
        string coord = "";
        for (int i = 0; i < numberOfTiles; i++)
        {
            coord += tilePos[i].x + ", " + tilePos[i].z + " || ";
        }

        Debug.Log(coord);
    }
}
