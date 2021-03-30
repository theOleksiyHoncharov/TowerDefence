using UnityEngine;
using UnityEngine.Events;

public abstract class Bullet : MonoBehaviour
{
    const int DAM_MIN = 1;
    const int DAM_MAX = 1000;

    [Range(DAM_MIN, DAM_MAX)]
    [SerializeField]
    protected int damage = DAM_MIN;

    //Башня при получении снаряда, задает урон
    public int Damage 
    {
        get { return damage; }
        set
        {
            if (value < DAM_MIN)
                damage = DAM_MIN;
            else if (value > DAM_MAX)
                damage = DAM_MAX;
            else
                damage = value;
        }
    }

    public event UnityAction<Bullet> EDestroy;

    protected EnemyController enemyController;

    protected bool isActive = false;

    protected abstract void Hit();

    protected Enemy enemy;
    protected Transform enemyTransform;

    protected Transform bulletPosition;
    protected Vector3 spawnPosition;

    protected float time;

    private void Awake()
    {
        bulletPosition = transform;

        var env = Environment.GetInstance;

        enemyController = env.enemyController;
    }

    //Метод указывающий цель "снаряда"
    public virtual void Move(Enemy enm)
    {
        enemy = enm;
        enemyTransform = enemy.transform;
        spawnPosition = bulletPosition.position;

        time = 0;

        isActive = 
            enabled = true;
    }

    void Update()
    {
        if (!isActive)
        {
            enabled = false;
            return;
        }

        if (!enemy.isActiveAndEnabled)
        {
            Done();
            return;
        }

        time += Time.deltaTime * 2;

        if (time < 1)
        {
            bulletPosition.LookAt(enemyTransform);
            transform.position = Vector3.Lerp(spawnPosition, enemyTransform.position, time);
        }
        else
        {
            Done();
            Hit();
        }
    }

    void Done()
    {
        isActive = false;
        enabled = false;
        time = 0f;
        EDestroy?.Invoke(this);
    }
}
