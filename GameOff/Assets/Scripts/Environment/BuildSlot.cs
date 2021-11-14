using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour
{
    public int Index;

    void OnMouseEnter()
    {
        BuildManager.instance.OnMouseEnter(Index);
    }

    void OnMouseOver()
    {
        BuildManager.instance.OnMouseOver();
    }

    void OnMouseExit()
    {
        BuildManager.instance.OnMouseExit();
    }
}
