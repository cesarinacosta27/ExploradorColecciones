using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
namespace ExploradorColecciones {
    class Jugadas {
        public int idJuegoMasJugado;
        public String MayorGanador;
        public String MayorPerdedor;
        public Dictionary<int, int> totalJugadas= new Dictionary<int, int>();
        //key idBgg elementKey cantidad de jugadores en la partida 
        public Dictionary<int, Dictionary<int,int>> totalGanadas = new Dictionary<int, Dictionary<int, int>> ();
        public Dictionary<int, Dictionary<int, int>> totalPerdidas = new Dictionary<int, Dictionary<int, int>> ();
        public Dictionary<int, Dictionary<int, int>> jugadasNumeroJugadores = new Dictionary<int, Dictionary<int, int>>();
        public SortedDictionary<String, SortedDictionary<String, int[]>> juegoJugadorResultado = new SortedDictionary<string, SortedDictionary<string, int[]>>();
        public SortedDictionary<String, SortedDictionary<String, int[]>> jugadorJuegoResultado = new SortedDictionary<string, SortedDictionary<string, int[]>>();
        private String directorioCacheUsuarioPlays;
        XmlDocument documentoJugadasJuego = new XmlDocument();
        public Jugadas(String usuario, Dictionary<String, Juego> idJuego, String directorioCacheUsuarios, SortedDictionary<String, List<String>> mecanicas) {
            cargarDatosJugadas( usuario,  idJuego,  directorioCacheUsuarios, mecanicas);
            obtenerJuegoMasJugado();
            obtenerMayorGanadorMayorPerdedor(usuario,idJuego);
        }
        public Jugadas() { }
        //SortedDictionary<String, SortedDictionary<String, int[]>>
        //jugadorJuegoResultado
        public void obtenerMayorGanadorMayorPerdedor(String usuario,Dictionary<String, Juego> idJuego) {
            String keyJugadorPerdidas="";
            int perdidasAcumuladasJuego=0;
            int perdidasAcumuladas = 0;
            String keyJugadorGanadas="";
            int ganadasAcumuladasJuego=0;
            int ganadasAcumuladas = 0;
            foreach (var item in jugadorJuegoResultado) {
                ganadasAcumuladasJuego = 0;
                perdidasAcumuladasJuego = 0;
                if (!item.Key.ToLower().Equals("anonymous player")) {
                    foreach (var item2 in item.Value) {
                        ganadasAcumuladasJuego = ganadasAcumuladasJuego + item2.Value[0];
                        perdidasAcumuladasJuego = perdidasAcumuladasJuego + item2.Value[1];
                    }
                    if (ganadasAcumuladasJuego > ganadasAcumuladas) {
                        keyJugadorGanadas = item.Key;
                        ganadasAcumuladas = ganadasAcumuladasJuego;
                    }
                    if (perdidasAcumuladasJuego > perdidasAcumuladas) {
                        keyJugadorPerdidas = item.Key;
                        perdidasAcumuladas = perdidasAcumuladasJuego;
                    } 
                   //Console.WriteLine("++" + item.Key + " ganadas " + ganadasAcumuladasJuego + " perdidas " + perdidasAcumuladasJuego);
                }
            }
            MayorGanador = keyJugadorGanadas;
            MayorPerdedor = keyJugadorPerdidas;
        }
        public void obtenerJuegoMasJugado() {
            int keyJuegoMasJugado = 0;
            int control = 0;
            foreach (var item in totalJugadas) {
                if (item.Value > control) {
                    keyJuegoMasJugado = item.Key;
                    control = item.Value;
                }
            }
            idJuegoMasJugado = keyJuegoMasJugado;
        }
        
        public void actualizarJugadas(String usuario, Dictionary<String, Juego> idJuego, String directorioCacheUsuarios) {
            foreach (var idBgg in idJuego) {
                asegurarExistenciaDirectorioUsuarioPlays(usuario, directorioCacheUsuarios, idBgg.Key);
                documentoJugadasJuego = Consultas.consultarApiJugadasJuego(usuario, idBgg.Key);
                documentoJugadasJuego.Save(directorioCacheUsuarioPlays + idBgg.Key);
            }
        }

