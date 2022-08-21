using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public Text numberText;
    public void init(string value, float scale = 1)
    {
        numberText.transform.localPosition = Vector3.zero;
        numberText.text = value.ToString();
        Color color = Color.white;
        if (scale > 0){
            color = Color.red;
        }else
        {
            color = Color.green;
        }
        numberText.color = color;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
