using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    public float changeTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changeTime-=Time.deltaTime;
    
        if(Mathf.Floor(changeTime) == 0){

            SceneManager.LoadScene(2);
        }
    }
}
