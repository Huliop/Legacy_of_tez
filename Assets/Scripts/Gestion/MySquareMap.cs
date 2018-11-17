
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MapNavKit;
using UnityEngine.UI;

/// <summary> Used in Sample 2 </summary>
public class MySquareMap : MapNavSquare
{
	public GameObject tileFab;
	public GameObject pathMarkerFab;	// used when marking out calculated path

	// ------------------------------------------------------------------------------------------------------------

	private MapNavNode selectedNode = null;		// keeps track of the selected node
	private MapNavNode altSelectedNode = null;	// keeps track of the second selected node
	private List<GameObject> markedObjects = new List<GameObject>();	// keeps track of nodes that where "marked"
	private List<MapNavNode> invalidNodes = new List<MapNavNode>();		// helper that tracks the nodes marked as "invalid" at runtime
    private List<MapNavNode> alreadyOccupedCells = new List<MapNavNode>();

    protected Camera _camera;

    public VillageManager _myVillage;
    public PlayerController player;
    public GameObject messagePanel;
    public GameObject messageDetailedPanel;

    protected GameObject currentObject = null;
    // ------------------------------------------------------------------------------------------------------------

    protected void Start()
	{
        //FocusCamera();
        _camera = GetComponent<Camera>();
        changeBuildingToBasicHouse();
    }

	/// <summary>
	/// I override this callback so that I can respond on the grid being
	/// changed and place/ update the actual tile objects
	/// </summary>
	public override void OnGridChanged(bool created)
	{
		// The parent object that will hold all the instantiated tile objects
		Transform parent = gameObject.transform;

		// Remove existing tiles and place new ones if map was (re)created
		// since the number of tiles might be different now
		if (created)
		{
			selectedNode = null;
			altSelectedNode = null;
			invalidNodes.Clear();

			for (int i = parent.childCount - 1; i >= 0; i--)
			{
				if (Application.isPlaying)
				{	// was called at runtime
					Object.Destroy(parent.GetChild(i).gameObject);
				}
				else
				{	// was called from editor
					Object.DestroyImmediate(parent.GetChild(i).gameObject);
				}
			}

			// Place tiles according to the generated grid
			for (int idx = 0; idx < grid.Length; idx++)
			{
				// make sure it is a valid node before placing tile here
				if (false == grid[idx].isValid) continue;

				// create a new tile
				GameObject go = (GameObject)Instantiate(tileFab);
				go.name = "Tile " + idx.ToString();
				go.transform.position = grid[idx].position;
				go.transform.parent = parent;
			}

		}

		// else, simply update the position of existing tiles
		else
		{
			for (int idx = 0; idx < grid.Length; idx++)
			{
				// make sure it is a valid node before processing it
				if (false == grid[idx].isValid) continue;

				// Since I gave the tiles proper names I can easily find them by name
				GameObject go = parent.Find("Tile " + idx.ToString()).gameObject;
				go.transform.position = grid[idx].position;
			}
		}

		// focus the camera on the center tile
		// if (Application.isPlaying) FocusCamera();
	}

	// ------------------------------------------------------------------------------------------------------------


	// ------------------------------------------------------------------------------------------------------------

	protected void Update()
	{
        if (!player.getManageMode())
        {
            CloseMessagePanel();
            CloseMessageDetailedPanel();
        }
            //if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && GUIUtility.hotControl == 0)
            if (Input.GetMouseButtonDown(0))
        {
			// check if clicked on a tile and make it selected
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
			{
				// should be a tile since I have nothing else in scene that has a collider
				// I remove the "Tile " bit from name to get the tile number (node index)
				GameObject go = hit.collider.gameObject;
                string tileIndexName = go.name.Replace("Tile ", "");
				int idx = -1;
				if (int.TryParse(tileIndexName, out idx))
				{
					{
						//if (false == invalidNodes.Contains(grid[idx]))
						{
	/*						if (Input.GetMouseButtonDown(1)) // clic droit
							{
								if (grid[idx] != selectedNode)
								{
									altSelectedNode = grid[idx];
								}
							}
							else // clic gauche*/
							{
								if (grid[idx] != altSelectedNode)
								{
									selectedNode = grid[idx];
                                    if (hit.transform != null )
                                    {
                                        Debug.Log("Click gauche sur la map");
                                        Vector3 position = hit.transform.position;
                                        if (!isNodeAlreadyOccuped())
                                        {
                                            addBuildingToScene(position);
                                            alreadyOccupedCells.Add(selectedNode);
                                        }
                                        else
                                            Debug.Log("Cell occuped");
                                    }
                                }
							}
						} 
					}
                    
				}
			}

		}
	}
    

