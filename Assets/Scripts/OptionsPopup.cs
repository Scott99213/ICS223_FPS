using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OptionsPopup : BasePopup
{
    [SerializeField] private UIController uiController;
    [SerializeField] private SettingsPopup settingsPopup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSettingsButton()
    {
        Debug.Log("settings clicked");
        Close();
        settingsPopup.Open();
    }
    public void OnExitGameButton()
    {
        Debug.Log("exit game");
        Application.Quit();
    }
    public void OnReturnToGameButton()
    {
        Debug.Log("return to game");
        Close();
        uiController.SetGameActive(true);
    }

}
