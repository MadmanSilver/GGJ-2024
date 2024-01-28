using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject storyStartPanel;
    [SerializeField] private GameObject storyIncompletePanel;
    [SerializeField] private GameObject storyCompletePanel;
    [SerializeField] private GameObject exit;

    public void NewGame(int lastScene) {
        if (GameManager.Instance.HasNone() && lastScene == 0)
        {
            storyStartPanel.SetActive(true);
        }

        DestroyMusic();
    }

    public void DoorInteract()
    {
        if (GameManager.Instance.HasAll())
        {
            storyCompletePanel.SetActive(true);
        }
        else
        {
            storyIncompletePanel.SetActive(true);
        }
    }

    public void NoTurningBack(int lastScene)
    {
        if (lastScene != 0)
        {
            exit.SetActive(false);
        }
    }

    private void DestroyMusic()
    {
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            if (source.name.Contains("Music"))
            {
                SceneManager.MoveGameObjectToScene(source.gameObject, gameObject.scene);
            }
        }
    }
}
