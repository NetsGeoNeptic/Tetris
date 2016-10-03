using UnityEngine;
using System.Collections;

public partial class MainActivity : MonoBehaviour
{
	// This part of the Main Activity is responsible for the game

	#region Variables

	public GameObject digitTopScore;
	public GameObject digitTopScoreShadow; 

	public GameObject digitScore;
	public GameObject digitScoreShadow;

	public GameObject digitLevel;
	public GameObject digitLevelShadow;

	public GameObject digitLines;
	public GameObject digitLinesShadow;

	public GameObject[] lettersColorPause; // Pause Button Color 
	public GameObject buttonPause; // Pause Button
	public GameObject triggerPause; // Trigger Pause

	/// <summary>
	/// Audio variables
	/// </summary>
	public AudioClip boom;
	public AudioClip disappearance;

	public GameObject spawn; // Spawn figure

	public GameObject pauseManager; // Pause Manager
	public GameObject gameOverManager; // Game Over Manager
	public GameObject topScoreManager; // Top Score manager

	#endregion
	///////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////
	#region Implementation

	/// <summary>
	/// Random number generation
	/// </summary>
	/// <param name="min">min number [inclusive]</param>
	/// <param name="max">max number [exclusive]</param>
	/// <returns>Returns a random integer number between min [inclusive] and max [exclusive]</returns>
	public int RandomRange(int min,int max)
	{
		return Random.Range(min,max);		
	}


	#endregion
	///////////////////////////////////////////////////////////////

}
