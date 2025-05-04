using UnityEngine;

[CreateAssetMenu(fileName = "NewFlower", menuName = "Flowers/Flower Data")]
public class FlowerData : ScriptableObject
{
    [Header("Basic Info")]
    public string commonName;
    public string scientificName;
    [TextArea(3, 10)] public string description;

    [Header("Visual Assets")]
    public Texture2D photo;
    public GameObject modelPrefab;

    [Header("AR Settings")]
    [Tooltip("Should match Vuforia target name without 'Target_' prefix")]
    public string vuforiaTargetName;
}