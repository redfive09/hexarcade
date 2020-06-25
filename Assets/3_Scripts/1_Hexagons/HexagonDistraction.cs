using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonDistraction : MonoBehaviour
{
    /* ------------------------------ FIELDS FOR BLINKING TILES ------------------------------  */
    [SerializeField] private bool onlyThisPlatformIsBlinking;
    [SerializeField] private bool multipleActivationsPossible;
    // [SerializeField] private bool randomBlinkingColour;
    [SerializeField] private int numberOfBlinkingTilesPerCycle;
    [SerializeField] private float lengthOfBlinkingDistraction;
    [SerializeField] private float timeToNextBlinkingCycle;
    [SerializeField] private float timeUntilChangingColourBack;
    [SerializeField] private bool setColorBackImmediately;

    private Dictionary<Hexagon, Color> tileColors;
    private bool blinking;

    /* ------------------------------ FIELDS FOR SCOLLING TEXT ------------------------------  */
    [SerializeField] private GameObject _scrollingText;
    

    /* ------------------------------ DISTRACTION-TILES NUMBERS ------------------------------  */
    private int distractionCase;
    private const int BLINKING_START = 0;
    private const int BLINKING_STOP = 1;
    private const int SCROLLING_TEXT = 2;
    private const int SCROLLING_TEXT_STOP = 3;


    /* ------------------------------ GENERAL INFORMATION FOR DIFFERENT OPERATIONS ------------------------------  */
    private Dictionary<int, List<Hexagon>> distractionTiles;
    private List<Ball> players = new List<Ball>();
    private Hexagon[] platformTiles;
    private Hexagon[] allTiles;
    private bool wasTouchedBefore = false;
    private Hexagon thisHexagon;


    /* ------------------------------ MAIN METHODS FOR DISTRACTION-TILES ------------------------------  */

    public void GetStarted(int distractionNumber, Hexagon[] platformTiles, Hexagon[] allTiles,
                            Dictionary<Hexagon, Color> tileColors, Dictionary<int, List<Hexagon>> distractionTiles, Hexagon hexagon)
    {
        distractionCase = distractionNumber;
        this.platformTiles = platformTiles;
        this.allTiles = allTiles;
        this.tileColors = tileColors;
        this.distractionTiles = distractionTiles;
        thisHexagon = hexagon;
        TileNeedSetup();
    }

    private void TileNeedSetup()
    {
        if(distractionCase == BLINKING_START || distractionCase == SCROLLING_TEXT)
        {
            thisHexagon.SetAudio("activation");
        }         
    }

    public void DistractionTileTouched(Ball player)
    {
        players.Add(player);

        switch(distractionCase)
        {
            case BLINKING_START:
                if(multipleActivationsPossible || !wasTouchedBefore)
                {
                    thisHexagon.GetAudioSource().Play();
                    blinking = true;
                    if(onlyThisPlatformIsBlinking)
                    {
                        StartCoroutine(StartBlinking(platformTiles));
                    }
                    else
                    {
                        StartCoroutine(StartBlinking(allTiles));
                    }
                }
            break;

            case BLINKING_STOP:
                StopBlinking();
                if(setColorBackImmediately) SetColorBackImmediately();
                break;

            case SCROLLING_TEXT:
                if (!wasTouchedBefore)
                {
                    GameObject ScrollingText = Instantiate(_scrollingText);
                    thisHexagon.GetAudioSource().Play();
                    var Distraction = new GameObject();
                    Distraction.name = "Player" + (player.GetPlayerNumber() + 1);
                    Distraction.transform.parent = GameObject.Find("/Map/Distractions").transform;
                    ScrollingText.transform.parent = Distraction.transform;
                }
            break;

            case SCROLLING_TEXT_STOP:            
                string distractionFolder = "/Map/Distractions/Player" + (player.GetPlayerNumber() + 1);
                GameObject distraction = GameObject.Find(distractionFolder);
                Destroy	(distraction);
            break;
        }

        wasTouchedBefore = true;
    }

    public void DistractionTileLeft(Ball player)
    {
        players.Remove(player);

    }


    /* ------------------------------ SPECIFIC METHODS FOR BLINKING TILES ------------------------------  */

    /*

    lengthOfBlinkingDistraction     // after how much seconds does the whole distraction process stop
    timeToNextBlinkingCycle         // after how many seconds does the next
    timeUntilChangingColourBack     // when does the hexagon get its original colour back
        numberOfBlinkingTilesPerCycle   // how many tiles get changed per cycle -> maximum is length of array
    */

    private IEnumerator StartBlinking(Hexagon[] hexagons)
    {
        // make sure the entered number is not too big for the array
        if(numberOfBlinkingTilesPerCycle > hexagons.Length) numberOfBlinkingTilesPerCycle = hexagons.Length;

        // set the time, when the method will end
        float stopBlinkingTime = Time.fixedTime + lengthOfBlinkingDistraction;

        // if the specified time is negative or 0, the blinking won't stop
        if(lengthOfBlinkingDistraction <= 0)
        {
            stopBlinkingTime = float.MaxValue;
        }

        // check if the specified time is over, otherwise start another cycle
        while(stopBlinkingTime > Time.fixedTime && blinking)
        {
            // array with the positions of the blinking tiles at this cycle
            int[] blinkingHexagons = GetRandomHexagonPositions(numberOfBlinkingTilesPerCycle, hexagons.Length);

            for(int i = 0; i < numberOfBlinkingTilesPerCycle; i++)
            {
                // get the hexagon from the randomly choosen position
                int randomlyChosen = blinkingHexagons[i];
                Hexagon hexagon = hexagons[randomlyChosen];

                // make sure not to do any operations on deleted hexagons
                if(hexagon != null)
                {
                    hexagon.SetColor(GetRandomColor());
                    if(timeUntilChangingColourBack > 0)
                    {
                        StartCoroutine(SetColorBack(hexagon));
                    }
                }
            }

            yield return new WaitForSeconds(timeToNextBlinkingCycle);
        }
    }


    private int[] GetRandomHexagonPositions(int numberOfBlinkingTilesPerCycle, int highestPosition)
    {
        // initialise an array for the blinking tiles of this cycle
        int[] blinkingHexagons = new int[numberOfBlinkingTilesPerCycle];

        // set all values to a negative one, in order be able to check,
        // if the random number we get later is unique
        for(int i = 0; i < numberOfBlinkingTilesPerCycle; i++)
        {
            blinkingHexagons[i] = -1;
        }

        // now collect random numbers, which will be the blinking hexagons later
        for(int i = 0; i < numberOfBlinkingTilesPerCycle; i++)
        {

            // get a random number, which will be the blinking hexagon
            int randomNumber = Random.Range(0, highestPosition);

            // since it's unclear yet, set it to false
            bool numberIsUnique = false;

            // as long as haven't found a unique number, we look for one
            while(!numberIsUnique)
            {

                // we have to check, if the number is already in the array
                bool containsRandomNumber = false;

                // we go through all choosen blinkingHexagons
                for(int j = 0; j < numberOfBlinkingTilesPerCycle; j++)
                {

                    // the randomNumber is indeed already choosen
                    if(blinkingHexagons[j] == randomNumber)
                    {
                        // increase the randomNumber by one (but not over the maximum of positions in the array)
                        randomNumber = (randomNumber + 1) % highestPosition;
                        containsRandomNumber = true;
                    }
                }

                // in case the randomNumber was not already in the array, we can stop the while-loop
                if(!containsRandomNumber) numberIsUnique = true;
            }

            // Now we can add the random number, which is unique for sure
            blinkingHexagons[i] = randomNumber;
        }
        return blinkingHexagons;
    }


    private Color GetRandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    private IEnumerator SetColorBack(Hexagon hexagon)
    {
        yield return new WaitForSeconds(timeUntilChangingColourBack);
        if(hexagon) hexagon.SetColor(tileColors[hexagon]); // just in case the hexagon got deleted in the meantime
    }

    private void StopBlinking()
    {
        int sizeOfDictionary = distractionTiles.Count;
        for(int i = 0; i < sizeOfDictionary; i++)
        {
            if(distractionTiles.TryGetValue(i, out List<Hexagon> hexagonList)) // if the key is available, then just procceed
            {
                for(int j = 0; j < hexagonList.Count; j++)
                {
                    HexagonDistraction hexagon = hexagonList[j].GetComponent<HexagonDistraction>();
                    if(hexagonList[j].GetDistractionNumber() == BLINKING_START)
                    {
                        hexagon.SetBlinking(false);
                    }
                }
            }
            else
            {
                sizeOfDictionary++;
            }
        }
    }

    public void SetBlinking(bool status)
    {
        blinking = status;
    }

    private void SetColorBackImmediately()
    {
        for(int i = 0; i < allTiles.Length; i++)
        {
            Hexagon hexagon = allTiles[i];
            if(hexagon) hexagon.SetColor(tileColors[hexagon]);
        }
    }


    /* ------------------------------ EDITOR METHODS ------------------------------  */

    public string GetNameOfFunction()
    {
        string prefix = "-> ";

        switch(distractionCase)
        {
            case BLINKING_START:
                return prefix + nameof(BLINKING_START).ToLower() + ", length: " + lengthOfBlinkingDistraction;

            case BLINKING_STOP:
                return prefix + nameof(BLINKING_STOP).ToLower();

            case SCROLLING_TEXT:
                return prefix + nameof(SCROLLING_TEXT).ToLower();

            case SCROLLING_TEXT_STOP:
                return prefix + nameof(SCROLLING_TEXT_STOP).ToLower();            
        }

        return "";
    }
}
