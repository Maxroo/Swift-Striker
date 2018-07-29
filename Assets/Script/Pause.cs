using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

   void Start()
    {

    }

        void Update()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();

        if (PlayerController.Pause)
        {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
        }


    }
}
