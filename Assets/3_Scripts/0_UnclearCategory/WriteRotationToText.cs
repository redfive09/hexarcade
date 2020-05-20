using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteRotationToText : MonoBehaviour
{
    private Transform source = null;
    [SerializeField] private Text rotationInfo = null;

    void Awake()
    {
        source = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (source == null)
        {
            rotationInfo.text = string.Format("X = {0:G3}\nY = {1:G3}\nZ = {2:G3}", Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        }
        else
        {
            rotationInfo.text = string.Format( "X={0:F6}\ny={1:F6}\nz={2:F6}", source.transform.eulerAngles.x, source.transform.eulerAngles.y, source.transform.eulerAngles.z);
        }
    }
}
