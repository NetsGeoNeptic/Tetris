using UnityEngine;
using System.Collections;
using System;


public class MenuState : StateBase
{
	///////////////////////////////////////////////////////////////
	#region Variables

	private string _rootGame = "triggerNewGame";
	private string _rootCredits = "triggerCredits"; 
	private string _rootQuit = "triggerQuit";
	private string _buttonName = null; //variable to determine the current trigger

	private GameObject[] _lettersColorQuit; // Quit Button Color 
	private GameObject _buttonQuit; // Quit Button
	private GameObject _triggerQuit; // Trigger Quit

	private GameObject[] _lettersColorNewGame; // New Game Button Color
	private GameObject _buttonNewGame; // New Game Button
	private GameObject _triggerNewGame; // Trigger New Game

	private GameObject[] _lettersColorCredits; // Credits color button
	private GameObject _buttonCredits; // Button Credits
	private GameObject _triggerCredits; // Credits Trigger

	private Color _colorBlock = Color.red;

	#endregion
	///////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////
	#region Properties

	public MenuState() : base(EStateId.Menu)
	{
	}

	#endregion
	///////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////
	#region Implementation

	public override void Activate()
	{
		base.Activate();
		Application.LoadLevel(1);
		//SceneManager.LoadScene (1);
	}

	public override void OnSceneLoaded () // Load the scene and activate the appropriate interface
	{
		Debug.Log("OnSceneLoaded");

		base.OnSceneLoaded ();

		_lettersColorNewGame = MainActivity.Instance.lettersColorNewGame; // New Game color Button
		_buttonNewGame = MainActivity.Instance.buttonNewGame; // New Game button
		_triggerNewGame  = MainActivity.Instance.triggerNewGame; // New Game Trigger 

		_lettersColorCredits = MainActivity.Instance.lettersColorCredits; // Credits Color Button 
		_buttonCredits = MainActivity.Instance.buttonCredits; // Credits Button
		_triggerCredits  = MainActivity.Instance.triggerCredits; // Credits Trigger in the current state

		_lettersColorQuit = MainActivity.Instance.lettersColorQuit;  // Quit Button Color in the current state
		_buttonQuit = MainActivity.Instance.buttonQuit; // Quit Button in the current state
		_triggerQuit  = MainActivity.Instance.triggerQuit; // Trigger Quit in the current state

		_buttonNewGame.SetActive(true);
		_triggerNewGame.SetActive(true);

		_buttonCredits.SetActive(true);
		_triggerCredits.SetActive(true);

		_buttonQuit.SetActive(true);
		_triggerQuit.SetActive(true);

	}

	/// <summary>
	/// Called when the mouse enters
	/// </summary>
	/// <param name="goName">Game object name</param>
	public override void OnMouseEnter(String goName)
	{
		//Debug.Log("MenuState OnMouseEnter");
		_buttonName = goName;
		if (goName.Equals (_rootGame) == true)
		{		
			_colorBlock = Color.green;
			foreach (GameObject go in _lettersColorNewGame)
			{
				go.GetComponent<Renderer>().material.SetColor("_Color", _colorBlock);
			}
			_buttonNewGame.transform.position = _buttonNewGame.transform.position - Vector3.forward * 2.0f;
		}	

		else if (goName.Equals (_rootCredits) == true)
		{		
			_colorBlock = Color.yellow;
			foreach (GameObject go in _lettersColorCredits)
			{
				go.GetComponent<Renderer>().material.SetColor("_Color", _colorBlock);
			}
			_buttonCredits.transform.position = _buttonCredits.transform.position - Vector3.forward * 2.0f;
		}

		else if(goName.Equals (_rootQuit) == true)
		{		
			_colorBlock = Color.red;
			foreach (GameObject go in _lettersColorQuit)
			{
				go.GetComponent<Renderer>().material.SetColor("_Color", _colorBlock);
			}
			_buttonQuit.transform.position = _buttonQuit.transform.position - Vector3.forward * 2.0f;
		}

	}

	/// <summary>
	/// This event is sent if the mouse came from the object
	/// </summary>
	/// <param name="goName">Game object name</param>
	public override void OnMouseExit(String goName)
	{
		//Debug.Log ("MenuState OnMouseExit");
		_buttonName = null;
		if (goName.Equals (_rootGame) == true)
		{			
			_colorBlock = Color.white;
			foreach (GameObject go in _lettersColorNewGame) {
				go.GetComponent<Renderer> ().material.SetColor ("_Color", _colorBlock);				
			}
			_buttonNewGame.transform.position = _buttonNewGame.transform.position + Vector3.forward * 2.0f;
		}
		else if (goName.Equals (_rootCredits) == true)
		{			
			_colorBlock = Color.white;
			foreach (GameObject go in _lettersColorCredits) {
				go.GetComponent<Renderer> ().material.SetColor ("_Color", _colorBlock);				
			}
			_buttonCredits.transform.position = _buttonCredits.transform.position + Vector3.forward * 2.0f;
		}
		else if (goName.Equals (_rootQuit) == true)
		{			
			_colorBlock = Color.white;
			foreach (GameObject go in _lettersColorQuit) {
				go.GetComponent<Renderer> ().material.SetColor ("_Color", _colorBlock);				
			}
			_buttonQuit.transform.position = _buttonQuit.transform.position + Vector3.forward * 2.0f;
		}
	}

	/// <summary>
	/// OnMouseUp is called when the user has released the mouse button.
	/// </summary>
	/// <param name="goName">Game object name</param>
	public override void OnMouseUp(String goName)
	{
		Debug.Log ("MenuState OnMouseUp");
		if (goName.Equals (_rootQuit) == true && goName == _buttonName)
		{
			Application.Quit();			
		}
		else if (goName.Equals (_rootCredits) == true && goName == _buttonName)
		{
			MainActivity.Instance.OnGarbageCollection ();
			ShowCredits();
		}
		else if (goName.Equals (_rootGame) == true && goName == _buttonName)
		{
			MainActivity.Instance.OnGarbageCollection ();
			StartNewGame();
		}
	}

	/// <summary>
	/// Reset obgect position
	/// </summary>
	public override void GarbageCollection()
	{
		_colorBlock = Color.white;
		_buttonNewGame.SetActive(false);
		_triggerNewGame.SetActive(false);
		_buttonNewGame.transform.position = new Vector3(-6.0f, -16.0f, 0.0f);
		foreach (GameObject go in _lettersColorNewGame)
		{
			go.GetComponent<Renderer> ().material.SetColor ("_Color", _colorBlock);				
		}

		_buttonCredits.SetActive(false);
		_triggerCredits.SetActive(false);
		_buttonCredits.transform.position = new Vector3(-6.0f, -17.0f, 0.0f);
		foreach (GameObject go in _lettersColorCredits)
		{
			go.GetComponent<Renderer> ().material.SetColor ("_Color", _colorBlock);				
		}

		_buttonQuit.SetActive(false);
		_triggerQuit.SetActive(false);
	}

	/// <summary>
	/// The transition to a Credits State
	/// </summary>
	public void ShowCredits()
	{
		MainActivity.Instance.SetState(EStateId.Credits); // Load Credits state
		Debug.Log ("ShowCredits");
	}

	/// <summary>
	/// The transition to a Game state
	/// </summary>
	public void StartNewGame()
	{
		MainActivity.Instance.SetState(EStateId.Game); // Load Game state
		Debug.Log ("StartNewGame");
	}
	#endregion
	///////////////////////////////////////////////////////////////
}

