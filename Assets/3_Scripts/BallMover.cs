using UnityEngine;
using System;

public class BallMover : MonoBehaviour 
{

    [SerializeField] float speed = 500.0f;

    public GameObject currentTile;


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        GetComponent<Rigidbody>().AddForce (movement * (speed * Time.fixedDeltaTime));
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Tile")
        {
            currentTile = collision.gameObject;
            Debug.Log(currentTile);
        }
    }

    public GameObject GetCurrentTile()
    {
        // Debug.Log(currentTile);
        return currentTile;
    }

    /*  
     *  Let the Ball spawn above the desired tile
    **/
    public void GoToSpawnPosition(GameObject spawnTile)
    {
        float distanceAboveTile = 1f; // Should go later to a central place for all settings
        gameObject.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + distanceAboveTile, spawnTile.transform.position.z);
    }
}