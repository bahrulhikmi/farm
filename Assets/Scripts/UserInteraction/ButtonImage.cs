using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour {
    private Button btn;
    private UIDisplayActionData mActionData;
    public UIDisplayActionData actionData
    {
        get
        {
            return mActionData;
        }
        set
        {
            mActionData = value;
            refreshData();
        }
    }


    private void refreshData()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnClick);
        btn.GetComponentsInChildren<Image>()[1].sprite = actionData.actionImage;
        btn.GetComponentInChildren<Text>().text = actionData.actionText;        
    }

    void OnClick()
    {
        EventManager.TriggerEvent(actionData.EventTrigger, new EventParam(actionData.EventParam));
    }
	
}
