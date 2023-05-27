using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider mouseSensivitySlider;
    public GameObject settingPanel;
    private bool isShowSettingPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MS"))
        {
            mouseSensivitySlider.value = PlayerPrefs.GetInt("MS");
        }
        else
        {
            mouseSensivitySlider.value = 550;
        }
    }

    public void SetMouseSensivity()
    {
        PlayerPrefs.SetInt("MS", (int)mouseSensivitySlider.value);
    }

    public void SettingButton()
    {
        settingPanel.SetActive(isShowSettingPanel);

        isShowSettingPanel = !isShowSettingPanel;
    }

}