        public void cargarDatosJugadas(String usuario, Dictionary<String, Juego> idJuego,String directorioCacheUsuarios, SortedDictionary<String, List<String>> mecanicas) {
            String nombreJugador;
            String numeroJuego;
            foreach (var idBgg in idJuego) {
                asegurarExistenciaDirectorioUsuarioPlays(usuario, directorioCacheUsuarios, idBgg.Key);
                if (File.Exists(directorioCacheUsuarioPlays + idBgg.Key)) {
                    documentoJugadasJuego.Load(directorioCacheUsuarioPlays + idBgg.Key);
                } else {
                    documentoJugadasJuego = Consultas.consultarApiJugadasJuego(usuario, idBgg.Key);
                    documentoJugadasJuego.Save(directorioCacheUsuarioPlays + idBgg.Key);
                }
                try {
                    totalJugadas[Int32.Parse(idBgg.Key)] = Int32.Parse(documentoJugadasJuego.DocumentElement.SelectSingleNode("/plays").Attributes["total"].Value);
                } catch (Exception e) { Console.WriteLine("excepcion total" + e.ToString()); }
                XmlNodeList Jugadores = documentoJugadasJuego.DocumentElement.SelectNodes("/plays/play");
                if (Jugadores != null) {
                    foreach (XmlNode nodoJugadores in Jugadores) {
                        int totalGanadas = 0;
                        int totalJugadores = 0;
                        numeroJuego = nodoJugadores.SelectSingleNode("item").Attributes["objectid"].Value;
                        
                        XmlNodeList listaNodosJugadores = nodoJugadores.SelectNodes("players/player");
                        if (listaNodosJugadores != null) {
                            totalJugadores = listaNodosJugadores.Count;
                            bool agregadoJuegoJugador = false;
                            bool agregadoJugadorJuego = false;
                            foreach (XmlNode jugador in listaNodosJugadores) {
                                String username = jugador.Attributes["username"].Value;
                                String win = jugador.Attributes["win"].Value; ;
                                nombreJugador = jugador.Attributes["name"].Value;
                                if (usuario.Equals(username)) {
                                    if (win.Equals("1")) { totalGanadas++; }
                                }
                                ////aqui agrego los datos al sortedDictionary 
                                /////SortedDictionary<String, SortedDictionary<String, int[]>> juegoJugadorResultado
                                if (username != usuario) { 
                                List<String> listaMecanicas;
                                if (mecanicas.TryGetValue("Cooperative Game", out listaMecanicas)) {
                                    if (listaMecanicas.Contains(numeroJuego)) {
                                        if (!agregadoJuegoJugador) {
                                            agregarDatosDiccionario(numeroJuego, numeroJuego, invertirWin(win), juegoJugadorResultado);
                                            agregadoJuegoJugador = true;
                                        }
                                    } else {
                                        if (username.Equals("")) {
                                            agregarDatosDiccionario(numeroJuego, nombreJugador, win, juegoJugadorResultado);
                                        } else {
                                            agregarDatosDiccionario(numeroJuego, username, win, juegoJugadorResultado);
                                        }
                                        
                                    }
                                } else {
                                    Console.WriteLine("chale");
                                }
                                ////aqui agrego los datos al sortedDictionary 
                                /////SortedDictionary<String, SortedDictionary<String, int[]>> jugadorJuegoResultado
                                if (mecanicas.TryGetValue("Cooperative Game", out listaMecanicas)) {
                                    if (listaMecanicas.Contains(numeroJuego)) {
                                        if (!agregadoJugadorJuego) {
                                            agregarDatosDiccionario(numeroJuego, numeroJuego, invertirWin(win), jugadorJuegoResultado);
                                            agregadoJugadorJuego = true;
                                        }
                                    } else {
                                            if (username.Equals("")) {
                                                agregarDatosDiccionario(nombreJugador, numeroJuego, win, jugadorJuegoResultado);
                                            } else {
                                                agregarDatosDiccionario(username, numeroJuego, win, jugadorJuegoResultado);
                                            }
                                    }
                                } else {
                                    Console.WriteLine("chale");
                                }
                               }
                            }
                            agregarDatosDictionary(this.totalGanadas, Int32.Parse(idBgg.Key), totalJugadores, totalGanadas);
                            if (totalGanadas > 0) {
                                agregarDatosDictionary(this.totalPerdidas, Int32.Parse(idBgg.Key), totalJugadores, 0);
                            } else {
                                agregarDatosDictionary(this.totalPerdidas, Int32.Parse(idBgg.Key), totalJugadores, 1);
                            }
                            agregarDatosDictionary(this.jugadasNumeroJugadores, Int32.Parse(idBgg.Key), totalJugadores, 1);
                            
                        } else {
                            agregarDatosDictionary(this.totalGanadas, Int32.Parse(idBgg.Key), totalJugadores, 0);
                            agregarDatosDictionary(this.totalPerdidas, Int32.Parse(idBgg.Key), totalJugadores, 0);
                            agregarDatosDictionary(this.jugadasNumeroJugadores, Int32.Parse(idBgg.Key), totalJugadores, 0);
                        }
                    }
                } else {
                    ///no hay ningun jugador
                }
            }
                
            }
        

