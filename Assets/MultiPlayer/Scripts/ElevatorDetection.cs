using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
public class ElevatorDetection : MonoBehaviourPun
{
    /*Je tiens à rappelez que les scripts avec Photon son exéctué parallélement, càd que chacun des joueur ont les même scripts.
     De ce fait, si un joueur rentre en collision avec le box collider cela va être le cas pour tout les autres joeueur même s'ils sont pas dedans.
     C'est pour cela qu'il est important de vérifier que ce soit le bon joueur qui est fait l'action.*/
    [SerializeField]
    private float defaultTime = 10; 
    private static float _timeBeforeElevate;
    private int _actorNumber;
    // Start is called before the first frame update
    private void Start()
    {
        _timeBeforeElevate = defaultTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_actorNumber<=0) return;//Continue uniqument si le personnage est à l'intérieur(càd si actorNumber !=0) 
        if (_timeBeforeElevate > 0)//On décrémente le timer juqu'à 0
        {
            _timeBeforeElevate -= Time.smoothDeltaTime;
            Debug.Log("Timer :"+_timeBeforeElevate);
        }
        else//On a atteint 0
        {
            
            Debug.Log("We are ready to go !");
            _timeBeforeElevate = defaultTime;//On reset le timer pour être réutilisé plus tard
             if(PhotonNetwork.LocalPlayer.ActorNumber.Equals(_actorNumber))//On s'assure que seulement la personne qui est à l'intérieur soit TP
            {
                SwitchingRoom.SwitchRoom(false);
            }
            _actorNumber = 0;//On réinitiliase à zéro vu qu'il a personne à l'intérieur après le TP
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;//On s'assure que OnTriggerEnter est trigger uniquement par le Collider lié au CharacterController
        _timeBeforeElevate = defaultTime; //On reset le timer pour synchroniser le changement de room/scene.
        if (other.gameObject.GetPhotonView().IsMine)//On s'assure que seule celui qui est en colision avec le box collider exécute la suite. 
            _actorNumber=other.gameObject.GetPhotonView().Owner.ActorNumber;//On enregistre l'identifiant de celui qui est entré en collision



    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;//On s'assure que OnTriggerExit est trigger uniquement par le Collider lié au CharacterController
        if (other.gameObject.GetPhotonView()
            .IsMine) //On s'assure que seule celui qui est en colision avec le box collider exécute la suite.  
            _actorNumber = 0; //On remet à zéro puisque le personnage à quitter le box collider. 

    }
    
}
