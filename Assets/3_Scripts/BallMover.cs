using UnityEngine;
using System;

public class BallMover : MonoBehaviour 
{

    [SerializeField] float speed = 1000.0f;
    private Hexagon currentTile;
    private Vector3 pos;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        pos = transform.position;

        GetComponent<Rigidbody>().AddForce (movement * (speed * Time.fixedDeltaTime));
    }

    void OnCollisionEnter(Collision collision)    
    {        
        GameObject tile = collision.gameObject;
        // Hexagon script = tile.FindGameObjectWithTag("Tile").GetComponent<Hexagon>();

        // Debug.Log();

        if(tile.tag == "Tile")
        {
            currentTile = tile.GetComponent<Hexagon>();
            tile.GetComponent<Hexagon>().SetColor(Color.blue);
            
        }
    }

    public Hexagon GetCurrentTile()
    {
        Debug.Log(currentTile);
        return currentTile;
    }

    public Vector3 GetPos()
    {
        return pos;
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