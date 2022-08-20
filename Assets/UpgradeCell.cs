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

    public ResourceCell[] resources;

    MonsterUpgradeInfo info;
    // Start is called before the first frame update
    public void init(MonsterUpgradeInfo i )
    {
        info = i;
        buttonText.text = info.upgradeName;
        levelText.text = info.currentLevel + "/" + info.maxLevel;
        button.onClick.RemoveAllListeners();

        resources[0].init(info.resourceNmae1, info.resourceAmount1);
        if (info.resourceNmae2.Length > 0)
        {
            resources[1].init(info.resourceNmae2, info.resourceAmount2);

        }
        else
        {
            resources[1].gameObject.SetActive(false);
        }
        if (isUpgradable() || CheatManager.Instance.unlimitResource)
        {
            button.interactable = true;
        }
        else
        {

            button.interactable = false;
        }

        button.onClick.AddListener(() =>
        {
            //buy item

            consumeUpgrade();
            GameLoopManager.Instance.monster.upgrade(info);

            EventPool.Trigger("updateUpgradeUI");
        });
    }


    void consumeUpgrade()
    {
        ResourceManager.Instance.changeAmount(info.resourceNmae1, -info.resourceAmount1);
        if (info.resourceNmae2.Length > 0)
        {
            ResourceManager.Instance.changeAmount(info.resourceNmae2, -info.resourceAmount2);
        }
    }
    bool isUpgradable()
    {
        if (ResourceManager.Instance.getAmount(info.resourceNmae1) >= info.resourceAmount1)
        {
            if(info.resourceNmae2.Length > 0)
            {
                if (ResourceManager.Instance.getAmount(info.resourceNmae2) >= info.resourceAmount2)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        return false;
    }
}
