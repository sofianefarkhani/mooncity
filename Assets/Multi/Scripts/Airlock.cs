using Photon.Pun;
using UnityEngine;

public class Airlock : MonoBehaviourPun
{
    public GameObject charniere;
    int opening = 0;
    public int CurAngle;
    private int Angle = 150;
    Vector3 posInit;
    Quaternion rotInit;
    public bool contactTerrain;
    public AudioSource bruit;

    public GameObject doorOut;
    public GameObject doorIn;
    public bool doorInOpen = false;

    public Vector3 axis;

    void Start()
    {
        //axis = new Vector3(1, 0, 1);
        //charniere = GameObject.Find("ID131");
        axis = transform.up * -1.0f;
        posInit = gameObject.transform.position;
        rotInit = gameObject.transform.rotation;
    }

    [PunRPC]
    public void ChangeState() 
    {
        if (contactTerrain) //fermeture de la porte
        {
            opening = -1;
        }
        else //ouverture de la porte
        {
            opening = 1;
            bruit.Play();
            Debug.Log("test son");
        }
        
    }

    [PunRPC]
    public void Door() {
        doorInOpen = !doorInOpen;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //opening = 1;

            //PhotonView photonView = PhotonView.Get(this);
            this.photonView.RPC("ChangeState",RpcTarget.All);

            //ChangeState();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            opening = -1;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.photonView.RPC("Door", RpcTarget.All);
            Door();
        }
        if (opening == 1)
        {
            if (CurAngle < Angle && !contactTerrain && doorIn.transform.localPosition.z > 149)
            {
                CurAngle += 1;
                gameObject.transform.RotateAround(charniere.transform.position, axis, CurAngle * Time.fixedDeltaTime);
            }
            else
            {
                opening = 0;
            }
        }
        if (opening == -1)
        {
            if (CurAngle >= 0)
            {
                CurAngle -= 1;
                gameObject.transform.RotateAround(charniere.transform.position, -1 * axis, CurAngle * Time.fixedDeltaTime);
            }
            if (CurAngle <= 0)
            {
                gameObject.transform.rotation = rotInit;
                gameObject.transform.position = posInit;
                opening = 0;
            }
        }
        if (contactTerrain == true && doorOut.transform.localPosition.z > 5 && doorIn.transform.localPosition.z > 149) {
            doorOut.transform.Translate(new Vector3(0,0,-1) * Time.deltaTime);
        }
        if (contactTerrain == false && doorOut.transform.localPosition.z < 150) {
            doorOut.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        }
        if (doorInOpen == true && doorIn.transform.localPosition.z > 5 && doorOut.transform.localPosition.z > 149)
        {
            doorIn.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);
        }
        if (doorInOpen == false && doorIn.transform.localPosition.z < 150 )
        {
            doorIn.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.photonView.RPC("Contact", RpcTarget.All, true);
            //contactTerrain = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.photonView.RPC("Contact", RpcTarget.All, false);
            //contactTerrain = false;
        }
    }

    [PunRPC]
    private void Contact(bool boul)
    {
        contactTerrain = boul;
    }
}

