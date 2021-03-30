using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ObjectPool : MonoBehaviour
{
    Stack<Enemy> enemyPool = new Stack<Enemy>(20);
    Stack<Bullet> bulletPoolSingle = new Stack<Bullet>(20);
    Stack<Bullet> bulletPoolMulty = new Stack<Bullet>(20);

    //Прототип противника, для создания в случае не хватки в пуле
    [SerializeField]
    GameObject enemy;
    //Прототип пули (в одну цель) для создания в случае не хватки в пуле
    [SerializeField]
    Bullet bulletSingle;
    //Прототип пули (сплеш) для создания в случае не хватки в пуле
    [SerializeField]
    Bullet bulletMulty;

    public Enemy GetEnemy()
    {
        if (enemyPool.Count == 0)
        {
            var en = Instantiate(enemy).GetComponent<Enemy>();
            return en;
        }
        else
            return enemyPool.Pop();
    }
    
    public Bullet GetBulletSingle(Vector3 pos, Quaternion rotate)
    {
        Bullet bullet = null;

        if (bulletPoolSingle.Count == 0)
        {
            bullet = Instantiate(bulletSingle);
            bullet.EDestroy += (Bullet arg) => { bulletPoolSingle.Push(arg); ClearBullet(arg);  };
        }
        else
            bullet = bulletPoolSingle.Pop();

        SetParameter(bullet, pos, rotate);

        return bullet;
    }

    public Bullet GetBulletMulty(Vector3 pos, Quaternion rotate)
    {
        Bullet bullet = null;

        if (bulletPoolMulty.Count == 0)
        {
            bullet = Instantiate(bulletMulty, pos, rotate);
            bullet.EDestroy += (Bullet arg) => { bulletPoolMulty.Push(arg); ClearBullet(arg); };
        }
        else
        {
            bullet = bulletPoolMulty.Pop();
            bullet.gameObject.SetActive(true);
        }

        SetParameter(bullet, pos, rotate);

        return bullet;
    }
    
    void SetParameter(Bullet b, Vector3 pos, Quaternion rotate)
    {
        var t = b.transform;
        t.position = pos;
        t.rotation = rotate;
        b.gameObject.SetActive(true);
    }

    void ClearBullet(Bullet obj)
    {
        obj.gameObject.SetActive(false);
    }

    public void PullEnemy(Enemy enemy)
    {
        enemy.transform.parent = transform;
        enemyPool.Push(enemy);
    }
}
