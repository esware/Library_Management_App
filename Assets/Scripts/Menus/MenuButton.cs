using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ActivatePanel);
    }

    private void ActivatePanel()
    {
        var menus =  NMenuManager.Instance.menus;

        foreach (var menu in menus)
        {
            if (menu == panel)
            {
                panel.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }
            
        }

    }
}
