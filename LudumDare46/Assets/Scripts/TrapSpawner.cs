using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Spawner Settings")]
    [SerializeField]
    private float waitTimeMin;
    [SerializeField]
    private float waitTimeMax;
    [SerializeField]
    private float warningOffset;
    [SerializeField]
    private GameObject warning;
    [SerializeField]
    private float warningDelay;

    [Header("Crusher")]
    public int crusherDamage;
    [SerializeField]
    private GameObject[] crusherSpawnPointsVertical;
    [SerializeField]
    private GameObject[] crusherSpawnPointsHorizontal;
    [SerializeField]
    private GameObject crusher;
    [SerializeField]
    private float crusherSpeed;
    [SerializeField]
    private float contractTimeVertical;
    [SerializeField]
    private float contractTimeHorizontal;

    [Header("Spike Wall")]
    public int spikeDamage;
    [SerializeField]
    private Transform[] topSpawns;
    [SerializeField]
    private Transform[] bottomSpawns;
    [SerializeField]
    private Transform[] leftSpawns;
    [SerializeField]
    private Transform[] rightSpawns;
    [SerializeField]
    private GameObject spike;
    [SerializeField]
    private float spikeLifeTime;

    [Header("Spikeball")]
    public int spikeballDamage;
    [SerializeField]
    private float spawnRangeX;
    [SerializeField]
    private float spawnRangeY;
    [SerializeField]
    private GameObject spikeball;
    [SerializeField]
    private float spikeballLifeTime;
    [SerializeField]
    private float spikeballRotateSpeed;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        StartCoroutine(Spawn(Random.Range(1, 4)));
    }
    
    private IEnumerator Spawn(int randomSpawn)
    {
        yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));

        switch (randomSpawn)
        {
            case 1:
                SpawnCrusher();
                break;
            case 2:
                PickSpikeWall();
                break;
            case 3:
                StartCoroutine(SpawnSpikeBall());
                break;
        }
        if(gameManager.gameHasEnded == false)
        {
            StartCoroutine(Spawn(Random.Range(1, 4)));
        }
    }

    private IEnumerator SpawnSpikeBall()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY));
        GameObject newWarning = Instantiate(warning, spawnPoint, Quaternion.identity);
        yield return new WaitForSeconds(warningDelay);
        Destroy(newWarning);
        GameObject newSpikeball = Instantiate(spikeball, spawnPoint, Quaternion.identity);
        Spikeball s = newSpikeball.GetComponent<Spikeball>();
        s.lifetime = spikeballLifeTime;
        s.rotateSpeed = spikeballRotateSpeed;
    }

    private void PickSpikeWall()
    {
        int randomWall = Random.Range(0, 4);

        switch (randomWall)
        {
            case 0:
                StartCoroutine(SpawnSpikeWall(rightSpawns, "Right"));
                break;
            case 1:
                StartCoroutine(SpawnSpikeWall(leftSpawns, "Left"));
                break;
            case 2:
                StartCoroutine(SpawnSpikeWall(topSpawns, "Top"));
                break;
            case 3:
                StartCoroutine(SpawnSpikeWall(bottomSpawns, "Bottom"));
                break;
        }        
    }

    private IEnumerator SpawnSpikeWall(Transform[] spawnPoints, string side)
    {
        GameObject newWarning;
        float rotation = 0f;
        Vector3 spawn = spawnPoints[0].position;
        List<GameObject> warnings = new List<GameObject>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            switch (side)
            {
                case "Top":
                    newWarning = Instantiate(warning, new Vector3(spawnPoints[i].position.x, spawn.y - warningOffset), Quaternion.identity);
                    warnings.Add(newWarning);
                    rotation = -180;
                    break;
                case "Bottom":
                    newWarning = Instantiate(warning, new Vector3(spawnPoints[i].position.x, spawn.y + warningOffset), Quaternion.identity);
                    warnings.Add(newWarning);
                    rotation = 0;
                    break;
                case "Left":
                    newWarning = Instantiate(warning, new Vector3(spawn.x + warningOffset, spawnPoints[i].position.y), Quaternion.identity);
                    warnings.Add(newWarning);
                    rotation = -90f;
                    break;
                case "Right":
                    newWarning = Instantiate(warning, new Vector3(spawn.x - warningOffset, spawnPoints[i].position.y), Quaternion.identity);
                    warnings.Add(newWarning);
                    rotation = 90f;
                    break;
            }
        }

        List<Vector2> spikeSpawnPoints = new List<Vector2>();
        List<GameObject> spikes = new List<GameObject>();
        yield return new WaitForSeconds(warningDelay);
        for (int i = 0; i < warnings.Count; i++)
        {
            spikeSpawnPoints.Add(warnings[i].transform.position);
            Destroy(warnings[i]);
        }
        for (int i = 0; i < spikeSpawnPoints.Count; i++)
        {
            GameObject newSpike = Instantiate(spike, spikeSpawnPoints[i], Quaternion.identity);
            newSpike.transform.Rotate(new Vector3(newSpike.transform.rotation.x, newSpike.transform.rotation.y, rotation));
            spikes.Add(newSpike);
        }
        yield return new WaitForSeconds(spikeLifeTime);
        for (int i = 0; i < spikes.Count; i++)
        {
            Destroy(spikes[i]);
        }
    }

    private void SpawnCrusher()
    {
        int direction = Random.Range(0, 2);
        if(direction == 0)
        {
            Transform randomSpawnPoint = crusherSpawnPointsVertical[Random.Range(0, crusherSpawnPointsVertical.Length)].transform;
            GameObject newCrusher = Instantiate(crusher, randomSpawnPoint.position, Quaternion.identity);
            Crusher c = newCrusher.GetComponent<Crusher>();
            c.isHorizontal = false;
            c.speed = crusherSpeed;
            c.contractTime = contractTimeVertical;
            c.delay = warningDelay;
            c.warning = warning;
            c.warningOffset = warningOffset;
        }
        else
        {
            Transform randomSpawnPoint = crusherSpawnPointsHorizontal[Random.Range(0, crusherSpawnPointsHorizontal.Length)].transform;
            GameObject newCrusher = Instantiate(crusher, randomSpawnPoint.position, Quaternion.identity);
            Crusher c = newCrusher.GetComponent<Crusher>();
            c.isHorizontal = true;
            c.speed = crusherSpeed;
            c.contractTime = contractTimeHorizontal;
            c.delay = warningDelay;
            c.warning = warning;
            c.warningOffset = warningOffset;
        }
    }
}


