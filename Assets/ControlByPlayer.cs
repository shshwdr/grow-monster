using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlByPlayer : MonoBehaviour
{
    public ControlByPlayer parent;
    public ControlByPlayer child;
    public SpriteRenderer render;
    public ArmJoint armjoint;
    // Start is called before the first frame update
    void Start()
    {
        OnMouseExit();
        EventPool.OptIn("stopControl",stopControl);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void stopControl()
    {
        armjoint.speedDown();
    }

    private void OnMouseUp()
    {

        if (PlayerControlManager.Instance.canControl())
        {
            var root = findRoot();
            control(root);
            PlayerControlManager.Instance.control();
        }
    }

    void control(ControlByPlayer root)
    {

            root.armjoint. speedUP();
        if (root.child)
        {
            control(root.child);
        }
        
    }

    private void OnMouseEnter()
    {
        var root = findRoot();
        changeMat(root, true);
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

    public ControlByPlayer findRoot()
    {
        if (parent)
        {
            parent.findRoot();
        }
        return this;
    }

    void changeMat(ControlByPlayer root, bool on)
    {
        root.changeMat(on);
        if (root.child)
        {
            changeMat(root.child,on);
        }
    }

    private void OnMouseExit()
    {
        var root = findRoot();

        changeMat(root, false);
    }
}