    public bool isNodeAlreadyOccuped()
    {
        bool result = false;
            for (int i = 0; i < alreadyOccupedCells.Count; i++)
            {
                Debug.Log("Recherche Alreadyselected" + i);
                if (selectedNode.Equals(alreadyOccupedCells[i]))
                {
                    result = true;
                OpenMessagePanel("This square is occupied");
            }
            }
            return result;
    }

    public void changeBuildingToBigHouse()
    {
        this.currentObject = FindObjectOfType<All3DObjects>().getThirdObject();
        Debug.Log("Big House Selected ");
    }

    public void changeBuildingToMediumHouse()
    {
        this.currentObject = FindObjectOfType<All3DObjects>().getNextObject();
        Debug.Log("Medium House Selected ");
    }

    public void changeBuildingToBasicHouse()
    {
        this.currentObject = FindObjectOfType<All3DObjects>().getCurrentObject();
        Debug.Log("Basic House selected !");
    }

    public void changeBuildingToLumber()
    {
        this.currentObject = FindObjectOfType<All3DObjects>().getForthObject();
        Debug.Log("Lumber Selected ");
    }
    public void changeBuildingToMiner()
    {
        this.currentObject = FindObjectOfType<All3DObjects>().getFifthObject();
        Debug.Log("Miner Selected ");
    }
    public void changeBuildingToWareHouse()
    {
        this.currentObject = FindObjectOfType<All3DObjects>().getSixthObject();
        Debug.Log("WareHouse Selected ");
    }
    public void changeBuildingToGoldWareHouse()
    {
        this.currentObject = FindObjectOfType<All3DObjects>().getSeventhObject();
        Debug.Log("GoldWareHouse Selected ");
    }
    public void changeBuildingToForge()
    {
        this.currentObject = FindObjectOfType<All3DObjects>().getHeightveObject();
        Debug.Log("Forge Selected ");
    }
    public void changeBuildingToArmurerie()
    {
        this.currentObject = FindObjectOfType<All3DObjects>().getNinthObject();
        Debug.Log("Armurerie Selected ");
    }

