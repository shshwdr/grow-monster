using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterUpgradeInfo
{
    public string upgradeName;
    public int maxLevel;
    public string resourceNmae1;
    public int resourceAmount1 { get { return (int)( resourcea1 * (1+increase*currentLevel)); } }
    public int resourcea1;
    public string resourceNmae2;
    public int resourcea2;
    public int resourceAmount2{ get { return (int) (resourcea2 * (1 + increase * currentLevel)); } }
    public int currentLevel;
    public float increase;
    public float hp;


    public bool isUpgradable()
    {
        if (currentLevel >= maxLevel)
        {
            return false;
        }

        if (ResourceManager.Instance.getAmount(resourceNmae1) >= resourceAmount1)
        {
            if (resourceNmae2.Length > 0)
            {
                if (ResourceManager.Instance.getAmount(resourceNmae2) >= resourceAmount2)
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
public class UpgradeMonsterManager : Singleton<UpgradeMonsterManager>
{
    public List<MonsterUpgradeInfo> monsterUpgrades = new List<MonsterUpgradeInfo>();
    public Dictionary<string, MonsterUpgradeInfo> monsterUpgradeDict = new Dictionary<string, MonsterUpgradeInfo>();
    // Start is called before the first frame update
    void Start()
    {
        monsterUpgrades = CsvUtil.LoadObjects<MonsterUpgradeInfo>("monsterUpgrades");
        foreach(var update in monsterUpgrades)
        {
            monsterUpgradeDict[update.upgradeName] = update;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
