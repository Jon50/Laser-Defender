using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private float _timeBetweenSpawns = 0.5f;
    //[SerializeField] private float _spawnRandomFactor = 0.3f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private int _numberOfEnemies = 5;

    public GameObject GetEnemyPrefab() { return _enemyPrefab; }
    public List<Transform> GetWayPoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in _pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    public float GetTimeBetweenSpawns() { return _timeBetweenSpawns; }
    //public float GetSpawnRandomFactor() { return _spawnRandomFactor; }
    public float GetMoveSpeed() { return _moveSpeed; }
    public int GetNumberOfEnemies() { return _numberOfEnemies; }
}
