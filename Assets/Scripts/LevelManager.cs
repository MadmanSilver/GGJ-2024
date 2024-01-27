using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<Vector2> spawnPoints;
    public UnityEvent<int> OnLevelInit;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private CharacterController playerController;

    private void Start() {
        Scene lastScene = SceneManager.GetActiveScene();
        InitLevel(lastScene.buildIndex);

        if (SceneManager.loadedSceneCount <= 1)
            return;

        SceneManager.SetActiveScene(gameObject.scene);
        SceneManager.UnloadSceneAsync(lastScene);
    }

    public void InitLevel(int lastScene)
    {
        playerController.enabled = false;
        playerTransform.position = spawnPoints[lastScene];
        playerController.enabled = true;
        OnLevelInit?.Invoke(lastScene);
    }
}
