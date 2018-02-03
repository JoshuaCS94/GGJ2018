using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [Header("References")]
    public Text robotText;
    public Text coreText;

    public List<GameObject> robots;
    public List<GameObject> cores;

    private GameObject m_loadedRobot;
    private GameObject m_loadedCore;

    [Header("Color")]
    private float red = 1;
    private float green = 1;
    private float blue = 1;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    private int m_currentRobot = 0;
    private int m_currentCore = 0;

    public Transform playerPreviewPosition;

    public string PlayerName { get; set; }

    public string CurrentBodyName
    {
        get { return robots[m_currentRobot].name; }
    }

    public string CurrentCoreName
    {
        get { return cores[m_currentCore].name; }
    }

    public Color Color
    {
        get { return new Color(red, green, blue); }
    }

    private void Awake()
    {
        foreach(var item in Resources.LoadAll("Robots/")) robots.Add(item as GameObject);
        foreach(var item in Resources.LoadAll("Cores/")) cores.Add(item as GameObject);

        UpdateRobot();
        UpdateCore();
        UpdateColor();

        PlayerName = "Noobie";
    }

    private void UpdateRobot()
    {
        if(m_loadedRobot != null) Destroy(m_loadedRobot);
        m_loadedRobot = Instantiate(robots[m_currentRobot], playerPreviewPosition);

        m_loadedRobot.transform.localPosition = new Vector3(0,0, -transform.position.z+1);
        robotText.text = robots[m_currentRobot].name;
    }

    private void UpdateCore()
    {
        if(m_loadedCore != null) Destroy(m_loadedCore);
        m_loadedCore = Instantiate(cores[m_currentCore], playerPreviewPosition);
        m_loadedCore.transform.localPosition = new Vector3(0, 0, -transform.position.z);
        coreText.text = cores[m_currentCore].name;
    }

    private void UpdateColor()
    {
        m_loadedCore.GetComponent<SpriteRenderer>().color = new Color(red, green, blue);
        m_loadedRobot.GetComponent<SpriteRenderer>().color = new Color(red, green, blue);
    }

    public void ChangeRed()
    {
        red = redSlider.value;
        UpdateColor();
    }

    public void ChangeGreen()
    {
        green = greenSlider.value;
        UpdateColor();
    }

    public void ChangeBlue()
    {
        blue = blueSlider.value;
        UpdateColor();
    }

    public void NextRobot()
    {
        if(++m_currentRobot >= robots.Count) m_currentRobot = 0;
        UpdateRobot();
    }

    public void NextCore()
    {
        if(++m_currentCore >= cores.Count) m_currentCore = 0;
        UpdateCore();
        UpdateColor();
    }

    public void PreviousRobot()
    {
        if(--m_currentRobot < 0) m_currentRobot = robots.Count-1;
        UpdateRobot();
    }

    public void PreviousCore()
    {
        if(--m_currentCore < 0) m_currentCore = cores.Count-1;
        UpdateCore();
        UpdateColor();
    }

    public void FinishSelection()
    {
        GameObject.Find("PartContainer").GetComponent<PartContainer>().parts = new string[] { robots[m_currentRobot].name, cores[m_currentCore].name };
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
