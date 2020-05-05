using UnityEngine;

public class BallCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Tile")
        {
            collisionInfo.collider.GetComponent<Renderer> ().material.color = Color.red;
        }
    }
}