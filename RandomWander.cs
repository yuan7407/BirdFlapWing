using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWander : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;

    private bool isWander = false;
    private bool isRotating = false;
    private bool isFlying = false;

    void Start()
    {
        moveSpeed = 1.5f;
        rotSpeed = 20f;
    }

    void Update()
    {

        //Michael Yuan: Check if it's initialized.
        if (isWander == false) 
        {
            StartCoroutine(Wander());
        }
        //Michael Yuan: Random rotation overtime.
        transform.Rotate(transform.up * Time.deltaTime * rotSpeed * Random.Range(0f, 2f));
        transform.Rotate(transform.right * Time.deltaTime * -rotSpeed * Random.Range(0f, 1f));

        //Michael Yuan: Random postion movement overtime.
        if (isFlying == true) {
            transform.position += transform.forward * moveSpeed * Random.value * Time.deltaTime;
        }

        Debug.Log("isRotating = " + isRotating);
    }

    IEnumerator Wander() 
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int flyWait = Random.Range(1, 4);
        int flyTime = 30;

        isWander = true;

        yield return new WaitForSeconds(flyWait);
        isFlying = true;
        yield return new WaitForSeconds(flyTime);
        isFlying = false;
        yield return new WaitForSeconds(rotateWait);
        isRotating = true;
        yield return new WaitForSeconds(rotTime);
        isFlying = false;
    }
}
