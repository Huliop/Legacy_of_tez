using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class All3DObjects : MonoBehaviour {
    [Header("Liste des objects 3D utilisés dans le jeu")]
    [SerializeField]
    protected StringObject all3DObjects;

    [SerializeField]
    protected GameObject currentObject=null;
    [SerializeField]
    protected GameObject nextObject=null;
    [SerializeField]
    protected GameObject thirdObject = null;
    [SerializeField]
    protected GameObject forthObject = null;
    [SerializeField]
    protected GameObject fifthObject = null;
    [SerializeField]
    protected GameObject sixthObject = null;
    [SerializeField]
    protected GameObject seventhObject = null;
    [SerializeField]
    protected GameObject heightveObject = null;
    [SerializeField]
    protected GameObject ninthObject = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public StringObject getAll3DObjects()
    {
        return all3DObjects;
    }

    public GameObject getCurrentObject()
    {
        return currentObject;
    }

    public GameObject getNextObject()
    {
        return nextObject;
    }

    public GameObject getThirdObject()
    {
        return thirdObject;
    }

    public GameObject getForthObject()
    {
        return forthObject;
    }
    public GameObject getFifthObject()
    {
        return fifthObject;
    }
    public GameObject getSixthObject()
    {
        return sixthObject;
    }
    public GameObject getSeventhObject()
    {
        return seventhObject;
    }
    public GameObject getHeightveObject()
    {
        return heightveObject;
    }
    public GameObject getNinthObject()
    {
        return ninthObject;
    }
}
