using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActionGroup : MonoBehaviour {

    public Text text;
    public Transform buttonContent;

    public string groupDisplayName;

    public List<UIDisplayActionData> actions;
	void Start () {
        LoadButtons("buildings");
	}
	
    public void LoadButtons(string actionGroup)
    {
        mLoadButtonFromResources(actionGroup);
        foreach (UIDisplayActionData action in actions)
        {
            ButtonImage button = ObjectPool.Instance.GetObject("ButtonImage").GetComponent<ButtonImage>();            
            button.actionData = action;
            button.transform.SetParent(buttonContent);
        }
    }

    private void mLoadButtonFromResources(string resource)
    {
        actions.Clear();
        object[] objResources = Resources.LoadAll("Actions\\"+resource);        
        foreach(object obj in objResources)
        {
            if(obj is GameObject)
            {
                UIDisplayAction uiAction = ((GameObject)obj).GetComponent<UIDisplayAction>();
                if (uiAction!=null)
                {
                    actions.Add(uiAction.data);
                }
            }
            else
            {
                continue;
            }
        }
    }
}
