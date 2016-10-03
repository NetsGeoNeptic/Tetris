using UnityEngine;
using System.Collections;

public class StartIntro : MonoBehaviour
{
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////
	#region Variables

	/// <summary>
	/// Fading scene
	/// </summary>
	public GameObject fade;


	/** Field information output */
	private GameObject[,] _imgView; //field mapping
	private GameObject _cubes;

	private int _width=21;
	private int _height=13;
	private bool[,] _pole = new bool[13+5,21+3]; // This is our field
	private bool[,] _block = new bool[4,4]; // our figure
	private int _iY,_jX = 0; // Coordinates of the figure.

	private float _vanish = 0.0f;

	/** figure */	
	private Palka _palka = new Palka();
	private Horse _horse = new Horse();
	private Ziga  _ziga  = new Ziga();
	private Krest _krest = new Krest();
	private Cube _cube = new Cube();
	private HorseR _horseR = new HorseR();
	private ZigaR  _zigaR  = new ZigaR();
	/** end figure */

	/** current events */
	private int[] _arrayForm = new int[] {5,1,4,6,4,2,7,6,2,6,1,2,6,1,2,6,1};
	private int[] _arrayPosition = new int[] {1,2,3,4,1,4,1,4,4,1,1,4,4,1,2,4,2};
	private int[] _arrayСoordinates = new int[] {1,1,1,5,5,5,9,10,9,12,14,13,16,16,19,19,18};
	private int[] _arraySteps = new int[] {13,12,11,14,11,10,13,12,10,13,13,10,14,12,14,12,10};
	/** end current events */

	/** The extra units */
	private int[] _excessBlockX = new int[] {0,0,0,2,3,5,8,8,10,10,11,14,15,15,17,17};
	private int[] _excessBlockY = new int[] {0,1,2,2,2,3,0,3,1,2,2,2,1,0,4,0};
	/** end  */

	/** Audio variable*/
	public AudioClip boom;
	public AudioClip disappearance;
	public AudioClip intro;
	/** end */

	/** Light */
	public GameObject floodlight;
	private float _powerOfLight = 0;
	private int _spot = 30;
	private Color _colorBlock = Color.red;

	#endregion
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////
	#region Implementation

	void Start ()
	{
		Debug.Log("Start ()");		 
		Initializer();
		StartCoroutine(IntroGame());
		Invoke("LoadLevel", 10f);
	}


