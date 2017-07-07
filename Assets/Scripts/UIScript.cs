using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public int level = 1;
    public InputField input;
    
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void start()
    {
        Application.LoadLevel(1);
    }

    public void addLevel()
    {
        if(level < 10)
            level++;
        input.text = level.ToString();
    }

    public void subLevel()
    {
        if (level > 1)
            level--;
        input.text = level.ToString();
    }
}
