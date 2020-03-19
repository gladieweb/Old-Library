using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace GladieLib.memory{
public class PersistenciaJson<TE> where TE : DatoPersistido{
	
	public static readonly PersistenciaJson<TE> Instance = new PersistenciaJson<TE>();
	// Use this for initialization
	private byte[] _password;
	
	private PersistenciaJson()
	{
	}
	private const int bytePassword = 23;
	
	public TE Read (string id, bool esResourceFolder)
	{
		try{
			if(esResourceFolder == false)
			{
				var data = PlayerPrefs.GetString(id);
		   		//If not blank then load it
			    if(!String.IsNullOrEmpty(data))
			    {
			       
			        byte[] b= new byte[data.Length];
					for(int k = 0 ; k < data.Length; k++)
					{
						b[k] =(byte)(((int) data[k]) - bytePassword);
					}
					string info = Encoding.ASCII.GetString(b);
					Debug.Log("devolviendo el asci");
					Debug.Log(info);
		
					Debug.Log("Decodificando");
					IDictionary t =(IDictionary) MiniJSON.Json.Deserialize(info);

					var result = (TE) Activator.CreateInstance(typeof(TE));
					result.SetDataToDeserealize(t);
					return result;
				}
			}
		}
		catch(IOException e)
		{
			Debug.Log(e.Message);
			return null;
		}
		return null;
	}
	public void Write(TE Dato, string id)
	{
		try{
			Hashtable table = Dato.getData();
			Debug.Log("tengo tabla");
            string info = MiniJSON.Json.Serialize(table);
			Debug.Log(info);
			byte[] b =  Encoding.ASCII.GetBytes(info);
			info = "";
			for(int i = 0; i< b.Length; i++)
			{
				info +=(char)( b[i]+bytePassword);
			}
			Debug.Log("asci codificado");
			Debug.Log(info);
				
			PlayerPrefs.SetString(id, info );
			PlayerPrefs.Save();
				
				
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
			return ;
        }
	}
	public void WriteList(string id, IList<TE> datos)
	{
			try{
				IList<string> ilista = new List<string>();
				foreach( TE item in datos)
				{
					string  it = MiniJSON.Json.Serialize(item.getData());
					ilista.Add(it);						;
				}
			string info = MiniJSON.Json.Serialize(ilista);
			byte[] b =  Encoding.ASCII.GetBytes(info);
			info = "";
			for(int i = 0; i< b.Length; i++)
			{
				info +=(char)( b[i]+bytePassword);
			}
			PlayerPrefs.SetString(id,info);
			PlayerPrefs.Save();
				
			
		
		
				
			//
			}
			catch(Exception e)
			{
				Debug.Log(e.Message);
			}
	}
	public void WriteTable(string id, Hashtable datos)
	{
			try{
				Hashtable ilista = new Hashtable();
				foreach(string item in datos.Keys)
				{
					TE aux =  (TE)datos[item];
					string  it = MiniJSON.Json.Serialize(aux.getData());
					ilista.Add(aux.id,it);
//					Debug.Log(it);
;
				}
			
			string info = MiniJSON.Json.Serialize(ilista);
			Debug.Log(info);
			byte[] b =  Encoding.ASCII.GetBytes(info);
			info = "";
			for(int i = 0; i< b.Length; i++)
			{
				info +=(char)( b[i]+bytePassword);
			}
			PlayerPrefs.SetString(id,info);
			PlayerPrefs.Save();
			}
			catch(Exception e)
			{
				Debug.Log("no se pudo guardar");
				Debug.Log(e.Message);
			}
	}
	public Hashtable ReadTable(string id, bool esResourceFolder)
	{
			try{
				
			if(esResourceFolder == false)
			{
				if(!PlayerPrefs.HasKey(id))
					{
						Debug.Log("no tiene" + id + " guadado");
					}
				var data = PlayerPrefs.GetString(id);
				
		   		//If not blank then load it
//				Debug.Log(data);
			    if(!String.IsNullOrEmpty(data))
			    {
			        byte[] b= new byte[data.Length];
					for(int k = 0 ; k < data.Length; k++)
					{
						b[k] =(byte)(((int) data[k]) - bytePassword);
					}
					data = Encoding.ASCII.GetString(b);
					//Debug.Log(data);
			      	IDictionary<string,object> l = (IDictionary<string,object>) MiniJSON.Json.Deserialize(data);
					//Debug.Log( l);
					Hashtable preList = new Hashtable();
					foreach(string  o in l.Keys)
					{
						//Debug.Log(o);
						var aux  =(TE) Activator.CreateInstance(typeof(TE));
						//Debug.Log(((string)l[o]));
						IDictionary table =(IDictionary) MiniJSON.Json.Deserialize((string)l[o]);
						aux.SetDataToDeserealize(table);
						preList.Add(aux.id,aux);
					}
					return preList;
				}
			}
		}
		catch(IOException e)
		{
			Debug.Log(e.Message);
			return null;
		}
		return null;
	}
	public IList<TE> ReadList (string id, bool esResourceFolder)
	{
		try{
				
			if(esResourceFolder == false)
			{
					if(!PlayerPrefs.HasKey(id))
					{
						Debug.Log("no tiene" + id + " guadado");
					}
				var data = PlayerPrefs.GetString(id);
		   		//If not blank then load it
			    if(!String.IsNullOrEmpty(data))
			    {
			        byte[] b= new byte[data.Length];
					for(int k = 0 ; k < data.Length; k++)
					{
						b[k] =(byte)(((int) data[k]) - bytePassword);
					}
					data = Encoding.ASCII.GetString(b);
						
			        IList<object> l = (IList<object>) MiniJSON.Json.Deserialize(data);
					IList<TE> preList = new List<TE>();
					foreach(object o in l)
					{
						var aux  =(TE) Activator.CreateInstance(typeof(TE));
						IDictionary table =(IDictionary) MiniJSON.Json.Deserialize((string)o);
						aux.SetDataToDeserealize(table);
						preList.Add(aux);
					}
					return preList;
				}
			}
		}
		catch(IOException e)
		{
			Debug.Log(e.Message);
			return null;
		}
		return null;
	}
		
		
}
}