using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;



[RequireComponent(typeof(NavMeshAgent))]
public class deplacementIA : MonoBehaviour
{
    public NavMeshAgent navig; 
    public Transform obj_cible; //objet que doit suivre le robot
    //public Rigidbody corps; //ça sert à rien
    private bool suivre; //true: suie l'objet cible en permanance, false: s'arrete
    public chemin circuit;
    private bool tourStarted = false;
    private Vector3 objectif;
    private float tempsPause = 0;
    private Animator anim;
    private float velovitesse; //ça servira à controller la transition entre la marche et l'idle
    private bool travail = false; //servira à déclancher une animation de travail
    private float vitesseDeBase; //la vitesse de la nav mesh défini sur unity
    private int selectRAND; //détermine quel animation jouer (gauche ou droite ou pinotage)
    private System.Random rand;




    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        navig = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //circuit = GetComponent<chemin>();//peut être pour plus tard
        suivre = false;
        vitesseDeBase = navig.speed;

    }

    // Update is called once per frame
    void Update()
    {
        velovitesse = (float)(Math.Pow(navig.velocity.x, 2) + Math.Pow(navig.velocity.z, 2));
        //Debug.Log("velocity:" + velovitesse);//debug
        anim.SetFloat("vitesse", velovitesse); //ça servira à controller la transition entre la marche et l'idle

        if(Input.GetKeyDown("p")) //mode suivre objet désactivé
        {
            suivre = false;
            Debug.Log("désactivé");
            //on arrete le robot
            navig.isStopped = true;
            navig.isStopped = false;
            navig.speed = 0;
        }
        if(Input.GetKeyDown("o")) //mode suivre objet activé
        {
            suivre = true;
            Debug.Log("activé");
        }
        if (suivre == true)
        {
            navig.speed = vitesseDeBase;
            bouge(obj_cible.position);
        }


        if (Input.GetKeyDown("m")) //aller à la position de l'objet
        {
            Debug.Log("va à: "+ obj_cible.position);
            navig.speed = vitesseDeBase;
            bouge(obj_cible.position);
            navig.isStopped = false;

        }

        //if(tourStarted || (Input.GetKeyDown(KeyCode.K) && !suivre)) //activer le mode chemin
        if(true)
        {
            
            navig.speed = vitesseDeBase;
            navig.isStopped = false;

            if (!tourStarted)
            {
                //Debug.Log("chemin activé");
                //Debug.Log("test 1");//debug
                tourStarted = true;
                objectif = circuit.getNearPoint(navig.transform.position);
                tempsPause = circuit.getTempsPause();
                bouge(objectif);
            }
            else
            {
                if(navig.transform.position.x == objectif.x && navig.transform.position.z == objectif.z) //arrivé au point cible
                {
                    //Debug.Log("temps de pause:" + tempsPause);
                    if (tempsPause <= 0) //pas en pause
                    {
                        anim.SetBool("travailOrdi", false);
                        //Debug.Log("test 2");//debug
                        //Debug.Log(circuit.getNextPoint());//debug
                        objectif = circuit.getNextPoint();
                        tempsPause = circuit.getTempsPause();
                        bouge(objectif);
                    }
                    else //en pause
                    {
                        tempsPause -= Time.deltaTime;
                        anim.SetBool("travailOrdi", true); //travail ordi
                        if(tempsPause%4 < 0.2) //il va régulièrement tirer un int entre [-1;1] pour savoir si il doit select à droite ou à gauche
                        {
                            anim.SetInteger("selectRAND", rand.Next(-1, 2));
                        }
                        else
                        {
                            anim.SetInteger("selectRAND", 0);
                        }
                    }
                }
            }

        }
        if(Input.GetKeyDown(KeyCode.L)) //désactiver le mode chemin
        {
            Debug.Log("chemin désactivé");
            tourStarted = false;
            //on arrete le robot
            navig.isStopped = true;
            navig.isStopped = false;
            navig.speed = 0;
        }
        

        
    }





    public void bouge (Vector3 cible)
    {
        navig.SetDestination(cible); //prand un vector3 en paramettre 
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //Vector3 stop = transform.position;
        //navig.SetDestination(stop);
        if(collision.collider.name != "Moon")
        {
            //on arrete le robot
            //Debug.Log(collision.collider.name);
            navig.isStopped = true;
            navig.isStopped = false;
            navig.speed = 0;
            suivre = false;
        }
        
        
    }


}
