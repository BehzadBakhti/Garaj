using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private int _number;
    
    
    private void OnEnable()
    {
        print("I am enabled");
    }

    public void TestFunction()
    {
        Debug.Log("I am testing");
        
    }
// Start is called before the first frame update
    void Start()
    {
        TestFunction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
