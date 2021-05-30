using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultData : MonoBehaviour
{
    public Text ftp;
    public Text weight;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("playerftp"))
        {
            ftp.text = PlayerPrefs.GetInt("playerftp") +"";
        }
        if (PlayerPrefs.HasKey("playerweight"))
        {
            weight.text = PlayerPrefs.GetInt("playerweight") + "";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
