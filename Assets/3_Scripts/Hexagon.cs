using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3_Scripts
{
    
    public class Hexagon : MonoBehaviour
    {
        public static bool isPath = false;
        void Start()
        {
            isPath = isPartOfPath();
        }

        private void Update()
        {
            if (isPath)
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            }
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