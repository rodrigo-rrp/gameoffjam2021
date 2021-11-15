using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class ConstructionMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private List<ConstructionMenuButton> _buttons;
    public bool MouseOver = false;

    void Awake()
    {
        _buttons = GetComponentsInChildren<ConstructionMenuButton>().ToList();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOver = _buttons.Any(b => b.MouseOver);
    }
}
