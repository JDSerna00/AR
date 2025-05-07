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

    [Header("Cameras")]
    public GameObject arCamera;
    public GameObject uiCamera;

    [Header("AR Button")]
    public TextMeshProUGUI arButtonText;

    private int currentIndex = 0;
    private bool isARActive = false;
    private bool isDropdownVisible = false;

    void Start()
    {
        // Asegúrate de que AR está desactivada al inicio
        arCamera.SetActive(false);
        uiCamera.SetActive(true);

        PopulateDropdown();
        flowerDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        infoPanel.SetActive(false);
        flowerDropdown.gameObject.SetActive(false);
        dropdownPanel.SetActive(true);

        arButtonText.text = "Activar AR";
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
        ShowInfoPanel();
    }

    public void ShowInfoPanel()
    {
        var flower = allFlowers[currentIndex];

        nameText.text = flower.commonName;
        sciNameText.text = flower.scientificName;
        descText.text = flower.description;
        flowerImage.sprite = flower.photo;
        Debug.Log($"Showing info for: {flower.commonName}");
        infoPanel.SetActive(true);
    }

    public void ToggleARCamera()
    {
        isARActive = !isARActive;

        arCamera.SetActive(isARActive);
        uiCamera.SetActive(!isARActive);
        dropdownPanel.SetActive(!isARActive);

        arButtonText.text = isARActive ? "Desactivar AR" : "Activar AR";
    }

    public void ToggleDropdown()
    {
        isDropdownVisible = !isDropdownVisible;
        flowerDropdown.gameObject.SetActive(isDropdownVisible);
    }
}
