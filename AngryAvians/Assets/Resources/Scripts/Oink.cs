using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oink : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    private void OnDestroy()
    {
        Debug.Log("rip oinker");
        GameCoordinator.Instance.RemoveFromList(this); // die = remove
    }
}
