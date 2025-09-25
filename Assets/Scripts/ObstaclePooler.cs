using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstaclePooler : MonoBehaviour
{
    private Dictionary<string, IObjectPool<GameObject>> poolDict = new Dictionary<string, IObjectPool<GameObject>>();

    private string obstacleName;

    private GameObject obstacleObject;

    private Vector3 spawn;

    [Header ("Scripts")]
    public DictionarySerializer obstInfoDict;
    public SpawnAndChase sacRef;
    public HitFlash hfRef;


    //Spawns Obstacle if ObjectPool for chosen obstacle exists calls Get
    //Otherwise Creates new ObjectPool and adds it to dictionary then calls Get
    public void SpawnObstacle(GameObject prefab, Vector3 spawnPos)
    {
        obstacleName = prefab.name;
        obstacleObject = prefab;
        spawn = spawnPos;
        if (poolDict.TryGetValue(obstacleName, out IObjectPool<GameObject> testerX))
        {
            poolDict[obstacleName].Get();

        }
        else
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreateObstacle, GetObstacle, ReleaseObstacle, Destroy, true, 2, 8);
            poolDict[obstacleName] = pool;
            poolDict[obstacleName].Get();
        }

    }

    //Public method for other scripts to despawn obstacles
    public void DespawnObstacle(GameObject obs)
    {
        poolDict[obs.name[..^7]].Release(obs);
    }

    //Creates Obstacle and Sets its SpawnAndChase script
    private GameObject CreateObstacle()
    {
        GameObject obstacleInstance = Instantiate(obstacleObject);
        obstacleInstance.GetComponent<SelfDestroy>().SetScripts(sacRef);
        return obstacleInstance;
    }

    //Reactivates Obstacle and resets its position and hp
    private void GetObstacle(GameObject pooledObj)
    {
        pooledObj.transform.position = spawn;
        pooledObj.transform.position = new Vector3(pooledObj.transform.position.x, pooledObj.transform.position.y, 1);
        pooledObj.SetActive(true);
        pooledObj.GetComponent<SelfDestroy>().SetHP(obstInfoDict.obstaclesInfo[pooledObj.name[..^7]]);
        sacRef.AddActive(pooledObj);
    }

    //Deactivates Obstacle
    private void ReleaseObstacle(GameObject pooledObj)
    {
        pooledObj.SetActive(false);
    }

    //Destroys Obstacle
    private void Destroy(GameObject pooledObj)
    {
        Destroy(pooledObj);

    }
}
