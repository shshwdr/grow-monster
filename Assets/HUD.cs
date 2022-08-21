using Doozy.Engine.UI;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    bool hasShownHUD = false;
    public Image hpImage;
    public Text levelText;
    public UIView uiview;



    void show()
    {
        if (!hasShownHUD)
        {
            hasShownHUD = true;
            uiview.Show();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        updateLevel();
        EventPool.OptIn<float, float>("changeHP", changeHP);
        EventPool.OptIn("changeLevel", updateLevel);
        EventPool.OptIn("startBuild",show);
    }

    void updateLevel()
    {

        levelText.text = "Level:" + (EnemyGeneratorManager.Instance.currentLevel+1);
    }

    void changeHP(float hp,float maxHP)
    {
        hpImage.fillAmount = hp/maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
