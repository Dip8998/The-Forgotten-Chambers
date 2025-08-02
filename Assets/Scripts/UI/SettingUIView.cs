using UnityEngine;
using UnityEngine.UI;
using ForgottonChambers.Main;

namespace ForgottonChambers.UI
{
    public class SettingUIView : MonoBehaviour
    {
        [SerializeField] private Button controlButton;
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject controlPanel;

        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;

        private void Start()
        {
            controlButton.onClick.AddListener(ControlPanel);
            backButton.onClick.AddListener(BackButton);

            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

            sfxVolumeSlider.value = GameService.Instance.SoundService.soundEffect.volume;
            musicVolumeSlider.value = GameService.Instance.SoundService.soundMusic.volume;
        }

        private void ControlPanel()
        {
            GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
            controlPanel.SetActive(true);
        }

        private void BackButton()
        {
            GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
            gameObject.SetActive(false);
        }

        private void SetSFXVolume(float volume)
        {
            GameService.Instance.SoundService.soundEffect.volume = volume;
        }

        private void SetMusicVolume(float volume)
        {
            GameService.Instance.SoundService.soundMusic.volume = volume;
        }
    }
}
