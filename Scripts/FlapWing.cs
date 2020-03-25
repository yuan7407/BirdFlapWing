using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlapWing : MonoBehaviour
{
    private Animator anim;
    private Material eagleMat;
    private VisualEffect featherVfx;
    private VisualEffect linesVfx;

    private float activeTime;
    private float animEagleMatFade;
    private float eagleMatFadeLength = 2;
    public float defaultFeatherNum;
    private float animFeatherNum;
    private int animTextureFlipbook;
    private float light1DirX, light1DirY, light1DirZ;
    private float colorValue;
    private Color light1Color;

    void Start()
    {
        anim = GetComponent<Animator>();
        eagleMat = GameObject.Find(transform.name + "/ヌル/body").GetComponent<Renderer>().material;
        eagleMat.SetFloat("_fade", 0);
        featherVfx = GameObject.Find("FeatherVFX").GetComponent<VisualEffect>();
        linesVfx = GameObject.Find("LinesVFX").GetComponent<VisualEffect>();
        eagleMat.SetFloat("myTest", 1f);
        defaultFeatherNum = featherVfx.GetFloat("WingFeatherNum");
        light1DirX = 0f; 
        light1DirY = 0f; 
        light1DirZ = 0f;
        
    }

    //
    void Update()
    {
        FlapEffect();
    }

    public void FlapEffect()
    {
        if (Input.anyKey)//Michael: any keyboard input or mouse click can trigger the effect
        {
            activeTime += Time.deltaTime;
            animTextureFlipbook = (int)activeTime % 120;
            
            animEagleMatFade += 0.005f;
            light1DirX = Mathf.PingPong(Time.time, 2f) - 1f ;    
            light1DirY = 1f;
            light1DirZ = Mathf.PingPong(Time.time, 1f) - 0.5f;

            colorValue = Mathf.Lerp(0f, 200f, activeTime/1000);
            light1Color = new Color(colorValue, colorValue, colorValue);

            anim.SetBool("isFlap", true);

            eagleMat.SetFloat("SeqPlay", animTextureFlipbook);
            eagleMat.SetFloat("myTest", 0f);
            eagleMat.SetVector("_Light1Dir", new Vector3(light1DirX, light1DirY, light1DirZ));
            eagleMat.SetColor("_Light1Color", light1Color);

            featherVfx.SendEvent("OnPlay");
            linesVfx.SendEvent("OnPlay");

            animFeatherNum -= 10f;//the effect of feather fade out of the body.
            featherVfx.SetFloat("WingFeatherNum", animFeatherNum);

            //Debug.Log("featherNum = " + animFeatherNum);
            eagleMat.SetFloat("_fade", animEagleMatFade);
        }
        else
        {
            activeTime = 0f;
            //eagleMat.SetFloat("SeqPlay", 0);
            animEagleMatFade = 0f;
            eagleMat.SetFloat("_fade", animEagleMatFade);
            eagleMat.SetVector("_Light1Dir", new Vector3(0f, 0f, 0f));
            light1Color = new Color(0f, 0f, 0f);
            eagleMat.SetColor("_Light1Color", light1Color);

            anim.SetBool("isFlap", false);
            animFeatherNum = defaultFeatherNum; // the feather cover the body.
            featherVfx.SetFloat("WingFeatherNum", animFeatherNum);

            //Debug.Log(eagleMat.GetVector("Light1Dir"));
            featherVfx.SendEvent("OnStop");
            linesVfx.SendEvent("OnStop");


        }

    }
}