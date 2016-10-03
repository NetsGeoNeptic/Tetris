using UnityEngine;
using System.Collections;

/// <summary>
/// Class for spawn figures.
/// </summary>
public class ObjectFeature : MonoBehaviour
{
	private float _moveUp;

	void Start ()
	{
		StartCoroutine(MoveUp());	
	}

	void Update ()
	{

	}

	private IEnumerator MoveUp ()
	{
		_moveUp = _moveUp + 0.1f;
		transform.position += new Vector3(0, 0.5f, 0);

		if (_moveUp >= 1.0f)
		{
			yield break;
		}
		yield return new WaitForSeconds(0.01f);
		StartCoroutine(MoveUp());
	}


}
