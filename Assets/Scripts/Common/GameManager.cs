using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public string ResourceToAddToCell = string.Empty;

    private static GameManager mGameManager;

    public static GameManager Instance
    {
        get
        {
            if (!mGameManager)
            {
                mGameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (!mGameManager)
                {
                    Debug.LogError("There must be one Game Manager in the scene!");
                }
                else
                {
                    mGameManager.Init();
                }                
            }

            return mGameManager;
        }
    }

    private void Init()
    {

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        //Check resource to add to cell if it exists
        if (Input.GetMouseButtonDown(0))
        {
          if(!string.IsNullOrEmpty(ResourceToAddToCell))
            {
                placeResource();
            }
        }
        
    }

    private void placeResource()
    {
        if (string.IsNullOrEmpty(ResourceToAddToCell)) return;
       GameObject resourceObj = Instantiate(ResourceManager.getEntity(ResourceToAddToCell)) as GameObject;
        resourceObj.transform.position = getMouseHitPosition();
    }
   
    private Vector3 getMouseHitPosition()
    {
        Vector3 hitPos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.NameToLayer("BaseGround")))
        {
            hitPos = hit.transform.position;
        }

        return hitPos;
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventConst.BUILD, OnBuildEvent);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventConst.BUILD, OnBuildEvent);
    }

    public void OnBuildEvent(EventParam param)
    {
        ResourceToAddToCell = param.paramString;
    }
}
