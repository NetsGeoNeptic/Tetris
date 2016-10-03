using UnityEngine;
using System.Collections;
using System.Threading;

public class StateCredits : StateBase
{
	///////////////////////////////////////////////////////////////
	#region Variables

	/// <summary>
	/// Footage of the scene in seconds
	/// </summary>
	int footageScene = 18;


	#endregion
	///////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////
	#region Properties

	public StateCredits() : base(EStateId.Credits)
	{
		
	}

	#endregion
	///////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////
	#region Interface

	public override void Activate()
	{
		base.Activate();
		Application.LoadLevel(2);
		MainActivity.Instance.InvokeMethod (footageScene);
	}

	#endregion
	///////////////////////////////////////////////////////////////
	#region Implementation

	/// <summary>
	/// Update method, called every frame
	/// </summary>
	public override void Update()
	{
		if (Input.anyKey)
		{
			MainActivity.Instance.SetState(EStateId.Menu);			
		}
	}


	public override void OnDelayMethodLoaded()
	{
		MainActivity.Instance.SetState(EStateId.Menu);
	}

	#endregion
	///////////////////////////////////////////////////////////////
	
}
