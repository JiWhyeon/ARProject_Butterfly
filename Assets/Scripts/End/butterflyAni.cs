using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butterflyAni : MonoBehaviour
{
    Animation playerAni;

    // Start is called before the first frame update
    void Start()
    {
        playerAni = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAni.Play("Butterfly_Flying");
    }
}
