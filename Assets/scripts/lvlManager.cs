using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlManager : MonoBehaviour
{ 
   // public GameObject Settings;
    // Start is called before the first frame update
    public Animator level1ButtonAnimator;
    public Animator level2ButtonAnimator;
    public Animator level3ButtonAnimator;
    public Animator level4ButtonAnimator;
    public Animator level5ButtonAnimator;
    void Start()
    { 
       // Settings.SetActive(false);
        if(PlayerPrefs.GetInt("level",0)==4){

            EnableButtonAnimation(level5ButtonAnimator);
        }

       else if (PlayerPrefs.GetInt("level", 0) == 3)
        {

            EnableButtonAnimation(level4ButtonAnimator);
        }

        else if (PlayerPrefs.GetInt("level", 0) == 2)
        {

            EnableButtonAnimation(level3ButtonAnimator);

        }

        else if (PlayerPrefs.GetInt("level", 0) == 1)
        {

            EnableButtonAnimation(level2ButtonAnimator);
        }

        else if (PlayerPrefs.GetInt("level", 0) ==0)
        {

            EnableButtonAnimation(level1ButtonAnimator);
        }

    }

    private void EnableButtonAnimation(Animator buttonAnimator)
    {
        DisableAllButtonAnimators(); 
        buttonAnimator.enabled = true; 
    }

    private void DisableAllButtonAnimators()
    {
        level1ButtonAnimator.enabled = false;
        level2ButtonAnimator.enabled = false;
        level3ButtonAnimator.enabled = false;
        level4ButtonAnimator.enabled = false;
        level5ButtonAnimator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("level",22));
    }

    

    public void level1()
    {
        
        SceneManager.LoadScene("forest");
       

    }

    public void level2(){


        
        if(PlayerPrefs.GetInt("level", 0)>=1){
            SceneManager.LoadScene(3);
         
        }
    }

    public void level3(){

        if (PlayerPrefs.GetInt("level", 0) >= 2){
            SceneManager.LoadScene("Level3");
        
        
        }
    }

    public void level4(){
        if (PlayerPrefs.GetInt("level", 0) >= 3){
            SceneManager.LoadScene(8);
          
            }
            
    }

    public void level5(){
        if (PlayerPrefs.GetInt("level", 0) >= 4){
            SceneManager.LoadScene(4);
       
        
        }
    }

   
   /* public void settingsbtnClicked(){
      
        Settings.SetActive(true);
        
    }*/
   
}
