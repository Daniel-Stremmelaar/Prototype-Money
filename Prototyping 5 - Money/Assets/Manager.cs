using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int bank;
    public int addmoney;
    public float moneyTimer;
    public bool loopTimer;

	void Start ()
    {
        loopTimer = true;
	}
	
	void Update ()
    {
        if (loopTimer == true)
        {
            StartCoroutine(AddmoneyTimer());
            loopTimer = false;
        }
	}

    IEnumerator AddmoneyTimer()
    {
        yield return new WaitForSeconds(moneyTimer);
        bank += addmoney;
        Debug.Log("hoi");
        loopTimer = true;
    }

}
