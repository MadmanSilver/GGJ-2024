using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text[] pages;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private PlayerMovement playerMovement;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (playerMovement == null)
            playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerMovement.enabled = false;

        pages[0].gameObject.SetActive(true);

        for (int i = 1; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(false);
        }

        nextButton.SetActive(pages.Length > 1);
        previousButton.SetActive(false);
    }

    public void ToggleMovement(bool val)
    {
        playerMovement.enabled = val;
    }

    public void Next()
    {
        if (index >= pages.Length - 1)
            return;

        pages[index].gameObject.SetActive(false);
        index++;
        pages[index].gameObject.SetActive(true);

        if (index >= pages.Length - 1)
            nextButton.SetActive(false);
        else if (index > 0)
            previousButton.SetActive(true);
    }

    public void Previous()
    {
        if (index <= 0)
            return;

        pages[index].gameObject.SetActive(false);
        index--;
        pages[index].gameObject.SetActive(true);

        if (index <= 0)
            previousButton.SetActive(false);
        else if (index < pages.Length - 1)
            nextButton.SetActive(true);
    }
}
