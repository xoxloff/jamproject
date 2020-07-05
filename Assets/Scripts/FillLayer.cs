using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillLayer : MonoBehaviour
{
    RectTransform rect;
    public float ratio;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        DrawLayer(ratio);
    }

    public void DrawLayer(float _ratio)
    {
        Debug.Log(_ratio.ToString());
        ratio = _ratio > 1 ? 1 : _ratio;
        rect.localScale = new Vector3(ratio, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
