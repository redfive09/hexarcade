using System.Collections.Generic;
using UnityEngine;

/* 
 * This class serves its purpose for the editor in order to change several tiles to a type at once
 */

public class TilesApplyForAll : MonoBehaviour
{
    // definition of operations
    private const int CRACKABLES = 0;
    private const int VELOCITIY = 1;
    private const int LOSING = 2;
    private const int REMOVE_LOSING = 3;
    private const int JUMP_PADS = 4;


    [SerializeField] private float crackedTileBreaksInSeconds;
    [SerializeField] private float velocity;
    [SerializeField] private Vector3 jumpDirection;


    public void SetTiles(int operationNumber)
    {
        Hexagon[] tiles = GetComponent<Tiles>().GetAllTiles();

        for(int i = 0; i < tiles.Length; i++)
        {
            switch(operationNumber)
            {
                case CRACKABLES:
                {
                    HexagonBehaviour hexagon = tiles[i].GetComponent<HexagonBehaviour>();
                    hexagon.SetCrackedTileBreaksInTime(crackedTileBreaksInSeconds);
                    break;
                }            

                case VELOCITIY:
                {
                    HexagonSpecial hexagon = tiles[i].GetComponent<HexagonSpecial>();
                    if(hexagon) hexagon.SetVelocity(velocity);
                    break;
                }

                case LOSING:
                {
                    Hexagon hexagon = tiles[i];
                    if(hexagon.IsStandardTile())
                    {
                        hexagon.SetIsSpecialTile(3);
                    }
                    break;
                }

                case REMOVE_LOSING:
                {
                    Hexagon hexagon = tiles[i];
                    if(hexagon.GetSpecialNumber() == 3)
                    {
                        hexagon.SetIsSpecialTile(-1);
                    }
                    break;
                }

                case JUMP_PADS:
                {
                    HexagonSpecial hexagon = tiles[i].GetComponent<HexagonSpecial>();
                    if(hexagon) hexagon.SetJumpDirection(jumpDirection);
                    break;
                }
            }
        }
    }
}