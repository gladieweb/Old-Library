using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace GladieLib.security{
[Serializable]
public class EnteroPersistido : DatoPersistido {
	
	/*public EnteroPersistido(string id_name, int valorInicial)
	{
		valor = valorInicial;
		id = id_name;
	}*/
	//public int valor;
	
	

        public EnteroPersistido(string id_name,int n)
        {
			id = id_name;
            Number = n;
        }

        protected EnteroPersistido(SerializationInfo info,
                              StreamingContext context)
        {
            _mask = info.GetInt32("m");
            _safeValue = info.GetInt32("s");
			id = info.GetString("i");
        }

        public int Number
        {
            get { return _safeValue - _mask - 9; }
            set
            {
                _mask = RandomUtils.Instance.FastInt(15000 + 18);
                _safeValue = _mask + value + 9;
            }
        }

        #region ISerializable Members

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m", _mask);
            info.AddValue("s", _safeValue);
			info.AddValue("i", id);
        }

        #endregion

        public bool IsZero()
        {
            return Number == 0;
        }
		
		public void Increment()
        {
            Number = Number + 1;
        }
		
		public void Decrement()
        {
            Number = Number - 1;
        }

        public void Add(int other)
        {
            Number = Number + other;
        }

        public void Add(EnteroPersistido other)
        {
            Number = Number + other.Number;
        }

        public void Substract(EnteroPersistido other)
        {
            Add(-other.Number);
        }

        public void Substract(int other)
        {
            Add(-other);
        }
}
}