        private void agregarDatosDiccionario(string nombreJugador, string nombreJuego, string win, SortedDictionary<String, SortedDictionary<String, int[]>> diccionario) {
            SortedDictionary<String, int[]> juegoResultado;
            if (diccionario.TryGetValue(nombreJugador, out juegoResultado)) {
                agregarJugadorResultado(nombreJuego, win, juegoResultado);
                diccionario[nombreJugador] = juegoResultado;
            } else {
                //no hay juego agregado con ese id
                juegoResultado = new SortedDictionary<string, int[]>();
                agregarJugadorResultado(nombreJuego, win, juegoResultado);
                diccionario.Add(nombreJugador, juegoResultado);
            }
        }
        private static String invertirWin(String win) {
            if (win.Equals("1")) {
                return "0";
            } else {
                return "1";
            }
        }
        private static void agregarJugadorResultado(string nombreJugador, string win, SortedDictionary<string, int[]> jugadorResultado) {
            int[] resultado;
            if (jugadorResultado.TryGetValue(nombreJugador, out resultado)) {
                agregarResultado(win, resultado);
                jugadorResultado[nombreJugador] = resultado;
            } else {
                //no habia resultado para ese jugador
                resultado = new int[2];
                agregarResultado(win, resultado);
                jugadorResultado.Add(nombreJugador, resultado);
            }
        }

        private static void agregarResultado(string win, int[] resultado) {
            if (win.Equals("1")) {
                resultado[0]++;
            } else {
                resultado[1]++;
            }
        }

        private void asegurarExistenciaDirectorioUsuarioPlays(String username,String directorioCacheUsuario, String idBgg) {
            directorioCacheUsuarioPlays = directorioCacheUsuario + "/" + username + "/plays/"+idBgg+"/";
            if (!Directory.Exists(directorioCacheUsuarioPlays)) {
                Directory.CreateDirectory(directorioCacheUsuarioPlays);
            }
        }              


        private void agregarDatosDictionary(Dictionary<int,Dictionary<int,int>> diccionario,int idBgg,int totalJugadores,int totalGanadas) {
            Dictionary<int, int> ganadasGuardadas;
            diccionario.TryGetValue(idBgg, out ganadasGuardadas);
            if (ganadasGuardadas != null) { 
                int totalGanadasGuardadas;
                if (ganadasGuardadas.TryGetValue(totalJugadores, out totalGanadasGuardadas)) {
                    ganadasGuardadas[totalJugadores] = totalGanadasGuardadas + totalGanadas;
                } else {
                    ganadasGuardadas.Add(totalJugadores, totalGanadas);
                }
            } else {
                ganadasGuardadas = new Dictionary<int, int>();
                ganadasGuardadas.Add(totalJugadores, totalGanadas);
                diccionario.Add(idBgg, ganadasGuardadas);
            }
        } 
        
    }
}

