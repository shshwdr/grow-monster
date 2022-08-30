using Doozy.Engine.UI;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    UIView view;


    public RectTransform scrollTransform;
    bool isShown = false;

    private void Awake()
    {
        view = GetComponent<UIView>();
        EventPool.OptIn("startBuild", show);
        EventPool.OptIn("updateResource", updateUI);
    }
    void show()
    {
        view.Show();
        isShown = true;
        updateUI();
        scrollTransform.position = new Vector3(0, 0, 0);
    }

    void updateUI()
    {
        if (!isShown)
        {
            return;
        }

        var upgradeCells = GetComponentsInChildren<UpgradeCell>(true);
        if (upgradeCells.Length <= 0)
        {
            return;
        }
        var buildItems = UpgradeMonsterManager.Instance.monsterUpgrades;
        int i = 0;
        foreach(var item in buildItems)
        {
            //if (item.isUpgradable())
            {
                if (i >= upgradeCells.Length)
                {
                    return;
                }
                upgradeCells[i].init(item);
                i++;
            }
        }

        //foreach (var item in buildItems)
        //{
        //    if (!item.isUpgradable())
        //    {

        //        upgradeCells[i].init(item);
        //        i++;
        //    }
        //}
        for (; i < upgradeCells.Length; i++)
        {
            upgradeCells[i].gameObject.SetActive(false);
        }
    }

    public void startBattle()
    {
        GameLoopManager.Instance.stopBuildMode();
        hide();
    }
    void hide()
    {
        isShown = false;
        view.Hide();
    }
    // Start is called before the first frame update
    void Start()
    {

        EventPool.OptIn("updateUpgradeUI", updateUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
