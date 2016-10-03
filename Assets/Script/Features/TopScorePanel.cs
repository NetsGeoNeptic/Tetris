using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TopScorePanel : MonoBehaviour
{
	public  Button  newGameButton;
	public  Button  exitButton;
	public  Text  topScore;
	public int iTopScore = 0;
	public GameObject modalPanelObject;


	private static TopScorePanel _modalPanel;

	public static TopScorePanel Instance ()
	{
		if (!_modalPanel)
		{
			_modalPanel = FindObjectOfType(typeof (TopScorePanel)) as TopScorePanel;
			if (!_modalPanel)
				Debug.LogError ("There needs to be one active TopScorePanel script on a GameObject in your scene.");
		}

		return _modalPanel;
	}

	// Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
	public void Choice (UnityAction resumeEvent)
	{
		modalPanelObject.SetActive (true);
		this.topScore.text = iTopScore.ToString();

		newGameButton.onClick.RemoveAllListeners();
		newGameButton.onClick.AddListener(resumeEvent);
		newGameButton.onClick.AddListener(NewGame);
		newGameButton.onClick.AddListener(ClosePanel);
		newGameButton.gameObject.SetActive (true);

		exitButton.onClick.RemoveAllListeners();
		exitButton.onClick.AddListener(resumeEvent);
		//exitButton.onClick.AddListener(ResumeGame);
		exitButton.onClick.AddListener(ClosePanel);
		exitButton.onClick.AddListener(ExitToMainMenu);
		exitButton.gameObject.SetActive (true);
	}

	void ClosePanel ()
	{
		modalPanelObject.SetActive (false);
	}

	void NewGame ()
	{
		MainActivity.Instance.OnNewGame ();
	}

	public void ExitToMainMenu()
	{
		MainActivity.Instance.OnGarbageCollection (); 
		MainActivity.Instance.SetState(EStateId.Menu);
	}

}