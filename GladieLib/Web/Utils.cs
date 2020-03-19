using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
namespace GladieLib.Web.Utility{
public class Utils : MonoBehaviour {

	public static Dictionary<string,string> GetQueryParams() {
		string src = Application.srcValue;
				
		if (src == "" 
			|| src == null 
			|| src.IndexOf("?") == -1 
			|| src.IndexOf("?") == src.Length - 1) return new Dictionary<string, string>();
		
		src = src.Substring(src.IndexOf("?") + 1);
		
		return ParseQueryString(src,null);
	}
	
	public static NameValueCollection ParseQueryString(string url) {
		NameValueCollection nvc = new NameValueCollection();
		 if(url.Contains("?"))
         {
                url = url.Substring(url.IndexOf('?') + 1);
         }
		string[] paramList = url.Split('&');
		
		for (int i=0;i<paramList.Length;i++) {
			string[] temp= paramList[i].Split('=');	
			
			string key = WWW.UnEscapeURL(temp[0]);
			string val = WWW.UnEscapeURL(temp[1]);
			
			nvc.Add(key, val);
		}
		
		return nvc;	
	}
		public static Dictionary<string,string> ParseQueryString(string query,object Nulleable) {
		Dictionary<string,string> urlParams = new Dictionary<string, string>();
		
		string[] paramList = query.Split('&');
		
		for (int i=0;i<paramList.Length;i++) {
			string[] temp= paramList[i].Split('=');	
			
			string key = WWW.UnEscapeURL(temp[0]);
			string val = WWW.UnEscapeURL(temp[1]);
			
			urlParams.Add(key, val);
		}
		
		return urlParams;	
	}	
	
	public static string QueryString(Dictionary<string,string> urlParams) {	
		string parameters = "";
		bool first = true;
		
		foreach (KeyValuePair<string,string> kvp in urlParams) {
			parameters += (first ? "?" : "&") + WWW.EscapeURL(kvp.Key) + "=" + WWW.EscapeURL(kvp.Value);
			first = false;
		}
		
		return parameters;
	}
	
	public static string GetFilename() {
		string src = Application.srcValue;
		
		if (src.IndexOf("?") >= 0) 
			return src.Substring(0,src.IndexOf("?"));
		
		return src;
	}
	
	public static string GetDomain() {
		if (Application.isEditor) return "http://localhost:8080";
		
		string url = Application.absoluteURL;
		
		string protocol = url.Substring(0, url.IndexOf("://") + 3);
				
		url = url.Substring(url.IndexOf("://") + 3);
				
		string domain = url.Substring(0, url.IndexOf("/"));
		
		return protocol + domain;
	}
	public static Dictionary<string,string> Parse (string response)
	{
		int index = 1;
		Dictionary<string,Dictionary<string,string>> request_information = new Dictionary<string, Dictionary<string, string>>();
		response = response.Replace(" ", "");
		response =response.Replace("	", "");
		response =response.Replace("\n", "");
		response =response.Replace("\r", "");
		response =response.Replace("\\", "");
		Dictionary<string,string> result = AbrirLLave(response,ref index);
		return result;
	}
	static Dictionary<string,string>  AbrirLLave(string response ,ref int index){
		Dictionary<string,string> parametros = new Dictionary<string,string>();
		bool whaitingForProperty= false;
		string key ="";
		string prop = "";
		while(response[index] != '}'){
			
			
			switch(response[index])
			{
			case '{': {
				if(whaitingForProperty )
				{
					prop  = itemValue(ref index,response,true);
				}
				break;
				}
			case '"': {
				if(!whaitingForProperty )
				{
					key = Key(ref index,response);
				}
				else
				{
					prop  = itemValue(ref index,response,false);
				}
				break;
				}
			case ':': {
				whaitingForProperty = true;
				break;
			}
			case ',': {
				
				//fijar
				parametros.Add(key,prop);
				key = "none";
				prop = "none";
				whaitingForProperty = false;
				
				break;
				}
			case '}': {
					return parametros;
				}
			default:
				{
				Debug.Log("Default Character : " + response[index]);
				break;
				}
			}
			index ++;
			if(index > response.Length)
			{
				Debug.LogError("no se encontro LLAVE final");
				break;
			}
		}
		parametros.Add(key,prop);
		return parametros;
	}
	
	static string Key(ref int index, string response)
	{
		string key= "";
		index ++;
		while(response[index] != '"')
		{
			key += response[index];
			index ++;
		}
		return key;		
	}
	static string itemValue(ref int index , string response, bool isCollection)
	{
		if(isCollection)
		{
			string key= "";
			while(response[index] != '}')
			{
				key += response[index];
				index ++;
			}
			key += response[index];
			return key;		
			
		}
		else{
			return Key(ref index, response);
		}
		
	}

}
}
