using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;
using UnityEngine.EventSystems;
using JetBrains.Annotations;

public class Toggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] private UnityEvent onSelect;

    //[HideInInspector]
    public ToggleGroup toggleGroup;

    private bool isSelected = false;
    public bool IsSelected => isSelected;

    private bool isDisabled = false;
    public bool IsDisabled => isDisabled;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDisabled) return;

        if (isSelected)
        {
            OnDeselect();
        }
        else
        {
            onSelect?.Invoke();
            SetSelected(true);
        }
    }

    public void OnDeselect()
    {
        if (isDisabled) return;

        SetSelected(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDisabled) return;

        if (!isSelected)
        {
            image.color = highlightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDisabled) return;

        if (!isSelected)
        {
            image.color = normalColor;
        }
    }

    public void SetSelected(bool value)
    {
        if (isDisabled) return;

        isSelected = value;
        if (isSelected == true)
        {
            if (toggleGroup)
            {
                toggleGroup.SetCurrentlySelected(this);
            }
            image.color = selectedColor;
        }
        else
        {
            image.color = normalColor;
        }
    }

    public void SetDisabled(bool value)
    {
        isDisabled = value;
        if (isDisabled == true)
        {
            isSelected = false;
            image.color = disabledColor;
        }
        else
        {
            image.color = normalColor;
        }
    }

    public void SetIcon(Sprite icon)
    {
        GetComponentInChildren<Image>().sprite = icon;
    }
}


