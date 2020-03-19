using System;
using System.Reflection;
using System.Runtime.Serialization;
namespace GladieLib.memory
{

public sealed class BinderDeDeserealizacion: SerializationBinder
{
    
	public override Type BindToType(string assemblyName, string typeName)
    {
        if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
        {
            assemblyName = Assembly.GetExecutingAssembly().FullName;

            // The following line of code returns the type.
            var typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

            return typeToDeserialize;
        }
        return null;
    }
    
}
}
