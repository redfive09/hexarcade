using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelorometerReading : MonoBehaviour
{
    public AccelorometerMovement userInput;
    private RectTransform direction;

    // Start is called before the first frame update
    void Start()
    {
        userInput = GameObject.Find("Player1").GetComponent<AccelorometerMovement>();
        direction = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position.x += userInput.Tilt.x;
        //transform.position.z = userInput.Tilt.z;
        direction.anchoredPosition = new Vector2(3.0f * userInput.Tilt.x, 3.0f * userInput.Tilt.z);
        //Debug.Log(userInput.Tilt);
    }
}
