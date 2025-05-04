using UnityEngine;
using System.Collections.Generic;

public class FlowerManager : MonoBehaviour
{
    public static FlowerManager Instance { get; private set; }

    [Header("Flower Database")]
    [SerializeField] private List<FlowerData> allFlowers = new List<FlowerData>();

    [Header("Discovery Settings")]
    [SerializeField] private bool persistDiscoveredFlowers = true;
    private readonly HashSet<string> _discoveredFlowerNames = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadDiscoveredFlowers();
    }
    void Start()
    {
        foreach (var flower in allFlowers)
        {
            Debug.Log($"Flower: {flower.commonName} | Vuforia ID: Target_{flower.vuforiaTargetName}");
        }
    }

    public FlowerData GetFlowerByName(string flowerName)
    {
        // Case-insensitive search
        return allFlowers.Find(f =>
            f.commonName.Equals(flowerName, System.StringComparison.OrdinalIgnoreCase));
    }

    public void MarkFlowerDiscovered(string flowerName)
    {
        if (_discoveredFlowerNames.Add(flowerName))
        {
            Debug.Log($"New flower discovered: {flowerName}");

            if (persistDiscoveredFlowers)
            {
                SaveDiscoveredFlowers();
            }
        }
    }

    public List<FlowerData> GetDiscoveredFlowers()
    {
        var discovered = new List<FlowerData>();
        foreach (var flowerName in _discoveredFlowerNames)
        {
            var flower = GetFlowerByName(flowerName);
            if (flower != null) discovered.Add(flower);
        }
        return discovered;
    }

    public List<FlowerData> SearchFlowers(string searchQuery)
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
            return new List<FlowerData>(allFlowers);

        return allFlowers.FindAll(f =>
            f.commonName.Contains(searchQuery, System.StringComparison.OrdinalIgnoreCase) ||
            f.scientificName.Contains(searchQuery, System.StringComparison.OrdinalIgnoreCase));
    }

    #region Persistence
    private const string DISCOVERED_FLOWERS_KEY = "DiscoveredFlowers";

    private void SaveDiscoveredFlowers()
    {
        var names = string.Join(",", _discoveredFlowerNames);
        PlayerPrefs.SetString(DISCOVERED_FLOWERS_KEY, names);
        PlayerPrefs.Save();
    }

    private void LoadDiscoveredFlowers()
    {
        if (!PlayerPrefs.HasKey(DISCOVERED_FLOWERS_KEY)) return;

        var savedNames = PlayerPrefs.GetString(DISCOVERED_FLOWERS_KEY);
        foreach (var name in savedNames.Split(','))
        {
            if (!string.IsNullOrEmpty(name))
                _discoveredFlowerNames.Add(name);
        }
    }
    #endregion

    #region Editor Utilities
#if UNITY_EDITOR
    [ContextMenu("Auto-Assign Flower Names")]
    private void AutoAssignFlowerNames()
    {
        foreach (var flower in allFlowers)
        {
            if (flower.photo != null)
            {
                flower.commonName = flower.photo.name.Replace("Photo_", "");
            }
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
    #endregion
}