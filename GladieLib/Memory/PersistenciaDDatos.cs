using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
namespace GladieLib.memory{
public class PersistenciaDDatos<TE> where TE : DatoPersistido {
	
	public static readonly PersistenciaDDatos<TE> Instance = new PersistenciaDDatos<TE>();
	// Use this for initialization
	private byte[] _password;
	
	private PersistenciaDDatos()
	{
		 BuildCrytoProvider();
	}
	private void BuildCrytoProvider()
    {
            var keyBytes = new byte[32];
            keyBytes[0] = 79;
            keyBytes[1] = 102;
            keyBytes[2] = 41;
            keyBytes[3] = 191;
            keyBytes[4] = 115;
            keyBytes[5] = 18;
            keyBytes[6] = 0;
            keyBytes[7] = 29;
            keyBytes[8] = 135;
            keyBytes[9] = 34;
            keyBytes[10] = 36;
            keyBytes[11] = 94;
            keyBytes[12] = 10;
            keyBytes[13] = 14;
            keyBytes[14] = 69;
            keyBytes[15] = 33;
            keyBytes[16] = 172;
            keyBytes[17] = 242;
            keyBytes[18] = 63;
            keyBytes[19] = 19;
            keyBytes[20] = 136;
            keyBytes[21] = 96;
            keyBytes[22] = 76;
            keyBytes[23] = 76;
            keyBytes[24] = 136;
            keyBytes[25] = 176;
            keyBytes[26] = 236;
            keyBytes[27] = 68;
            keyBytes[28] = 236;
            keyBytes[29] = 87;
            keyBytes[30] = 236;
            keyBytes[31] = 136;

			_password = keyBytes;
      }
	public TE Read (string id, bool esResourceFolder)
	{
			try{
			if(esResourceFolder == false)
			{
				var data = PlayerPrefs.GetString(id);
		   		//If not blank then load it
			    if(!String.IsNullOrEmpty(data))
			    {
			        //Binary formatter for loading back
			        var binaryFormatter = new BinaryFormatter();
			        //Create a memory stream with the data
			    	var inputStream = new MemoryStream(Convert.FromBase64String(data));
					string typeName;
					object objeto;
				    using (inputStream)
	                {
	                    using (var reader = new XORStream(inputStream, _password))
	                    {
	                      //  typeName = (string) binaryFormatter.Deserialize(reader);
							objeto = binaryFormatter.Deserialize(reader) as object;
	                    }
	                }
					
					var result =(TE)objeto;
					return result;
				}
			}}
			catch(IOException e)
			{
				Debug.Log(e.Message);
				return null;
			}
			return null;
	}
	public void Write(TE Dato)
	{
		 try
            {
                var binaryFormatter = new BinaryFormatter {Binder = new BinderDeDeserealizacion()};
             
							
				// writing to memory
				var memoryStream = new MemoryStream(2048);
				var writer = new XORStream(memoryStream, _password);
				
			
				binaryFormatter.Serialize(writer, Dato);
				writer.Flush();
				string info = Convert.ToBase64String(memoryStream.GetBuffer());
				PlayerPrefs.SetString(Dato.id, info );
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
			var binaryFormatter = new BinaryFormatter {Binder = new BinderDeDeserealizacion()};
			var memoryStream = new MemoryStream(2048);
			var writer = new XORStream(memoryStream,_password);
			binaryFormatter.Serialize(writer,datos);
			writer.Flush();
			string info = Convert.ToBase64String(memoryStream.GetBuffer());
			PlayerPrefs.SetString(id,info);
			PlayerPrefs.Save();
			}
			catch(Exception e)
			{
				Debug.Log(e.Message);
			}
	}
	public void WriteTable(string id, Hashtable datos)
	{
			var binaryFormatter = new BinaryFormatter {Binder = new BinderDeDeserealizacion()};
			var memoryStream = new MemoryStream(2048);
			var writer = new XORStream(memoryStream,_password);
			binaryFormatter.Serialize(writer,datos);
			writer.Flush();
			string info = Convert.ToBase64String(memoryStream.GetBuffer());
			PlayerPrefs.SetString(id,info);
			PlayerPrefs.Save();
	}
	public Hashtable ReadTable(string id, bool esResourceFolder)
	{
			try{
			if(esResourceFolder == false)
			{
				var data = PlayerPrefs.GetString(id);
		   		//If not blank then load it
			    if(!String.IsNullOrEmpty(data))
			    {
			        //Binary formatter for loading back
			        var binaryFormatter = new BinaryFormatter();
			        //Create a memory stream with the data
			    	var inputStream = new MemoryStream(Convert.FromBase64String(data));
					string typeName;
					object objeto;
				    using (inputStream)
	                {
	                    using (var reader = new XORStream(inputStream, _password))
	                    {
							objeto = binaryFormatter.Deserialize(reader) as object;
	                    }
	                }
					var result =(Hashtable)objeto;
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
	public IList<TE> ReadList (string id, bool esResourceFolder)
	{
			try{
				
			if(esResourceFolder == false)
			{
				var data = PlayerPrefs.GetString(id);
		   		//If not blank then load it
			    if(!String.IsNullOrEmpty(data))
			    {
			        //Binary formatter for loading back
			        var binaryFormatter = new BinaryFormatter();
			        //Create a memory stream with the data
			    	var inputStream = new MemoryStream(Convert.FromBase64String(data));
					string typeName;
					object objeto;
				    using (inputStream)
	                {
	                    using (var reader = new XORStream(inputStream, _password))
	                    {
							objeto = binaryFormatter.Deserialize(reader) as object;
	                    }
	                }
					
					var result =(IList<TE>)objeto;
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
		
		
}
}