using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
[Serializable]
public class DatoPersistido:DatoJson, ISerializable {
	public string id;
	protected int _mask;
    protected int _safeValue;
	public virtual object GetValue()
	{
		return null;
	}
	public DatoPersistido()
	{
	}
	 public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
            info.AddValue("m", _mask);
            info.AddValue("s", _safeValue);
			info.AddValue("i", id);
			
    }
	protected override void SetPropertiesToSerialize()
	{
			data.Clear();
			data.Add("_mask", _mask);
            data.Add("_safeValue", _safeValue);
			data.Add("id", id);
	}
	public override void SetDataToDeserealize(IDictionary info)
	{
		 _mask =(int) info["_mask"];
		_safeValue =(int) info["_safeValue"];
		id =(string) info["id"];
	}
}
