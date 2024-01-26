using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CycleSelector : Option
{
    public float cycleSpeed = 4f;
    public float spacing = 10f;
    public int value = 0;
    public List<SelectorOption> options = new List<SelectorOption> {new SelectorOption {label = "Option 1", applyAction = null},
                                        new SelectorOption {label = "Option 2", applyAction = null},
                                        new SelectorOption {label = "Option 3", applyAction = null}};

    [SerializeField] private RectTransform labelParent;
    [SerializeField] private TMP_Text currentLabel, nextLabel;
    
    private int cycleDir = 0;

    [Serializable]
    public struct SelectorOption
    {
        public string label;
        public UnityEvent applyAction;
    }

    public override void Init()
    {
        value = PlayerPrefs.GetInt(prefKey, value);
        currentLabel.text = options[value].label;
        currentLabel.rectTransform.anchoredPosition = Vector2.zero;
        options[value].applyAction.Invoke();
    }

    public override void Apply()
    {
        PlayerPrefs.SetInt(prefKey, value);
        options[value].applyAction.Invoke();
    }

    public override void Revert()
    {
        value = PlayerPrefs.GetInt(prefKey, 0);
        currentLabel.text = options[value].label;
    }

    // Update is called once per frame
    void Update()
    {
        if (cycleDir == 0)
            return;

        var distance = labelParent.rect.width + spacing;
        var translation = new Vector2(cycleSpeed * cycleDir * distance * Time.deltaTime, 0f);
        currentLabel.rectTransform.anchoredPosition += translation;
        nextLabel.rectTransform.anchoredPosition += translation;

        if (Mathf.Abs(currentLabel.rectTransform.anchoredPosition.x) >= distance)
        {
            currentLabel.rectTransform.anchoredPosition = new Vector2(distance * cycleDir, 0f);
            nextLabel.rectTransform.anchoredPosition = new Vector2(0f, 0f);
            cycleDir = 0;
            var tmp = currentLabel;
            currentLabel = nextLabel;
            nextLabel = tmp;
        }
    }

    public void CycleRight()
    {
        value++;

        if (value >= options.Count)
            value = value % options.Count;

        cycleDir = -1;

        if (nextLabel == null)
            nextLabel = Instantiate(currentLabel, labelParent);

        nextLabel.text = options[value].label;
        nextLabel.rectTransform.anchoredPosition = new Vector2((labelParent.rect.width + spacing) * -cycleDir, 0f);
    }

    public void CycleLeft()
    {
        value--;

        if (value < 0)
            value += options.Count;

        cycleDir = 1;

        if (nextLabel == null)
            nextLabel = Instantiate(currentLabel, labelParent);

        nextLabel.text = options[value].label;
        nextLabel.rectTransform.anchoredPosition = new Vector2((labelParent.rect.width + spacing) * -cycleDir, 0f);
    }
}
