using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;

public class Grid : MonoBehaviour {

    private GridData mData;
	public GridData Data
    {
        get
        {
            if (mData == null)
            {
                mData = new GridData();
            }
            return mData;
        }

    }

    private string SavePath
    {
        get
        {
            return Constants.SavePath +Data.ID+".grd";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        var serializer = new XmlSerializer(typeof(GridData));
        using (var stream = new FileStream(SavePath, FileMode.Create, FileAccess.Write))
        {
            serializer.Serialize(stream, this);
        }
    }

    public void Load(string gridId)
    {
        var serializer = new XmlSerializer(typeof(GridData));
        using (var stream = new FileStream(SavePath, FileMode.Open, FileAccess.Read))
        {
            mData= serializer.Deserialize(stream) as GridData;
        }
    }


    public void Load()
    {
        Load(Data.ID);

    }

}
