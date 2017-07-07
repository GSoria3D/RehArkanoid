using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateBlocks : MonoBehaviour {

    public List<Material> colours;
    public GameObject block;

    public int level;
    int counter = 0;

	// Use this for initialization
	void Start () {
        this.level = GameObject.FindGameObjectWithTag("Configuration").GetComponent<UIScript>().level;
        createBlocks();
	}
	
	// Update is called once per frame
	void Update () {
        if (counter == 0)
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerScript>().win();
    }

    public void createBlocks()
    {
        GameObject b;

        for (int j = 0; j < level; j++)
            for (int i = 0; i < 10; i++)
            {
                //Generamos los bloques y lo pintamos de colores distintos
                b = (GameObject)Instantiate(block, new Vector3(-9 + 2 * i, 1, j + 10), new Quaternion());
                b.GetComponent<Renderer>().material.color = colours[counter % colours.Count].color;
                counter++;
            }
    }
}
