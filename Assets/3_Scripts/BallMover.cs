using UnityEngine;
using System;

public class BallMover : MonoBehaviour 
{

    [SerializeField] float speed = 500.0f;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        GetComponent<Rigidbody>().AddForce (movement * (speed * Time.fixedDeltaTime));
    }

    /*  Let the Ball spawn above the desired tile
     *  Not working yet, eventhough the spawnTile is correct
    **/
    public void GoToSpawnPosition(GameObject spawnTile)
    {
        float distanceAboveTile = 1f;
        transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + distanceAboveTile, spawnTile.transform.position.z);
    }

}