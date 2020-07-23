using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Generate and assign correct action to the UserInteraction classes
/// </summary>
public class ActionUI : MonoBehaviour {

    public Button[] buttons;
    public IUserInteraction selectedObject;
    public List<string> actions;    

    private void Start()
    {
        foreach (Button button in buttons)
        {    
            button.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventConst.OBJECT_SELECTED_USERINTERACTION, OnUserInteractionObjectSelected);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventConst.OBJECT_SELECTED_USERINTERACTION, OnUserInteractionObjectSelected);
    }

    public void OnUserInteractionObjectSelected(EventParam param)
    {        
        assignActions(param.gameObject.GetComponent<IUserInteraction>());
    }

    /// <summary>
    /// Clear the UI, hiding all the buttons
    /// </summary>
    public void Clear()
    {       
        foreach (Button button in buttons)
        {
           button.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Assign current actions to the Buttons
    /// </summary>
    /// <param name="userInteraction"></param>
    private void assignActions(IUserInteraction userInteraction)
    {

        selectedObject = userInteraction;
        if (selectedObject == null) { Clear(); return; }
            

        actions = selectedObject.getAvailableActions();                

        int index = 0;
        foreach (Button button in buttons)
        {
            if (index < actions.Count)
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<Text>().text = actions[index];
            }
            else
            {
                button.gameObject.SetActive(false);
            }

            index++;
        }
    }

    /// <summary>
    /// Run the Action of the Mapped method which is specfied by the Index, To be called by the Button
    /// </summary>    
    public void RunAction(int index)
    {
        if (selectedObject!=null)
        {
           if( selectedObject.RunAction(actions[index]))
            {
                Clear();
            }
        }
    }
}
