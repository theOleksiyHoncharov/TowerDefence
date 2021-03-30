using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Environment environment;

    List<Enemy> enemies = new List<Enemy>(50);

    ObjectPool enemyPull;

    GameController gameController;

    //Возвращает всех противников в радиусе (нужно для сплеша)
    public List<Enemy> GetEnemysInRange(Vector3 position, float range)
    {
        List<Enemy> target = new List<Enemy>(10);

        foreach (var item in enemies)
            if (Vector3.Distance(item.transform.position, position) <= range)
                target.Add(item);


        return target;
    }

    public Enemy GetEnemyMaxHP(Vector3 position, float range)
    {
        Enemy target = null;
        int maxhp = 0;
        foreach (var item in enemies)
        {
            if (Vector3.Distance(item.transform.position, position) <= range && item.heals > maxhp)
            {
                target = item;
                maxhp = target.heals;
            }
        }
        return target;
    }

    //Отдает противника который ближе всего к цели
    public Enemy GetСlosestToTarget(Vector3 position, float range)
    {
        Enemy target = null;
        int NumberPosition = -1;
        float distanation = -1f;

        foreach (var item in enemies)
        {
            int numPosition;
            float distance = item.getDistanation(out numPosition);

            if (Vector3.Distance(item.transform.position, position) <= range)
            {
                if (NumberPosition < numPosition)
                {
                    NumberPosition = numPosition;
                    distanation = distance;
                    target = item;
                }
                else if (NumberPosition == numPosition && distanation > distance)
                {
                    distanation = distance;
                    target = item;
                }

            }
        }

        return target;
    }

    public void Initialize(Environment environment)
    {
        this.environment = environment;
        this.enemyPull = this.environment.objectPull;
        this.gameController = this.environment.gameController;
        this.gameController.gameOver += GameOver;
    }

    public void GameOver()
    {
        foreach (var item in enemies)
        {
            item.gameObject.SetActive(false);
            item.Die = null;
            item.Finish = null;
            enemyPull.PullEnemy(item);
        }

        enemies.Clear();
    }

    public void SpawnedEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemies.Add(enemy);
        enemy.transform.parent = this.transform;
        enemy.Die = DieEnemy;
        enemy.Finish = EnemyEndRoad;
    }

    void DieEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        enemy.gameObject.SetActive(false);
        enemy.Die = null;
        enemy.Finish = null;
        gameController.GetCoin(enemy.maxHeals);
        enemyPull.PullEnemy(enemy);
    }

    void EnemyEndRoad(Enemy enemy)
    {
        enemies.Remove(enemy);
        enemy.gameObject.SetActive(false);
        enemy.Die = null;
        enemy.Finish = null;
        gameController.takeAway();
        enemyPull.PullEnemy(enemy);
    }
}
