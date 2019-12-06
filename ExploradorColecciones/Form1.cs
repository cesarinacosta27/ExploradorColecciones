using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
namespace ExploradorColecciones
{
    public partial class Form1 : Form
    {
        String directorioCache;
        String directorioCacheUsuarios;
        String directorioCacheJuegos;
        private String nombreUsuario;
        coleccionJuegos coleccion;
        Jugadas jugadas= new Jugadas();
        public Dictionary<String, String> indiceImagenArbol = new Dictionary<String, String>();
        private Image imagenAutor;
        private Image imagenJuegos;
        private Image imagenMecanicas;
        private Image imagenNJugadores;
        private Image imagenCategoria;
        private Image imagenFamilia;
        private Image imagenNombre;
        private Image imagenAdversario;
        private Image imagenResumen;
        public enum NodoRaiz {
            Mecanicas,
            Familia,
            Categoria,
            Autor,
            Juego,
            Nombre
        }
        public Form1()
        {
            InitializeComponent();
        }

        public void asegurarExistenciaDirectorioCache() {
            directorioCache = Application.LocalUserAppDataPath;
            directorioCacheUsuarios = directorioCache + "/usuarios/";
            directorioCacheJuegos = directorioCache + "/juegos/";
            if (!Directory.Exists(directorioCacheUsuarios)) {
                Directory.CreateDirectory(directorioCacheUsuarios);
            }
            if (!Directory.Exists(directorioCacheJuegos)) {
                Directory.CreateDirectory(directorioCacheJuegos);
            }
        }
        private void Form1_Load(object sender, EventArgs e) {
            asegurarExistenciaDirectorioCache();
            guardarImagenesArbol();
             imagenAutor = Image.FromFile(directorioCacheJuegos + "img/thubmnail/autor.png");
             imagenJuegos = Image.FromFile(directorioCacheJuegos + "img/thubmnail/juegos.png");
             imagenMecanicas = Image.FromFile(directorioCacheJuegos + "img/thubmnail/mecanicas.png");
             imagenNJugadores = Image.FromFile(directorioCacheJuegos + "img/thubmnail/Njugadores.png");
             imagenCategoria = Image.FromFile(directorioCacheJuegos + "img/thubmnail/categoria.png");
             imagenFamilia = Image.FromFile(directorioCacheJuegos + "img/thubmnail/familia.png");
             imagenNombre = Image.FromFile(directorioCacheJuegos + "img/thubmnail/nombre.png");
             imagenAdversario = Image.FromFile(directorioCacheJuegos + "img/thubmnail/adversario.png");
            imagenResumen = Image.FromFile(directorioCacheJuegos + "img/thubmnail/resumen.png");
            imglArbol.Images.Add(imagenAutor);
            imglArbol.Images.Add(imagenJuegos);
            imglArbol.Images.Add(imagenMecanicas);
            imglArbol.Images.Add(imagenNJugadores);
            imglArbol.Images.Add(imagenCategoria);
            imglArbol.Images.Add(imagenFamilia);
            imglArbol.Images.Add(imagenNombre);
            imglArbol.Images.Add(imagenAdversario);
            imglArbol.Images.Add(imagenResumen);
            lblElementosEncontrados.Visible = false;
            lblElementosEncontradosTitulo.Visible = false;
            reiniciarArbol();
            tvArbol.Visible = false;
        }

