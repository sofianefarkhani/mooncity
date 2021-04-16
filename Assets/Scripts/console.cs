using UnityEngine;
using UnityEngine.Video;

public class console : MonoBehaviour
{
    public GameObject robot;
    public GameObject terminal;
    public VideoPlayer videoPlayer;
    public VideoClip video1;
    public VideoClip video2;
    public VideoClip video3;
    public const float delai_video1 = 1;
    public const float delai_video2 = 2;
    public const float delai_video3 = 3;
    public int rayon = 1;
    void Start()
    {

    }

    void Update()
    {
        float diffx = terminal.transform.position.x - robot.transform.position.x;
        float diffy = terminal.transform.position.y - robot.transform.position.y;
        float diffz = terminal.transform.position.z - robot.transform.position.z;
        if (diffx < rayon && diffx > -rayon && diffy < rayon && diffy > -rayon && diffz < rayon && diffz > -rayon)
        {
            float time1 = Time.time;
            if (Time.time == time1 + delai_video1)
            {
                videoPlayer.clip = video1;
                videoPlayer.playbackSpeed = 1;
            }
            else
            {
                if (Time.time == time1 + delai_video2)
                {
                    videoPlayer.clip = video2;
                    videoPlayer.playbackSpeed = 1;
                }
                else
                {
                    if (Time.time == time1 + delai_video3)
                    {
                        videoPlayer.clip = video3;
                        videoPlayer.playbackSpeed = 1;
                    }
                }
            }
        }
    }
}



