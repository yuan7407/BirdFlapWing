using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureControl : MonoBehaviour
{
    public float Eagle_sh_Amount;
    MeshRenderer Eagle_TextureShader;

    void Start()
    {
        Eagle_TextureShader = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Eagle_sh_Amount = Mathf.Lerp(Eagle_sh_Amount,0,Time.deltaTime);
        //Eagle_TextureShader.material.SetFloat("MouseClick", Eagle_sh_Amount);

        if (Input.GetMouseButtonDown(0))
        {
            Eagle_sh_Amount += 1f;

        }
    }
}
