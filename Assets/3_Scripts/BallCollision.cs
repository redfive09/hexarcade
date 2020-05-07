using UnityEngine;

/* Class purpose: Gathering information of the current tile the ball is on 
*  ToFix: Should pass on its information to the GameLogic, which is responsible to decide what happens with it
**/ 
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