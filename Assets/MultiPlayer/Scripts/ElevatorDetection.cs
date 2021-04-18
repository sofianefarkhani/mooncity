using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElevatorDetection : MonoBehaviour
{
    
    private const float DefaultTime = 10;
    private static float _timeBeforeElevate;
    private static int _numberOfPersonIn;

    private List<CharacterController> _characters;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        /********** Un simple timer qui se décrémente que s'il y a au moin une personne à l'intérieur de l'ascenseur **********/
        if(_numberOfPersonIn<=0) return;
        if (_timeBeforeElevate > 0 )
        {
            _timeBeforeElevate -= Time.smoothDeltaTime;
            Debug.Log("Timer :"+_timeBeforeElevate);
        }
        else
        {
            /********** Le timer est arriver à zéro donc on TP les personnes qui sont à l'intérieur de l'ascenseur **********/
            Debug.Log("We are ready to go !");
            _numberOfPersonIn = 0;
            _timeBeforeElevate = DefaultTime;
            foreach (var character in _characters)
            {
                var component = character.GetComponent(typeof(SwitchingRoom));
                Debug.Log(component.gameObject);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;
        _characters.Add((CharacterController) other);
        _numberOfPersonIn++;
        _timeBeforeElevate = DefaultTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;
        _numberOfPersonIn--;
        _characters.Remove((CharacterController) other);

    }
}
