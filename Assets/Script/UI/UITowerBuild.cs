using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITowerBuild : UIComponent
{
    [SerializeField]
    Button[] buttons;

    [SerializeField]
    Text[] text;

    BuildType[] buildTypes;

    public event UnityAction<BuildType> TryBuild;

    //При открытии отображает кнопки "возможных пушек, для постройки" на интерфейсе строитльства
    public void Open(Sprite[] buttonSprites, int[] Prices, BuildType[] buildTypes)
    {
        this.buildTypes = buildTypes;

        int indexButtons;

        for (indexButtons = 0; indexButtons < buttons.Length && indexButtons < buttonSprites.Length; indexButtons++)
        {
            buttons[indexButtons].gameObject.SetActive(true);
            buttons[indexButtons].image.sprite = buttonSprites[indexButtons];
            text[indexButtons].text = Prices[indexButtons].ToString();
            buttons[indexButtons].onClick.RemoveAllListeners();
        }

        for (; indexButtons < buttons.Length; indexButtons++)
        {
            buttons[indexButtons].gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    public void TryBuildTower(int num)
    {
        TryBuild(buildTypes[num]);
    }


}
