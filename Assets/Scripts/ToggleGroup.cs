using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
//by Yiran Luo

public class MyToggleGroup : MonoBehaviour
{
    [SerializeField] private MyToggle lastSelectedToggle;
    [SerializeField] private MyToggle currentSelectedToggle;

    [SerializeField] private MyToggle[] toggles;
    public MultiplayerEventSystem eventSys;

    private void OnEnable()
    {
        toggles = GetComponentsInChildren<MyToggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].toggleGroup = this;
        }
    }

    private void OnDisable()
    {
        CleanToggleGroup();
    }

    private void Update()
    {
        if (IsAnyToggleSelected())
        {
            MyToggle toggle = eventSys.currentSelectedGameObject.GetComponent<MyToggle>();

            if(lastSelectedToggle != toggle)
            {
                if (toggle.IsSelected)
                {
                    toggle.OnDeselect();
                }
                else
                {
                    toggle.SetSelected(true);
                }
            }
            else
            {

            }
            lastSelectedToggle = toggle;
        }


    }

    public bool IsAnyToggleSelected()
    {
        foreach (var toggle in toggles)
        {
            if (eventSys.currentSelectedGameObject == toggle.gameObject)
            {
                return true;
            }
        }
        return false;
    }


    public void CleanToggleGroup()
    {
        //lastSelectedToggle = null;
        currentSelectedToggle = null;
        toggles = null;

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

    }


    public void SetCurrentlySelected(MyToggle value)
    {
        if (currentSelectedToggle != null)
        {
            if (currentSelectedToggle != value)
            {
                currentSelectedToggle.SetSelected(false);
            }
            else
            {
                currentSelectedToggle = null;
                return;
            }
        }
        currentSelectedToggle = value;
    }

    public void DeselectEverything()
    {
        if (currentSelectedToggle != null)
        {
            currentSelectedToggle = null;
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].SetSelected(false);
        }
    }

    public MyToggle GetCurrentToggle()
    {
        return currentSelectedToggle;
    }
}
