using UnityEngine;
using System.Collections;

abstract class Figura
{	
	void testAbstract()
	{
		Debug.Log("All Systems Nominal");		
	}
	
	abstract public void getBlock(bool[,] block, int position);	
}