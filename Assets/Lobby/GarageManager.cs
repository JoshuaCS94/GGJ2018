using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{
    [Header("References")]
    public Text robotText;
    public Text coreText;
    
    public List<GameObject> robots;
    public List<GameObject> cores;    

    GameObject loadedRobot;
    GameObject loadedCore;

    [Header("Color")] 
    private float red = 1;
    private float green = 1;
    private float blue = 1;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    
    int currentRobot = 0;
    int currentCore = 0;

    void Awake()
    {
        foreach(var item in Resources.LoadAll("Robots/")) robots.Add(item as GameObject);
        foreach(var item in Resources.LoadAll("Cores/")) cores.Add(item as GameObject);
        UpdateRobot();
        UpdateCore();
        UpdateColor();
    }

    void UpdateRobot()
    {
        if(loadedRobot != null) Destroy(loadedRobot);
        loadedRobot = Instantiate(robots[currentRobot], transform);
        
        loadedRobot.transform.localPosition = new Vector3(0,0, -transform.position.z+1);
        robotText.text = robots[currentRobot].name;
    }

    void UpdateCore()
    {
        if(loadedCore != null) Destroy(loadedCore);
        loadedCore = Instantiate(cores[currentCore], transform);
        loadedCore.transform.localPosition = new Vector3(0, 0, -transform.position.z);
        coreText.text = cores[currentCore].name;
    }

    private void UpdateColor()
    {
        loadedCore.GetComponent<SpriteRenderer>().color = new Color(red, green, blue);
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
        if(++currentRobot >= robots.Count) currentRobot = 0;
        UpdateRobot();
    }

    public void NextCore()
    {
        if(++currentCore >= cores.Count) currentCore = 0;
        UpdateCore();
        UpdateColor();
    }


    public void PreviousRobot()
    {
        if(--currentRobot < 0) currentRobot = robots.Count-1;
        UpdateRobot();
    }

    public void PreviousCore()
    {
        if(--currentCore < 0) currentCore = cores.Count-1;
        UpdateCore();
        UpdateColor();
    }

    
    public void FinishSelection()
    {
        GameObject.Find("PartContainer").GetComponent<PartContainer>().parts = new string[] { robots[currentRobot].name, cores[currentCore].name };
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
