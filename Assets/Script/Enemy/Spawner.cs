using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject wayObj;

    ObjectPool enemyPull;

    EnemyController enemyController;

    Vector3[] way;

    public void Initialize(Environment environment)
    {
        this.enemyController = environment.enemyController;

        enemyPull = environment.objectPull;

        Transform[] waysTransform = wayObj.GetComponentsInChildren<Transform>();

        way = new Vector3[waysTransform.Length];

        for (int i = 0; i < waysTransform.Length; i++)
        {
            way[i] = waysTransform[i].position;
        }
    }

    public void Spawn(int difficulty)
    {
        Enemy newEnemy = enemyPull.GetEnemy();
        newEnemy.Initialize(this.transform.position, this.transform.rotation, way, difficulty);
        enemyController.SpawnedEnemy(newEnemy);
    }
}
