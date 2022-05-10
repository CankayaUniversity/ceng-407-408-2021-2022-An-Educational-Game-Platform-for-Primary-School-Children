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

    private float volume = 0.5f;
    private float sound = 0.5f;

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
        OpenMainPanel();
        settingsPanel.Find("SoundSlider").GetComponent<Slider>().value = sound;
        settingsPanel.Find("VolumeSlider").GetComponent<Slider>().value = volume;
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

    public void ChangeSound(float snd) => sound = snd;

    public void ChangeVolume(float vlm) => volume = vlm;

    public void LoadFirstGradeFirstGame() => SceneManager.LoadScene(1); //Birinci Sýnýf Birinci Oyun
    public void LoadFirstGradeSecondGame() => SceneManager.LoadScene(2); //Birinci Sýnýf Ýkinci Oyun
    public void LoadFirstGradeThirdGame() => SceneManager.LoadScene(3); //Birinci Sýnýf Üçüncü Oyun

    public void LoadSecondGradeFirstGame() => SceneManager.LoadScene(4); //Ýkinci Sýnýf Birinci Oyun
    public void LoadSecondGradeSecondGame() => SceneManager.LoadScene(5); //Ýkinci Sýnýf Ýkinci Oyun
    public void LoadSecondGradeThirdGame() => SceneManager.LoadScene(6); //Ýkinci Sýnýf Üçüncü Oyun

    public void LoadThirdGradeFirstGame() => SceneManager.LoadScene(7); //Üçüncü Sýnýf Birinci Oyun
    public void LoadThirdGradeSecondGame() => SceneManager.LoadScene(8); //Üçüncü Sýnýf Ýkinci Oyun
    public void LoadThirdGradeThirdGame() => SceneManager.LoadScene(0); //Üçüncü Sýnýf Üçüncü Oyun

    public void LoadForuthGradeFirstGame() => SceneManager.LoadScene(9); //Dördüncü Sýnýf Birinci Oyun
    public void LoadForuthGradeSecondGame() => SceneManager.LoadScene(10); //Dördüncü Sýnýf Ýkinci Oyun
    public void LoadForuthGradeThirdGame() => SceneManager.LoadScene(0); //Dördüncü Sýnýf Üçüncü Oyun

}
