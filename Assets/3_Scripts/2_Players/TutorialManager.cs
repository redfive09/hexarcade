using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
   public TextMeshProUGUI[] popUps;
   private int popUpIndex;
   public float waitTime;
   
   
   void Start()
   {
      Time.timeScale = 1.0f;
      for (int i = 0; i < popUps.Length; i++)
         {
            if (i == popUpIndex)
            {
               popUps[i].gameObject.SetActive(true);
            }
            else
            {
               popUps[i].gameObject.SetActive(false);
            }

            StartCoroutine(ShowInstructions());
      }
   }
   void Update()
   {
      //Debug.Log(Time.timeScale);
    
         

   }

   IEnumerator ShowInstructions()
   {      
      {
         yield return new WaitForSeconds(waitTime);
         if (popUpIndex == 0 )
         {
           if (Input.deviceOrientation == DeviceOrientation.FaceUp)
           {
               popUpIndex++;
           }
         }
         else if (popUpIndex == 1)
         {
            if (Input.acceleration.x > 0)
            {
               popUpIndex++;
            }
         }
      }
   }

   public void CheckForNonStandardTiles(Hexagon hexagon, Tiles tiles, Ball player)
   {
      Dictionary<int, List<Hexagon>>[] nonStandardTiles = tiles.GetAllNonStandardTiles();        
      int nonStandardTilesCount = 0;

      for(int i = 0; i < nonStandardTiles.Length; i++)
      {
         Dictionary<int, List<Hexagon>> currentHexagonDictionary = nonStandardTiles[i];
         int listLength = currentHexagonDictionary.Count;

         for(int j = 0; j < listLength; j++)
         {
            if(currentHexagonDictionary.TryGetValue(j, out List<Hexagon> hexagonList))
            {
               for(int h = 0; h < hexagonList.Count; h++)
               {
                  if(!hexagonList[h].IsTouched())
                  {
                     nonStandardTilesCount++;
                  }
               }
            }
            else
            {
               listLength++;
            }
         } 
      }

      if(nonStandardTilesCount == 0)
      {
         player.Won();
      }
      else
      {
         string information = "Find the remaining " + nonStandardTilesCount;
         if(nonStandardTilesCount > 1)
         {
            information += " non-standard hexagons.";
         }
         else
         {
            information += " non-standard hexagon.";
         }
            
         SetInformationText(3, 0.3f, information);
      }  
   }

   public void SetInformationText(float startFading, float fadingSpeed, string information)
   {
      for (int i = 0; i < popUps.Length; i++)
      {
         popUps[i].gameObject.SetActive(false);
      }

      popUps[0].gameObject.SetActive(true);
      popUps[0].text = information;   
   }
}
