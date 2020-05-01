using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MOVE_SPEED = 10f;
    private const float ROTATION_SPEED = 300f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        private void FixedUpdate()
    {

        float y = Time.deltaTime * ROTATION_SPEED;
        float z = Time.deltaTime * MOVE_SPEED;
        
        transform.Rotate(0, y, 0);
        transform.Translate(0, 0, z);

    }
}
