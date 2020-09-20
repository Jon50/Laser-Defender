using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config params
    [Header("Player")]
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] [Range(0, 1)] private float _deathVolume = 1f;
    [SerializeField] private float _playerSpeed = 10f;
    [SerializeField] private float _gameOverDelay = 2f;
    [SerializeField] private int _health = 300;

    [Header("Projectile")]
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] [Range(0, 1)] private float _shootVolume = 1f;
    [SerializeField] private GameObject _playerLaser;
    [SerializeField] private float _projectileSpeed = 20f;
    [SerializeField] private float _projectileFiringPeriod = 0.1f;

    [Header("References")]
    [SerializeField] private SceneLoad _sceneLoad;
    [SerializeField] private GameAreaSetup _gameArea;


    private Coroutine _fireCoroutine;


    void Start()
    {
        SetValue.PlayerHealth = _health;
        SetValue.Score = 0;

        CanvasManager.UpdateHealthUI();
        CanvasManager.UpdateScoreUI();

        _gameArea = FindObjectOfType<GameAreaSetup>();
    }


    void Update()
    {
        Move();
        Fire();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (!damageDealer)
            return;

        ProcessHit(damageDealer);
    }


    private void ProcessHit(DamageDealer damageDealer)
    {
        SetValue.PlayerHealth -= damageDealer.GetDamage();
        CanvasManager.UpdateHealthUI();

        damageDealer.Hit();

        if (SetValue.PlayerHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(_deathClip, Camera.main.transform.position, _deathVolume);
            Invoke("GameOverDelay", _gameOverDelay);
            gameObject.SetActive(false);
        }
    }


    private void GameOverDelay()
    {
        _sceneLoad.LoadGameOver();
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_fireCoroutine);
        }
    }


    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(_playerLaser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _projectileSpeed);
            AudioSource.PlayClipAtPoint(_shootClip, Camera.main.transform.position, _shootVolume);
            yield return new WaitForSeconds(_projectileFiringPeriod);
        }
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * _playerSpeed * Time.deltaTime;
        var deltaY = Input.GetAxis("Vertical") * _playerSpeed * Time.deltaTime;

        var newXtPos = Mathf.Clamp(transform.position.x + deltaX, _gameArea.XMin, _gameArea.XMax);
        var newYtPos = Mathf.Clamp(transform.position.y + deltaY, _gameArea.YMin, _gameArea.YMax);

        transform.position = new Vector2(newXtPos, newYtPos);
    }
}
