using UnityEngine;

public class muzikDetector : MonoBehaviour
{
    public ParticleSystem particl1;
    public ParticleSystem particl2;
    public Light lumiere;
    private AudioSource son;

    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        son = GetComponent<AudioSource>();
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    public void onOff()
    {
        if(isPlaying)
        {
            son.Stop();
            particl1.Stop();
            particl2.Stop();
            lumiere.color = Color.red;
        }
        else
        {
            son.Play();
            particl1.Play();
            particl2.Play();
            lumiere.color = Color.green;
        }
        isPlaying = !isPlaying;
    }
}
