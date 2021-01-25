using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terre_explosion : MonoBehaviour
{
    public ParticleSystem feu; //durée total: 15s
    //public ParticleSystem expP;
    public ParticleSystem expG; //durée total: 1.5s
    public ParticleSystem expA; //durée total: 5s
    public ParticleSystem expA2; //durée total: 5s
    public ParticleSystem expL; //durée total: 30s
    public bool explodeButton;
    private float explodeTime;
    public MeshRenderer rend;
    private GameObject terre;

    public AudioSource sound;
    public bool enableSound;

    private float startFeu = 0;
    private float startG = 8;
    private float startA = 8.2f;
    private float startL = 8f;

    private float anneauTiltX;
    private float anneauTiltY;

    // Start is called before the first frame update
    void Start()
    {
        terre = GameObject.Find("Terre");
        explodeTime = 0;


        anneauTiltX = Random.Range(0, 360);
        anneauTiltY = Random.Range(0, 360);
        var shape = expL.shape;
        shape.rotation = new Vector3(anneauTiltX, anneauTiltY, 0); //ça marche pas
        //ça marche tellement pas que j'laisse le code là et ça change rien
    }

    // Update is called once per frame
    void Update()
    {
        if(explodeButton) //condition à remplacer par le trigger du bouton
        {
            if (explodeButton)
            {
                explodeTime += Time.deltaTime;
            }
            if (explodeTime > startG - 1 && enableSound) //début du bruit
            {
                sound.Play();
                enableSound = false;
            }
            if (explodeTime > startFeu) //début du feu
            {
                feu.Play();
            }

            if (explodeTime > startA) //début de l'anneau
            {
                expA.Play();
                expA2.Play();
            }
            if (explodeTime > startG) //début de la grosse explosion et stop du feu
            {
                expG.Play();
                rend.enabled = false;
                feu.Stop();
            }
            if (explodeTime > startG + 1.5) //stop de la grosse explosion
            {
                expG.Stop();
            }
            if (explodeTime > startA + 4.9) //stop de l'anneau
            {
                expA.Stop();
                expA2.Stop();
            }
            if (explodeTime > startL) //début de l'explosion résiduel
            {
                expL.Play();
            }
            if (explodeTime > startL + 29.9) //fin de l'explosion résiduel
            {
                expL.Stop();
            }

            if (explodeTime > 120) //fin de l'explosion
            {
                Destroy(terre); //suppretion de la terre
            }
        }

        
    }

    public void explosion()
    {
        explodeButton = true;

        
    }
}
