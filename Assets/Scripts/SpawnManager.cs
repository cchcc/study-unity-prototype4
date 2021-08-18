using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    public int wave = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(wave);
        SpawnPowerUpWave(wave);
    }

    private void SpawnEnemyWave(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            var x = Random.Range(-6f, 6f);
            var z = Random.Range(-6f, 6f);
            var position = new Vector3(x, 3, z);
            Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation);
        }
    }
    
    private void SpawnPowerUpWave(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            var x = Random.Range(-9f, 9f);
            var z = Random.Range(-9f, 9f);
            var position = new Vector3(x, 0, z);
            Instantiate(powerUpPrefab, position, powerUpPrefab.transform.rotation);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        var enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            ++wave;
            foreach (var o in GameObject.FindGameObjectsWithTag("PowerUp"))
            {
                Destroy(o);
            }
            
            SpawnEnemyWave(wave);
            SpawnPowerUpWave(Math.Max(1, wave - 1));
            Debug.Log($"wave: {wave}");
        }
    }
}
