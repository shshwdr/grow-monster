using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyInfo
{
    public string type;
    public float hp;
    public float attack;
    public float moveSpeed;
    public float attackSpeed;
    public float attackRange;

}
public class EnemyManager : Singleton<EnemyManager>
{
    List<EnemyInfo> enemyInfos = new List<EnemyInfo>();
    Dictionary<string, EnemyInfo> enemyInfoDict = new Dictionary<string, EnemyInfo>();
    // Start is called before the first frame update
    void Start()
    {
        enemyInfos = CsvUtil.LoadObjects<EnemyInfo>("enemy");
        foreach(var enemy in enemyInfos)
        {
            enemyInfoDict[enemy.type] = enemy;
        }
    }

    public EnemyInfo getEnemyInfo(string type)
    {
        if (!enemyInfoDict.ContainsKey(type))
        {
            Debug.LogError("not type " + type);
        }
        return enemyInfoDict[type];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
