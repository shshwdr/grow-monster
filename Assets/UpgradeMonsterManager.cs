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
