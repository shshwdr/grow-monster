using Doozy.Engine.UI;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    UIView view;


    public RectTransform scrollTransform;

    private void Awake()
    {
        view = GetComponent<UIView>();
        EventPool.OptIn("startBuild", show);
    }
    void show()
    {
        updateUI();
        view.Show();

        scrollTransform.position = new Vector3(0, 0, 0);
    }

    void updateUI()
    {
        var upgradeCells = GetComponentsInChildren<UpgradeCell>();
        var buildItems = UpgradeMonsterManager.Instance.monsterUpgrades;
        int i = 0;
        foreach(var item in buildItems)
        {
            if (item.isUpgradable())
            {

                upgradeCells[i].init(item);
                i++;
            }
        }

        foreach (var item in buildItems)
        {
            if (!item.isUpgradable())
            {

                upgradeCells[i].init(item);
                i++;
            }
        }
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
