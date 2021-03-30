using UnityEngine;
using UnityEngine.Events;

public enum BuildType
{
    None=0,
    Default=1,
    Splash=2,
    BigDamage=3,
    BigRangeSplash=4,
    LowRangeSplash=5,
    BigDamageRange=6,
    BiggestDamage=7
}

public enum BulletType
{
    Single,
    Splash,
}

public class BuildController : MonoBehaviour
{
    [SerializeField]
    Tower[] towersPrefab; //Список возможных башен для постройки

    [SerializeField]
    Tower[] towers;

    UITowerBuild uITowerBuild;

    Environment environment;

    Tower targetTower;
    int IndexTower;
    MouseCast mouseCast;
    GameController gameController;
    GameData gameData = GameData.Instance;

    event UnityAction<Sprite[], int[], BuildType[]> TowerTargeting; 
    
    //Взятие в таргет башни
    private void TargetTower(int id)
    {
        targetTower = null;
        IndexTower = 0;
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i].gameObject.GetInstanceID() == id)
            {
                targetTower = towers[i];
                IndexTower = i;
                break;
            }    
        }

        if (targetTower != null)
        {
            int length = targetTower.transformation.Length;
            Sprite[] sprites = new Sprite[length];
            int[] prices = new int[length];

            for (int i = 0; i < length; i++)
                sprites[i] = towersPrefab[(int)targetTower.transformation[i]].imageInUI;
            for (int i = 0; i < length; i++)
                prices[i] = towersPrefab[(int)targetTower.transformation[i]].Price;


            TowerTargeting(sprites,prices,targetTower.transformation); //Открывается интерфейс с меню апгрейда башен
        }
    }

    //отработка гейм овера
    void DestroyAllTower()
    {
        for (int i = 0; i < towers.Length; i++)
        {
            Vector3 position = towers[i].transform.position;
            Quaternion rotation = towers[i].transform.rotation;
            Destroy(towers[i].gameObject);
            towers[i] = Instantiate(towersPrefab[0]);
            towers[i].transform.position = position;
            towers[i].transform.rotation = rotation;
        }
    }

    public void Initialize(Environment environment)
    {
        this.environment = environment;
        gameController = this.environment.gameController;
        mouseCast = this.environment.mouseCast;
        uITowerBuild = this.environment.uiTowerBuild;
        mouseCast.targetingTower += TargetTower;
        uITowerBuild.TryBuild += TryBuild;
        TowerTargeting += uITowerBuild.Open;
        gameController.gameStart += mouseCast.Enable;
        gameController.gameOver += mouseCast.Disable;
        gameController.gameOver += DestroyAllTower;
        gameController.gameOver += uITowerBuild.Close;
    }

    //Попытка строительства башни из "префабов"
    void TryBuild(BuildType type)
    {
        if (gameData.coin >= towersPrefab[(int)type].Price)
        {
            gameData.coin -= towersPrefab[(int)type].Price;
            Vector3 position=towers[IndexTower].transform.position;
            Quaternion rotation = towers[IndexTower].transform.rotation;
            Destroy(towers[IndexTower].gameObject);
            towers[IndexTower] = Instantiate(towersPrefab[(int)type]);
            towers[IndexTower].transform.position = position;
            towers[IndexTower].transform.rotation = rotation;
            towers[IndexTower].Initialize(environment);
            TargetTower(towers[IndexTower].gameObject.GetInstanceID());
        }
    }
}
