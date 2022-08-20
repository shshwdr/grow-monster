using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageMenu : Singleton<MessageMenu>
{
    public Text text;


    public void show(string t, float time = 2f)
    {
        text.text = t;
        GetComponent<UIView>().Show();
        StartCoroutine( hide(time));
    }

    IEnumerator hide(float time = 2f)
    {
        yield return new WaitForSeconds(time);
        GetComponent<UIView>().Hide();

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
