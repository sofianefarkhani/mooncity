using UnityEngine;
public class ElevatorDetection : MonoBehaviour
{
    private const float DefaultTime = 10; 
    private static float _timeBeforeElevate;
    private static int _numberOfPersonIn;
    private bool _isInOrOut;//True = il est à l'intérieur de l'ascenseur, False = il est à l'extérieur de l'ascenseur(par défaut)
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        /**********Un simple timer qui se décrémente que s'il est à l'intérieur et qu'il est au moins une personne dans l'ascenseur**********/
        if (!_isInOrOut || _numberOfPersonIn <= 0) return;
        if (_timeBeforeElevate > 0)
        {
            _timeBeforeElevate -= Time.smoothDeltaTime;
            Debug.Log("Timer :"+_timeBeforeElevate);
        }
        else
        {
            /**********Le timer est arriver à zéro donc on TP les personnes qui sont à l'intérieur de l'ascenseur**********/
            Debug.Log("We are ready to go !");
            _isInOrOut = false;
            _timeBeforeElevate = DefaultTime;
            SwitchingRoom.SwitchRoom();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isInOrOut) return;//Si il est déjà à l'intérieur, on ne fait rien.
        /***********S'il est pas à l'intérieur de base et qu'il entre dans l'ascenseur************/
        _timeBeforeElevate = DefaultTime;//On reset le timer pour que tout le monde soit synchroniser pour la téléportation.
        _isInOrOut = true;//On indique qu'il est à l'intérieur de l'ascenseur.
        _numberOfPersonIn++;
        Debug.Log("A person has entered the elevator ! Number of person inside :"+_numberOfPersonIn);

    }

    private void OnTriggerExit(Collider other)
    {
        if(!_isInOrOut)return;//Si il est déjà à l'extérieur de l'ascenseur, on fait rien.
        /**********S'il est pas à l'extérieur(càd qu'il est à l'intérieur de l'ascenseur) et qu'il sort de l'ascenseur**********/
        _isInOrOut = false;//On indique qu'il est à l'extérieur;(càd qu'il est sortie de l'ascenseur)
        if (_numberOfPersonIn < 0) return;
        _numberOfPersonIn--;//On mets à jour le nombre de personne à l'intérieur de l'ascenseur
        Debug.Log("One person has left the elevator ! Number of person left inside : "+_numberOfPersonIn);

    }
}
