using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
	[Header("Sorting Order")]
	[SerializeField]
	private bool _setSortingOrder;
	[SerializeField]
	private int _bgOrder; //order in layer backgroound
	[SerializeField]
	private int _mgOrder; //order in layer backgroound
	[SerializeField]
	private int _fgOrder; //order in layer foregroound

	[Header("Background")]
	[SerializeField]
	private Renderer _background;
	[SerializeField]
	private float _speedBG;
	[SerializeField]
	private Renderer _midground;
	[SerializeField]
	private float _speedMG;
	[SerializeField]
	private Renderer _foreground;
	[SerializeField]
	private float _speedFG;

	private Transform _camera => Camera.main.transform;
	float startPosX;

	// Use this for initialization
	void Start()
	{
		startPosX = _camera.position.x;

        if (_setSortingOrder)
        {
			_background.sortingOrder = _bgOrder;
			_midground.sortingOrder = _mgOrder;
			_foreground.sortingOrder = _fgOrder;
		}
	}

	// Update is called once per frame
	void Update()
	{
		float x = _camera.position.x - startPosX;

		if (_background != null)
		{
			float offset = (x * _speedBG) % 1;
			_background.material.mainTextureOffset = new Vector2(offset, _background.material.mainTextureOffset.y);
		}

		if (_midground != null)
		{
			float offset = (x * _speedMG) % 1;
			_midground.material.mainTextureOffset = new Vector2(offset, _midground.material.mainTextureOffset.y);
		}

		if (_foreground != null)
		{
			float offset = (x * _speedFG) % 1;
			_foreground.material.mainTextureOffset = new Vector2(offset, _foreground.material.mainTextureOffset.y);
		}
	}
}
