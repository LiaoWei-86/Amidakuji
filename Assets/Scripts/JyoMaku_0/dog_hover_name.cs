using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog_hover_name : MonoBehaviour
{
    public GameObject dog_name;
    // Start is called before the first frame update
    void Start()
    {
        dog_name.SetActive(false);
    }

    private void OnMouseEnter()
    {
        dog_name.SetActive(true);
    }

    private void OnMouseExit()
    {
        dog_name.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
