using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.Events;

public partial class StateGame : StateBase
{
	///////////////////////////////////////////////////////////////
	#region Variables

	/** figure */		
	private Palka _palka = new Palka();
	private Horse _horse = new Horse();
	private Ziga  _ziga  = new Ziga();
	private Krest _krest = new Krest();
	private Cube _cubes = new Cube();
	private HorseR _horseR = new HorseR();
	private ZigaR  _zigaR  = new ZigaR();
	/** end figure */

	/** Field information output */
	private GameObject[,] _imgView; //field mapping
	private int _width=10;
	private int _height=20;
	private bool[,] _pole = new bool[20+5,10+3]; // This is our field
	private bool[,] _block = new bool[4,4]; // our figure
	/** end  */

	/** Variable Game */
	private bool _play = true; // stop game
	private int _topScore; //top score
	private int _score = 0;//score
	private int _level = 1;//level (effect on game speed)
	private int _lines = 0;//delete lines (effect on level)
	private float _gameSpeed = 0.5f;//game speed
	private bool _gamesOver = false; // game over
	private int _iY,_jX; // coordinates of the figure.
	private int _form, _formNext; //figure number
	private int _position; //position figure
	/** end Variable Game */

	/** Variable management game */
	private float _lastMove = 0.0f;//to move while holding the left / right.
	private bool _boostDown = false;//acceleration of the fall
	private float _lastFall = 0.0f;
	/** end */

	private int _vanishi = 0;
	private float _vanish = 0.0f;
	private bool _vanLine = false;

	private int _count = 0; // calculation of the reduced lines

	#endregion
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////
	#region Implementation

	/// <summary>
	/// Beginning of the game
	/// </summary>
	private void Start ()
	{
		Debug.Log("Start ()");
		Initializer();
		NewGame();
	}	

