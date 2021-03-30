using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    const float RATE_MIN = 0.1f;
    const float RATE_MAX = 10f;

    [Range(RATE_MIN, RATE_MAX)]
    [SerializeField]
    protected float fireRate = RATE_MIN;

    const int RANGE_MIN = 1;
    const int RANGE_MAX = 50;

    [Range(RANGE_MIN, RANGE_MAX)]
    [SerializeField]
    protected int fireDistance = RANGE_MIN;

    const int DAMAGE_MIN=1;
    const int DAMAGE_MAX=25;
    [Range(DAMAGE_MIN,DAMAGE_MAX)]
    [SerializeField]
    protected int Damage;

    [SerializeField]
    public BuildType[] transformation;

    [SerializeField]
    public int Price;

    [SerializeField]
    public Sprite imageInUI; 

    [SerializeField]
    protected Transform weaponPoint;

    [SerializeField]
    public BoxCollider boxColider;


    protected EnemyController enemyController;
    protected ObjectPool objectPool;
    protected Environment environment;

    protected float timeToShot;
    protected float time = 0f;
    public void Initialize(Environment environment)
    {
        timeToShot = 1f / FireRate;
        this.environment = environment;
        objectPool = this.environment.objectPull;
        enemyController = this.environment.enemyController;
    }


    public float FireRate
    {
        get { return fireRate; }
        set
        {
            if (value < RATE_MIN)
                fireRate = RATE_MIN;
            else if (value > RATE_MAX)
                fireRate = RATE_MAX;
            else
                fireRate = value;
        }
    }

    public int FireDistance
    {
        get { return fireDistance; }
        set
        {
            if (value < RANGE_MIN)
                fireDistance = RANGE_MIN;
            else if (value > RANGE_MAX)
                fireDistance = RANGE_MAX;
            else
                fireDistance = value;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= timeToShot)
        {
            time = 0f;
            Shoot();
        }
    }
    
    protected abstract void Shoot();
}
