using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour
{
	private Transform _tx;
	private ParticleSystem.Particle[] _points;
	private ParticleSystem _ps;	
	public int starsMax = 100;
	public float starSize = 1;
	public float starDistance = 10;
	public float starClipDistance = 1;
	private float _starDistanceSqr;
	private float _starClipDistanceSqr;
	
	
	// Use this for initialization
	void Start ()
	{
		_ps=GetComponent<ParticleSystem>();
		_tx = transform;
		_starDistanceSqr = starDistance * starDistance;
		_starClipDistanceSqr = starClipDistance * starClipDistance;
	}
	
	
	private void CreateStars() {
		_points = new ParticleSystem.Particle[starsMax];
		
		for (var i = 0; i < starsMax; i++) {
			_points[i].position = Random.insideUnitSphere * starDistance + _tx.position;
			_points[i].startColor = new Color(1,1,1, 1);
			_points[i].startSize = starSize;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		if ( _points == null ) CreateStars();
		
		for (var i = 0; i < starsMax; i++)
		{
			_points[i].position += Vector3.back * Time.deltaTime*20.0f;

			
			if ((_points[i].position - _tx.position).sqrMagnitude > _starDistanceSqr) {
				_points[i].position = Random.insideUnitSphere.normalized * starDistance + _tx.position;
			}
			
			if ((_points[i].position - _tx.position).sqrMagnitude <= _starClipDistanceSqr) {
				float percent = (_points[i].position - _tx.position).sqrMagnitude / _starClipDistanceSqr;
				_points[i].startColor = new Color(1,1,1, percent);
				_points[i].startSize = percent * starSize;
			}			
		}		
		_ps.SetParticles ( _points, _points.Length );

	}
}
