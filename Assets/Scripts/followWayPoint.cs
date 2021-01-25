using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followWayPoint : MonoBehaviour
{
    public GameObject wayPoint;
    public Vector3 coordBase;
    public BoxCollider col;
    private Vector3 wayPointPos;
    private float speed = 2.0f;
    private float dist;
    public Rigidbody mob;
    private float timer;

    //variation du colldier utile ?
    void Start()
    {
        coordBase = transform.position;
        this.GetComponent<Animator>().enabled = false;
        col.center = new Vector3(0f, 0.05f, 0f);
        col.size = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void lookTarget(Vector3 coord)
    {
        Vector3 lookVector = coord - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }

    void Update()
    {
        //I'm coming0
        if (CameraPos.isGrounded == true || VRController.isGrounded == true)
        {
            timer = 5;
            if (this.GetComponent<Animator>().enabled == false)
            {
                this.GetComponent<Animator>().enabled = true;
            }
            mob.constraints = RigidbodyConstraints.None;
            wayPointPos = new Vector3(wayPoint.transform.position.x, transform.position.y, wayPoint.transform.position.z);
            dist = Vector3.Distance(wayPointPos, transform.position);

            //Is coming
            if (dist > 5)
            {
                this.GetComponent<Animator>().SetBool("Open_Anim", true);
                if ((dist > 20 && this.GetComponent<Animator>().GetBool("Roll_Anim") == false) || (dist > 6 && this.GetComponent<Animator>().GetBool("Roll_Anim") == true))
                {
                    this.GetComponent<Animator>().SetBool("Walk_Anim", false);
                    this.GetComponent<Animator>().SetBool("Roll_Anim", true);
                    speed = 6.0f;
                }
                else
                {
                    this.GetComponent<Animator>().SetBool("Roll_Anim", false);
                    this.GetComponent<Animator>().SetBool("Walk_Anim", true);
                    speed = 2.0f;
                }
                if (!this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("anim_open") && !this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("anim_close"))
                {
                    transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
                }
            }
            //Wait
            else
            {
                mob.constraints = RigidbodyConstraints.FreezeRotation;
                mob.constraints = RigidbodyConstraints.FreezePositionX;
                mob.constraints = RigidbodyConstraints.FreezePositionZ;
                //mob.constraints = RigidbodyConstraints.FreezeAll;
                this.GetComponent<Animator>().SetBool("Roll_Anim", false);
                this.GetComponent<Animator>().SetBool("Walk_Anim", false);
            }

            lookTarget(wayPoint.transform.position);
        }
        //Come back
        else if ((CameraPos.isGrounded == false || VRController.isGrounded == false) && timer <= 0)
        {
            //Come back to base
            if (Vector3.Distance(transform.position, coordBase) > 0.2)
            {
                speed = 2.0f;
                mob.constraints = RigidbodyConstraints.None;
                transform.position = Vector3.MoveTowards(transform.position, coordBase, speed * Time.deltaTime);
                lookTarget(coordBase);
                this.GetComponent<Animator>().SetBool("Walk_Anim", true);
            }
            //Stay base
            else
            {
                mob.constraints = RigidbodyConstraints.FreezeAll;
                this.GetComponent<Animator>().SetBool("Walk_Anim", false);
                this.GetComponent<Animator>().SetBool("Open_Anim", false);
            }
        }
        //Is he moving again ?
        else
        {
            mob.constraints = RigidbodyConstraints.FreezeRotation;
            mob.constraints = RigidbodyConstraints.FreezePositionX;
            mob.constraints = RigidbodyConstraints.FreezePositionZ;
            //mob.constraints = RigidbodyConstraints.FreezeAll;
            this.GetComponent<Animator>().SetBool("Roll_Anim", false);
            this.GetComponent<Animator>().SetBool("Walk_Anim", false);
            if (this.GetComponent<Animator>().enabled == true)
            {
                lookTarget(wayPoint.transform.position);
            }
            timer -= Time.deltaTime;
            //Debug.Log(timer);
        }
        //Collider adaption
        if (Vector3.Distance(transform.position, coordBase) > 0.3)
        {
            col.center = new Vector3(0f, 0.06f, 0f);
        }
        else
        {
            col.center = new Vector3(0f, 0.05f, 0f);
        }

    }
}