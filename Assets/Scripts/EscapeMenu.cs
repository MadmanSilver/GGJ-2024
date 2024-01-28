using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        DontDestroyOnLoad(this);
    }

    private void Update() {
        if (Input.GetKeyDown("escape"))
        {
            canvas.enabled = !canvas.enabled;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ExitToMenu()
    {
        SceneManager.MoveGameObjectToScene(FindAnyObjectByType<GameManager>().gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        SceneManager.LoadScene(0);
    }

    public void Close()
    {
        canvas.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
