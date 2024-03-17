using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : BasePopup
{
    [SerializeField] TextMeshProUGUI difficultyNum;
    [SerializeField] Slider difficultySlider;
    [SerializeField] OptionsPopup optionsPopup;

    private void Start()
    {
        
    }

    override public void Open()
    {
        base.Open();
        difficultySlider.value = PlayerPrefs.GetInt("difficulty", 1);
        UpdateDifficulty(difficultySlider.value);
    }

    public void OnOkButton()
    {
        Debug.Log("ok button clicked");
        Close();
        optionsPopup.Open();
        PlayerPrefs.SetInt("difficulty", (int)difficultySlider.value);
        Messenger<int>.Broadcast(GameEvent.DIFFICULTY_CHANGED, (int)difficultySlider.value);
    }

    public void OnCancelButton()
    {
        Debug.Log("cancel button clicked");
        Close();
        optionsPopup.Open();
    }

    public void UpdateDifficulty(float difficulty)
    {
        difficultyNum.text = ((int)difficulty).ToString();
    }
    public void OnDifficultyValueChanged(float difficulty)
    {
        UpdateDifficulty(difficulty);
    }

}
