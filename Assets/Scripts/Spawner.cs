using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject prefab;
        public float minSpawnTime;
        public float maxSpawnTime;
    }
    private float spawnPosX=15f;
    private float spawnPosZ=0;

    public Spawnable[] spawnables;

    private void Start()
    {
        foreach (var spawnable in spawnables)
        {
            StartCoroutine(SpawnCoroutine(spawnable));
        }
    }

    private IEnumerator SpawnCoroutine(Spawnable spawnable)
    {
        float initialDelay = Random.Range(spawnable.minSpawnTime, spawnable.maxSpawnTime);
        yield return new WaitForSeconds(initialDelay);
        while (GameManager.Instance.currentState==GameState.Playing)
        {
            SpawnObject(spawnable.prefab);
            float randomSpawnTime = Random.Range(spawnable.minSpawnTime, spawnable.maxSpawnTime);
            yield return new WaitForSeconds(randomSpawnTime);
        }
    }

    private void SpawnObject(GameObject prefab)
    {
        Instantiate(prefab, GenerateSpawnPosition(), prefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosY = Random.Range(-3f,1f);
        return new Vector3(spawnPosX, spawnPosY, spawnPosZ);
    }
}
