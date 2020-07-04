using UnityEngine;
using UnityEngine.UI;

public class LayoutManager : MonoBehaviour
{

    [SerializeField]
    private GameObject factoryRef;
    [SerializeField]
    private GameObject manufacturePrefab;
    [SerializeField]
    private GameObject exitPanel;
    [SerializeField]
    private GameObject factoryUpgradePanel;
    [SerializeField]
    private GameObject scientistsUpgradePanel;
    [SerializeField]
    private GameObject playerInfoPanel;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
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
        if (playerInfoPanel.activeSelf)
        {
            playerInfoPanel.SetActive(false);
        }
        else
        {
            exitPanel.SetActive(!exitPanel.activeSelf);
        }
    }

    public void ClosePopupExitPanel()
    {
        exitPanel.SetActive(false);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    public void OnClickFactoryButton()
    {
        factoryUpgradePanel.SetActive(false);
        scientistsUpgradePanel.SetActive(false);
    }

    public void OnClickFactoryUpgradeButton()
    {
        factoryUpgradePanel.SetActive(!factoryUpgradePanel.activeSelf);
        scientistsUpgradePanel.SetActive(false);
    }

    public void OnClickScientistsUpgrageButton()
    {
        factoryUpgradePanel.SetActive(false);
        scientistsUpgradePanel.SetActive(!scientistsUpgradePanel.activeSelf);
    }

    public void OnClickPlayerInfoButton()
    {
        playerInfoPanel.SetActive(!playerInfoPanel.activeSelf);
    }

}
