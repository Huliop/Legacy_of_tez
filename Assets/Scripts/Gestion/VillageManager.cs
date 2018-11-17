using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageManager : MonoBehaviour {


    protected int wood;
    protected int stone;
    protected int people;
    protected int lumber = 0;
    protected int miner = 0;

    private int countWood = 0;
    private int countStone = 0;

    // Use this for initialization
    void Start () {
        people = 8;
    }
	
	// Update is called once per frame
	void Update () {


        if (countWood > 250)
        {
            setWood(getWood() + 1 * lumber);
            countWood = 0;
        }

        if (countStone > 350)
        {
            setStone(getStone() + 1 * miner);
            countStone = 0;
        }
        countWood++;
        countStone++;
    }


    //---------------//
    //    SETTERS    //
    //---------------//
    public void setWood(int wd)
    {
        if (wd >= 0) wood = wd;
    }
    public void setStone(int st)
    {
        if (st >= 0) stone = st;
    }
    public void setPeople(int pl)
    {
        if (pl >= 0) people = pl;
    }
    public void setLumber(int lb)
    {
        if (lb >= 0) lumber = lb;
    }
    public void setMiner(int mn)
    {
        if (mn >= 0) miner = mn;
    }


    //---------------//
    //    GETTERS    //
    //---------------//

    public int getWood()
    {
        return wood;
    }
    public int getStone()
    {
        return stone;
    }
    public int getPeople()
    {
        return people;
    }
    public int getLumber()
    {
        return lumber;
    }
    public int getMiner()
    {
        return miner;
    }

}
