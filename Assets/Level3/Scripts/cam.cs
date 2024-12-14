using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{

    private GameObject character;
    public float xMinimum;
    public float xMaximum;
    public float yMinimum;
    public float yMaximum;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Mathf.Clamp(character.transform.position.x, xMinimum, xMaximum);
        float y = Mathf.Clamp(character.transform.position.y, yMinimum, yMaximum);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}