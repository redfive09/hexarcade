using UnityEngine;
using System.Collections;
using TMPro;


// Thx to -> https://github.com/SebLague/Dreamlo-Highscores/blob/master/Episode%2002/DisplayHighscores.cs // https://www.youtube.com/watch?v=9jejKPPKomg
public class HighscoresDisplay : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    private TextMeshProUGUI[] highscoreFields;
	private Highscores highscoresManager;

	void Start() {

        highscoreFields = new TextMeshProUGUI[leaderboard.transform.childCount];

        for (int i = 0; i < highscoreFields.Length; i ++) {
            highscoreFields[i] = leaderboard.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
			highscoreFields[i].text = i + 1 + ". Fetching...";
		}
				
		highscoresManager = GetComponent<Highscores>();
		StartCoroutine("RefreshHighscores");
	}
	
	public void OnHighscoresDownloaded(Highscore[] highscoreList) {
		for (int i =0; i < highscoreFields.Length; i ++) {
			highscoreFields[i].text = i+1 + ". ";
			if (i < highscoreList.Length) {
				highscoreFields[i].text += highscoreList[i].username + " - " + highscoreList[i].time;
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

}