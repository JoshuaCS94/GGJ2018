using UnityEngine;

public class PartContainer : MonoBehaviour {

    public string[] parts;
    public string playerName;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }    
   
}
