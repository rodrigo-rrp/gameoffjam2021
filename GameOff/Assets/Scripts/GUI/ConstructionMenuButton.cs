using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject _selectedGO;
    public bool MouseOver = false;
    public int Index;

    void Awake()
    {
        _selectedGO = transform.Find("Selected").gameObject;
    }

    public void OnClick()
    {
        BuildManager.instance.Build(Index);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selectedGO.SetActive(true);
        MouseOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _selectedGO.SetActive(false);
        MouseOver = false;
    }
}
