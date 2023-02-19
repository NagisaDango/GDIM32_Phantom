using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//by Yiran Luo

public class ToggleGroup : MonoBehaviour
{
    [SerializeField] private Toggle lastSelectedToggle;
    [SerializeField] private Toggle currentSelectedToggle;

    [SerializeField] private Toggle[] toggles;

    private void OnEnable()
    {
        toggles = GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].toggleGroup = this;
        }
    }

    private void OnDisable()
    {
        CleanToggleGroup();
    }

    public void CleanToggleGroup()
    {
        lastSelectedToggle = null;
        currentSelectedToggle = null;
        toggles = null;

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

    }


    public void SetCurrentlySelected(Toggle value)
    {
        if (currentSelectedToggle != null)
        {
            if (currentSelectedToggle != value)
            {
                currentSelectedToggle.SetSelected(false);
                lastSelectedToggle = currentSelectedToggle;
            }
        }
        currentSelectedToggle = value;
    }

    public void DeselectEverything()
    {
        if (currentSelectedToggle != null)
        {
            lastSelectedToggle = currentSelectedToggle;
            currentSelectedToggle = null;
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].SetSelected(false);
        }
    }

    public Toggle GetCurrentToggle()
    {
        return currentSelectedToggle;
    }
}
