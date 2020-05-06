using System.Collections;
using System.Collections.Generic;
using _3_Scripts;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
[SerializeField] GameObject Ball;
int [] startPoint;
private GeneratePath gP;
    // Start is called before the first frame update
    void Start()
    {
        gP = new GeneratePath();
        startPoint = gP.getStartTile();
        Ball.transform.position = new Vector3(startPoint[0], 1, startPoint[1]);

    }

   
}
