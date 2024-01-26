using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : Singleton<MainMenuController>
{
    public void Exit()
    {
        Application.Quit();
    }
}
