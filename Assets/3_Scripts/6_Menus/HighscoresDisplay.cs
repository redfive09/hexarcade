using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;


// Thx to -> https://github.com/SebLague/Dreamlo-Highscores/blob/master/Episode%2002/DisplayHighscores.cs // https://www.youtube.com/watch?v=9jejKPPKomg
public class HighscoresDisplay : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
	[SerializeField] private GameObject levelGO;
    private TextMeshProUGUI[] highscoreFields;
	private Highscores highscoresManager;	
	private string level;
	private bool error = false;
	

	void Start() 
	{		
		level = SceneTransitionValues.currentSceneName;
		if(string.IsNullOrEmpty(level))
		{
			level = "Level_1";
		}

		levelGO.GetComponent<LevelSelectionButton>().SetLevelData(level);

        highscoreFields = new TextMeshProUGUI[leaderboard.transform.childCount];

        SetupHighscoreFields();
		
		highscoresManager = GetComponent<Highscores>();
		StartCoroutine(RefreshHighscores());		
	}

	private void SetupHighscoreFields()
	{
		for (int i = 0; i < highscoreFields.Length; i ++) 
		{			
            highscoreFields[i] = leaderboard.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
			highscoreFields[i].gameObject.SetActive(true);
			highscoreFields[i].text = "Fetching...";
		}
	}
	
	public void OnHighscoresDownloaded(Dictionary<string, LinkedList<Highscore>> levelBestTimes) 
	{
		// int startPositionLevels = 0;

		LinkedList<Highscore> levelHighscores = levelBestTimes[level];
		LinkedListNode<Highscore> currentEntry = levelHighscores.First;	
		
		for (int i = 0; i < highscoreFields.Length; i ++) 
		{
			if(currentEntry != null)
			{
				highscoreFields[i].text = i + 1 + ". " + currentEntry.Value.username + " - " + Timer.GetTimeAsString(currentEntry.Value.time, 3);
				currentEntry = currentEntry.Next;
			}
			else
			{
				highscoreFields[i].gameObject.SetActive(false);
			}

				
			// while(currentEntry != null )
			// {
			// 	Debug.Log(currentEntry.Value.level + ": " + currentEntry.Value.username + ": " + currentEntry.Value.time);
			// 	currentEntry = currentEntry.Next;
			// 		// Debug.Log(levelHighscores.ElementAt(i).level + ": " + levelHighscores[i].username + ": " + levelHighscores[i].time);
				
			// }


			// if (i < highscoreList.Length)
			// {
			// 	bool notEnoughTimes = true;
			// 	for(int j = startPositionLevels; j < highscoreList.Length; j++)
			// 	{
			// 		if(highscoreList[j].level == level)
			// 		{
			// 			// Debug.Log(i);
			// 			// Debug.Log(j);
			// 			highscoreFields[i].text = i + 1 + ". " + highscoreList[i].username + " - " + Timer.GetTimeAsString(highscoreList[i].time, 3);
			// 			startPositionLevels = j + 1;
			// 			notEnoughTimes = false;
			// 			// Debug.Log(highscoreFields[i].text);

			// 			break;
			// 		}
			// 	}
			// 	if(notEnoughTimes)
			// 	{
			// 		// Debug.Log("not");
			// 		highscoreFields[i].gameObject.SetActive(false);
			// 	}				
			// }
			// else
			// {
			// 	highscoreFields[i].gameObject.SetActive(false);
			// }
		}
	}
	
	IEnumerator RefreshHighscores() {
		while (true) {
			if(error)
			{
				SetupHighscoreFields();
			}

			highscoresManager.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}

    public void Error()
    {
        string message = "We're sorry, but this service is currently not available.";

        for(int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].gameObject.SetActive(false);
        }

        int middleTextField = highscoreFields.Length / 2;
        highscoreFields[middleTextField].gameObject.SetActive(true);
        highscoreFields[middleTextField].text = message;
    }

	public void SetError(bool status)
	{
		error = status;
	}

	public void GoBack()
	{
		if(string.IsNullOrEmpty(SceneTransitionValues.lastMenuName))
		{
			SceneManager.LoadScene(0);
		}
		else
		{
			SceneManager.LoadScene(SceneTransitionValues.lastMenuName);
		}		
	}

}