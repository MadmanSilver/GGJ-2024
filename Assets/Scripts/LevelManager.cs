using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<Vector2> spawnPoints;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private CharacterController playerController;

    private void Start() {
        Scene lastScene = SceneManager.GetActiveScene();
        InitLevel(lastScene.buildIndex);

        SceneManager.SetActiveScene(gameObject.scene);
        SceneManager.UnloadSceneAsync(lastScene);
    }

    public void InitLevel(int lastScene)
    {
        playerController.enabled = false;
        playerTransform.position = spawnPoints[lastScene];
        playerController.enabled = true;
    }
}
