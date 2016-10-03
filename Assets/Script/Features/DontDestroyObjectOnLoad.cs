using UnityEngine;
using System.Collections;

/// <summary>
/// Assign this script to object you don't want to delete on load level.
/// </summary>
public class DontDestroyObjectOnLoad : MonoBehaviour 
{
	void Start() 
	{
		UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
	}
}