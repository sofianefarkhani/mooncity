using UnityEngine;

public class startDoor : MonoBehaviour
{

    Animator oc;

    // Start is called before the first frame update
    void Start()
    {
        oc = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oc.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
        {
            if (Input.GetKeyDown("p"))
            {
                oc.SetTrigger("starter");
            }
           
        }
    }
}
