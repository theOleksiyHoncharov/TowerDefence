using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    Environment environment;

    Spawner[] spawners;

    GameData gameData;

    UnityAction _updateState,_update;

    public event UnityAction gameOver,gameStart;

    //Максимальное количество противников должна быть кратно количеству спавнеров
    int countEnemyInWave = 0;
           
    float time = 0f;

    float nextEnemytime = 2f;

    float waitAWaveTime = 20f;

    public void Initialize(Environment environment)
    {
        this.environment = environment;
        this.spawners = this.environment.spawners;
        this.gameData = GameData.Instance;
    }
    //Метод запуска игрового цикла
    public void StartGame()
    {
        gameData.coin = gameData.startCoin;
        gameData.wave = 1;
        countEnemyInWave = 0;
        gameData.countToEnd = gameData.maxCountToEnd;
        gameData.EnemyInGame = gameData.maxCountEnemy;
        gameData.state = GameState.InWave;
        _update = GameUpdate;
        _updateState = WaveState;
        time = 0;
        gameStart();
    }
    
    //Отработка этапа "Волны"
    void WaveState()
    {
        time += Time.deltaTime;

        if (time < nextEnemytime) 
            return;

        time = 0f;

        if(countEnemyInWave < gameData.maxCountEnemy)
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].Spawn(gameData.wave);
                countEnemyInWave++;
            }
    }

    //отработка игрового цикла
    void GameUpdate()
    {
        if (gameData.countToEnd <= 0)
            GameOver();

        switch (gameData.state)
        {
            case GameState.InWave:
                if (gameData.EnemyInGame <= 0)
                    StartRest();
                break;
            case GameState.Rest:
                time += Time.deltaTime;
                if (time >= waitAWaveTime)
                    StartWave();
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if(_updateState != null)
            _updateState();
        if(_update != null)
            _update();
    }

    public void GetCoin(int count)
    {
        gameData.EnemyInGame--;
        gameData.coin += count;
    }

    //Уменьшение количества жизней, если противник добрался до базы
    public void takeAway()
    {
        gameData.EnemyInGame--;
        gameData.countToEnd -= 1;
    }

    void StartRest()
    {
        gameData.state = GameState.Rest;
        time = 0f;
        _updateState = null;
    }

    void StartWave()
    {
        gameData.wave++;
        gameData.state = GameState.InWave;
        gameData.EnemyInGame = gameData.maxCountEnemy;
        time = 0f;
        countEnemyInWave = 0;
        _updateState = WaveState;
    }

    //Завершение игрового цикла
    void GameOver()
    {
        _update = null;
        _updateState = null;
        gameData.state = GameState.GameOver;
        time = 0f;
        gameOver();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
