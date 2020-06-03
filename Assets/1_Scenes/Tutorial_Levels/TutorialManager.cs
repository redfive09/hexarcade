using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
   public GameObject[] popUps;
   private int popUpIndex;
   public float waitTime;
   void Update()
   {
      for (int i = 0; i < popUps.Length; i++)
      {
         if (i == popUpIndex)
         {
            popUps[i].SetActive(true);
         }
         else
         {
            popUps[i].SetActive(false);
         }
      }

      StartCoroutine(ShowInstructions());

   }

   IEnumerator ShowInstructions()
   {
      yield return new WaitForSeconds(waitTime);
      if (popUpIndex == 0)
      {
         if (Input.deviceOrientation == DeviceOrientation.FaceUp ){         
            popUpIndex++;
         }
      } else if (popUpIndex == 1) {
         if (Input.acceleration.x > 0)
         {
            popUpIndex++;
         }
      }
   }
}
