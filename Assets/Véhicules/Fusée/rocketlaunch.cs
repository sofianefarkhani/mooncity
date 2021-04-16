using UnityEngine;

public class rocketlaunch : MonoBehaviour
{
    public float dist=6000;
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public Light l;
    public float speedMax=1000;
    public bool triggerLaunch=false;
    private const float Gamma = 0.4f;
    private float _speed = 0;
    // Start is called before the first frame update
    void Start()
    {
       
        //GameObject rocket = new GameObject();
        print("Fusée : "+gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if(triggerLaunch)
        {
            if (_speed <= speedMax)
            {
                _speed += Gamma + Time.deltaTime;
            }
           
            
            print(transform.position += new Vector3(0, Time.deltaTime * _speed, 0));
            
            if (transform.position.y >= dist )
            {
                triggerLaunch = false;
                ps1.Stop();
                ps2.Stop();
                l.intensity = 0;
                Destroy(gameObject);
                
            }
        }
    }

    public void go()
    {
        triggerLaunch = true;
        ps1.Play();
        ps2.Play();
        l.intensity = 100;

    }
}
