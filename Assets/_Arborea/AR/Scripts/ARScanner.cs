using UnityEngine;
using Vuforia;

public class ARScanner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float defaultModelScale = 0.1f;

    private GameObject _currentModel;
    private Transform _currentModelAnchor;

    void Start()
    {
        RegisterAllTargets();
    }

    private void RegisterAllTargets()
    {
        var imageTargets = FindObjectsOfType<ImageTargetBehaviour>();
        foreach (var target in imageTargets)
        {
            // Ensure each target has a ModelAnchor child
            var anchor = target.transform.Find("ModelAnchor");
            if (anchor == null)
            {
                anchor = new GameObject("ModelAnchor").transform;
                anchor.SetParent(target.transform);
                anchor.localPosition = Vector3.zero;
            }

            target.OnTargetStatusChanged += (behaviour, status) =>
            {
                if (status.Status == Status.TRACKED)
                    OnTargetFound(behaviour, anchor);
                else if (_currentModelAnchor == anchor)
                    OnTargetLost();
            };
        }
    }

    private void OnTargetFound(ObserverBehaviour behaviour, Transform modelAnchor)
    {
        string targetName = behaviour.TargetName.Replace("Target_", "");
        FlowerData flower = FlowerManager.Instance.GetFlowerByName(targetName);

        if (flower != null)
        {
            FlowerManager.Instance.MarkFlowerDiscovered(flower.commonName);
            _currentModelAnchor = modelAnchor;
            SpawnModel(flower.modelPrefab);
        }
    }

    private void SpawnModel(GameObject modelPrefab)
    {
        DestroyCurrentModel();

        _currentModel = Instantiate(modelPrefab, _currentModelAnchor);
        _currentModel.transform.localPosition = Vector3.zero;
        _currentModel.transform.localRotation = Quaternion.identity;
        _currentModel.transform.localScale = Vector3.one * defaultModelScale;

        SetupMVCComponents(_currentModel);
    }

    private void SetupMVCComponents(GameObject model)
    {
        var controller = model.AddComponent<TouchInputController>();
        var view = model.AddComponent<ObjectView>();
        controller.Initialize(
            new ObjectTransformModel(model.transform.localScale),
            view
        );
    }

    private void OnTargetLost()
    {
        DestroyCurrentModel();
        _currentModelAnchor = null;
        Debug.Log("Target lost");
    }

    private void DestroyCurrentModel()
    {
        if (_currentModel != null)
        {
            Destroy(_currentModel);
            _currentModel = null;
        }
    }
}