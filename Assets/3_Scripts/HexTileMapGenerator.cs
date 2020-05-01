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
        CreateHexTileMap();
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
    void CreateHexTileMap()
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

    IEnumerator SetTileInfo(GameObject GO, float x, float z, Vector3 pos)
    {
        yield return new WaitForSeconds(0.00001f);
        GO.transform.parent = holder;
        GO.name = x.ToString() + ", " + z.ToString();
        GO.transform.position = pos;
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
