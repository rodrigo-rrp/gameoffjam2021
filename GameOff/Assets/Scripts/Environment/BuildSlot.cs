using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour
{
    public GameObject[] BuildableObjects;
    private List<GameObject> _instances = new List<GameObject>();
    private int _currentPlaceholder = 0;

    void Awake()
    {
        foreach(GameObject go in BuildableObjects)
        {
            GameObject current = Instantiate(go, transform.position, transform.rotation);
            current.GetComponentInChildren<Entity>().enabled = false;
            SetLayerRecursively(current, LayerMask.NameToLayer("Ignore Raycast"));
            Color lowOpacityColor = current.GetComponentInChildren<Renderer>().material.color;
            lowOpacityColor.a = 0.5f;
            current.GetComponentInChildren<Renderer>().material.color = lowOpacityColor;
            current.SetActive(false);
            _instances.Add(current);
        }
    }


    void OnMouseEnter()
    {
       _instances[_currentPlaceholder].SetActive(true);
    }

    void OnMouseOver()
    {
        if (PlayerInput.Instance.LeftMouseButton) 
        {
            Instantiate(BuildableObjects[0], transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    void OnMouseExit()
    {
       _instances[_currentPlaceholder].SetActive(false);
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
