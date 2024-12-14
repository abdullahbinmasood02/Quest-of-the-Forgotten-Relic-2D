using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class desertScene : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource desertAudio;
    public Animator cameraAnimator;
    public float changeTime=31f;
    
void Start()
    {
        cameraAnimator.Play("desertCutscene");
    }

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (Mathf.Floor(changeTime) == 0)
        {

            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {

            cameraAnimator.enabled = false;
            SceneManager.LoadScene(0);
        }
    }
}
