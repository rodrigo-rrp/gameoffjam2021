using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class ConstructionMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private List<ConstructionMenuButton> _buttons;
    public bool MouseOver = false;
    private bool _selfMouseOver = false;

    void Awake()
    {
        _buttons = GetComponentsInChildren<ConstructionMenuButton>().ToList();
    }

    void Update()
    {
        MouseOver = _selfMouseOver || _buttons.Any(b => b.MouseOver);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selfMouseOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _selfMouseOver = false;
    }
}
