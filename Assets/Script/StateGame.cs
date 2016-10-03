using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System;


public partial class StateGame : StateBase
{
	///////////////////////////////////////////////////////////////
	#region Variables

	private string _rootPause = "triggerPause";
	private string _buttonName = null; //variable to determine the current trigger

	private GameObject[] _lettersColorPause; // Pause Button Color 
	private GameObject _buttonPause; // Pause Button
	private GameObject _triggerPause; // Trigger Pause

	private GameObject _digitTopScore;
	private GameObject _digitTopScoreShadow; 

	private GameObject _digitScore;
	private GameObject _digitScoreShadow;

	private GameObject _digitLevel;
	private GameObject _digitLevelShadow;

	private GameObject _digitLines;
	private GameObject _digitLinesShadow;

	private GameObject _cube;
	private GameObject _spawn;

	private GameObject _pauseManager;
	private PausePanel _pausePanel;// Pause Panel

	private GameObject _gameOverManager; 
	private GameOverPanel _gameOverPanel;// Game Over Panel

	private GameObject _topScoreManager; 
	private TopScorePanel _topScorePanel;// Top Score Panel


	/** Audio variable*/
	private AudioClip _boom;
	private AudioClip _disappearance;
	/** end */

	private Color _colorBlock = Color.red;

	#endregion
	///////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////
	#region Properties

	public StateGame() : base(EStateId.Game)
	{
	}

	#endregion
	///////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////
	#region Interface

	public override void Activate()
	{
		base.Activate();
		Application.LoadLevel(3);
		//SceneManager.LoadScene (3);
	}

	public override void OnSceneLoaded () // Load the scene and activate the appropriate interface
	{
		Debug.Log("OnSceneLoaded");

		base.OnSceneLoaded ();

		_lettersColorPause = MainActivity.Instance.lettersColorPause; 
		_buttonPause = MainActivity.Instance.buttonPause; 
		_triggerPause  = MainActivity.Instance.triggerPause; 
		_buttonPause.SetActive(true);
		_triggerPause.SetActive(true);

		_digitTopScore = MainActivity.Instance.digitTopScore;
		_digitTopScoreShadow = MainActivity.Instance.digitTopScoreShadow;
		_digitTopScore.SetActive(true);
		_digitTopScoreShadow.SetActive(true);

		_digitScore = MainActivity.Instance.digitScore;
		_digitScoreShadow = MainActivity.Instance.digitScoreShadow;
		_digitScore.SetActive(true);
		_digitScoreShadow.SetActive(true);

		_digitLevel = MainActivity.Instance.digitLevel;
		_digitLevelShadow = MainActivity.Instance.digitLevelShadow;
		_digitLevel.SetActive(true);
		_digitLevelShadow.SetActive(true);

		_digitLines = MainActivity.Instance.digitLines;
		_digitLinesShadow = MainActivity.Instance.digitLinesShadow;
		_digitLines.SetActive(true);
		_digitLinesShadow.SetActive(true);

		_spawn = MainActivity.Instance.spawn;
		_spawn.SetActive(true);

		_pauseManager = MainActivity.Instance.pauseManager;
		_pauseManager.SetActive(true);
		_pausePanel = PausePanel.Instance();

		_gameOverManager = MainActivity.Instance.gameOverManager; 
		_gameOverManager.SetActive(true);
		_gameOverPanel = GameOverPanel.Instance();

		_topScoreManager = MainActivity.Instance.topScoreManager; 
		_topScoreManager.SetActive(true);
		_topScorePanel = TopScorePanel.Instance();// Top Score Panel


		_cube = MainActivity.Instance.cube;

		/** Audio variable*/
		_boom = MainActivity.Instance.boom;
		_disappearance = MainActivity.Instance.disappearance;
		/** end */

		Start ();

	}

	/// <summary>
	/// Called when the mouse enters
	/// </summary>
	/// <param name="goName">Game object name</param>
	public override void OnMouseEnter(String goName)
	{
		//Debug.Log("MenuState OnMouseEnter");
		_buttonName = goName;
		if (goName.Equals (_rootPause) == true)
		{		
			_colorBlock = Color.green;
			foreach (GameObject go in _lettersColorPause)
			{
				go.GetComponent<Renderer>().material.SetColor("_Color", _colorBlock);
			}
			_buttonPause.transform.position = _buttonPause.transform.position - Vector3.forward * 0.25f;
		}
	}

	/// <summary>
	/// This event is sent if the mouse came from the object
	/// </summary>
	/// <param name="goName">Game object name</param>
	public override void OnMouseExit(String goName)
	{
		_buttonName = null;
		if (goName.Equals (_rootPause) == true)
		{			
			_colorBlock = Color.white;
			foreach (GameObject go in _lettersColorPause) {
				go.GetComponent<Renderer> ().material.SetColor ("_Color", _colorBlock);				
			}
			_buttonPause.transform.position = _buttonPause.transform.position + Vector3.forward * 0.25f;
		}
	}

	/// <summary>
	/// OnMouseUp is called when the user has released the mouse button.
	/// </summary>
	/// <param name="goName">Game object name</param>
	public override void OnMouseUp(String goName)
	{
		if (goName.Equals (_rootPause) == true && goName == _buttonName)
		{
			_play = false;// pause
			_triggerPause.SetActive(false);//turn off the pause button
			OnMouseExit(_rootPause);//roll away button
			MainActivity.Instance.OnGetPausePanel();//show pauses panel
		}
	}

	public override void OnPauseGame()
	{
		if (_play == true)
		{
			_play = false;
		}
		else
		{
			_play = true;
		}
		_triggerPause.SetActive(true);//Turn on the pause button
	}

	public override void GetPausePanel () 
	{
		_pausePanel.Choice (ResumeFunction);
	}

	public void GetGameOverPanel () 
	{
		_gameOverPanel.Choice (ResumeFunction);
	}

	public void GetTopScorePanel () 
	{
		_topScorePanel.Choice (ResumeFunction);
	}

	/// <summary>
	/// Rudiment, not functional
	/// </summary>
	void ResumeFunction () 
	{
		Debug.Log("ResumeFunction");
	}


	/// <summary>
	/// Reset obgect position
	/// </summary>
	public override void GarbageCollection()
	{
		_buttonPause.SetActive(false);
		_colorBlock = Color.white;
		_triggerPause.SetActive(false);
		_digitTopScore.SetActive(false);
		_digitTopScoreShadow.SetActive(false);
		_digitScore.SetActive(false);
		_digitScoreShadow.SetActive(false);
		_digitLevel.SetActive(false);
		_digitLevelShadow.SetActive(false);
		_digitLines.SetActive(false);
		_digitLinesShadow.SetActive(false);
		_spawn.SetActive(false);
		_pauseManager.SetActive(false);
		_gameOverManager.SetActive(false);
		_topScoreManager.SetActive(false);
	}

	#endregion
	///////////////////////////////////////////////////////////////
}
