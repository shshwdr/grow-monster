using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Image hpImage;

    // Start is called before the first frame update
    void Start()
    {
        EventPool.OptIn<float,float>("changeHP", changeHP);
    }

    void changeHP(float hp,float maxHP)
    {
        hpImage.fillAmount = hp/maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
