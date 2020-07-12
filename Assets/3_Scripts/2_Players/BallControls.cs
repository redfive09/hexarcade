using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControls : MonoBehaviour
{
    [SerializeField] private float speed = 1000.0f;
    private Rigidbody rb;
    [SerializeField] private float multiplier = 2.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!Game.isPaused)
        {
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");
            Vector3 movement = new Vector3 (moveHorizontal * multiplier, 0.0f, moveVertical * multiplier);
            rb.AddForce (movement * (speed * Time.fixedDeltaTime));
        }
    }

    public void SetMultiplier(float newMultiplier)
    {
        multiplier = newMultiplier;
    }

    public float GetMultiplier()
    {
        return multiplier;
    }
}