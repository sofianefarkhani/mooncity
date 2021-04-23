using UnityEngine;

public class TriggerLightAuto : MonoBehaviour
{
    public Light L;
    //public Camera camera;
    //public GameObject interrupteur;
    public GameObject collider;
    int rayon = 10;
    void Start()
    {
        L.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*float diffx = interrupteur.transform.position.x - camera.transform.position.x;
        float diffy = interrupteur.transform.position.y - camera.transform.position.y;
        float diffz = interrupteur.transform.position.z - camera.transform.position.z;
        if (Input.GetMouseButtonDown(0) == true && diffx < rayon && diffx > -rayon && diffy < rayon && diffy > -rayon && diffz < rayon && diffz > -rayon)
        {
            L.enabled = !L.enabled;
        }*/
    }
    private void OnTriggerEnter(Collider collider)
    {
        L.enabled = true;
    }
    private void OnTriggerExit(Collider collider)
    {
        L.enabled = false;
    }
}
