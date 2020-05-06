using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3_Scripts
{
    
    public class Hexagon : MonoBehaviour
    {
        private bool isPath = false;
        
        void Start()
        {
            if (isPartOfPath())
            {
                gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.yellow;
                isPath = true;
            }
        }

        private void Update()
        {
 
        }

        bool isPartOfPath()
        {
            foreach (var VARIABLE in GeneratePath.getPathList())
            {
                
                if (gameObject.name.Equals(VARIABLE[0] + ", " + VARIABLE[1]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}