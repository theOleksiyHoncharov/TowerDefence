using System;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public ObjectPool objectPull;  //объектный пул
    public GameController gameController; //Контроллер игровой "петли"
    public EnemyController enemyController; //Контроллер противников
    public BuildController buildController; //Контроллер строительства
    public UIController uIController; //Контроллер интерфеса
    public MainMenu mainMenu; //интерфейс главного меню
    public GameUI gameUI; // игровой интерфейс
    public UITowerBuild uiTowerBuild; //интерфейс строительства
    public Spawner[] spawners; //спавнеры
    public MouseCast mouseCast; //класс отрабатывающий взятие в таргет башни, для ее строительства
    
    static Environment _instance;
    public static Environment GetInstance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;

        gameController.Initialize(this);
        enemyController.Initialize(this);
        uIController.Initialize(this);
        mouseCast.Initialize();
        buildController.Initialize(this);

        for (int i = 0; i < spawners.Length; i++)
            spawners[i].Initialize(this);        
    }
}
