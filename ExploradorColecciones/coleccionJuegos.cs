using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
namespace ExploradorColecciones {
    class coleccionJuegos
    {
        public String masJuegosAutor;
        public int totalColeccion;
        public SortedDictionary<String, List<String>> autores = new SortedDictionary<String, List<String>>();
        public SortedDictionary<String, List<String>> mecanicas = new SortedDictionary<String, List<String>>();
        public SortedDictionary<String, List<String>> familia = new SortedDictionary<String, List<String>>();
        public SortedDictionary<String, List<String>> categoria = new SortedDictionary<String, List<String>>();
        public SortedDictionary<int, List<String>> juegos = new SortedDictionary<int, List<String>>();
        public Dictionary<String, Juego> idJuego = new Dictionary<String, Juego>();
        public coleccionJuegos()
        {

        }
        public coleccionJuegos(String usuario, String rutaJuego)
        {
            XmlDocument documentoJuego;
            if (File.Exists(rutaJuego + "colecciones/" + usuario))
            {
                documentoJuego = new XmlDocument();
                documentoJuego.Load(rutaJuego + "colecciones/" + usuario);
            }
            else
            {
                documentoJuego = Consultas.consultarApiColección(usuario);
                asegurarExistenciaDirectorioColecciones(rutaJuego);
                documentoJuego.Save(rutaJuego + "colecciones/" + usuario);
            }
            //totalitems
            if (documentoJuego.SelectSingleNode("/items")!=null) { 
                totalColeccion= Int32.Parse(documentoJuego.SelectSingleNode("/items").Attributes["totalitems"].Value);
                foreach (XmlNode nodo in documentoJuego.SelectNodes("/items/item")) {
                    Thread.CurrentThread.IsBackground = true;
                    Juego agregarJuego = new Juego(nodo.Attributes["objectid"].Value, rutaJuego);
                    agregarJuegoIdJuegos(agregarJuego);
                    agregarJuegoJuegos(agregarJuego);
                    agregarJuegoGenerico(autores, agregarJuego.autor, agregarJuego.idBgg);
                    agregarJuegoGenerico(mecanicas,agregarJuego.mecanicas,agregarJuego.idBgg);
                    agregarJuegoGenerico(familia,agregarJuego.familia,agregarJuego.idBgg);
                    agregarJuegoGenerico(categoria,agregarJuego.categoria,agregarJuego.idBgg);
                }
                asignarMasJuegosAutor();
            } 
        }
        public void agregarJuegoGenerico(SortedDictionary<String, List<String>> coleccion,List<String> key,String element) {
            List<String> manipularJuegos;
            foreach (String autor in key) {
                if (coleccion.TryGetValue(autor, out manipularJuegos)) {
                    manipularJuegos.Add(element);
                    coleccion[autor] = manipularJuegos;
                } else {
                    manipularJuegos = new List<String>();
                    manipularJuegos.Add(element);
                    coleccion.Add(autor, manipularJuegos);
                }
            }
        }
        public void asignarMasJuegosAutor(){
            String keyJuegoMasAutores="";
            int control=0;
            foreach (var item in autores) {
                if (item.Value.Count() > control) {
                    keyJuegoMasAutores = item.Key;
                    control = item.Value.Count();
                } 
            }
            masJuegosAutor = keyJuegoMasAutores;
        }

        public void agregarJuegoJuegos(Juego agregarJuego) {
            List<String> manipularJuegos;
            foreach (String numeroJugadores in agregarJuego.numeroJugadores) {
                if (juegos.TryGetValue(Int32.Parse(numeroJugadores), out manipularJuegos)) {
                    manipularJuegos.Add(agregarJuego.idBgg);
                    juegos[Int32.Parse(numeroJugadores)] = manipularJuegos;
                } else {
                    manipularJuegos = new List<String>();
                    manipularJuegos.Add(agregarJuego.idBgg);
                    juegos.Add(Int32.Parse(numeroJugadores), manipularJuegos);
                }
            }
        }

        public void agregarJuegoIdJuegos(Juego agregarJuego) {
            if (!idJuego.ContainsKey(agregarJuego.idBgg)) {
                idJuego.Add(agregarJuego.idBgg, agregarJuego);
            }
        }

        public void asegurarExistenciaDirectorioColecciones(String rutaJuego)
        {
            if (!Directory.Exists(rutaJuego + "colecciones/"))
            {
                Directory.CreateDirectory(rutaJuego + "colecciones/");
            }
        }

        public void actualizarColeccion(String usuario,String ruta) {
            XmlDocument documentoXMl = new XmlDocument();
            documentoXMl = Consultas.consultarApiColección(usuario);
            documentoXMl.Save(ruta+"colecciones/" + usuario);
        }
    }
}
