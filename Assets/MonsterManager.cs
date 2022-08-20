using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : HPObject
{
    public ArmJoint[] armJoints;
    public void upgrade(MonsterUpgradeInfo info)
    {
        Debug.Log("buy " + info.upgradeName);
        switch (info.upgradeName)
        {
            case "Add Arm":

                armJoints[info.currentLevel].init();
                break;
            default:
                Debug.LogError("not support buy");
                break;
        }

        info.currentLevel++;
        
    }
    public override void Start()
    {
        base.Start();
    }

    public override void die()
    {
        base.die();
        GameLoopManager.Instance.battleEnd(false);
    }

    public override void getDamage(float d)
    {
        base.getDamage(d);
        EventPool.Trigger<float, float>("changeHP", currentHP,maxhp);
    }

    public void  restoreFromBattle()
    {
        currentHP = maxhp;
        EventPool.Trigger<float, float>("changeHP", currentHP, maxhp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
