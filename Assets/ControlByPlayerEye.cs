using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlByPlayerEye : MonoBehaviour
{
    public SpriteRenderer render;
    public EyeLine[] lines;

    int lineIndex = 0;
    public void upgrade()
    {
        lineIndex++;
        lines[lineIndex].gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        lines[lineIndex].gameObject.SetActive(true);
        OnMouseExit();
        EventPool.OptIn("stopControl", stopControl);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void stopControl()
    {
        foreach (var line in lines)
        {

            line.speedDown();
        }
    }

    private void OnMouseUp()
    {

        if (PlayerControlManager.Instance.canControl())
        {
            control(this);
        }
    }

    void control(ControlByPlayerEye root)
    {
        foreach (var line in lines)
        {

            line.speedUP();
        }

    }

    private void OnMouseEnter()
    {
        changeMat(true);
    }

    void changeMat(bool On)
    {
        if (On)
        {

            if (PlayerControlManager.Instance.canControl())
            {
                var mat = render.material;

                mat.EnableKeyword("OUTBASE_ON");
            }
        }
        else
        {

            var mat = render.material;

            mat.DisableKeyword("OUTBASE_ON");
        }
    }



    private void OnMouseExit()
    {
        changeMat(false);
    }
}
