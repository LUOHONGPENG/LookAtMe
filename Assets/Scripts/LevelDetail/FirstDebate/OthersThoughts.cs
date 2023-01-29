using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersThoughts : MonoBehaviour
{
    public Texture[] Images;
    public GameObject others1;
    public GameObject others2;
    public GameObject others3;

    void Start()
    {
        //check which decision player drag
        others1.SetActive(false);
        others2.SetActive(false);
        others3.SetActive(false);
    }//start

    // NPC's will use 
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            others1.SetActive(true);
            others2.SetActive(true);
            others3.SetActive(true);
        }//if

    }//update
    
}//class