        private void BtnBuscar_Click(object sender, EventArgs e) {
            if (txtNombreUsuario.TextLength > 0) { 
                 coleccion = new coleccionJuegos(txtNombreUsuario.Text,directorioCacheJuegos);
                if (coleccion.totalColeccion > 0) {
                    reiniciarComponentes();
                    reiniciarArbol();
                    agregarNodosArbol(coleccion.autores, NodoRaiz.Autor);
                    agregarNodosArbolJuegos(coleccion.juegos);
                    agregarNodosArbol(coleccion.mecanicas, NodoRaiz.Mecanicas);
                    agregarNodosArbol(coleccion.familia, NodoRaiz.Familia);
                    agregarNodosArbol(coleccion.categoria, NodoRaiz.Categoria);
                    nombreUsuario = txtNombreUsuario.Text;
                    jugadas = new Jugadas(nombreUsuario, coleccion.idJuego, directorioCacheUsuarios, coleccion.mecanicas);
                    agregarNodosArbolAdversarios(jugadas.juegoJugadorResultado, NodoRaiz.Juego);
                    agregarNodosArbolAdversarios(jugadas.jugadorJuegoResultado, NodoRaiz.Nombre);
                    tvArbol.Visible = true;
                } else {
                    MessageBox.Show("El ususario no cuenta con objetos en su coleccion");
                }
            } else {
                MessageBox.Show("Ingrese un Nombre de Usuario");
            }
        }
        public void agregarNodosArbolAdversarios(SortedDictionary<String, SortedDictionary<String, int[]>> colecciones, NodoRaiz nodoRaiz) {
            TreeNode nodoAgregar = new TreeNode();
            if (nodoRaiz == NodoRaiz.Juego) {
                nodoAgregar = tvArbol.GetNodeAt(0, 0).NextVisibleNode.NextVisibleNode.FirstNode;
            } else if (nodoRaiz == NodoRaiz.Nombre) {
                nodoAgregar = tvArbol.GetNodeAt(0, 0).NextVisibleNode.NextVisibleNode.FirstNode.NextNode;
            }
            ///SortedDictionary<String, SortedDictionary<String, int[]>>
            foreach (var item in colecciones) {
                TreeNode agregarNodo = new TreeNode(item.Key);
                if (nodoRaiz == NodoRaiz.Juego) {
                    Juego juego = coleccion.idJuego[item.Key];
                    agregarNodo = new TreeNode(juego.nombre);
                    agregarNodo.Tag = "J"+juego.idBgg;
                    agregarNodo.ImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                    agregarNodo.SelectedImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                } else if (nodoRaiz == NodoRaiz.Nombre) {
                    Juego juego;
                    if (coleccion.idJuego.TryGetValue(item.Key, out juego)) {
                        agregarNodo = new TreeNode(juego.nombre);
                        agregarNodo.Tag = "N" + juego.idBgg;
                        agregarNodo.ImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                        agregarNodo.SelectedImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                    } else {
                        agregarNodo.Tag = "N";
                        agregarNodo.ImageIndex = 6;
                        agregarNodo.SelectedImageIndex = 6;
                        //agregarNodo.Tag = "0000000";
                    }
                }
                foreach (var jugadorResultado in item.Value) {
                    TreeNode nodoJugador = new TreeNode(jugadorResultado.Key, 0, 0);
                    if (nodoRaiz == NodoRaiz.Nombre) {
                        Juego juego = coleccion.idJuego[jugadorResultado.Key];
                        nodoJugador = new TreeNode(juego.nombre);
                        if (agregarNodo.Tag.ToString().Length == 1) {
                            nodoJugador.Tag = "NR" + juego.idBgg;
                        } else {
                            nodoJugador.Tag = "NRJ" + juego.idBgg;
                        }
                        nodoJugador.ImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]); 
                        nodoJugador.SelectedImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                    } else {
                        Juego juego;
                        if(coleccion.idJuego.TryGetValue(jugadorResultado.Key,out juego)) {
                            nodoJugador = new TreeNode(juego.nombre);
                            nodoJugador.Tag = "JRJ"+juego.idBgg;
                            nodoJugador.ImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                            nodoJugador.SelectedImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                        } else {
                            nodoJugador.Tag = "JR"+item.Key;
                            nodoJugador.ImageIndex = 6;
                            nodoJugador.SelectedImageIndex = 6;
                        }
                        
                    } 
                    agregarNodo.Nodes.Add(nodoJugador);
                }
                nodoAgregar.Nodes.Add(agregarNodo);
                }
        }
        public void agregarNodosArbol(SortedDictionary<String, List<String>> colecciones,NodoRaiz nodoRaiz) {
            TreeNode nodoAgregar=new TreeNode();
            if (nodoRaiz == NodoRaiz.Autor) {
                 nodoAgregar= tvArbol.GetNodeAt(0, 0);
            } else if (nodoRaiz == NodoRaiz.Mecanicas) {
                nodoAgregar = tvArbol.GetNodeAt(0, 0).NextVisibleNode.FirstNode.NextNode.NextNode.NextNode;
            } else if (nodoRaiz == NodoRaiz.Familia) {
                nodoAgregar = tvArbol.GetNodeAt(0, 0).NextVisibleNode.FirstNode.NextNode.NextNode;
            } else if (nodoRaiz == NodoRaiz.Categoria) {
                nodoAgregar = tvArbol.GetNodeAt(0, 0).NextVisibleNode.FirstNode.NextNode;
            }
            foreach (var item in colecciones) {
                TreeNode agregarNodo = new TreeNode(item.Key);
                if (nodoRaiz == NodoRaiz.Autor) {
                    agregarNodo.Tag = "0";
                    agregarNodo.ImageIndex = 0;
                    agregarNodo.SelectedImageIndex = 0;
                } else if (nodoRaiz == NodoRaiz.Mecanicas) {
                    agregarNodo.Tag = "000";
                    agregarNodo.ImageIndex = 2;
                    agregarNodo.SelectedImageIndex = 2;
                } else if (nodoRaiz == NodoRaiz.Familia) {
                    agregarNodo.Tag = "0000";
                    agregarNodo.ImageIndex = 5;
                    agregarNodo.SelectedImageIndex = 5;
                } else if (nodoRaiz == NodoRaiz.Categoria) {
                    agregarNodo.Tag = "00000";
                    agregarNodo.ImageIndex = 4;
                    agregarNodo.SelectedImageIndex = 4;
                }
                
                foreach (var iJjuego in item.Value) {
                    Juego juego = coleccion.idJuego[iJjuego];
                    TreeNode nodoJuego = new TreeNode(juego.nombre, 0, 0);
                    nodoJuego.Tag = juego.idBgg;
                    if (indiceImagenArbol.ContainsKey(juego.idBgg)) {
                        nodoJuego.ImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                        nodoJuego.SelectedImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                    } else {
                        nodoJuego.ImageIndex = imglArbol.Images.Count;
                        nodoJuego.SelectedImageIndex = imglArbol.Images.Count;
                        indiceImagenArbol.Add(juego.idBgg, imglArbol.Images.Count.ToString());
                        imglArbol.Images.Add(juego.cargarImagenMiniatura(directorioCacheJuegos, juego.idBgg));
                    }
                    agregarNodo.Nodes.Add(nodoJuego);
                }
                nodoAgregar.Nodes.Add(agregarNodo);
            }
        }

       
        public void agregarNodosArbolJuegos(SortedDictionary<int, List<String>> colecciones) {
            TreeNode nodoNumJugadores= tvArbol.GetNodeAt(0, 0).NextVisibleNode.FirstNode;
            foreach (var item in colecciones) {
                TreeNode nodoAgregar = nodoNumJugadores;
                TreeNode agregarNodo = new TreeNode(item.Key.ToString());
                agregarNodo.Tag = "00";
                agregarNodo.ImageIndex = 3;
                agregarNodo.SelectedImageIndex = 3;
                foreach (var idJuego in item.Value) {
                    Juego juego = coleccion.idJuego[idJuego];
                    TreeNode nodoJuego = new TreeNode(juego.nombre, 0, 0);
                    nodoJuego.Tag = juego.idBgg;
                    if (indiceImagenArbol.ContainsKey(juego.idBgg)) {
                        nodoJuego.ImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                        nodoJuego.SelectedImageIndex = Int32.Parse(indiceImagenArbol[juego.idBgg]);
                    } else {
                        nodoJuego.ImageIndex = imglArbol.Images.Count;
                        nodoJuego.SelectedImageIndex = imglArbol.Images.Count;
                        indiceImagenArbol.Add(juego.idBgg, imglArbol.Images.Count.ToString());
                        imglArbol.Images.Add(juego.cargarImagenMiniatura(directorioCacheJuegos, juego.idBgg));
                    }
                    agregarNodo.Nodes.Add(nodoJuego);
                }
                nodoAgregar.Nodes.Add(agregarNodo);
            }
        }

        public void clickJuego() {
            reiniciarComponentes();
            foreach (var item in jugadas.juegoJugadorResultado) {
                Juego juego = coleccion.idJuego[item.Key];
                agregarJuegoListViewAdversario(juego,juego.idBgg,NodoRaiz.Juego);
            }
        }
        public void clickNombre() {
            reiniciarComponentes();
            foreach (var item in jugadas.jugadorJuegoResultado) {
                Juego juego;
                if(coleccion.idJuego.TryGetValue(item.Key,out juego)) {
                    agregarJuegoListViewAdversario(juego,"-2", NodoRaiz.Nombre);
                } else {
                    agregarAutorListView(item.Key, 6);
                }
            }
        }

        private void TvArbol_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node.Tag.Equals("Juego")) {
                clickJuego();
            } else if (e.Node.Tag.Equals("J")) {
                if (e.Node.GetNodeCount(true) > 1) {
                    clickNumJugadores();
                }
                mostrarCantidadElementos(e);
            } else if (e.Node.Tag.Equals("Ju")) {
                reiniciarComponentes();
                agregarAutorListView("# Jugadores", 3);
                agregarAutorListView("Categoria", 4);
                agregarAutorListView("Familia", 5);
                agregarAutorListView("Mecanicas", 2);
                mostrarCantidadElementos(e);
            } else if (e.Node.Tag.Equals("AD")) {
                reiniciarComponentes();
                agregarAutorListView("Juego", 1);
                agregarAutorListView("Nombre", 6);
            } else if(e.Node.Tag.Equals("Nombre")) {
                clickNombre();   
            } else if (e.Node.Tag.ToString().StartsWith("N")) {
                if (e.Node.Tag.ToString().StartsWith("NR")) {
                    String idBgg="";
                    //nr los que son personas nrj los que son juego
                    String key;
                    if (e.Node.Tag.ToString().StartsWith("NRJ")) {
                        key = e.Node.Tag.ToString().Substring(3);
                        idBgg = e.Node.Tag.ToString().Substring(3);
                    } else {
                        key = e.Node.Parent.Text;
                        idBgg = e.Node.Tag.ToString().Substring(2);
                    }
                    mostarDatosJuego(idBgg);
                    mostrarDatosAdversario(key, idBgg, jugadas.jugadorJuegoResultado, NodoRaiz.Nombre);
                } else {
                    String key;
                    if (e.Node.Tag.ToString().Length > 1) {
                        key = e.Node.Tag.ToString().Substring(1);
                    } else {
                        key = e.Node.Text;
                    }
                    reiniciarComponentes();
                    SortedDictionary<String, int[]> juegoResultado;
                    if (jugadas.jugadorJuegoResultado.TryGetValue(key, out juegoResultado)) {
                        reiniciarComponentes();
                        foreach (var item in juegoResultado) {
                            Juego juego = coleccion.idJuego[item.Key];
                            agregarJuegoListViewAdversario(juego, key, NodoRaiz.Nombre);
                        }
                    }
                }
            } else if (e.Node.Tag.ToString().StartsWith("J")) {
                if (e.Node.Tag.ToString().StartsWith("JR")) {
                    String idBgg = "";
                    //jr los que son personas jr14564 los que son juego
                    String key;
                    if (e.Node.Tag.ToString().StartsWith("JRJ")) {
                        key = e.Node.Tag.ToString().Substring(3);
                        idBgg = e.Node.Tag.ToString().Substring(3);
                    } else {
                        key = e.Node.Text;
                        idBgg = e.Node.Tag.ToString().Substring(2);
                    }
                  mostarDatosJuego(idBgg);
                   mostrarDatosAdversario(idBgg, key, jugadas.juegoJugadorResultado, NodoRaiz.Nombre);
                } else {
                    String key="";
                    if (e.Node.Tag.ToString().Length > 1) {
                        key = e.Node.Tag.ToString().Substring(1);
                    } 
                    if (jugadas.juegoJugadorResultado.ContainsKey(key)) {
                        SortedDictionary<String, int[]> juegoResultado;
                        if (jugadas.juegoJugadorResultado.TryGetValue(key, out juegoResultado)) {
                            reiniciarComponentes();
                            foreach (var item in juegoResultado) {
                                if (coleccion.idJuego.ContainsKey(item.Key)) {
                                    Juego juego = coleccion.idJuego[item.Key];
                                    agregarJuegoListViewAdversario(juego, "A1" + juego.idBgg, NodoRaiz.Nombre);
                                } else {
                                    agregarAdversarioListView(item.Key, key, 6);
                                }
                                    
                            }
                        }
                    }
                }
            } else if (e.Node.Tag.Equals("A")) {
                if (e.Node.GetNodeCount(true) > 1) {
                    reiniciarComponentes();
                    foreach (var item in coleccion.autores) {
                        agregarAutorListView(item.Key,0);
                    }
                }
                mostrarCantidadElementos(e);
            } else if (e.Node.Tag.Equals("M")) {
                clickMecanicas();
                mostrarCantidadElementos(e);
            } else if (e.Node.Tag.Equals("F")) {
                clickFamilia();
                mostrarCantidadElementos(e);
            } else if (e.Node.Tag.Equals("C")) {
                clickCategoria();
                mostrarCantidadElementos(e);
            } else if (e.Node.Tag.Equals("Resumen")) {
                if (coleccion!=null) {
                    if (coleccion.idJuego.ContainsKey(jugadas.idJuegoMasJugado.ToString())) {
                        Juego juego = coleccion.idJuego[jugadas.idJuegoMasJugado.ToString()];
                        MessageBox.Show("Tienes mas juegos de " + coleccion.masJuegosAutor + "\nEl juego que mas juegas " + juego.nombre
                            + "\nLe has ganado mas veces a " + jugadas.MayorPerdedor + "\nTe ha ganado mas veces " + jugadas.MayorGanador);
                    }
                } else {
                    MessageBox.Show("Tienes que consultar un jugador");
                }
            } else {
                mostrarListViewTag(e.Node.Tag.ToString(), e.Node.Text);
                mostrarCantidadElementos(e);
            }
            int valorTag;
            try {
                valorTag = Int32.Parse(e.Node.Tag.ToString());
            } catch (Exception excep) {
                valorTag = -1;
            }
            if (valorTag>0) {
                mostrarListViewTag(e.Node.Parent.Tag.ToString(), e.Node.Parent.Text);
                mostarDatosJuego(e.Node.Tag.ToString());
                mostrarDatosJugadaJuego(e.Node.Tag.ToString());
                mostrarCantidadElementos(e);
            } 
        }

        public void mostrarListViewTag(String Tag,String texto) {
            if (Tag.Equals("0")) {
                mostrarJuegosAutorListView(coleccion.autores, texto);
            } else if (Tag.Equals("00")) {
                mostrarJuegosJuegosListView(coleccion.juegos, Int32.Parse(texto));
            } else if (Tag.Equals("000")) {
                mostrarJuegosAutorListView(coleccion.mecanicas, texto);
            } else if (Tag.Equals("0000")) {
                mostrarJuegosAutorListView(coleccion.familia, texto);
            } else if (Tag.Equals("00000")) {
                mostrarJuegosAutorListView(coleccion.categoria, texto);
            } else if (Tag.Equals("000000")) {
                
            } else if (Tag.Equals("0000000")) {
                
            }
        }
        private void mostrarCantidadElementos(TreeNodeMouseClickEventArgs e) {
            TreeNode nodoPadre;
            int valorTag=-1;
            try {
               valorTag= Int32.Parse(e.Node.Tag.ToString());
            } catch (Exception exception) { }
            if(valorTag>0) {
                nodoPadre = e.Node.Parent;
            } else {
                nodoPadre = e.Node;
            }
            lblElementosEncontrados.Text =nodoPadre.GetNodeCount(false).ToString();
            lblElementosEncontrados.Visible = true;
            lblElementosEncontradosTitulo.Visible = true;

        }
        private void mostrarJuegosAutorListView(SortedDictionary<String, List<String>> coleccionRecorrido,String autor) {
            reiniciarComponentes();
            List<String> listaJuegos = new List<String>();
            coleccionRecorrido.TryGetValue(autor, out listaJuegos);
            if (listaJuegos != null) {
                foreach (String idJuego in listaJuegos) {
                    Juego juego = coleccion.idJuego[idJuego];
                    agregarJuegoListView(juego);
                }
            }
        }
        private void mostrarJuegosJuegosListView(SortedDictionary<int, List<String>> coleccionRecorrido, int cantidadJugadores) {
            reiniciarComponentes();
            List<String> listaJuegos = new List<String>();
            coleccionRecorrido.TryGetValue(cantidadJugadores, out listaJuegos);
            if (listaJuegos != null) {
                foreach (String idJuego in listaJuegos) {
                    Juego juego = coleccion.idJuego[idJuego];
                    agregarJuegoListView(juego);
                }
            }
        }
        private void agregarJuegoListView(Juego juego) {
            ListViewItem item = new ListViewItem(juego.nombre, imglIconos.Images.Count);
            imglIconos.Images.Add(juego.cargarImagenMiniatura(directorioCacheJuegos, juego.idBgg));
            imglGrandes.Images.Add(juego.cargarImagenMiniatura(directorioCacheJuegos, juego.idBgg));
            item.SubItems.Add(juego.idBgg);
            lvContenido.Items.Add(item);
        }
        private void agregarJuegoListViewAdversario(Juego juego,String tag,NodoRaiz nodoRaiz) {
            ListViewItem item = new ListViewItem(juego.nombre, imglIconos.Images.Count);
            imglIconos.Images.Add(juego.cargarImagenMiniatura(directorioCacheJuegos, juego.idBgg));
            imglGrandes.Images.Add(juego.cargarImagenMiniatura(directorioCacheJuegos, juego.idBgg));
            if (nodoRaiz == NodoRaiz.Nombre) {
                item.SubItems.Add(tag);
                item.Tag = juego.idBgg;
            } else {
                item.SubItems.Add("-1");
                item.Tag = tag;
            }
            
            lvContenido.Items.Add(item);
        }
        private void agregarAutorListView(String autor,int indice) {
            ListViewItem item = new ListViewItem(autor, indice);
            item.SubItems.Add("0");
            lvContenido.Items.Add(item);
        }
        private void agregarAdversarioListView(String autor,String idBgg, int indice) {
            ListViewItem item = new ListViewItem(autor, indice);
            item.SubItems.Add("A1"+idBgg);
            lvContenido.Items.Add(item);
        }
        private void reiniciarComponentes() {
            pnlDetalles.Visible = false;
            lvContenido.Items.Clear();
            imglGrandes.Images.Clear();
            imglIconos.Images.Clear();
            imglGrandes.Images.Add(imagenAutor);
            imglIconos.Images.Add(imagenAutor);
            imglGrandes.Images.Add(imagenJuegos);
            imglIconos.Images.Add(imagenJuegos);
            imglGrandes.Images.Add(imagenMecanicas);
            imglIconos.Images.Add(imagenMecanicas);
            imglGrandes.Images.Add(imagenNJugadores);
            imglIconos.Images.Add(imagenNJugadores);
            imglGrandes.Images.Add(imagenCategoria);
            imglIconos.Images.Add(imagenCategoria);
            imglGrandes.Images.Add(imagenFamilia);
            imglIconos.Images.Add(imagenFamilia);
            imglGrandes.Images.Add(imagenNombre);
            imglIconos.Images.Add(imagenNombre);
            imglGrandes.Images.Add(imagenAdversario);
            imglIconos.Images.Add(imagenAdversario);
        }
        public void reiniciarArbol() {
            tvArbol.Nodes.Clear();
            TreeNode nodoRaiz = new TreeNode("Autores", 0, 0);
            nodoRaiz.Tag = "A";
            nodoRaiz.ImageIndex = 0;
            nodoRaiz.SelectedImageIndex = 0;
            tvArbol.Nodes.Add(nodoRaiz);
            TreeNode nodoJuegos = new TreeNode("Juegos", 0, 0);
            nodoJuegos.Tag = "Ju";
            nodoJuegos.ImageIndex = 1;
            nodoJuegos.SelectedImageIndex = 1;
            TreeNode numJugadores = new TreeNode("# Jugadores", 0, 0);
            numJugadores.Tag = "J";
            numJugadores.ImageIndex = 3;
            numJugadores.SelectedImageIndex = 3;
            nodoJuegos.Nodes.Add(numJugadores);
            TreeNode nodoCategoria = new TreeNode("Categoria", 0, 0);
            nodoCategoria.Tag = "C";
            nodoCategoria.ImageIndex = 4;
            nodoCategoria.SelectedImageIndex = 4;
            nodoJuegos.Nodes.Add(nodoCategoria);
            TreeNode nodoFamilia = new TreeNode("Familia", 0, 0);
            nodoFamilia.Tag = "F";
            nodoFamilia.ImageIndex = 5;
            nodoFamilia.SelectedImageIndex = 5;
            nodoJuegos.Nodes.Add(nodoFamilia);
            TreeNode nodoMecanicas = new TreeNode("Mecanicas", 0, 0);
            nodoMecanicas.Tag = "M";
            nodoMecanicas.ImageIndex = 2;
            nodoMecanicas.SelectedImageIndex = 2;
            nodoJuegos.Nodes.Add(nodoMecanicas);
            tvArbol.Nodes.Add(nodoJuegos);
            TreeNode nodoAdversarios = new TreeNode("Adversarios", 0, 0);
            nodoAdversarios.Tag = "AD";
            nodoAdversarios.ImageIndex = 7;
            nodoAdversarios.SelectedImageIndex = 7;
            TreeNode nodoJuego = new TreeNode("Juego", 0, 0);
            nodoJuego.Tag = "Juego";
            nodoJuego.ImageIndex = 1;
            nodoJuego.SelectedImageIndex = 1;
            nodoAdversarios.Nodes.Add(nodoJuego);
            TreeNode nodoNombre = new TreeNode("Nombre", 0, 0);
            nodoNombre.Tag = "Nombre";
            nodoNombre.ImageIndex = 6;
            nodoNombre.SelectedImageIndex = 6;
            nodoAdversarios.Nodes.Add(nodoNombre);
            tvArbol.Nodes.Add(nodoAdversarios);
            TreeNode nodoResumen = new TreeNode("Resumen");
            nodoResumen.Tag = "Resumen";
            nodoResumen.ImageIndex = 8;
            nodoResumen.SelectedImageIndex = 8;
            tvArbol.Nodes.Add(nodoResumen);
        }

        private void LvContenido_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (e.Item.SubItems[1].Text.Equals("0")) {
                if (coleccion.autores.ContainsKey(e.Item.Text)) {
                    mostrarJuegosAutorListView(coleccion.autores, e.Item.Text);
                } else if (e.Item.Text.Equals("# Jugadores")) {
                    clickNumJugadores();
                } else if (e.Item.Text.Equals("Mecanicas")) {
                    clickMecanicas();
                } else if (e.Item.Text.Equals("Familia")) {
                    clickFamilia();
                } else if (e.Item.Text.Equals("Categoria")) {
                    clickCategoria();
                } else if (e.Item.Text.Equals("Juego")) {
                    clickJuego();
                } else if (e.Item.Text.Equals("Nombre")) {
                    clickNombre();
                } else if (coleccion.mecanicas.ContainsKey(e.Item.Text)) {
                    mostrarJuegosAutorListView(coleccion.mecanicas, e.Item.Text);
                } else if (coleccion.familia.ContainsKey(e.Item.Text)) {
                    mostrarJuegosAutorListView(coleccion.familia, e.Item.Text);
                } else if (coleccion.categoria.ContainsKey(e.Item.Text)) {
                    mostrarJuegosAutorListView(coleccion.categoria, e.Item.Text);
                } else if (jugadas.jugadorJuegoResultado.ContainsKey(e.Item.Text)) {
                    SortedDictionary<String, int[]> juegoResultado;
                    if (jugadas.jugadorJuegoResultado.TryGetValue(e.Item.Text, out juegoResultado)) {
                        reiniciarComponentes();
                        foreach (var item in juegoResultado) {
                            Juego juego = coleccion.idJuego[item.Key];
                            agregarJuegoListViewAdversario(juego, e.Item.Text,NodoRaiz.Nombre);
                        }
                    }
                } else if (coleccion.juegos.ContainsKey(Int32.Parse(e.Item.Text))) {
                    mostrarJuegosJuegosListView(coleccion.juegos, Int32.Parse(e.Item.Text));
                }
            } else if(e.Item.SubItems[1].Text.StartsWith("A1")){
                //jugadas.juegoJugadorResultado;
                mostarDatosJuego(e.Item.SubItems[1].Text.Substring(2)); 
                mostrarDatosAdversario(e.Item.SubItems[1].Text.Substring(2),e.Item.Text,jugadas.juegoJugadorResultado,NodoRaiz.Juego);
            } else if (jugadas.jugadorJuegoResultado.ContainsKey(e.Item.SubItems[1].Text)) {
                mostarDatosJuego(e.Item.Tag.ToString());
                mostrarDatosAdversario(e.Item.SubItems[1].Text, e.Item.Tag.ToString(), jugadas.jugadorJuegoResultado,NodoRaiz.Nombre);
            } else if (e.Item.Tag!=null) {
                if (!e.Item.SubItems[1].Text.Equals("-2")) {
                    if (jugadas.juegoJugadorResultado.ContainsKey(e.Item.Tag.ToString())) {
                        SortedDictionary<String, int[]> juegoResultado;
                        if (jugadas.juegoJugadorResultado.TryGetValue(e.Item.Tag.ToString(), out juegoResultado)) {
                            reiniciarComponentes();
                            foreach (var item in juegoResultado) {
                                if (coleccion.idJuego.ContainsKey(item.Key)) {
                                    Juego juego = coleccion.idJuego[item.Key];
                                    agregarJuegoListViewAdversario(juego, "A1" + juego.idBgg, NodoRaiz.Nombre);
                                } else {
                                    agregarAdversarioListView(item.Key, e.Item.Tag.ToString(), 6);
                                }
                                
                            }
                        }
                    }
                } else {
                    //SortedDictionary<String, SortedDictionary<String, int[]>> jugadorJuegoResultado
                    SortedDictionary<String, int[]> juegoResultado;
                    if (jugadas.jugadorJuegoResultado.TryGetValue(e.Item.Tag.ToString(),out juegoResultado)) {
                        reiniciarComponentes();
                        Juego juego = coleccion.idJuego[e.Item.Tag.ToString()];
                        agregarJuegoListViewAdversario(juego, e.Item.Tag.ToString(),NodoRaiz.Nombre);
                    }
                }
                
            } else {
                mostarDatosJuego(e.Item.SubItems[1].Text);
                mostrarDatosJugadaJuego(e.Item.SubItems[1].Text);
            }
            
        }
        public void clickNumJugadores() {
            reiniciarComponentes();
            foreach (var item in coleccion.juegos) {
                agregarAutorListView(item.Key.ToString(),3);
            }
        }
        public void clickMecanicas() {
            reiniciarComponentes();
            foreach (var item in coleccion.mecanicas) {
                agregarAutorListView(item.Key,2);
            }
        }
        public void clickFamilia() {
            reiniciarComponentes();
            foreach (var item in coleccion.familia) {
                agregarAutorListView(item.Key,5);
            }
        }
        public void clickCategoria() {
            reiniciarComponentes();
            foreach (var item in coleccion.categoria) {
                agregarAutorListView(item.Key,4);
            }
        }
        private void mostarDatosJuego(String id) {
            Juego juego = new Juego(id, directorioCacheJuegos);
            lblNombreJuego.Text = juego.nombre;
            lstbxAutor.Items.Clear();
            lstbxAutor.BeginUpdate();
            foreach (String autor in juego.autor) {
                lstbxAutor.Items.Add(autor);
            }
            lstbxAutor.EndUpdate();
            lblIlustrador.Text = juego.ilustrador;
            pbxMiniatura.Image = juego.cargarImagenMiniatura(directorioCacheJuegos, juego.idBgg);
            pnlDetalles.Visible = true;
        }
        private void mostrarDatosJugadaJuego(String id) {
            lstbxJugadas.BeginUpdate();
            lstbxJugadas.Items.Clear();
            lstbxJugadas.Items.Add("Total de Partidas Jugadas " + jugadas.totalJugadas[Int32.Parse(id)]);
            if (jugadas.totalJugadas[Int32.Parse(id)] > 0) {
                Dictionary<int, int> diccionarioGanadas = jugadas.totalGanadas[Int32.Parse(id)];
                foreach (var item in diccionarioGanadas) {
                    lstbxJugadas.Items.Add("Total de Partidas Ganadas con " + item.Key + " jugadores: " + item.Value);
                }
                Dictionary<int, int> diccionarioPerdidas = jugadas.totalPerdidas[Int32.Parse(id)];
                foreach (var item in diccionarioPerdidas) {
                    lstbxJugadas.Items.Add("Total de Partidas Perdidas con " + item.Key + " jugadores: " + item.Value);
                }
                Dictionary<int, int> diccionarioJugadasNumeroJugadores = jugadas.jugadasNumeroJugadores[Int32.Parse(id)];
                foreach (var item in diccionarioJugadasNumeroJugadores) {
                    lstbxJugadas.Items.Add("Total de Partidas con " + item.Key + " jugadores: " + item.Value);
                }
            }
            lstbxJugadas.EndUpdate();
        }
        private void mostrarDatosAdversario(String idBgg,String jugador,SortedDictionary<String, SortedDictionary<String, int[]>> juegoJugadorResultado,NodoRaiz nodoRaiz) {
            //SortedDictionary<String, SortedDictionary<String, int[]>> juegoJugadorResultado
            SortedDictionary<String, int[]> jugadorResultado;
            int[] resultado;
            lstbxJugadas.BeginUpdate();
            lstbxJugadas.Items.Clear();
            jugadorResultado= juegoJugadorResultado[idBgg];
            resultado = jugadorResultado[jugador];
            if (nodoRaiz == NodoRaiz.Nombre) {
                Juego juego;
                if (coleccion.idJuego.TryGetValue(idBgg,out juego)) {
                    lstbxJugadas.Items.Add(juego.nombre);
                } else {
                    lstbxJugadas.Items.Add(idBgg);
                }
                
            } else {
                lstbxJugadas.Items.Add(jugador);
            }
            lstbxJugadas.Items.Add("Te ha ganado "+resultado[0].ToString()+" veces");
            lstbxJugadas.Items.Add("Le has ganado " + resultado[1].ToString() + " veces");
            lstbxJugadas.EndUpdate();
        }
        private void BtnActualizarColeccion_Click(object sender, EventArgs e) {
            if(txtNombreUsuario.Text.Length > 0) {
                if(coleccion != null) {
                    jugadas.actualizarJugadas(nombreUsuario, coleccion.idJuego, directorioCacheUsuarios);
                    actualizarColeccion();
                } else {
                    coleccion = new coleccionJuegos();
                    actualizarColeccion();
                }
            } else {
                MessageBox.Show("Ingrese un nombre de usuario");
            }
        }

        private void actualizarColeccion() {
            coleccion.actualizarColeccion(txtNombreUsuario.Text, directorioCacheJuegos);
            MessageBox.Show("Información Actualizada Satisfactoriamente");
        }
        private void guardarImagenesArbol() {
            guardarImagenesMiniatura("https://cdn.icon-icons.com/icons2/11/PNG/256/writer_person_people_man_you_1633.png", directorioCacheJuegos, "autor");
            guardarImagenesMiniatura("https://image.flaticon.com/icons/png/512/103/103236.png", directorioCacheJuegos, "juegos");
            guardarImagenesMiniatura("https://image.flaticon.com/icons/png/512/103/103228.png", directorioCacheJuegos, "mecanicas");
            guardarImagenesMiniatura("https://cdn.onlinewebfonts.com/svg/img_351582.png", directorioCacheJuegos, "Njugadores");
            guardarImagenesMiniatura("https://cdn3.iconfinder.com/data/icons/common-3/100/inbox-2_-512.png", directorioCacheJuegos, "categoria");
            guardarImagenesMiniatura("https://icon-library.net/images/icon-for-family/icon-for-family-9.jpg", directorioCacheJuegos, "familia");
            guardarImagenesMiniatura("https://cdn2.iconfinder.com/data/icons/user-icon-2-1/100/user_5-15-512.png", directorioCacheJuegos, "nombre");
            guardarImagenesMiniatura("https://cdn3.iconfinder.com/data/icons/election-outline/64/Democracy_President_Election_Rival-512.png", directorioCacheJuegos, "adversario");
            guardarImagenesMiniatura("https://image.flaticon.com/icons/png/512/30/30427.png", directorioCacheJuegos, "resumen");
        
        }
        public void guardarImagenesMiniatura(String linkDescarga, String rutaJuego, String nombre) {
            asegurarExistenciaDirectorioMiniaturas(rutaJuego);
            String rutaCompleta = rutaJuego + "img/thubmnail/" + nombre + ".png";
            if (!File.Exists(rutaCompleta)) { 
                WebClient clienteDescarga = new WebClient();
                clienteDescarga.DownloadFile(linkDescarga,rutaCompleta);
            }
        }
        public void asegurarExistenciaDirectorioMiniaturas(String rutaJuego) {
            if (!Directory.Exists(rutaJuego + "img/thubmnail/")) {
                Directory.CreateDirectory(rutaJuego + "img/thubmnail/");
            }
        }
    }
}

