using System;
using System.Xml;
using System.IO;

public class Usuario{
    public String nombre;
    public String apellidos;
    public String fechaRegistro;
    public String usuarioBgg;

	public Usuario(String nombreUsuario, String rutaCacheUsuario){
        XmlDocument documentoUsuario;
        if (File.Exists(rutaCacheUsuario + nombreUsuario))
        {
            documentoUsuario = new XmlDocument();
            documentoUsuario.Load(rutaCacheUsuario + nombreUsuario);
        }
        else
        {
            documentoUsuario = Consultas.consultarApiUsuario(nombreUsuario);
        }
        nombre = documentoUsuario.DocumentElement.SelectSingleNode("/user/firstname").Attributes["value"].Value;
        apellidos = documentoUsuario.DocumentElement.SelectSingleNode("/user/lastname").Attributes["value"].Value;
        fechaRegistro = documentoUsuario.DocumentElement.SelectSingleNode("/user/yearregistered").Attributes["value"].Value;
        usuarioBgg = documentoUsuario.DocumentElement.SelectSingleNode("/user").Attributes["name"].Value;
        if (fechaRegistro.Length ==0)
        {
            throw new Exception("No existe un usuario con el ID ingresado.");
        }
        documentoUsuario.Save(rutaCacheUsuario + nombreUsuario);
    }
    
}
