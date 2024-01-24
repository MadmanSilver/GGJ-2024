using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
    public int sceneIndex = 0;

    public void LoadScene()
    {
        LoadScene(sceneIndex);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }
}
