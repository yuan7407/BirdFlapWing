using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{

    private Material eagleMat;
    private Color bodyColor;
    private float light1DirX, light1DirY, light1DirZ;
    private float randomColorValueR, randomColorValueG, randomColorValueB;
    private float activeTime;
    private float changeTimeLength;

    void Start()
    {
        eagleMat = this.gameObject.GetComponent<Renderer>().material;

        activeTime = 0f;
        changeTimeLength = 2f;

        randomColorValueR = Random.Range(0f, 2f);
        randomColorValueG = Random.Range(0f, 2f);
        randomColorValueB = Random.Range(0f, 2f);

        //Michael Yuan: Give the flat color with some shadows.
        light1DirX = -0.8f;
        light1DirY = 0.3f;
        light1DirZ = -0.3f;
    }

    void Update()
    {
        ColorChange();
    }

    public void ColorChange() 
    {
        activeTime += Time.deltaTime;

        Color c = eagleMat.GetColor("_bodyColor");
        if (activeTime < changeTimeLength)
        {
            //Michael Yuan: Core function for changing random color overtime.
            bodyColor = Color.Lerp(c, new Color(randomColorValueR, randomColorValueG, randomColorValueB), activeTime);
        }
        else if (activeTime >= changeTimeLength) 
        {
            activeTime = 0;
            randomColorValueR = Random.Range(0f, 2f);
            randomColorValueG = Random.Range(0f, 2f);
            randomColorValueB = Random.Range(0f, 2f);
        }

        eagleMat.SetVector("_Light1Dir", new Vector3(light1DirX, light1DirY, light1DirZ));
        eagleMat.SetColor("_bodyColor", bodyColor); //Michael Yuan: Give the value to the Shader Graph.
    }
}
