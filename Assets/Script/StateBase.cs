using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StateBase
{

	///////////////////////////////////////////////////////////////
	#region Variables


	private readonly EStateId _stateId;

	#endregion
	///////////////////////////////////////////////////////////////



	///////////////////////////////////////////////////////////////
	#region Interface

	/// <summary>
	/// Initialise a new instance of the class
	/// </summary>
	public StateBase(EStateId stateId)
	{
		_stateId = stateId;
	}

	/// <summary>
	/// Activates data
	/// </summary>
	public virtual void Activate()
	{
		OnActivate();	
	}

	/// <summary>
	/// Deactivates data
	/// </summary>
	public void Deactivate()
	{
		OnDeactivate();	
	}

	protected virtual void OnActivate()
	{

	}

	protected virtual void OnDeactivate()
	{

	}

	/// <summary>
	/// Update method, called every frame
	/// </summary>
	public virtual void Update()
	{

	}

	public virtual void OnSceneLoaded()
	{

	}

	/// <summary>
	/// Called when the mouse enters
	/// </summary>
	/// <param name="goName">Game object name</param>
	public virtual void OnMouseEnter(String goName)
	{

	}

	/// <summary>
	/// This event is sent if the mouse came from the object
	/// </summary>
	/// <param name="goName">Game object name</param>
	public virtual void OnMouseExit(String goName)
	{
		
	}

	/// <summary>
	/// This event is sent if pressing the mouse
	/// </summary>
	/// <param name="goName">Game object name</param>
	public virtual void OnMouseDown(String goName)
	{

	}

	/// <summary>
	/// OnMouseUp is called when the user has released the mouse button.
	/// </summary>
	/// <param name="goName">Game object name</param>
	public virtual void OnMouseUp(String goName)
	{

	}

	/// <summary>
	/// delayed method call
	/// </summary>
	public virtual void OnDelayMethodLoaded()
	{

	}

	/// <summary>
	/// Game Stop.
	/// </summary>
	public virtual void OnPauseGame()
	{
		
	}

	/// <summary>
	/// Get Pause Panel
	/// </summary>
	public virtual void GetPausePanel()
	{

	}

	/// <summary>
	/// Start New Game
	/// </summary>
	public virtual void NewGame()
	{
		
	}

	/// <summary>
	/// Garbage Collection
	/// </summary>
	public virtual void GarbageCollection()
	{

	}



	#endregion
	///////////////////////////////////////////////////////////////


	///////////////////////////////////////////////////////////////
	#region Properties

	public EStateId StateId
	{
		get
		{
			return _stateId;
		}
	}

	#endregion
	///////////////////////////////////////////////////////////////

}

///////////////////////////////////////////////////////////////////////////
#region Types

/// <summary>
/// enumeration states
/// </summary>
public enum EStateId
{
	Menu,
	Game,
	Credits,
	None
}

#endregion
///////////////////////////////////////////////////////////////////////////
