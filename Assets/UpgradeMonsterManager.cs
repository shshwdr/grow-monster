using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterUpgradeInfo
{
    public string upgradeName;
    public int maxLevel;
    public string resourceNmae1;
    public int resourceAmount1;
    public string resourceNmae2;
    public int resourceAmount2;
    public int currentLevel;
    public float increase;
}
public class UpgradeMonsterManager : Singleton<UpgradeMonsterManager>
{
    public List<MonsterUpgradeInfo> monsterUpgrades = new List<MonsterUpgradeInfo>();
    // Start is called before the first frame update
    void Start()
    {
        monsterUpgrades = CsvUtil.LoadObjects<MonsterUpgradeInfo>("monsterUpgrades");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
