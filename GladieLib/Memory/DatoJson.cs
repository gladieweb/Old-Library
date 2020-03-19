using UnityEngine;
using System.Collections;


public class DatoJson {
	
	protected Hashtable data = new Hashtable();
	// Use this for initialization
	protected virtual void SetPropertiesToSerialize(){}
	public virtual void SetDataToDeserealize(IDictionary info){}
	public DatoJson()
	{
	}
	public  Hashtable getData ()
	{
		SetPropertiesToSerialize();
		return data;
	}
	public override string ToString ()
	{
		SetPropertiesToSerialize();
		return MiniJSON.Json.Serialize(data);
	}
	
}
