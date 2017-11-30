using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightController : WebVRController {
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
        if (manager.RMenu)
        {
            if (axis != Vector2.zero)
            {
                pointer.transform.localPosition = axis.normalized * 0.045f;
                manager.colorposition = axis;
            }
        }
    }

    protected override void ButtonEnter(int index)
    {
        if (index == 3)
        {
            if (manager.RMenu)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
                pointer.transform.localPosition = manager.colorposition * 0.045f;
                if (!manager.LMenu) StartCoroutine(manager.colorChange());
            }
            manager.RMenu = !manager.RMenu;
        }
    }
}
