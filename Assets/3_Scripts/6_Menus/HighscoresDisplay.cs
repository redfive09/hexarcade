using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;


// Thx to -> https://github.com/SebLague/Dreamlo-Highscores/blob/master/Episode%2002/DisplayHighscores.cs // https://www.youtube.com/watch?v=9jejKPPKomg
public class HighscoresDisplay : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
	[SerializeField] private GameObject levelGO;
    private TextMeshProUGUI[] highscoreFields;
	private Highscores highscoresManager;	
	private string level;
	

	void Start() 
	{
		level = SceneTransitionValues.currentSceneName;
		levelGO.GetComponent<LevelSelectionButton>().SetLevelData(level);

        highscoreFields = new TextMeshProUGUI[leaderboard.transform.childCount];

        for (int i = 0; i < highscoreFields.Length; i ++) 
		{			
            highscoreFields[i] = leaderboard.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
			highscoreFields[i].gameObject.SetActive(true);
			highscoreFields[i].text = "Fetching...";
		}
		
		highscoresManager = GetComponent<Highscores>();
		StartCoroutine(RefreshHighscores());		
	}
	
	public void OnHighscoresDownloaded(Highscore[] highscoreList) 
	{
		int startPositionLevels = 0;
		
		for (int i = 0; i < highscoreFields.Length; i ++) 
		{
			if (i < highscoreList.Length)
			{
				bool notEnoughTimes = true;
				for(int j = startPositionLevels; j < highscoreList.Length; j++)
				{
					if(highscoreList[j].level == level)
					{
						Debug.Log(i);
						Debug.Log(j);
						highscoreFields[i].text = i + 1 + ". " + highscoreList[i].username + " - " + Timer.GetTimeAsString(highscoreList[i].time, 3);
						startPositionLevels = j + 1;
						notEnoughTimes = false;
						Debug.Log(highscoreFields[i].text);

						break;
					}
				}
				if(notEnoughTimes)
				{
					Debug.Log("not");
					highscoreFields[i].gameObject.SetActive(false);
				}				
			}
			else
			{
				highscoreFields[i].gameObject.SetActive(false);
			}
		}
	}
	
	IEnumerator RefreshHighscores() {
		while (true) {
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

	public void GoBack()
	{		
		SceneManager.LoadScene(SceneTransitionValues.lastMenuName);
	}

}