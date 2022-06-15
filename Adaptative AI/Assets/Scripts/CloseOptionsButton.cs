using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOptionsButton : MonoBehaviour
{
    public GameObject escMenu;
    public GameObject endMenu;
    private bool lastMenuIsEsc = false;
    private bool lastMenuIsEnd = false;

    public void ActivateOptionsMenu(bool escMenu)
    {
        if (escMenu)
        {
            lastMenuIsEsc = true;
        }
        else
        {
            lastMenuIsEnd = true;
        }
    }
    public void DeactivateOptionsMenu()
    {
        if (lastMenuIsEnd)
        {
            lastMenuIsEnd = false;
            endMenu.SetActive(true);
        }
        else if (lastMenuIsEsc)
        {
            lastMenuIsEsc = false;
            escMenu.SetActive(true);
        }
    }
}
