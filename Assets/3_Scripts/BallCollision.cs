using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private float delay = 2f;
    void OnCollisionEnter(Collision collisionInfo)
    
    {
        if(collisionInfo.collider.tag == "Tile")
        {
            collisionInfo.collider.GetComponent<Renderer> ().material.color = Color.red;
        }
        
        if(collisionInfo.collider.tag == "Cracked Tile")
        {
            collisionInfo.collider.GetComponent<Renderer> ().material.color = Color.blue;
            Destroy (collisionInfo.collider.gameObject, delay);
        }
    }
}