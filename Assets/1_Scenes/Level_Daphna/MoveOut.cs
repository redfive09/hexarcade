using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
public class MoveOut : MonoBehaviour
{
    // Start is called before the first frame update

    private LTDescr desc;
    public Transform obj;
    public Vector3 pos;
    public LeanTweenType ease = LeanTweenType.linear;
    public float seconds;
    
    public void Start()
    {
        desc = LeanTween.move(gameObject, obj.transform, seconds).setEase(ease).setDelay(2f);
    }

}
