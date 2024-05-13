using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using UnityEngine.UIElements;
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
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        Debug.Log(eventSys.currentSelectedGameObject);


        if (IsAnyToggleSelected())
        {
            MyToggle toggle = eventSys.currentSelectedGameObject.GetComponent<MyToggle>();

            if (lastSelectedToggle==null)
            {
                lastSelectedToggle = toggle;
                Debug.Log(5555);

                return;
            }


            ///*
            //if (lastSelectedToggle != toggle)// && lastSelectedToggle != null)
            //{
                Debug.Log(1111+" ,  "+ lastSelectedToggle);
                if (lastSelectedToggle.IsSelected )
                {
                    Debug.Log(2222);
                    if(lastSelectedToggle != toggle)
                    {
                        lastSelectedToggle.OnDeselect();

                    }
                }
                else
                {
                    Debug.Log(3333);

                    toggle.SetSelected(true);
                }
            //}
            //else
            //{
              //  Debug.Log(4444);

            //}
            lastSelectedToggle = toggle;
        }
        else
        {

            if (toggles.Count() > 0)
            {
                //lastSelectedToggle = toggles[0].GetComponent<MyToggle>();
                //lastSelectedToggle.SetSelected(true);
            }
            //lastSelectedToggle = null;
        }//*/

            //lastSelectedToggle.OnDeselect();
            //toggle.SetSelected(true);
            //lastSelectedToggle = null;
        
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
