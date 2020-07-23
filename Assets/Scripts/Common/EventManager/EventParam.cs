using UnityEngine;

/// <summary>
/// Generic Event parameter.
/// </summary>
public class EventParam {

	public EventParam(){}
    public EventParam(string paramString)
    {
        this.paramString = paramString;        
    }
    public object sender;
    public GameObject gameObject;
    public string paramString;

}
