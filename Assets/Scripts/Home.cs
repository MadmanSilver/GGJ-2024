using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject storyStartPanel;
    [SerializeField] private GameObject storyIncompletePanel;
    [SerializeField] private GameObject storyCompletePanel;

    private void Start() {
        if (GameManager.Instance.HasNone())
        {
            storyStartPanel.SetActive(true);
        }
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
}
