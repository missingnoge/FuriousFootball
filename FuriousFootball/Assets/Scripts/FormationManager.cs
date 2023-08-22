using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    public GameObject[] Formations;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCurrentFormIndex()
    {
        return currentIndex;
    }

    public void setFormationList(int index)
    {
        currentIndex = index;
    }

    public void advanceFormList()
    {
        if (currentIndex != Formations.Length - 1)
        {
            currentIndex++;
        }
    }
}
