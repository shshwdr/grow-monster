using Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : HPObject
{
    public List<ArmJoint> startArmJoints;
    public List<ArmJoint> lockedArmJoints;
    public List<ArmJoint> unlockedArmJoints;
    public List<EyeJoint> lockedEyeJoints;
    public List<ArmJoint> unlockedEyeJoints;
    public List<MouthJoint> lockedMouthJoints;
    public List<ArmJoint> unlockedMouthJoints;
    bool hasMouth = false;
    public void upgrade(MonsterUpgradeInfo info)
    {
        Debug.Log("buy " + info.upgradeName);
        switch (info.upgradeName)
        {
            case "Add Arm":
                ArmJoint joint3 = null;
                if (info.currentLevel < 2)
                {
                    joint3 = startArmJoints[info.currentLevel];
                    lockedArmJoints.Remove(startArmJoints[info.currentLevel]);
                }
                else
                {
                    joint3 = lockedArmJoints[ lockedArmJoints.Count-1];
                    //joint3 = lockedArmJoints[Random.Range(0, lockedArmJoints.Count)];

                }
                joint3.init();
                lockedArmJoints.Remove(joint3);
                unlockedArmJoints.Add(joint3);
                var child = joint3.transform.GetChild(0);
                var newJoint = child.GetComponentInChildren<ArmJoint>();
                lockedArmJoints.Add(newJoint);

                UpgradeMonsterManager.Instance.monsterUpgradeDict["Improve Arm"].maxLevel += 2;
                break;
            case "Improve Arm":
                var joint4 = unlockedArmJoints[Random.Range(0, unlockedArmJoints.Count)];
                joint4.upgrade();
                if (joint4.atMaxLevel())
                {
                    unlockedArmJoints.Remove(joint4);
                }

                UpgradeMonsterManager.Instance.monsterUpgradeDict["Improve Arm"].maxLevel -= 2;
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
                hasMouth = true;
                break;
                
            default:
                Debug.LogError("not support buy");
                break;
        }
        SFXManager.Instance.playGrowClip();
        maxhp += info.hp;
        info.currentLevel++;
        
    }

    void findAllJoints()
    {
        lockedArmJoints = GetComponentsInChildren<ArmJoint>().ToList();
        lockedEyeJoints = GetComponentsInChildren<EyeJoint>().ToList();
        lockedMouthJoints = GetComponentsInChildren<MouthJoint>().ToList();
        unlockedArmJoints = new List<ArmJoint>();
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

        SFXManager.Instance.playMonsterDieClip();
    }

    public override void getDamage(float d)
    {
        base.getDamage(d);
        EventPool.Trigger<float, float>("changeHP", currentHP,maxhp);

        if (hasMouth)
        {

            SFXManager.Instance.playMonsterHurtClip();
        }
    }

    public void  restoreFromBattle()
    {
        currentHP = maxhp;
        isDead = false;
        EventPool.Trigger<float, float>("changeHP", currentHP, maxhp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
