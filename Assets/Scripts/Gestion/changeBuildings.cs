using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBuildings : MonoBehaviour {

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeBuildingToMediumHouse()
    {
        MySquareMap myMap = (MySquareMap)FindObjectOfType(typeof(MySquareMap));
        myMap.changeBuildingToMediumHouse();
    }

    public void changeBuildingToBasicHouse()
    {
        MySquareMap myMap = (MySquareMap)FindObjectOfType(typeof(MySquareMap));
        myMap.changeBuildingToBasicHouse();
    }

    public void changeBuildingToBigHouse()
    {
        MySquareMap myMap = (MySquareMap)FindObjectOfType(typeof(MySquareMap));
        myMap.changeBuildingToBigHouse();
    }

    public void changeBuildingToLumber()
    {
        MySquareMap myMap = (MySquareMap)FindObjectOfType(typeof(MySquareMap));
        myMap.changeBuildingToLumber();
    }

    public void changeBuildingToMiner()
    {
        MySquareMap myMap = (MySquareMap)FindObjectOfType(typeof(MySquareMap));
        myMap.changeBuildingToMiner();
    }
    public void changeBuildingToWareHouse()
    {
        MySquareMap myMap = (MySquareMap)FindObjectOfType(typeof(MySquareMap));
        myMap.changeBuildingToWareHouse();
    }
    public void changeBuildingToGoldWareHouse()
    {
        MySquareMap myMap = (MySquareMap)FindObjectOfType(typeof(MySquareMap));
        myMap.changeBuildingToGoldWareHouse();
    }
    public void changeBuildingToForge()
    {
        MySquareMap myMap = (MySquareMap)FindObjectOfType(typeof(MySquareMap));
        myMap.changeBuildingToForge();
    }
    public void changeBuildingToArmurerie()
    {
        MySquareMap myMap = (MySquareMap)FindObjectOfType(typeof(MySquareMap));
        myMap.changeBuildingToArmurerie();
    }
}
