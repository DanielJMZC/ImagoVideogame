using System;
using UnityEngine;

public class fxManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioClip win;
    public AudioClip lose;
    public AudioClip pageFlip;
    public AudioClip paperSlide;
    private AudioSource music;
    public AudioSource footsteps;

    public AudioClip MaxPoints;
    public AudioClip LessPoints;
    public AudioClip LesserPoints;
    public AudioClip NoPoints;

    [HideInInspector]
    public Boolean footstepsPlaying;

    void Start()
    {
        music = GetComponent<AudioSource>();
        footstepsPlaying = false;
    }

    public void winSound()
    {
        AudioSource.PlayClipAtPoint(win, Camera.main.transform.position, 0.5f);
    }

    public void maxPoints()
    {
        AudioSource.PlayClipAtPoint(MaxPoints, Camera.main.transform.position, 0.5f);
    }

    public void lessPoints()
    {
        AudioSource.PlayClipAtPoint(LessPoints, Camera.main.transform.position, 0.5f);
    }

    public void lesserPoints()
    {
        AudioSource.PlayClipAtPoint(LesserPoints, Camera.main.transform.position, 0.5f);
    }

    public void noPoints()
    {
        AudioSource.PlayClipAtPoint(NoPoints, Camera.main.transform.position, 0.5f);
    }

    public void loseSound()
    {
        AudioSource.PlayClipAtPoint(lose, Camera.main.transform.position, 0.25f);
    }

    public void pageFlipSound()
    {
        AudioSource.PlayClipAtPoint(pageFlip, Camera.main.transform.position, 0.25f);
    }

    public void paperSlideSound()
    {
        AudioSource.PlayClipAtPoint(paperSlide, Camera.main.transform.position, 0.25f);
    }

    public void PauseMusic()
    {
        if (music.isPlaying)
        {
            music.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (!music.isPlaying)
        {

            music.Play();
        }
    }
    public void StopMusic()
    {
        music.Stop();
    }

    void Update()
    {
        if(!footstepsPlaying && GameController.Instance.player.animatorController.GetBool("Moving") == true)
        {
            footsteps.Play();
            footstepsPlaying = true;
        } else if (footstepsPlaying && GameController.Instance.player.animatorController.GetBool("Moving") == false)
        {
            footsteps.Pause();
            footstepsPlaying = false;
        }
    }
    
}
