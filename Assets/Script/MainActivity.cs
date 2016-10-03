using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class MainActivity : MonoBehaviour
{
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////
	#region Variables

	/// <summary>
	/// Сurrent state.
	/// </summary>
	private static MainActivity _instance = null;


	public GameObject cube; // The basis for the construction of the field;
	public GameObject[] lettersColorQuit; // Quit Button Color 
	public GameObject buttonQuit; // Button Quit
	public GameObject triggerQuit; // Trigger Quit

	public GameObject[] lettersColorNewGame; 
	public GameObject buttonNewGame; 
	public GameObject triggerNewGame; 

	public GameObject[] lettersColorCredits; 
	public GameObject buttonCredits; 
	public GameObject triggerCredits; 

	private Dictionary<EStateId, StateBase> _states = new Dictionary<EStateId,StateBase>();
	private StateBase _currentState;
	#endregion
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////
	#region Interface

	public MainActivity()
	{
		_instance = this;
	}

	public void Start()
	{
		InitStates();
		//SetState(EStateId.None);
	}

	/// <summary>
	/// Update method, called every frame
	/// </summary>
	public void Update()
	{
		UpdateStates();
	}


	public void OnLevelWasLoaded()
	{
		OnSceneLoadedState();	
	}

	/// <summary>
	/// Garbage Collection
	/// </summary>
	public void OnGarbageCollection()
	{
		GarbageCollection ();	
	}

	/// <summary>
	/// Start new Games
	/// </summary>
	public void OnNewGame()
	{
		NewGame();
	}

	/// <summary>
	/// Get Pause Panel
	/// </summary>
	public void OnGetPausePanel ()
	{
		GetPausePanel ();		
	}

	/// <summary>
	/// Method, called on application pause
	/// </summary>
	public void OnPauseInGame()
	{
		OnScenePause();	
	}

	/// <summary>
	/// Called when the mouse enters
	/// </summary>
	/// <param name="goName">Game object name</param>
	public void TriggerMouseEnter(String goName)
	{
		OnMouseEnterStates(goName);	
	}

	/// <summary>
	/// This event is sent if the mouse came from the object
	/// </summary>
	/// <param name="goName">Game object name</param>
	public void TriggerMouseExit(String goName)
	{		
		OnMouseExitStates(goName);
	}

	/// <summary>
	/// This event is sent if pressing the mouse
	/// </summary>
	/// <param name="goName">Game object name</param>
	public void TriggerMouseDown(String goName)
	{		
		OnMouseDownStates(goName);
	}

	/// <summary>
	/// OnMouseUp is called when the user has released the mouse button.
	/// </summary>
	/// <param name="goName">Game object name</param>
	public void TriggerMouseUp(String goName)
	{		
		OnMouseUpStates(goName);
	}

	/// <summary>
	/// Load Method (OnDelayMethodLoaded)
	/// </summary>
	/// <param name="delay">the delay in seconds</param>
	public void InvokeMethod(int delay)
	{
		CancelInvoke ();
		//CancelInvoke ("OnInvokeMethod");
		Invoke ("OnInvokeMethod", delay);				
	}


	#endregion
	///////////////////////////////////////////////////////////////



	///////////////////////////////////////////////////////////////
	#region Properties

	public static MainActivity Instance
	{
		get
		{
			return _instance;
		}
	}

	#endregion
	///////////////////////////////////////////////////////////////



	///////////////////////////////////////////////////////////////
	#region Interface
	/// <summary>
	/// Set state
	/// </summary>
	public void SetState(EStateId stateId)
	{
		print("SetState = " + stateId);

		if(!_states.ContainsKey(stateId))
		{
			print ("State " + stateId + " does not exist");
			return;
		}

		if(_currentState != null)
		{
			if(_currentState.StateId != stateId)
			{
				_currentState.Deactivate();
				_currentState = null;
			}
		}

		StateBase nState = _states[stateId];
		_currentState = nState;
		_currentState.Activate();
	}

	#endregion
	///////////////////////////////////////////////////////////////


	///////////////////////////////////////////////////////////////
	#region Implementation

	/// <summary>
	/// Initialization states
	/// </summary>
	public void InitStates()
	{
		_states[EStateId.Menu] = new MenuState();
		_states[EStateId.Credits] = new StateCredits();
		_states[EStateId.Game] = new StateGame();
	}

	/// <summary>
	/// Garbage Collection
	/// </summary>
	private void GarbageCollection ()
	{
		if(_currentState != null)
		{
			_currentState.GarbageCollection();
		}	

	}

	/// <summary>
	/// Start New Game
	/// </summary>
	private void NewGame()
	{
		if(_currentState != null)
		{
			_currentState.NewGame();
		}			
	}

	/// <summary>
	/// Get Pause Panel in the current state
	/// </summary>
	private void GetPausePanel()
	{
		if(_currentState != null)
		{
			_currentState.GetPausePanel();
		}		
	}

	/// <summary>
	/// Game Stop.
	/// </summary>
	private void OnScenePause()
	{
		if(_currentState != null)
		{
			_currentState.OnPauseGame();
		}
		
	}

	/// <summary>
	/// Calling OnMouseEnter() in the current state
	/// </summary>
	private void OnMouseEnterStates(String goName)
	{
		if(_currentState != null)
		{
			_currentState.OnMouseEnter(goName);
		}
	}

	/// <summary>
	/// Calling OnMouseDown() in the current state
	/// </summary>
	private void OnMouseDownStates(String goName)
	{
		if(_currentState != null)
		{
			_currentState.OnMouseDown(goName);
		}
	}

	/// <summary>
	/// Calling OnMouseEnter() in the current state
	/// </summary>
	private void OnMouseUpStates(String goName)
	{
		if(_currentState != null)
		{
			_currentState.OnMouseUp(goName);
		}
	}

	/// <summary>
	/// Calling OnMouseExit() in the current state
	/// </summary>
	private void OnMouseExitStates(String goName)
	{
		if(_currentState != null)
		{
			_currentState.OnMouseExit(goName);
		}
	}

	/// <summary>
	/// Update frame in the current state
	/// </summary>
	private void UpdateStates()
	{
		if(_currentState != null)
		{
			_currentState.Update();
		}
	}

	/// <summary>
	/// delayed method call
	/// </summary>
	private void OnInvokeMethod()
	{
		if(_currentState != null)
		{
			_currentState.OnDelayMethodLoaded();
		}
		
	}


	private void OnSceneLoadedState()
	{
		if(_currentState != null)
		{
			_currentState.OnSceneLoaded();	
		}
	}

	#endregion
	///////////////////////////////////////////////////////////////


	///////////////////////////////////////////////////////////////
	#region Properties

	/// <summary>
	/// Gets current state
	/// </summary>
	public StateBase CurrentState
	{
		get
		{
			return _currentState;
		}
	}

	public EStateId CurrentStateId
	{
		get
		{
			if (_currentState != null)
			{
				return _currentState.StateId;
			}

			return EStateId.None;
		}
	}

	#endregion
	///////////////////////////////////////////////////////////////

}