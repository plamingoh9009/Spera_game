using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    bool turnLeft;
    bool turnRight;
    bool run;
    public Vector3 turnL = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("turnTime");
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            transform.Translate(Vector3.forward * 20 * Time.deltaTime);
        }
        if (turnLeft)
        {
            transform.Rotate(Vector3.up * 60 * Time.deltaTime);
        }
        if(turnRight)
        {
            transform.Rotate(Vector3.down * 60 * Time.deltaTime);
        }
    }

    IEnumerator turnTime()
    {
        run = true;
        turnLeft = true;
        turnRight = false;
        yield return new WaitForSeconds(0.8f);
        turnLeft = true;
        turnRight = true;
        yield return new WaitForSeconds(0.5f);
        turnRight = true;
        turnLeft = false;
        yield return new WaitForSeconds(1.3f);
        turnLeft = false;
        turnRight = false;
        yield return new WaitForSeconds(1f);
        run = false;
    }
}
