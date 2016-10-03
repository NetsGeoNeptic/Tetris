using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class PausePanel : MonoBehaviour 
{
	public  Button  resumeButton;
	public  Button  exitButton;
	public GameObject modalPanelObject;

	private static PausePanel _modalPanel;

	public static PausePanel Instance ()
	{
		if (!_modalPanel)
		{
			_modalPanel = FindObjectOfType(typeof (PausePanel)) as PausePanel;
			if (!_modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}
		return _modalPanel;
	}

	/// <summary>
	/// Choice Panel
	/// </summary>
	public void Choice (UnityAction resumeEvent)
	{
		modalPanelObject.SetActive (true);

		resumeButton.onClick.RemoveAllListeners();
		resumeButton.onClick.AddListener(resumeEvent);
		resumeButton.onClick.AddListener(ResumeGame);
		resumeButton.onClick.AddListener(ClosePanel);
		resumeButton.gameObject.SetActive (true);

		exitButton.onClick.RemoveAllListeners();
		exitButton.onClick.AddListener(resumeEvent);
		exitButton.onClick.AddListener(ResumeGame);
		exitButton.onClick.AddListener(ClosePanel);
		exitButton.onClick.AddListener(ExitToMainMenu);
		exitButton.gameObject.SetActive (true);
	}

	/// <summary>
	/// Close Panel
	/// </summary>
	void ClosePanel ()
	{
		modalPanelObject.SetActive (false);
	}

	void ResumeGame ()
	{
		MainActivity.Instance.OnPauseInGame();
	}

	public void ExitToMainMenu()
	{
		Debug.Log("ExitToMainMenu");
		MainActivity.Instance.OnGarbageCollection ();
		MainActivity.Instance.SetState(EStateId.Menu);
	}

	void Awake () 
	{

	}

	public void HandleOnStateChange ()
	{
		Debug.Log("OnStateChange!");
	}
}

