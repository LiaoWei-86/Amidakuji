using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class characterInfoHoverT5 : MonoBehaviour
{
    private GameObject character;

    private GameObject charaInfoPrefab;
    private GameObject charaInfoInstance;

    public DrawLineT5 DrawLineT5Script;

    // Start is called before the first frame update
    void Start()
    {
        if (DrawLineT5Script == null)
        {
            DrawLineT5Script = FindObjectOfType<DrawLineT5>();
        }
    }



    public void Initialize(GameObject character, GameObject charaInfoPrefab)
    {
        this.character = character;

        this.charaInfoPrefab = charaInfoPrefab;

        Debug.Log($"HoverArea initialized with");
    }

    void OnMouseEnter()
    {
        if (charaInfoPrefab != null)
        {
            charaInfoInstance = Instantiate(charaInfoPrefab, transform.position, Quaternion.identity);
            Debug.Log($"Tooltip instantiated at {transform.position}");
        }
    }


    void OnMouseExit()
    {
        if (charaInfoInstance != null)
        {
            Destroy(charaInfoInstance);
            Debug.Log("Tooltip destroyed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