	/// <summary>
	/// Object initialization
	/// </summary>
	private void Initializer()
	{
		
		_imgView = new GameObject[_height,_width];
		for (int i =0;i<_imgView.GetLength(0);i++)// Initialize field components
		{
			for (int j =0;j<_imgView.GetLength(1);j++)
			{
				_imgView[i,j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
				_imgView[i,j].transform.position = new Vector3(j, i , 0);
				_imgView[i,j].transform.GetComponent<MeshFilter>().mesh = _cube.GetComponent<MeshFilter>().mesh;
				_imgView[i,j].transform.GetComponent<MeshRenderer>().material = _cube.GetComponent<MeshRenderer>().material;
				_imgView[i,j].GetComponent<Renderer>().enabled = true;
			}
		}
	}

	/// <summary>
	/// New Game beginning
	/// </summary>
	public override void NewGame()
	{

		LoadTopScore(); //load best result
		_gamesOver = false;
		_triggerPause.SetActive(true);

		//reset result
		_score = 0;//score
		_level = 1;//level (effect on game speed)
		_lines = 0;//delete lines (effect on level)
		_gameSpeed = 0.5f;//game speed

		_digitTopScore.GetComponent<TextMesh>().text = ""+_topScore;
		_digitTopScoreShadow.GetComponent<TextMesh>().text = ""+_topScore;

		_digitScore.GetComponent<TextMesh>().text = ""+_score;
		_digitScoreShadow.GetComponent<TextMesh>().text = ""+_score;

		_digitLevel.GetComponent<TextMesh>().text = ""+_level;
		_digitLevelShadow.GetComponent<TextMesh>().text = ""+_level;

		_digitLines.GetComponent<TextMesh>().text = ""+_lines;
		_digitLinesShadow.GetComponent<TextMesh>().text = ""+_lines;

		_iY = 0; 
		_jX = 4; 

		_form = Random.Range(1, 7); 
		_position = 1; 
		BuildBlock(_form,_position,_block); 
		_formNext = Random.Range(1, 7); 

		_spawn.GetComponent<Spawner>().remove = true; 
		_spawn.GetComponent<Spawner>().i = _formNext;
		_spawn.GetComponent<Spawner>().spawnNext();

		for (int i = 0; i < _pole.GetLength(0); i++) 
		{
			for (int j = 0; j < _pole.GetLength(1); j++) {
				_pole[i,j] = false;
			}
		}
		DrawPole();
		_play = true;
	}

	/// <summary>
	/// Forming a Figure
	/// </summary>
	/// <param name="rand">figure number</param>
	/// <param name="position">figure position</param>
	/// <param name="block">Link to figure</param>
	void BuildBlock(int rand, int position,bool[,] mBlock)
	{		
		/** Forming a Figure */
		switch (rand)
		{
		case 1: _palka.getBlock(mBlock,position);break;
		case 2: _horse.getBlock(mBlock,position);break;
		case 3: _ziga.getBlock(mBlock,position);break;
		case 4: _krest.getBlock(mBlock,position);break;
		case 5: _cubes.getBlock(mBlock,position);break;
		case 6: _horseR.getBlock(mBlock,position);break;
		case 7: _zigaR.getBlock(mBlock,position);break;
		default:break;
		}	
	}/** End build */

	/// <summary>
	/// Field Rendering
	/// </summary>
	private void DrawPole()
	{
		for (int i = 0; i < _pole.GetLength(0); i++)
		{
			for (int j = 0; j < _pole.GetLength(1); j++)
			{
				if (_pole[i,j] == true && j >= 1 && j < _width+1 && i >= 4 && i <= _height+3)
				{
					_imgView[(_height-i)+3,j - 1].GetComponent<Renderer>().material.SetFloat("_Cutoff", 0.0f);
					_imgView[(_height-i)+3,j - 1].GetComponent<Renderer>().enabled = true;
				}
				if (_pole[i,j] == false && j >= 1 && j < _width+1 && i >= 4 && i <= _height+3)
				{
					_imgView[(_height-i)+3,j - 1].GetComponent<Renderer>().material.SetFloat("_Cutoff", 0.0f);
					_imgView[(_height-i)+3,j - 1].GetComponent<Renderer>().enabled = false;
				}				
			}
		}
	}

	/// <summary>
	/// Move (Left/Right)
	/// </summary>
	/// <param name="m">shift direction</param>
	private void Move(int m)
	{
		bool stop = false;
		for (int i = 0; i < _block.GetLength(0); i++)
		{
			for (int j = 0; j < _block.GetLength(1); j++) 
			{
				if ((_block[i,j] == true) && ((j + _jX + m <= 0) || (j + _jX + m >= _width+1) || (_pole[i + _iY,j+ _jX + m] == true)))
				{
					stop = true;
					break;					
				}				
			}
			if (stop == true) break;
		}
		if (stop == false)
		{
			DrawPole();
			if (m == 1) _jX++;
			else _jX--;
			DrawBlock();			
		}	
	}

	/// <summary>
	/// Figure Rendering
	/// </summary>
	public void DrawBlock()
	{
		for (int i = 0; i < _block.GetLength(0); i++)
		{
			for (int j = 0; j < _block.GetLength(1); j++)
			{
				if ((_block[i,j] == true) && (j + _jX >= 1) && (i + _iY <= _height+3) && (i + _iY >= 4)) 
				{
					_imgView[(_height-i)+3 - _iY,j - 1 + _jX].GetComponent<Renderer>().enabled = true;			
				} 				
			}
		}
	}/** End drawBlock */

	/// <summary>
	/// Figure Rotation
	/// </summary>
	private void Rotate()
	{
		bool[,] buferBlock = new bool[4,4];
		bool stop = false; 
		_position++;
		if (_position > 4) _position = 1;
		BuildBlock(_form, _position, buferBlock);		
		for (int i = 0; i < buferBlock.GetLength(0); i++)
		{
			for (int j = 0; j < buferBlock.GetLength(1); j++) 
			{

				if ((buferBlock[i,j] == true) && ((j + _jX <= 0) || (j + _jX >= _width+1) || (_pole[i + _iY,j+ _jX] == true) || (i+_iY>_height+3)))
				{
					stop = true;
					break;					
				}				
			}
			if (stop == true) break;
		}

		if (stop == false)
		{
			DrawPole();
			_block = buferBlock;
			DrawBlock();			
		}
		else
		{
			_position--; 
			if (_position < 1) _position = 4;			
		}		
	}/** End rotate */

	/// <summary>
	/// Figure Move Down
	/// </summary>
	void MoveDown()
	{
		if (_play == true)
		{
			//Debug.Log ("movedown()");
			for (int i = 0; i < _block.GetLength(0); i++)
			{
				for (int j = 0; j < _block.GetLength(1); j++)
				{
					//Debug.Log("i+iY ="+(i+iY));
					if ((_block [i, j] == true) && ((_pole [i + _iY + 1, j + _jX] == true) || (i + _iY >= 23)))
					{
						Debug.Log ("clash");
						_count = 0; // calculation of the reduced lines

						if (_iY == 0)
						{
							GameOver ();
						}							
						else
						{
							for (int i1 = 0; i1 < _block.GetLength(0); i1++)
							{
								for (int j1 = 0; j1 < _block.GetLength(1); j1++)
								{
									if (_block [i1, j1] == true)
										_pole [i1 + _iY, j1 + _jX] = true;
								}
							}
							AudioSource.PlayClipAtPoint(_boom, new Vector3(0.0f, 0.0f, 0.0f));

						}
						DelLine();
						_boostDown = false;
						_iY = 0; 
						_jX = 4; 
						_form = _formNext; 
						_formNext = Random.Range(1, 7); 

						_spawn.GetComponent<Spawner>().remove = true; 
						_spawn.GetComponent<Spawner>().i = _formNext;
						_spawn.GetComponent<Spawner>().spawnNext();

						_position = 1; 
						BuildBlock (_form, _position, _block); 		
					}				
				}
			}
			DrawPole ();
			if (_play == true)
			{
				_iY = _iY + 1;
				DrawBlock ();
			}
		}
	}

	/// <summary>
	/// Delete Line
	/// </summary>
	private void DelLine()
	{
		_play = false;
		int line = 0;

		for (int i = 0; i < _pole.GetLength(0); i++)
		{
			line = 0;
			for (int j = 0; j < _pole.GetLength(1); j++)
			{
				if (_pole[i,j] == true) line++;
				if (line == _width) 
				{
					_count++; // calculation of the reduced lines
					_vanishi = i;
					_vanish = 0;
					_vanLine = false;
					AudioSource.PlayClipAtPoint(_disappearance, new Vector3(0.0f, 0.0f, 0.0f)); 
					i = _pole.GetLength (0) + 1;
					break;
				}
			}
		}

		if (_gamesOver == false && _vanLine != false)
		{
			ScoreUpdate(_count); 
			_play = true; 
			DrawBlock();
		}
	}

	/// <summary>
	/// Field Down
	/// </summary>
	private void PoleDown()
	{
		for (int j = 0; j < _pole.GetLength(1); j++) _pole[_vanishi,j]=false;

		for (int i=_vanishi; i > 0; i--)
		{
			for (int j = 0; j < _pole.GetLength(1); j++)
			{
				_pole[i,j]=_pole[i-1,j]; 
			}
		}
		DrawPole();	
		DelLine ();
	}

	/// <summary>
	/// Vanish Line
	/// </summary>
	private void VanishLine()
	{
		_vanish = _vanish + 0.025f;
		for (int j = 0; j < _pole.GetLength(1); j++)
		{
			if (_pole[_vanishi,j] == true && j >= 1 && j < _width+1 && _vanishi >= 4 && _vanishi <= _height+3)
			{
				_imgView[(_height - _vanishi)+3,j - 1].GetComponent<Renderer>().material.SetFloat("_Cutoff", _vanish);			    
			}
		}
		if (_vanish >= 1.0f)
		{
			_lines++; 
			_digitLines.GetComponent<TextMesh>().text = ""+_lines;
			_digitLinesShadow.GetComponent<TextMesh>().text = ""+_lines;

			if ((_lines>=25*_level)&&(_level<10))LevelUpdate();

			_vanLine = true;
			PoleDown();
		}
	}

	/// <summary>
	/// Score Update
	/// </summary>
	/// <param name="count">number of deleted lines</param>
	public void ScoreUpdate(int count)
	{		
		switch (count)
		{
		case 1: _score++;break;
		case 2: _score = _score + 3;break;
		case 3: _score = _score + 5;break;
		case 4: _score = _score + 10;break;
		default:
			break;
		}

		// enter results
		_digitScore.GetComponent<TextMesh>().text = ""+_score;
		_digitScoreShadow.GetComponent<TextMesh>().text = ""+_score;

	}/** End ScoreUpdate */

	/// <summary>
	/// Increasing levels of difficulty (game speed)
	/// </summary>
	void LevelUpdate()
	{
		_gameSpeed = _gameSpeed - 0.04f;
		_level = _level + 1; // level++ Increasing levels
		_digitLevel.GetComponent<TextMesh>().text = ""+_level;
		_digitLevelShadow.GetComponent<TextMesh>().text = ""+_level;
	}/** End LevelUpdate */


	/// <summary>
	/// Update method, called every frame
	/// </summary>
	public override void Update()
	{
		if (_play == true && _vanLine == true)
		{		
			if (Input.GetKey(KeyCode.LeftArrow) && Time.time - _lastMove >= 0.1f)
			{
				Move(-1);
				_lastMove = Time.time;
			}	
			else if (Input.GetKey(KeyCode.RightArrow) && Time.time - _lastMove >= 0.1f)
			{
				Move(1);
				_lastMove = Time.time;
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				Rotate();
			}
			// Move Downwards and Fall
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				_boostDown = true;				
			}

			else if (Input.GetKey(KeyCode.DownArrow) && _boostDown == true)
			{
				MoveDown();				
			}

			else if (Time.time - _lastFall >= _gameSpeed)
			{
				MoveDown();			
				_lastFall = Time.time;
			}
		}

		if (_vanLine == false)
		{
			VanishLine ();			
		}

	}/** End Update */

	/// <summary>
	/// Load Top Score
	/// </summary>
	private void LoadTopScore() 
	{
		//PlayerPrefs.DeleteKey("TopScore");
		if (PlayerPrefs.HasKey ("TopScore")) {
			_topScore = PlayerPrefs.GetInt("TopScore");
		} else _topScore = 0;
	}

	/// <summary>
	/// Game Over
	/// </summary>
	private void GameOver()
	{
		Debug.Log("gameOver()");
		_gamesOver = true;
		_play = false;
		_triggerPause.SetActive(false);
	
		if (_score > _topScore)
		{
			PlayerPrefs.SetInt ("TopScore", _score); 
			PlayerPrefs.Save (); 
			_topScorePanel.iTopScore = _score;
			GetTopScorePanel () ;
		}
		else
		GetGameOverPanel ();
	}	
	#endregion
	///////////////////////////////////////////////////////////////
}
