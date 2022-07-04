using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : BasedButtonUIScrept
{
    GameObject UsingTarget;
    public void Button_NewGame()
    {

    }

    public void Button_Continue()
    {

    }
    public void Button_Setting()
    {
        Target[1].SetActive(true);
        UsingTarget = Target[1];
    }
    public void Button_GameOver()
    {
        Application.Quit();
    }

    public void Button_Back()
    {
        if (UsingTarget = Target[1])
            Target[1].SetActive(false);
        else
        {
            UsingTarget.SetActive(false);
            UsingTarget = Target[1];
        }
    }

    public void Button_VideoWindowOn()
    {
        UsingTarget.SetActive(false);
        Target[2].SetActive(true);
        UsingTarget = Target[2];
    }
    public void Button_AudioWindowOn()
    {
        UsingTarget.SetActive(false);
        Target[3].SetActive(true);
        UsingTarget = Target[3];
    }
    public void Button_KeyWindowOn()
    {
        UsingTarget.SetActive(false);
        Target[4].SetActive(true);
        UsingTarget = Target[4];
    }
    public void Button_AchievementsWindowOn()
    {
        UsingTarget.SetActive(false);
        Target[5].SetActive(true);
        UsingTarget = Target[5];
    }
    public void Button_CreditWindowOn()
    {
        UsingTarget.SetActive(false);
        Target[6].SetActive(true);
        UsingTarget = Target[6];
    }
}
