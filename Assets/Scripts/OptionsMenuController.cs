using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionsMenuController : Singleton<OptionsMenuController>
{
    [SerializeField] private CycleSelector resolutionSelector;

    private Option[] options;
    
    private void Start()
    {
        options = GetComponentsInChildren<Option>(true);

        LoadResolutions();

        for (int i = 0; i < options.Length; i++)
        {
            options[i].Init();
        }
    }

    public void Apply()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].Apply();
        }
    }

    public void Revert()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].Revert();
        }
    }

    public void SetDisplayMode(int fullScreenMode)
    {
        Screen.fullScreenMode = (FullScreenMode)fullScreenMode;
    }

    private void LoadResolutions()
    {
        resolutionSelector.options.Clear();
        
        for (int i = Screen.resolutions.Length - 1; i >= 0; i--)
        {
            var res = Screen.resolutions[i];
            float aspect = (float)res.width / (float)res.height;

            if (aspect <= 16f / 9f + 0.0001f && aspect >= 16f / 9f - 0.0001f)
            {
                CycleSelector.SelectorOption option = new CycleSelector.SelectorOption() {
                    label = $"{res.width}x{res.height}",
                    applyAction = new UnityEvent()
                };
                option.applyAction.AddListener(() => {Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);});
                resolutionSelector.options.Add(option);
            }
        }
    }
}

public abstract class Option : MonoBehaviour
{
    public string prefKey = "";

    public abstract void Init();
    public abstract void Apply();
    public abstract void Revert();
}