	/// <summary>
	/// Object initialization
	/// </summary>
	private void Initializer()
	{
		_cubes = MainActivity.Instance.cube;
		_imgView = new GameObject[_height,_width];
		for (var i = 0;i<_imgView.GetLength(0);i++)// Initialize field components
		{
			for (var j = 0;j<_imgView.GetLength(1);j++)
			{
				_imgView[i,j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
				_imgView[i,j].transform.position = new Vector3(j, i , 0);
				_imgView[i,j].transform.GetComponent<MeshFilter>().mesh = _cubes.GetComponent<MeshFilter>().mesh;
				_imgView[i,j].transform.GetComponent<MeshRenderer>().material = _cubes.GetComponent<MeshRenderer>().material;
				_imgView[i,j].GetComponent<Renderer>().enabled = true;
			}
		}

		for (var i = 0; i < _pole.GetLength(0); i++) // clear the field
		{
			for (var j = 0; j < _pole.GetLength(1); j++)
			{
				_pole[i,j] = false;
			}
		}
		DrawPole();
	}

	/// <summary>
	/// Intro beginning
	/// </summary>
	private IEnumerator IntroGame()
	{

		for (var i = 0; i < _arrayForm.GetLength(0); i++)
		{
			BuildBlock(_arrayForm[i],_arrayPosition[i],_block);
			_iY = 0; // Coordinates of the initial position of the figure
			_jX = _arrayСoordinates[i];

			for (var step = 0; step < _arraySteps[i]; step++)
			{
				Movedown(step,_arraySteps[i]);
				yield return new WaitForSeconds(0.01f);
			}
		}
		StartCoroutine(LightOn());
		StartCoroutine(VanishLine());
		AudioSource.PlayClipAtPoint(intro, transform.position); 
		AudioSource.PlayClipAtPoint(disappearance, transform.position); 

	}

	/// <summary>
	/// Figures fall
	/// </summary>
	private void Movedown(int step, int steps)
	{		
		DrawPole ();
		_iY = _iY + 1;
		DrawBlock ();

		if (step == steps - 1)
		{/**Save the figure on the field*/
			//Debug.Log ("movedown()");
			for (var i = 0; i < _block.GetLength(0); i++) {
				for (var j = 0; j < _block.GetLength(1); j++) {
					if (_block [i, j] == true)
						_pole [i + _iY, j + _jX] = true;
				}
			}
			AudioSource.PlayClipAtPoint(boom, transform.position); 
		}
	}

	/// <summary>
	/// Field Rendering
	/// </summary>
	private void DrawPole()
	{
		for (var i = 0; i < _pole.GetLength(0); i++)
		{
			for (var j = 0; j < _pole.GetLength(1); j++)
			{
				if (_pole[i,j] == true && j >= 1 && j < _width+1 && i >= 4 && i <= _height+3)
				{
					_imgView[(_height-i)+3,j - 1].GetComponent<Renderer>().material.SetFloat("_Cutoff", 0);
					_imgView[(_height-i)+3,j - 1].GetComponent<Renderer>().enabled = true;					
				}
				else if (_pole[i,j] == false && j >= 1 && j < _width+1 && i >= 4 && i <= _height+3)
				{
					_imgView[(_height-i)+3,j - 1].GetComponent<Renderer>().material.SetFloat("_Cutoff", 0);
					_imgView[(_height-i)+3,j - 1].GetComponent<Renderer>().enabled = false;
				}				
			}
		}
	}

	/// <summary>
	/// Figure Rendering
	/// </summary>
	private void DrawBlock()
	{
		for (var i = 0; i < _block.GetLength(0); i++)
		{
			for (var j = 0; j < _block.GetLength(1); j++)
			{
				if ((_block[i,j] == true) && (j + _jX >= 1) && (i + _iY <= _height+3) && (i + _iY >= 4)) 
				{
					_imgView[(_height-i)+3 - _iY,j - 1 + _jX].GetComponent<Renderer>().enabled = true;
					_imgView[(_height-i)+3 - _iY,j - 1 + _jX].GetComponent<Renderer>().material.SetColor("_Color", _colorBlock);
				} 				
			}
		}
	}/** End drawBlock */

	/// <summary>
	/// Forming a Figure
	/// </summary>
	/// <param name="rand">figure number</param>
	/// <param name="position">figure position</param>
	/// <param name="block">Link to figure</param>
	private void BuildBlock(int rand, int position,bool[,] block)
	{		
		/** Forming a Figure */
		switch (rand)
		{
		case 1: _colorBlock = Color.red    ;_palka.getBlock(block,position); break;
		case 2: _colorBlock = Color.blue   ;_horse.getBlock(block,position);break;
		case 3: _colorBlock = Color.yellow ;_ziga.getBlock(block,position);break;
		case 4: _colorBlock = Color.green  ;_krest.getBlock(block,position);break;
		case 5: _colorBlock = Color.magenta;_cube.getBlock(block,position);break;
		case 6: _colorBlock = Color.cyan   ;_horseR.getBlock(block,position);break;
		case 7: _colorBlock = Color.white  ;_zigaR.getBlock(block,position);break;
		default:
			break;
		}		
	}/** End build */

	/// <summary>
	/// Remove the extra blocks
	/// </summary>
	private IEnumerator VanishLine()
	{
		_vanish = _vanish + 0.025f;

		for (var i = 0; i < _excessBlockX.GetLength(0); i++)
		{
			_imgView[_excessBlockY[i],_excessBlockX[i]].GetComponent<Renderer>().material.SetFloat("_Cutoff", _vanish);
		}

		if (_vanish >= 1.0f)
		{
			yield break; //Stop Corutine when the line disappeared
		}

		yield return new WaitForSeconds(0.02f);
		StartCoroutine(VanishLine());
	}

	/// <summary>
	/// Turn on the Light
	/// </summary>
	private IEnumerator LightOn()
	{
		_powerOfLight = _powerOfLight + 0.025f;

		floodlight.GetComponent<Light>().intensity = _powerOfLight;

		if (_spot <= 179)
		{
			_spot = _spot + 2;
			floodlight.GetComponent<Light>().spotAngle = _spot;
		}

		if (_powerOfLight >= 3.0f)
		{
			yield break; //stop the Coroutine
		}

		yield return new WaitForSeconds(0.02f);
		StartCoroutine(LightOn());
	}

	/// <summary>
	/// Load main Menu
	/// </summary>
	private void LoadLevel()
	{
		Debug.Log("Invoking LoadLevel");
		MainActivity.Instance.SetState(EStateId.Menu);
	}


	/// <summary>
	/// Pass Intro by pressing any button
	/// </summary>
	void Update() 
	{
		if (Input.anyKey)
		{
			//Debug.Log("A key or mouse click has been detected");
			ScreenFader screenFader = fade.GetComponent<ScreenFader>();
			screenFader.fadeSpeed = 2.5f;
			screenFader.sceneStarting = false;
			screenFader.sceneEnding = true;
		}
		//Debug.Log("A key or mouse click has been detected");
	}
	#endregion
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
