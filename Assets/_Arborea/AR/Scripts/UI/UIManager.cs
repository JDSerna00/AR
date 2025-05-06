using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Dropdown flowerDropdown;
    public GameObject infoPanel;
    public Image flowerImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sciNameText;
    public TextMeshProUGUI descText;

    [Header("Flower Data")]
    public FlowerData[] allFlowers;

    private int currentIndex = 0;

    void Start()
    {
        PopulateDropdown();
        flowerDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        infoPanel.SetActive(false);
    }

    void PopulateDropdown()
    {
        flowerDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();
        foreach (var flower in allFlowers)
        {
            options.Add(flower.commonName);
        }
        flowerDropdown.AddOptions(options);
    }

    void OnDropdownValueChanged(int index)
    {
        currentIndex = index;
    }

    public void ShowInfoPanel()
    {
        var flower = allFlowers[currentIndex];

        nameText.text = flower.commonName;
        sciNameText.text = flower.scientificName;
        descText.text = flower.description;
        flowerImage.sprite = flower.photo;

        infoPanel.SetActive(true);
    }
}
