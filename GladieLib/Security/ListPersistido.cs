using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/***
 * 
 * 
 * 
 * NOTA, ESTO TODABIA ES MEDIO INUTIL DE UTILIZAR.
 * 
 * 
 * 
 * 
 * 
 * *///
namespace GladieLib.security{
	[Serializable]
	public class ListPersistido<TE> : DatoPersistido {
	
		protected List<TE> _list = new List<TE>();
		
		public void Add (TE obj)
		{
			_list.Add(obj);
		}
		public bool Remove (TE obj)
		{
			return _list.Remove(obj);
		}
		public int GetCount()
		{
			return _list.Count;
		}
		public TE Get(int index){
			return _list[index];
		}
		protected ListPersistido(SerializationInfo info,
                              StreamingContext context)
        {
            _mask = info.GetInt32("m");
            _safeValue = info.GetInt32("s");
			id = info.GetString("i");
        }
        #region ISerializable Members

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m", _mask);
            info.AddValue("s", _safeValue);
			info.AddValue("i", id);
        }

        #endregion
	}
}
