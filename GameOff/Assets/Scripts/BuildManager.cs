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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (GameObject go in BuildableObjects)
        {
            GameObject current = Instantiate(go, transform.position, transform.rotation);
            current.GetComponentInChildren<Tower>().enabled = false;
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
        Color lowOpacityColor = current.GetComponentInChildren<Renderer>().material.color;
        lowOpacityColor.a = 0.5f;
        lowOpacityColor.r = 0.3f;
        lowOpacityColor.g = 0.7f;
        lowOpacityColor.b = 0.3f;
        current.GetComponentInChildren<Renderer>().material.color = lowOpacityColor;
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
        _currentBuildSlot = _buildSlots[bsIndex];
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

    public void OnMouseOver()
    {
        if (_currentBuildSlot == null)
            return;

        if (PlayerInput.Instance.LeftMouseButton && GameManager.instance.Buy(_instances[_currentPlaceholderIndex].GetComponentInChildren<Tower>().Cost))
        {
            Instantiate(BuildableObjects[_currentPlaceholderIndex], _currentBuildSlot.transform.position, _currentBuildSlot.transform.rotation);
            _currentBuildSlot.SetActive(false);
            OnMouseExit();
        }
    }

    public void OnMouseExit()
    {
        _instances[_currentPlaceholderIndex].SetActive(false);
        _currentBuildSlot = null;
    }

    public void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

}