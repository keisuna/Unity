using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftController : WebVRController {
    ProjectManager manager;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject pointer;
    void Start()
    {
        manager = ProjectManager.Instance;
    }
    void Update()
    {
        UpdatePose();
        if (manager.LMenu)
        {
            if (axis != Vector2.zero)
            {
                pointer.transform.localPosition = new Vector2(0, axis.y) * 0.045f;
                manager.range = (axis.y + 1) / 2;
            }
        }
    }

    protected override void ButtonEnter(int index)
    {
        if (index == 3)
        {
            if (manager.LMenu)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
                pointer.transform.localPosition = new Vector2(0, manager.range*2-1) * 0.045f;
                if (!manager.RMenu) StartCoroutine(manager.colorChange());
            }
            manager.LMenu = !manager.LMenu;
        }
    }
}
