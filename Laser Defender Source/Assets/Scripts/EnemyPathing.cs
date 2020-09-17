using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private WaveConfig _waveConfig;
    private List<Transform> _waypoints;
    private float _enemySpeed;

    private int _waypointIndex = 0;

    private void Start()
    {
        _waypoints = _waveConfig.GetWayPoints();
        transform.position = _waypoints[_waypointIndex].transform.position;
        _enemySpeed = _waveConfig.GetMoveSpeed();
    }

    private void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this._waveConfig = waveConfig;
    }

    private void Move()
    {
        if (_waypointIndex <= _waypoints.Count - 1)
        {
            var targetPosition = _waypoints[_waypointIndex].transform.position;
            var movementThisFrame = _enemySpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
