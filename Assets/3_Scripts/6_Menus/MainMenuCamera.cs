using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuCamera : MonoBehaviour
{
    [SerializeField]
    AnimationCurve movement; //ymax = 0.475911
    private Transform ThisTransform;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        ThisTransform = GetComponent<Transform>();
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ThisTransform.position = initialPosition + Vector3.forward * movement.Evaluate(Time.time);
    }
}
