using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject _deathVFX;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] [Range(0, 1)] private float _deathVolume = 1f;
    [SerializeField] private float _durationOfExplosion = 1f;
    [SerializeField] private float _health = 100f;
    [SerializeField] private int _scoreValue = 10;

    [Header("Projectile")]
    [SerializeField] private GameObject _enemyLaser;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] [Range(0, 1)] private float _shootVolume = 1f;
    [SerializeField] private float _projectileSpeed = 20f;
    [SerializeField] private float _minTimeBetweenShots = 0.2f;
    [SerializeField] private float _maxTimeBetweenShots = 2f;

    private float _shotCounter;


    // Start is called before the first frame update
    void Start()
    {
        _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        _shotCounter -= Time.deltaTime;

        if(_shotCounter <= 0)
        {
            Fire();
            _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        var newEnemyLaser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
        newEnemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_projectileSpeed);
        AudioSource.PlayClipAtPoint(_shootClip, Camera.main.transform.position, _shootVolume);
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
        _health -= damageDealer.GetDamage();

        SetValue.Score += _scoreValue;
        CanvasManager.UpdateScoreUI();

        damageDealer.Hit();

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(_deathClip, Camera.main.transform.position, _deathVolume);
        Destroy(Instantiate(_deathVFX, transform.position, Quaternion.identity), _durationOfExplosion);
        Destroy(gameObject);
    }
}
