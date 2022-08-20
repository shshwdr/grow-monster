using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMenu : MonoBehaviour
{
    ResourceCell[] cells;
    // Start is called before the first frame update
    void Start()
    {
        cells = GetComponentsInChildren<ResourceCell>();
        updateUI();
        EventPool.OptIn("updateResource", updateUI);
    }

    public void updateUI()
    {

        int i = 0;
        foreach (var resource in ResourceManager.Instance.resourceInfos)
        {
            cells[i].init(resource.type, ResourceManager.Instance.getAmount(resource.type));
            i++;
        }
        //for (; i < cells.Length; i++)
        //{
        //    cells[i].gameObject.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
