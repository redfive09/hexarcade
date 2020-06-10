
using UnityEngine;
using TMPro;
/*
 * https://youtu.be/mptVj9-I0gQ
 */
public class TickerItem : MonoBehaviour
{
     float tickerwidth;
     float pixelsPerSecond;
     RectTransform rt;
   
      public float GetXPosition { get { return rt.anchoredPosition.x; } }
      public float GetWidth { get { return rt.rect.width; } }

     public void Initialize(float tickerWidth,float pixelsPerSecond,string message)
     {
        tickerwidth = tickerWidth;
        this.pixelsPerSecond = pixelsPerSecond;
        rt = GetComponent<RectTransform>();
        GetComponent<TextMeshProUGUI>().text = message + "  ";
     }
      
    // Update is called once per frame
    void Update () {
           rt.position += Vector3.left * (pixelsPerSecond * Time.deltaTime);
            if(GetXPosition<=0 - tickerwidth - GetWidth)
           {
               Destroy(gameObject);
           }
    }
}
