using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    GameOver,
    InWave,
    Rest
}

public class GameData
{
    static GameData _instance;
    public static GameData Instance
    {
        get{
            if (_instance == null)
                _instance = new GameData();
            return _instance;
        }
    }

    public int coin;

    public int countToEnd; //Количество жизней в игре

    public int wave;

    public int EnemyInGame; //Сколько осталось убить противников до завершения волны

    public int maxCountToEnd=30;  //Максимальное количетво жизней в игре

    public int maxCountEnemy = 50;  //Количество противников в одной волне

    public int startCoin = 100; //Стартовое количество монет

    public GameState state;
}