    protected void addBuildingToScene(Vector3 position)
    {
        //GameObject currentObject = FindObjectOfType<All3DObjects>().getCurrentObject();

        // Ici ajouter currentObject = batiment selectionné

        if (currentObject != null)
        {
            Debug.Log("OK");
            //Instantiate
       
            _myVillage = FindObjectOfType<VillageManager>();
            Debug.Log(currentObject.name);
            switch (currentObject.name)
            {
                case "BasicHousePrefab":
                    if (player.getGold() >= 100 && _myVillage.getWood() >= 60 && _myVillage.getStone() >= 30)
                    {
                        _myVillage.setStone(_myVillage.getStone() - 30);
                        _myVillage.setWood(_myVillage.getWood() - 60);
                        player.setGold(player.getGold() - 100);
                        _myVillage.setPeople(_myVillage.getPeople() + 5);
                        Instantiate(currentObject, position, Quaternion.identity);
                        CloseMessagePanel();
                        CloseMessageDetailedPanel();
                    }
                    else OpenMessagePanel("Not enough ressources");

                    break;
                case "MediumHousePrefab":
                    if (player.getGold() >= 180 && _myVillage.getWood() >= 150 && _myVillage.getStone() >= 100)
                    {
                        _myVillage.setStone(_myVillage.getStone() - 100);
                        _myVillage.setWood(_myVillage.getWood() - 150);
                        player.setGold(player.getGold() - 180);
                        _myVillage.setPeople(_myVillage.getPeople() + 10);
                        Instantiate(currentObject, position, Quaternion.identity);
                        CloseMessagePanel();
                        CloseMessageDetailedPanel();
                    }
                    else OpenMessagePanel("Not enough ressources");
                    break;
                case "BigHousePrefab":
                    if (player.getGold() >= 300 && _myVillage.getWood() >= 350 && _myVillage.getStone() >= 220)
                    {
                        _myVillage.setStone(_myVillage.getStone() - 220);
                        _myVillage.setWood(_myVillage.getWood() - 350);
                        _myVillage.setPeople(_myVillage.getPeople() + 25);
                        player.setGold(player.getGold() - 300);
                        Instantiate(currentObject, position, Quaternion.identity);
                        CloseMessagePanel();
                        CloseMessageDetailedPanel();
                    }
                    else OpenMessagePanel("Not enough ressources");
                    break;
                case "LumberPrefab":
                    if (player.getGold() >= 220 && _myVillage.getWood() >= 250)
                    {
                        _myVillage.setWood(_myVillage.getWood() - 250);
                        _myVillage.setLumber(_myVillage.getLumber() + 1);
                        player.setGold(player.getGold() - 220);
                        Instantiate(currentObject, position, Quaternion.identity);
                        CloseMessagePanel();
                        CloseMessageDetailedPanel();
                    }
                    else OpenMessagePanel("Not enough ressources");
                    break;
                case "MinerPrefab":
                    if (player.getGold() >= 220 && _myVillage.getStone() >= 180)
                    {
                        _myVillage.setStone(_myVillage.getStone() - 180);
                        _myVillage.setMiner(_myVillage.getMiner() + 1);
                        player.setGold(player.getGold() - 220);
                        Instantiate(currentObject, position, Quaternion.identity);
                        CloseMessagePanel();
                        CloseMessageDetailedPanel();
                    }
                    else OpenMessagePanel("Not enough ressources");
                    break;
                case "WareHousePrefab":
                    if (player.getGold() >= 350 && _myVillage.getStone() >= 350 && _myVillage.getWood() >= 400 && _myVillage.getPeople() >= 30)
                    {
                        _myVillage.setWood(_myVillage.getWood() - 400);
                        _myVillage.setStone(_myVillage.getStone() - 350);
                        player.setGold(player.getGold() - 350);
                        player.setMaxWood(player.getMaxWood() + 800);
                        player.setMaxStone(player.getMaxStone() + 600);
                        Instantiate(currentObject, position, Quaternion.identity);
                        CloseMessagePanel();
                        CloseMessageDetailedPanel();
                    }
                    else if (_myVillage.getPeople() < 30) OpenMessagePanel("Not enough inhabitants");
                    else OpenMessagePanel("Not enough ressources");
                    break;
                case "GoldWareHousePrefab":
                    if (player.getGold() >= 400 && _myVillage.getStone() >= 450 && _myVillage.getWood() >= 500 && _myVillage.getPeople() >= 50)
                    {
                        _myVillage.setWood(_myVillage.getWood() - 500);
                        _myVillage.setStone(_myVillage.getStone() - 450);
                        player.setGold(player.getGold() - 400);
                        player.setMaxGold(player.getMaxGold() + 700);
                        Instantiate(currentObject, position, Quaternion.identity);
                        CloseMessagePanel();
                        CloseMessageDetailedPanel();
                    }
                    else if (_myVillage.getPeople() < 50) OpenMessagePanel("Not enough inhabitants");
                    else OpenMessagePanel("Not enough ressources");
                    break;
                case "ForgePrefab":
                    if (player.getGold() >= 320 && _myVillage.getStone() >= 250 && _myVillage.getWood() >= 300 && _myVillage.getPeople() >= 40)
                    {
                        _myVillage.setWood(_myVillage.getWood() - 300);
                        _myVillage.setStone(_myVillage.getStone() - 250);
                        player.setGold(player.getGold() - 320);
                        Instantiate(currentObject, position, Quaternion.identity);
                        player.setDEF(player.getDEF() + 10);
                        CloseMessagePanel();
                        CloseMessageDetailedPanel();
                    }
                    else if (_myVillage.getPeople() < 40) OpenMessagePanel("Not enough inhabitants");
                    else OpenMessagePanel("Not enough ressources");
                    break;
                case "ArmureriePrefab":
                    if (player.getGold() >= 320 && _myVillage.getStone() >= 240 && _myVillage.getWood() >= 320 && _myVillage.getPeople() >= 40)
                    {
                        _myVillage.setWood(_myVillage.getWood() - 320);
                        _myVillage.setStone(_myVillage.getStone() - 240);
                        player.setGold(player.getGold() - 320);
                        player.setATK(player.getATK() + 10);
                        Instantiate(currentObject, position, Quaternion.identity);
                        CloseMessagePanel();
                        CloseMessageDetailedPanel();
                    }
                    else if (_myVillage.getPeople() < 40) OpenMessagePanel("Not enough inhabitants");
                    else OpenMessagePanel("Not enough ressources");
                    break;
            }

        }
    }

    public void OpenMessagePanel(string txt)
    {
        messagePanel.SetActive(true);
        if (txt != null) messagePanel.transform.GetChild(0).GetComponent<Text>().text = txt;
    }
    public void OpenMessageDetailedPanel(string txt)
    {
        messageDetailedPanel.SetActive(true);
        if (txt != null) messageDetailedPanel.transform.GetChild(0).GetComponent<Text>().text = txt;
    }

    public void CloseMessagePanel()
    {
        messagePanel.SetActive(false);
    }
    public void CloseMessageDetailedPanel()
    {
        messageDetailedPanel.SetActive(false);
    }



    // ------------------------------------------------------------------------------------------------------------
}
