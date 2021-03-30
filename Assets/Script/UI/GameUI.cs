using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UIComponent
{
    [SerializeField]
    Text wave,
         coin,
         countToEndBar;

    [SerializeField]
    Image bar;

    GameData gameData = GameData.Instance;

    private void Update()
    {
        if (gameData.state == GameState.InWave)
            wave.text = "Wave: " + gameData.wave;
        else if (gameData.state == GameState.Rest)
            wave.text = "Rest Time!";

        bar.fillAmount = (float)gameData.countToEnd / gameData.maxCountToEnd;
        countToEndBar.text = gameData.countToEnd + "/" + gameData.maxCountToEnd;
        coin.text = gameData.coin.ToString();
    }
}
