using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileMapGenerator : MonoBehaviour
{
    [SerializeField]  GameObject hexTilePrefab;
    [SerializeField]  Transform holder;
    [SerializeField] int mapWidth = 25;
    [SerializeField] int mapHeight = 12;
    [SerializeField] float tileXOffset = 1.8f;
    [SerializeField]  float tileZOffset = 1.565f;

    void Start()
    {
        SphereCollider SphereCollider = GetComponent<SphereCollider>();
        CreateHexTileMap(SphereCollider.radius);
        
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
                }
            }
        }
    }

    IEnumerator SetTileInfo(GameObject GO, float x, float z, Vector3 pos)
    {
        yield return new WaitForSeconds(0.00001f);
        GO.transform.parent = holder;
        GO.name = x.ToString() + ", " + z.ToString();
        GO.transform.position = pos;
    }
}
