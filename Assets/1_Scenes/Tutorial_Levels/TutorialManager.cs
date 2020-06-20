using System.Collections;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI informationText;
   public GameObject[] popUps;
   private int popUpIndex;
   public float waitTime;
   
   
   void Start()
   {
      Time.timeScale = 1.0f;
   }
   void Update()
   {
      //Debug.Log(Time.timeScale);
    
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

            StartCoroutine(ShowInstructions());
      }

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

   public void SetInformationText(float startFading, float fadingSpeed, string information)
   {      
      if(informationText.text == "" || informationText.text == null)
      {
         informationText = Instantiate(informationText);
         informationText.transform.parent = this.transform;         
      }

      informationText.text = information;
      StartCoroutine(FadeOutInformation(startFading, fadingSpeed, informationText));
   }

   // Thx to --> https://stackoverflow.com/questions/56031067/using-coroutines-to-fade-in-out-textmeshpro-text-element
   private IEnumerator FadeOutInformation(float startFading, float fadingSpeed, TextMeshProUGUI text) 
    {         
         text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
         yield return new WaitForSeconds(startFading);
         while (text.color.a > 0.0f)
         {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * fadingSpeed));
            yield return null;
         }
    }
}
