using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    Environment environment;
    MainMenu mainMain;
    GameController gameController;
    GameUI gameUI;

    public void Initialize(Environment environment)
    {
        this.environment = environment;
        mainMain = this.environment.mainMenu;
        gameUI = this.environment.gameUI;
        gameController = this.environment.gameController;
        gameController.gameStart += mainMain.Close;
        gameController.gameStart += gameUI.Open;
        gameController.gameOver += mainMain.Open;
        gameController.gameOver += gameUI.Close;
        mainMain.startGame = gameController.StartGame;
        mainMain.quitGame = gameController.QuitGame;
    }

}
