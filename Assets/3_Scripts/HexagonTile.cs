using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonTile : MonoBehaviour
{
    [SerializeField] Color myColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void OnCollisionEnter(Collision other)
{
    if (other.gameObject.tag == "Player") 
    {
        Debug.Log(gameObject.GetComponent<Renderer>().material.color);
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        Debug.Log(gameObject.GetComponent<Renderer>().material.color);
        Debug.Log(gameObject);
    }
    
}
    

    

}
