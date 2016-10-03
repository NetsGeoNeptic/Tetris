using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject[] groups;	
	public int i = 1; 
	public bool remove = false; 
	private GameObject _instantiatedObj; 


	public void spawnNext()
	{
		StartCoroutine(Spawn());
	}

	private IEnumerator Spawn()
	{
		if (remove == true)
		{
			TerminateObject ();// Destroy Object
		}
		remove = false;		
		_instantiatedObj = (GameObject) Instantiate(groups[i-1], transform.position, Quaternion.identity);// Spawn Group at current Position
		yield break;	
	}

	public void TerminateObject()
	{
		Debug.Log ("TerminateObject()");
		if (_instantiatedObj != null)
			Destroy(_instantiatedObj);
	}
}
