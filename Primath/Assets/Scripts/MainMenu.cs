using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Canvas mainMenu;
    private Transform mainPanel, playPanel, settingsPanel, gradeGames;
    private Transform firstGrade, secondGrade, thirdGrade, fourthGrade;

    [SerializeField] Music music;
    [SerializeField] Sound sound;
    private void Awake()
    {
        mainMenu = this.gameObject.GetComponent<Canvas>();

        mainPanel = mainMenu.transform.Find("MainPanel");
        playPanel = mainMenu.transform.Find("PlayPanel");
        settingsPanel = mainMenu.transform.Find("SettingsPanel");
        gradeGames = mainMenu.transform.Find("GradeGames");

        firstGrade = gradeGames.Find("FirstGradePanel");
        secondGrade = gradeGames.Find("SecondGradePanel");
        thirdGrade = gradeGames.Find("ThirdGradePanel");
        fourthGrade = gradeGames.Find("ForuthGradePanel");

        firstGrade.gameObject.SetActive(false);
        secondGrade.gameObject.SetActive(false);
        thirdGrade.gameObject.SetActive(false);
        fourthGrade.gameObject.SetActive(false);

        gradeGames.gameObject.SetActive(true);
    }
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sound>();
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<Music>();

        settingsPanel.Find("SoundSlider").GetComponent<Slider>().value = music.GetMusicVolume();
        settingsPanel.Find("VolumeSlider").GetComponent<Slider>().value = sound.GetSoundVolume();

        OpenMainPanel();
    }

    public void ChangeMusicVol(float vol)
    {
        music.ChangeMusicVolume(vol);
    }

    public void ChangeSoundVol(float vol)
    {
        sound.ChangeSoundVolume(vol);
    }

    public void OpenMainPanel()
    {
        mainPanel.gameObject.SetActive(true);
        playPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
    }

    public void OpenPlayPanel()
    {
        mainPanel.gameObject.SetActive(false);
        playPanel.gameObject.SetActive(true);
        settingsPanel.gameObject.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        mainPanel.gameObject.SetActive(false);
        playPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
    }

    public void OpenFirstGrade()
    {
        firstGrade.gameObject.SetActive(true);
        playPanel.gameObject.SetActive(false);
    }

    public void OpenSecondGrade()
    {
        secondGrade.gameObject.SetActive(true);
        playPanel.gameObject.SetActive(false);
    }

    public void OpenThirdGrade()
    {
        thirdGrade.gameObject.SetActive(true);
        playPanel.gameObject.SetActive(false);
    }

    public void OpenFourthGrade()
    {
        fourthGrade.gameObject.SetActive(true);
        playPanel.gameObject.SetActive(false);
    }

    public void GoBackPlayPanel(GameObject obj)
    {
        obj.transform.parent.gameObject.SetActive(false);
        playPanel.gameObject.SetActive(true);
    }

    public void LoadFirstGradeFirstGame() => SceneManager.LoadScene(1); //Birinci Sýnýf Birinci Oyun
    public void LoadFirstGradeSecondGame() => SceneManager.LoadScene(2); //Birinci Sýnýf Ýkinci Oyun
    public void LoadFirstGradeThirdGame() => SceneManager.LoadScene(3); //Birinci Sýnýf Üçüncü Oyun

    public void LoadSecondGradeFirstGame() => SceneManager.LoadScene(4); //Ýkinci Sýnýf Birinci Oyun
    public void LoadSecondGradeSecondGame() => SceneManager.LoadScene(5); //Ýkinci Sýnýf Ýkinci Oyun
    public void LoadSecondGradeThirdGame() => SceneManager.LoadScene(6); //Ýkinci Sýnýf Üçüncü Oyun

    public void LoadThirdGradeFirstGame() => SceneManager.LoadScene(7); //Üçüncü Sýnýf Birinci Oyun
    public void LoadThirdGradeSecondGame() => SceneManager.LoadScene(8); //Üçüncü Sýnýf Ýkinci Oyun
    public void LoadThirdGradeThirdGame() => SceneManager.LoadScene(9); //Üçüncü Sýnýf Üçüncü Oyun

    public void LoadForuthGradeFirstGame() => SceneManager.LoadScene(10); //Dördüncü Sýnýf Birinci Oyun
    public void LoadForuthGradeSecondGame() => SceneManager.LoadScene(11); //Dördüncü Sýnýf Ýkinci Oyun
    public void LoadForuthGradeThirdGame() => SceneManager.LoadScene(21); //Dördüncü Sýnýf Üçüncü Oyun

}
