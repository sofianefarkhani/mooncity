using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interact : MonoBehaviour
{
    [HideInInspector]
    public Hand activehand = null;
    /*
    private void Update()
    {
        if (activehand == null)
        {
            this.GetComponent<Collider>().enabled = true;
        }
        else {
            this.GetComponent<Collider>().enabled = false;
        }
    }
    */
}