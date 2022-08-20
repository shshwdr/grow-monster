using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCell : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    public Text levelText;

    MonsterUpgradeInfo info;
    // Start is called before the first frame update
    public void init(MonsterUpgradeInfo i )
    {
        info = i;
        buttonText.text = info.upgradeName;
        levelText.text = info.currentLevel + "/" + info.maxLevel;
        button.onClick.AddListener(() =>
        {
            //buy item

            GameLoopManager.Instance.monster.upgrade(info);


            EventPool.Trigger("updateUpgradeUI");
        });
    }
}
