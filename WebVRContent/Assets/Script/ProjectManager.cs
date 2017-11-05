using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour {

    #region Singleton
    private static ProjectManager instance;
    public static ProjectManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("create");
        }
        else
        {
            Debug.Log("delete");
            Destroy(this.gameObject);
            return;
        }
        range = 0.3f;
        colorposition = Vector2.right;
        StartCoroutine(colorChange());
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    public bool RMenu;
    public bool LMenu;
    public Color color;
    public float range;
    public Vector2 colorposition;
    private float result;
    private float resultAbs;

    [SerializeField]
    private Material material;

    public IEnumerator colorChange()
    {
        while (true)
        {
            result = Mathf.Atan2(colorposition.y, colorposition.x) * Mathf.Rad2Deg;
            resultAbs = Mathf.Abs(result);
            if (resultAbs >= 0 && resultAbs < 60)
            {
                color = (Mathf.Sign(result) > 0) ? new Color(1, 1 - range, (1 - range) + range * resultAbs / 60f) : new Color(1, (1 - range) + range * resultAbs / 60f, 1 - range);
            }
            else if (resultAbs >= 60 && resultAbs < 120)
            {
                color = (Mathf.Sign(result) > 0) ? new Color((1 - range) + range * (120 - resultAbs) / 60f, 1 - range, 1) : new Color((1 - range) + range * (120 - resultAbs) / 60f, 1, 1 - range);
            }
            else if (resultAbs >= 120 && resultAbs < 180)
            {
                color = (Mathf.Sign(result) > 0) ? new Color(1 - range, (1 - range) + range * (resultAbs - 120) / 60f, 1) : new Color(1 - range, 1, (1 - range) + range * (resultAbs - 120) / 60f);
            }
            material.SetColor("_Color", color);
            yield return null;
        }
    }
}
