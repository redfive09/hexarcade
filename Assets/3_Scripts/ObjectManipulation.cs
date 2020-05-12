using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


public class ObjectManipulation : MonoBehaviour
{

    public GameObject playingPiece;

    [SerializeField] private float rotationPower;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Time.deltaTime * rotationPower;
        playingPiece.transform.Rotate(90.0f * rotation, 0.0f * rotation, 0.0f * rotation, Space.Self);
    }
}
