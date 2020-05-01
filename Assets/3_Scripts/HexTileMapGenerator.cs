using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileMapGenerator : MonoBehaviour
{
    public GameObject hexTilePrefab;
    public Transform holder;

    [SerializeField] int mapWidth = 25;
    [SerializeField] int mapHeight = 12;
    public float tileXOffset = 1.8f;
    public float tileZOffset = 1.565f;


    void Start()
    {
        SphereCollider SphereCollider = GetComponent<SphereCollider>();
        CreateHexTileMap(SphereCollider.radius);
        
    }

    /* Tutorial 1: https://www.youtube.com/watch?reload=9&v=rjBD-4gNcfA&t=71s */
    // void CreateHexTileMap()
    // {
    //     for(int x = 0; x <= mapWidth; x++)
    //     {
    //         for(int z = 0; z <= mapHeight; z++)
    //         {
    //             GameObject TempGO = Instantiate(hexTilePrefab);

    //             if(z % 2 == 0)
    //             {
    //                 TempGO.transform.position = new Vector3(x * tileXOffset, 0, z * tileZOffset);
    //             }
    //             else
    //             {
    //                 TempGO.transform.position = new Vector3(x * tileXOffset + tileXOffset/2, 0, z * tileZOffset);
    //             }
    //             SetTileInfo(TempGO, x, z);
    //         }
    //     }
    // }

    // void SetTileInfo(GameObject GO, int x, int z)
    // {
    //     GO.transform.parent = transform;
    //     GO.name = x.ToString() + ", " + z.ToString();
    // }
    

    /* Tutorial 2: https://www.youtube.com/watch?v=BE54igXh5-Q */
    /* parameter not used here, it's just meant for the improved, but not working method below */
    void CreateHexTileMap(float mapRadius)
    {
        float mapXMin = -mapWidth/2;
        float mapXMax = mapWidth/2;

        float mapZMin = -mapHeight/2;
        float mapZMax = mapHeight/2;

        for(float x = mapXMin; x < mapXMax; x++)
        {
            for(float z = mapZMin; z < mapZMax; z++)
            {
                GameObject TempGO = Instantiate(hexTilePrefab);
                Vector3 pos;

                if(z % 2 == 0)
                {
                    pos = new Vector3(x * tileXOffset, 0, z * tileZOffset);
                }
                else
                {
                    pos = new Vector3(x * tileXOffset + tileXOffset/2, 0, z * tileZOffset);
                }
                StartCoroutine(SetTileInfo(TempGO, x, z, pos));
            }
        }
    }


     /* Not working, yet */
     /* Idea is to get rid of the OnTriggerExit-method, which might make us some problem later, 
     that's why this method needs the mapRadius as a parameter here, so it only Instantiates the tiles inside of the radius */

    // void CreateHexTileMap(float mapRadius)
    // {
    //     float mapXMin = -mapWidth/2;
    //     float mapXMax = mapWidth/2;

    //     float mapZMin = -mapHeight/2;
    //     float mapZMax = mapHeight/2;

    //     for(float x = mapXMin; x < mapXMax; x++)
    //     {
    //         for(float z = mapZMin; z < mapZMax; z++)
    //         {                
    //             Vector3 pos;                
    //             float xPos = x * tileXOffset; 
    //             float zPos = z * tileZOffset;

    //             if(z % 2 == 1)
    //             {
    //                 xPos = xPos + tileXOffset/2;
    //             }

    //             float distanceToCenter = Mathf.Sqrt(Mathf.Pow(xPos, 2) + Mathf.Pow(zPos, 2));

    //             if(distanceToCenter < mapRadius)
    //             {
    //                 // Debug.Log(distanceToCenter);
    //                 GameObject TempGO = Instantiate(hexTilePrefab);
    //                 pos = new Vector3(xPos, 0, zPos);
    //                 StartCoroutine(SetTileInfo(TempGO, x, z, pos));
    //             }
    //         }
    //     }
    // }


    IEnumerator SetTileInfo(GameObject GO, float x, float z, Vector3 pos)
    {
        yield return new WaitForSeconds(0.00001f);
        GO.transform.parent = holder;
        GO.name = x.ToString() + ", " + z.ToString();
        GO.transform.position = pos;
    }

    // We should get rid of this, might cause problems when we wanna use the tiles later
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
