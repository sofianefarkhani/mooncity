using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class chemin : MonoBehaviour
{
    public List<Vector3> checkPoint = new List<Vector3>();
    public List<float> tempsPause = new List<float>(); //ça doit avoir la même taille que la list checkPoint
    private int next = 0;
    //public ParticleSystem parti;//ça marche pas mais au pire pas besoin
    //private List<ParticleSystem> partis = new List<ParticleSystem>();//ça marche pas mais au pire pas besoin

    public void setPoint(Vector3 p)
    {
        Vector3 pt = new Vector3(p.x, p.y, p.z);
        Debug.Log(pt);
        checkPoint.Add(pt);
        //partis.Add(new ParticleSystem()); //particules pour marquer les points//ça marche pas mais au pire pas besoin
        //partis[partis.Count-1].transform.position = checkPoint[checkPoint.Count-1];//ça marche pas mais au pire pas besoin
        tempsPause.Add(0.0f);
    }

    public float getTempsPause()
    {
        return tempsPause[next];
    }

    public Vector3 getNextPoint()
    {
        Vector3 point = new Vector3(checkPoint[next].x, checkPoint[next].y, checkPoint[next].z);
        //Debug.Log("next avent modulo:"+next);
        next++;
        if(next >= checkPoint.Count)
        {
            next = 0;
        }
        //Debug.Log("next après modulo:" + next);
        return point;
    }

    public Vector3 getNearPoint(Vector3 p)
    {
        Vector3 point = new Vector3();
        point = checkPoint[0]; //le vector qu'on vas renvoyer aka le plus proche de p
        int compteur = 0;
        int plusProche = 0; //numéro du point le plus proche
        foreach(Vector3 check in checkPoint) //au final on aurais pu fair un for normal
        {
            double d = Math.Sqrt(Math.Pow(check.x - p.x, 2) + Math.Pow(check.z - p.z, 2)); //distance entre le robot et le point suivent dans le chemin
            double D = Math.Sqrt(Math.Pow(point.x - p.x, 2) + Math.Pow(point.z - p.z, 2)); //distance entre le robot et le point le plus proche acctuel
            if ( d < D )
            {
                point.x = check.x;
                point.y = check.y;
                point.z = check.z;
                plusProche = compteur;
            }
            compteur++;
        }

        next = (plusProche++) % checkPoint.Count;
        return point;
    }


}
