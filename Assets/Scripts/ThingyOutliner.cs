using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingyOutliner : MonoBehaviour
{
    public bool isOutlined = false;
    Outline ol;

    // Start is called before the first frame update
    void Start()
    {
        ol = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        ol.enabled = isOutlined;
        isOutlined = false;
    }

    public void LookingAtObject()
    {
        isOutlined = true;
    }
}
