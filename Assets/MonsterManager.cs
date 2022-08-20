using Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : HPObject
{
    public List<ArmJoint> startArmJoints;
    public List<ArmJoint> lockedArmJoints;
    public List<EyeJoint> lockedEyeJoints;
    public List<MouthJoint> lockedMouthJoints;
    public void upgrade(MonsterUpgradeInfo info)
    {
        Debug.Log("buy " + info.upgradeName);
        
        switch (info.upgradeName)
        {
            case "Add Arm":
                if (info.currentLevel < 2)
                {
                    startArmJoints[info.currentLevel].init();
                    lockedArmJoints.Remove(startArmJoints[info.currentLevel]);
                }
                else
                {
                    var joint3 = lockedArmJoints[Random.Range(0, lockedArmJoints.Count)];
                    joint3.init();

                    lockedArmJoints.Remove(joint3);
                }
                break;
            case "Add Eye":

                var joint = lockedEyeJoints[Random.Range(0, lockedEyeJoints.Count)];
                joint.init();
                lockedEyeJoints.Remove(joint);
                break;
            case "Add Mouth":
                var joint2 = lockedMouthJoints[Random.Range(0, lockedMouthJoints.Count)];
                joint2.init();
                lockedMouthJoints.Remove(joint2);
                break;
                
            default:
                Debug.LogError("not support buy");
                break;
        }

        info.currentLevel++;
        
    }

    void findAllJoints()
    {
        lockedArmJoints = GetComponentsInChildren<ArmJoint>().ToList();
        lockedEyeJoints = GetComponentsInChildren<EyeJoint>().ToList();
        lockedMouthJoints = GetComponentsInChildren<MouthJoint>().ToList();
    }

   

    public override void Start()
    {
        base.Start();
        findAllJoints();
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
