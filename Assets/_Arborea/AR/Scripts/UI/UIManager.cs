using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using Vuforia; 

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Dropdown flowerDropdown;
    public GameObject infoPanel;
    public UnityEngine.UI.Image flowerImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sciNameText;
    public TextMeshProUGUI descText;

    [Header("Flower Data")]
    public FlowerData[] allFlowers;

    [Header("Panels")]
    public GameObject dropdownPanel;

    private int currentIndex = 0;
    private bool isARActive = false;
    private bool isDropdownVisible = false;

    void Start()
    {
        PopulateDropdown();
        flowerDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        infoPanel.SetActive(false);
        flowerDropdown.gameObject.SetActive(false);
    }

    void PopulateDropdown()
    {
        flowerDropdown.ClearOptions();
        var options = new List<string>();
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

    public void ToggleARCamera()
    {
        isARActive = !isARActive;

        VuforiaBehaviour.Instance.enabled = isARActive;

        // Si la cámara AR está activa, ocultamos el panel del dropdown
        dropdownPanel.SetActive(!isARActive);
    }

    public void ToggleDropdown()
    {
        isDropdownVisible = !isDropdownVisible;
        flowerDropdown.gameObject.SetActive(isDropdownVisible);
    }
}
