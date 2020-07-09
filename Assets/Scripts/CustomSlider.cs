using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private RectTransform fillLayer;
    private float ratio;

    public Text Text { get=>text; set=>text=value; }
    public RectTransform FillLayer { get => fillLayer; set => fillLayer = value; }
    // Start is called before the first frame update
    void Start()
    {
        fillLayer = fillLayer.GetComponent<RectTransform>();
        DrawLayer(0);
    }

    public void DrawLayer(float _ratio)
    {
        ratio = _ratio > 1 ? 1 : _ratio;
        fillLayer.localScale = new Vector3(ratio, 1, 1);
    }

    void Update()
    {
        
    }
}
