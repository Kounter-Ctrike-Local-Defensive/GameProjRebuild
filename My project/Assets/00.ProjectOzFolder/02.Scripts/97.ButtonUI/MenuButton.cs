using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : BasedButtonUIScrept
{
    GameObject UsingTarget;
    public void Button_NewGame()
    {
        //StartCoroutine(LoadingScene());
    }

    public void Button_Continue()
    {

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

    public void Button_WindowOn_Option()
    {
        Target[1].SetActive(true);
        UsingTarget = Target[1];
    }
    public void Button_WindowOn_Video()
    {
        UsingTarget.SetActive(false);
        Target[2].SetActive(true);
        UsingTarget = Target[2];
    }
    public void Button_WindowOn_Audio()
    {
        UsingTarget.SetActive(false);
        Target[3].SetActive(true);
        UsingTarget = Target[3];
    }
    public void Button_WindowOn_Key()
    {
        UsingTarget.SetActive(false);
        Target[4].SetActive(true);
        UsingTarget = Target[4];
    }
    public void Button_WindowOn_Achievements()
    {
        UsingTarget.SetActive(false);
        Target[5].SetActive(true);
        UsingTarget = Target[5];
    }
    public void Button_WindowOn_Credit()
    {
        UsingTarget.SetActive(false);
        Target[6].SetActive(true);
        UsingTarget = Target[6];
    }

    public void Button_Keep()
    {

    }

    public void Button_MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
        //메인 메뉴로 갈땐 로딩 필요 없을거 같아서 그냥 로드씬 사용했습니다.
    }

    IEnumerator LoadingScene()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("InGameScene01");
        //로딩화면 등 추후 추가
        while(!asyncload.isDone)
            yield return null;
    }
}
