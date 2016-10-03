using UnityEngine;
using System.Collections;

public class ScrollUVvertical : MonoBehaviour 
{
	void Update ()
	{
		MeshRenderer mr = GetComponent<MeshRenderer> ();
		Material mat = mr.material;
		Vector2 offset = mat.mainTextureOffset;
		offset.y -= Time.deltaTime / 25.0f;
		mat.mainTextureOffset = offset;			
	}
}
