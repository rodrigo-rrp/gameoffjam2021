using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildManager : MonoBehaviour
{
    private List<GameObject> _instances = new List<GameObject>();
    private int _currentPlaceholderIndex = 0;
    private GameObject[] _buildSlots;
    public GameObject[] BuildableObjects;

    public static BuildManager instance { get; private set; }

    private GameObject _currentBuildSlot = null;

    public bool IsBuilding = false;

    private ConstructionMenu _constructionMenu;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _constructionMenu = UIManager.instance.constructionPanel.GetComponent<ConstructionMenu>();

        foreach (GameObject go in BuildableObjects)
        {
            GameObject current = Instantiate(go, transform.position, transform.rotation);
            // current.GetComponentInChildren<Tower>().enabled = false;
            SetLayerRecursively(current, LayerMask.NameToLayer("Ignore Raycast"));
            current.SetActive(false);
            _instances.Add(current);
        }

        _buildSlots = GameObject.FindGameObjectsWithTag("BuildSlot");

        for (int i = 0; i < _buildSlots.Length; i++)
        {
            _buildSlots[i].GetComponent<BuildSlot>().Index = i;
        }
    }

    void SetAvailable()
    {
        GameObject current = _instances[_currentPlaceholderIndex];
        Renderer[] renderers = current.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Color lowOpacityColor = renderer.material.color;
            lowOpacityColor.a = 0.3f;
            lowOpacityColor.r = 0.2f;
            lowOpacityColor.g = 0.5f;
            lowOpacityColor.b = 0.2f;
            renderer.material.color = lowOpacityColor;
        }
    }

    void SetUnavailable()
    {
        GameObject current = _instances[_currentPlaceholderIndex];
        Color lowOpacityColor = current.GetComponentInChildren<Renderer>().material.color;
        lowOpacityColor.a = 0.5f;
        lowOpacityColor.r = 0.7f;
        lowOpacityColor.g = 0.3f;
        lowOpacityColor.b = 0.3f;
        current.GetComponentInChildren<Renderer>().material.color = lowOpacityColor;
    }


    public void OnMouseEnter(int bsIndex)
    {
        if (IsBuilding || SpawnManager.instance.isPlaying) return;
        _currentBuildSlot = _buildSlots[bsIndex];
        _spawnPlaceholder();
    }

    private void _spawnPlaceholder()
    {
        _instances[_currentPlaceholderIndex].transform.position = _currentBuildSlot.transform.position;
        _instances[_currentPlaceholderIndex].SetActive(true);

        if (GameManager.instance.Currency >= _instances[_currentPlaceholderIndex].GetComponentInChildren<Tower>().Cost)
        {
            SetAvailable();
        }
        else
        {
            SetUnavailable();
        }
    }

    public void OnBuildButtonEnter(int index)
    {
        _instances[_currentPlaceholderIndex].SetActive(false);
        _currentPlaceholderIndex = index;
        _spawnPlaceholder();
    }

    public void OnMouseOver(int bsIndex)
    {
        if (SpawnManager.instance.isWaiting && PlayerInput.Instance.LeftMouseButton)
        {
            _currentBuildSlot = IsBuilding ? _currentBuildSlot : _buildSlots[bsIndex];
            _instances[_currentPlaceholderIndex].transform.position = _currentBuildSlot.transform.position;
            _instances[_currentPlaceholderIndex].SetActive(true);

            if (GameManager.instance.Currency >= _instances[_currentPlaceholderIndex].GetComponentInChildren<Tower>().Cost)
            {
                SetAvailable();
            }
            else
            {
                SetUnavailable();
            }

            UIManager.instance.PositionConstructionPanel(_currentBuildSlot.transform.position);
            IsBuilding = true;
        }
    }

    public void Build(int buttonIndex)
    {
        if (GameManager.instance.Buy(_instances[buttonIndex].GetComponentInChildren<Tower>().Cost))
        {
            Instantiate(BuildableObjects[buttonIndex], _currentBuildSlot.transform.position, _currentBuildSlot.transform.rotation);
            _currentBuildSlot.SetActive(false);
            _instances[_currentPlaceholderIndex].SetActive(false);
            IsBuilding = false;
            UIManager.instance.DeactivateConstructionPanel();
        }
    }

    public void OnMouseExit()
    {
        if (IsBuilding) return;
        _instances[_currentPlaceholderIndex].SetActive(false);
        _currentBuildSlot = null;
    }

    void Update()
    {
        if ((IsBuilding && PlayerInput.Instance.LeftMouseButton && !_constructionMenu.MouseOver) || SpawnManager.instance.isPlaying)
        {
            _instances[_currentPlaceholderIndex].SetActive(false);
            _currentBuildSlot = null;
            IsBuilding = false;
            UIManager.instance.DeactivateConstructionPanel();
        }
    }

    public void SetLayerRecursively(GameObject obj, int layer)
    {
        if (obj.layer != LayerMask.NameToLayer("MiniMap"))
            obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

}