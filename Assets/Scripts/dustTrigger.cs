using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dustTrigger : MonoBehaviour
{
    public GameObject dust;
    // Start is called before the first frame update
    void Start()
    {
        dust.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (VRController.isGrounded == true) {
            dust.SetActive(true);
        }
        else if (CameraPos.isGrounded == true)
        {
            dust.SetActive(true);
        }
        else
        {
            dust.SetActive(false);
        }
    }
}
