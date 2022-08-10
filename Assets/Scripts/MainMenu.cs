using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject settings, setResolution, quitMenu;
    public AudioMixer audioMixer;

    Resolution[] resolutions;
    Dropdown resolutionDropdown;
    List<string> options;

    private void Start()
    {     
        
        resolutionDropdown = setResolution.GetComponent<Dropdown>();
        
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        options = new List<string>();
        options.Clear();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        settings.SetActive(false);
        quitMenu.SetActive(false);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;        
    }

    public void SetGraphic(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void SaveSettings()
    {
        settings.SetActive(false);
    }

    public void SettingsButton()
    {
        settings.SetActive(true);
    }

    public void QuitGame()
    {
        quitMenu.SetActive(true);
    }

    public void Quit(string answer)
    {
        if (answer.Equals("yes"))
        {
            Application.Quit();
        }
        else
        {
            quitMenu.SetActive(false);
        }
        
    }
}
