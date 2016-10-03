using UnityEngine;
using System.Collections;

class Cube : Figura
{
	public override	void getBlock(bool[,] block, int position) 
	{
		switch (position)
		{
		case 1:
		case 2:
		case 3:
		case 4:			 
			block[0,0]=false; block[0,1]=false; block[0,2]=false; block[0,3]=false; 
			block[1,0]=false; block[1,1]=false; block[1,2]=false; block[1,3]=false; 
			block[2,0]=true; block[2,1]=true; block[2,2]=false; block[2,3]=false; 
			block[3,0]=true; block[3,1]=true; block[3,2]=false; block[3,3]=false;
			break;
		default:
			break;
		}
	}
	
}
