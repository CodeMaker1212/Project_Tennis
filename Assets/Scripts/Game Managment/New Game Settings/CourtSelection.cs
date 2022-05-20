using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using environments = ProjectEnums.Enums.Environments;


public class CourtSelection : NewGameSettings
{
    private Image _courtPreviewImage;
    private Text _previewText;
    [SerializeField] protected List<Sprite> _previewImages;

    private int _currentEnvironmentIndex;

    public void SwitchNextEnvironment()
    {
        if (_currentEnvironmentIndex == 2)
            _currentEnvironmentIndex--;

        _courtPreviewImage.sprite = _previewImages[_currentEnvironmentIndex + 1];
        _currentEnvironmentIndex = Mathf.Clamp(_currentEnvironmentIndex + 1, 0, _previewImages.Count);

        ChangePreviewText();
    }
    public void SwitchPreviousEnvironment()
    {
        if (_currentEnvironmentIndex == 0)
            _currentEnvironmentIndex++;

        _courtPreviewImage.sprite = _previewImages[_currentEnvironmentIndex - 1];
        _currentEnvironmentIndex = Mathf.Clamp(_currentEnvironmentIndex - 1, 0, _previewImages.Count);

        ChangePreviewText();
    }
    private void ChangePreviewText()
    {
        switch (_currentEnvironmentIndex)
        {
            case 0:
                _previewText.text = environments.Crystals.ToString();
                ChooseEnvironment(environments.Crystals);
                break;
            case 1:
                _previewText.text = environments.West.ToString();
                ChooseEnvironment(environments.West);
                break;
            case 2:
                _previewText.text = "Flower Field";
                ChooseEnvironment(environments.Flower_field);
                break;
        }
    }
    private void SetDefaultEnvironment()
    {
        _courtPreviewImage.sprite = _previewImages[(int)environments.Crystals];
        _currentEnvironmentIndex = (int)environments.Crystals;
        _previewText.text = environments.Crystals.ToString();
    }




    private void Awake()
    {
        _courtPreviewImage = GameObject.Find("Court_Preview").GetComponent<Image>();
        _previewText = GameObject.Find("Environment_Name").GetComponent<Text>();

        SetDefaultEnvironment();

    }
}
