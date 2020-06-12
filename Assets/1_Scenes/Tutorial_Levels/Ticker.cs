using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * https://youtu.be/mptVj9-I0gQ
 */
public class Ticker : MonoBehaviour
{
    public TickerItem tickerItemPrefab;
    [Range(1,10)] // 1 is faster text, 10 is slower 
    public float itemDuration = 3f;

    public string[] fillerItems;

    private float width;

    private float PixelsPerSecond;

    private TickerItem currentItem;
    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        PixelsPerSecond = width / itemDuration;
        AddTickerItem(fillerItems[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItem.GetXPosition <= -currentItem.GetWidth)
        {
            AddTickerItem(fillerItems[Random.Range(0, fillerItems.Length)]);
        }
    }

    void AddTickerItem(string message)
    {
        currentItem = Instantiate(tickerItemPrefab, transform);
        currentItem.Initialize(width, PixelsPerSecond, message);
    }
}
