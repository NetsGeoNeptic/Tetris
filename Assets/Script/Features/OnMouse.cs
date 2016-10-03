using UnityEngine;
using System.Collections;

public class OnMouse : MonoBehaviour
{
	/// <summary>
	/// Called when the mouse enters
	/// </summary>
	void OnMouseEnter()
	{
		MainActivity.Instance.TriggerMouseEnter(this.gameObject.name); //this.gameObject.name - <param name="goName">Game object name</param>
	}

	/// <summary>
	/// This event is sent if pressing the mouse
	/// </summary>
	void OnMouseDown()
	{
		MainActivity.Instance.TriggerMouseDown(this.gameObject.name); //this.gameObject.name - <param name="goName">Game object name</param>	
	}

	/// <summary>
	/// This event is sent if the mouse came from the object
	/// </summary>
	void OnMouseExit()
	{
		MainActivity.Instance.TriggerMouseExit(this.gameObject.name);//this.gameObject.name - <param name="goName">Game object name</param>
	}

	/// <summary>
	/// OnMouseUp is called when the user has released the mouse button.
	/// </summary>
	void OnMouseUp()
	{
		MainActivity.Instance.TriggerMouseUp(this.gameObject.name);//this.gameObject.name - <param name="goName">Game object name</param>
	}
}
