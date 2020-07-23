using UnityEngine;

/// <summary>
/// This class detect any user action and trigger Approperiate Event based on the action
/// </summary>
public class UserAction : MonoBehaviour {

    private static UserAction userAction;

    /// <summary>
    /// Instance of the UserAction
    /// </summary>
    public static UserAction instance
    {
        get
        {
            if (!userAction)
            {
                userAction = FindObjectOfType(typeof(UserAction)) as UserAction;

                if (!userAction)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    userAction.Init();
                }
            }

            return userAction;
        }
    }

    private GameObject selectedObject;

    /// <summary>
    /// Set currently selected object that user can interact with
    /// </summary>
    public GameObject SelectedObject
    {
        get{
            return selectedObject;
        }
        set{
            if(selectedObject==null || !selectedObject.Equals(value))
            {
                raiseUserSelectingObjectEvent(value);
            }
        }
    }

    private void Init()
    {
        
    }

    // Update is called once per frame
    void Update () {
        checkUserSelectingObject();
	}

    /// <summary>
    /// Check whether a user is selecting object and select the object
    /// </summary>
    private void checkUserSelectingObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, ~(1 << LayerMask.NameToLayer("UI"))))
            {
                if (hit.transform != null)
                {
                    if (hit.transform)
                    {
                        SelectedObject = hit.transform.gameObject;
                    }
                }
            }
        }      
    }

    /// <summary>
    /// Raise User Selecting Object
    /// </summary>
    /// <param name="selecedObject"></param>
    private void raiseUserSelectingObjectEvent(GameObject selecedObject)
    {

        EventParam param = new EventParam() { gameObject= selecedObject};

        EventManager.TriggerEvent(EventConst.OBJECT_SELECTED, param);
        if(selecedObject.GetComponent<IUserInteraction>()!=null)
        {
            EventManager.TriggerEvent(EventConst.OBJECT_SELECTED_USERINTERACTION, param);
        }
    }
}
