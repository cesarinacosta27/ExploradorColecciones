using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Net;
using System.Drawing;
namespace ExploradorColecciones {
    public class Juego
    {
        public String idBgg;
        public String nombre;
        public List<String> autor= new List<String>();
        public String ilustrador;
        private String linkMiniatura;
        public List<String> numeroJugadores = new List<String>();
        public List<String> mecanicas = new List<String>();
        public List<String> familia = new List<String>();
        public List<String> categoria = new List<String>();
        public Juego(String nombreJuego, String rutaJuego)
        {
            XmlDocument documentoJuego;
            if (File.Exists(rutaJuego + nombreJuego))
            {
                documentoJuego = new XmlDocument();
                documentoJuego.Load(rutaJuego + nombreJuego);
            }else{
                documentoJuego = Consultas.consultarApiJuego(nombreJuego);
            }
            idBgg = nombreJuego;
            try
            {
                nombre = documentoJuego.DocumentElement.SelectSingleNode("/items/item/name").Attributes["value"].Value;
            }catch(Exception e) {
                nombre = "No encontrado";
            }
            try{
                XmlNodeList nodosAutor=documentoJuego.DocumentElement.SelectNodes("/items/item/link[@type='boardgamedesigner']");
                foreach (XmlNode nodo in nodosAutor) {                    
                    autor.Add(nodo.Attributes["value"].Value);
                }
            }catch(Exception e){
                Console.WriteLine("////////" + e.Message);
                autor.Add( "(Uncredited)");
            }
            try
            {
                ilustrador = documentoJuego.DocumentElement.SelectSingleNode("/items/item/link[@type='boardgameartist']").Attributes["value"].Value;
            }
            catch(Exception e)
            {
                ilustrador = "No encontrado";
            }
            try
            {
                linkMiniatura = documentoJuego.DocumentElement.SelectSingleNode("/items/item/thumbnail").InnerText;
            }catch(Exception e){
                linkMiniatura= "https://cf.geekdo-images.com/itemrep/img/hC_yq4W24pxHV5e8ISUAg1FDOyU=/fit-in/246x300/pic1657689.jpg";
            }

            try {
                
                int minJugadores= Int32.Parse(documentoJuego.DocumentElement.SelectSingleNode("/items/item/minplayers").Attributes["value"].Value);
                int maxJugadores=Int32.Parse(documentoJuego.DocumentElement.SelectSingleNode("/items/item/maxplayers").Attributes["value"].Value);
                for (int i = minJugadores; i <= maxJugadores; i++) {
                        numeroJugadores.Add(i.ToString());                    
                }                
            }catch(Exception e) {
                Console.WriteLine("******" + e.Message);
            }
            try {
                XmlNodeList nodosMecanicas = documentoJuego.DocumentElement.SelectNodes("/items/item/link[@type='boardgamemechanic']");
                foreach (XmlNode nodo in nodosMecanicas) {
                    mecanicas.Add(nodo.Attributes["value"].Value);
                }
            } catch (Exception e) {
                Console.WriteLine("////////" + e.Message);
                mecanicas.Add("(Uncredited)");
            }
            try {
                XmlNodeList nodosFamilia = documentoJuego.DocumentElement.SelectNodes("/items/item/link[@type='boardgamefamily']");
                foreach (XmlNode nodo in nodosFamilia) {
                    familia.Add(nodo.Attributes["value"].Value);
                }
            } catch (Exception e) {
                Console.WriteLine("////////" + e.Message);
                familia.Add("(Uncredited)");
            }
            try {
                XmlNodeList nodosCategoria = documentoJuego.DocumentElement.SelectNodes("/items/item/link[@type='boardgamecategory']");
                foreach (XmlNode nodo in nodosCategoria) {
                    categoria.Add(nodo.Attributes["value"].Value);
                }
            } catch (Exception e) {
                Console.WriteLine("////////" + e.Message);
                categoria.Add("(Uncredited)");
            }
            if (nombre.Length == 0)
            {
                throw new Exception("No existe un juego con el ID ingresado.");
            }
            

            documentoJuego.Save(rutaJuego + nombreJuego);
            if (!existeImagenMiniatura(rutaJuego,this.idBgg)){
                guardarImagenMiniatura(this.linkMiniatura, rutaJuego, this.idBgg);
            }
        }
        public Boolean existeImagenMiniatura(String rutaJuego, String id){
            return File.Exists(rutaJuego + "img/thubmnail/" + id + ".jpg");
        }
        public Image cargarImagenMiniatura(String rutaJuego,String id)
        {
            return Image.FromFile(rutaJuego + "img/thubmnail/" + id + ".jpg");
        }

        public void guardarImagenMiniatura(String linkDescarga,String rutaJuego,String id){
            asegurarExistenciaDirectorioMiniaturas(rutaJuego);
            WebClient clienteDescarga = new WebClient();
            clienteDescarga.DownloadFile(linkDescarga, rutaJuego + "img/thubmnail/"  + id + ".jpg");
        }

        public void asegurarExistenciaDirectorioMiniaturas(String rutaJuego){
            if(!Directory.Exists(rutaJuego + "img/thubmnail/")) {
                Directory.CreateDirectory(rutaJuego + "img/thubmnail/");
            }
        }

    }
}
