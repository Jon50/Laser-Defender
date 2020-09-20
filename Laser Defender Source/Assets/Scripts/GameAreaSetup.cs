using UnityEngine;

public class GameAreaSetup : MonoBehaviour
{
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private float _padding = 1f;

    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;

    public float XMin { get => _xMin; private set => _xMin = value; }
    public float XMax { get => _xMax; private set => _xMax = value; }
    public float YMin { get => _yMin; private set => _yMin = value; }
    public float YMax { get => _yMax; private set => _yMax = value; }
    public Camera GameCamera
    {
        get
        {
            if (_gameCamera == null)
                _gameCamera = Camera.main;
            return _gameCamera;
        }
    }

    private void Start()
    {
        SetupMoveBoudries();
    }


    private void SetupMoveBoudries()
    {
        _xMin = GameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _padding;
        _xMax = GameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _padding;
        _yMin = GameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _padding;
        _yMax = GameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _padding;
    }


    private void Update()
    {
        if (Camera.main.pixelWidth > Camera.main.pixelHeight)
        {
            Camera.main.rect = new Rect(0.333f, 0, 0.333f, 1f);
            SetupMoveBoudries();
        }

        if (Camera.main.aspect < 0.2f)
        {
            Camera.main.rect = new Rect(0, 0, 1f, 1f);
            SetupMoveBoudries();
        }
    }
}
