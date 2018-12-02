using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour {

    Transform prisoner;

    [SerializeField]
    TrapType trapType;

    enum TrapType
    {
        CAGE
    }

    void Start()
    {
        prisoner = GetComponent<Transform>();
    }
   

    public void kill()
    {
        if (trapType == TrapType.CAGE)
        {
            StartCoroutine(collapseCage());
        }
    }

    IEnumerator collapseCage()
    {
        Vector3 scale = prisoner.localScale;

        while (prisoner.localScale.y > 0.05f)
        {
            scale.y -= 0.5f * Time.deltaTime;
            prisoner.localScale = scale;

            yield return new WaitForEndOfFrame();
        }
    }
}
