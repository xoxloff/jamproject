﻿using UnityEngine;
using UnityEngine.UI;

public class LayoutManager : MonoBehaviour
{

    [SerializeField]
    private GameObject factoryRef;
    [SerializeField]
    private GameObject manufacturePrefab;
    [SerializeField]
    private GameObject exitPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickExitButton();
        }
    }

    public void AddManufacturePrefabToFactory()
    {
        if (factoryRef == null && manufacturePrefab == null)
        {
            return;
        }

        GameObject newChild = Instantiate(manufacturePrefab, factoryRef.transform);

        //newChild.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = (factoryRef.transform.childCount).ToString();
    }

    public void OnClickExitButton()
    {
        exitPanel.SetActive(!exitPanel.activeSelf);
    }

    public void ClosePopupExitPanel()
    {
        exitPanel.SetActive(false);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}
