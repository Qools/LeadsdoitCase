using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    public List<UIPanel> panels = new List<UIPanel>();
    public GameObject loadingScreen;

    public void Init()
    {
        CloseAllPanels();

        SetStatus(Status.ready);
    }
      
    #region Panel Functions

    public void CloseAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.Close();
        }
    }

    public T GetPanel<T>(object data = null) where T : UIPanel
    {
        for (int i = 0; i < panels.Count; ++i)
        {
            T view = panels[i] as T;
            if (view != null)
            {
                return view;
            }
        }

        return null;
    }

    public T OpenPanel<T>(object data = null) where T : UIPanel
    {

        for (int i = 0; i < panels.Count; ++i)
        {
            T view = panels[i] as T;
            if (view != null)
            {
                view.Open();
                return view;
            }
        }

        return null;
    }

    public T ClosePanel<T>(object data = null) where T : UIPanel
    {

        for (int i = 0; i < panels.Count; ++i)
        {
            T view = panels[i] as T;
            if (view != null)
            {
                view.Close();
                return view;
            }
        }

        return null;
    }


    public T SwitchPanel<T>(object data = null) where T : UIPanel
    {
        CloseAllPanels();
        for (int i = 0; i < panels.Count; ++i)
        {
            T view = panels[i] as T;
            if (view != null)
            {
                view.Open();
                return view;
            }
        }

        return null;
    }

    #endregion
}
