namespace ExploradorColecciones
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Autores");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pnlInformacion = new System.Windows.Forms.Panel();
            this.lblElementosEncontrados = new System.Windows.Forms.Label();
            this.lblElementosEncontradosTitulo = new System.Windows.Forms.Label();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.btnActualizarColeccion = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlDetalles = new System.Windows.Forms.Panel();
            this.lstbxJugadas = new System.Windows.Forms.ListBox();
            this.lstbxAutor = new System.Windows.Forms.ListBox();
            this.pbxMiniatura = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblIlustrador = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblNombreJuego = new System.Windows.Forms.Label();
            this.pnlTree = new System.Windows.Forms.Panel();
            this.tvArbol = new System.Windows.Forms.TreeView();
            this.imglArbol = new System.Windows.Forms.ImageList(this.components);
            this.imglIconos = new System.Windows.Forms.ImageList(this.components);
            this.pnlListView = new System.Windows.Forms.Panel();
            this.lvContenido = new System.Windows.Forms.ListView();
            this.imglGrandes = new System.Windows.Forms.ImageList(this.components);
            this.pnlInformacion.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.pnlDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMiniatura)).BeginInit();
            this.pnlTree.SuspendLayout();
            this.pnlListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInformacion
            // 
            this.pnlInformacion.Controls.Add(this.lblElementosEncontrados);
            this.pnlInformacion.Controls.Add(this.lblElementosEncontradosTitulo);
            this.pnlInformacion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInformacion.Location = new System.Drawing.Point(0, 454);
            this.pnlInformacion.Name = "pnlInformacion";
            this.pnlInformacion.Size = new System.Drawing.Size(832, 47);
            this.pnlInformacion.TabIndex = 0;
            // 
            // lblElementosEncontrados
            // 
            this.lblElementosEncontrados.AutoSize = true;
            this.lblElementosEncontrados.Location = new System.Drawing.Point(172, 16);
            this.lblElementosEncontrados.Name = "lblElementosEncontrados";
            this.lblElementosEncontrados.Size = new System.Drawing.Size(0, 13);
            this.lblElementosEncontrados.TabIndex = 2;
            // 
            // lblElementosEncontradosTitulo
            // 
            this.lblElementosEncontradosTitulo.AutoSize = true;
            this.lblElementosEncontradosTitulo.Location = new System.Drawing.Point(24, 16);
            this.lblElementosEncontradosTitulo.Name = "lblElementosEncontradosTitulo";
            this.lblElementosEncontradosTitulo.Size = new System.Drawing.Size(122, 13);
            this.lblElementosEncontradosTitulo.TabIndex = 1;
            this.lblElementosEncontradosTitulo.Text = "Elementos Encontrados:";
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.btnActualizarColeccion);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Controls.Add(this.txtNombreUsuario);
            this.pnlBusqueda.Controls.Add(this.label1);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusqueda.Location = new System.Drawing.Point(0, 0);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(832, 79);
            this.pnlBusqueda.TabIndex = 2;
            // 
            // btnActualizarColeccion
            // 
            this.btnActualizarColeccion.Location = new System.Drawing.Point(369, 34);
            this.btnActualizarColeccion.Name = "btnActualizarColeccion";
            this.btnActualizarColeccion.Size = new System.Drawing.Size(126, 28);
            this.btnActualizarColeccion.TabIndex = 3;
            this.btnActualizarColeccion.Text = "Actualizar Colección";
            this.btnActualizarColeccion.UseVisualStyleBackColor = true;
            this.btnActualizarColeccion.Click += new System.EventHandler(this.BtnActualizarColeccion_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(263, 34);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(83, 28);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Location = new System.Drawing.Point(147, 39);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(100, 20);
            this.txtNombreUsuario.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario";
            // 
            // pnlDetalles
            // 
            this.pnlDetalles.Controls.Add(this.lstbxJugadas);
            this.pnlDetalles.Controls.Add(this.lstbxAutor);
            this.pnlDetalles.Controls.Add(this.pbxMiniatura);
            this.pnlDetalles.Controls.Add(this.label13);
            this.pnlDetalles.Controls.Add(this.lblIlustrador);
            this.pnlDetalles.Controls.Add(this.label12);
            this.pnlDetalles.Controls.Add(this.label11);
            this.pnlDetalles.Controls.Add(this.lblNombreJuego);
            this.pnlDetalles.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetalles.Location = new System.Drawing.Point(455, 79);
            this.pnlDetalles.Name = "pnlDetalles";
            this.pnlDetalles.Size = new System.Drawing.Size(377, 375);
            this.pnlDetalles.TabIndex = 3;
            this.pnlDetalles.Visible = false;
            // 
            // lstbxJugadas
            // 
            this.lstbxJugadas.FormattingEnabled = true;
            this.lstbxJugadas.Location = new System.Drawing.Point(21, 154);
            this.lstbxJugadas.Name = "lstbxJugadas";
            this.lstbxJugadas.Size = new System.Drawing.Size(344, 69);
            this.lstbxJugadas.TabIndex = 9;
            // 
            // lstbxAutor
            // 
            this.lstbxAutor.FormattingEnabled = true;
            this.lstbxAutor.Location = new System.Drawing.Point(99, 65);
            this.lstbxAutor.Name = "lstbxAutor";
            this.lstbxAutor.Size = new System.Drawing.Size(176, 43);
            this.lstbxAutor.TabIndex = 8;
            // 
            // pbxMiniatura
            // 
            this.pbxMiniatura.Location = new System.Drawing.Point(111, 225);
            this.pbxMiniatura.MaximumSize = new System.Drawing.Size(200, 200);
            this.pbxMiniatura.Name = "pbxMiniatura";
            this.pbxMiniatura.Size = new System.Drawing.Size(150, 150);
            this.pbxMiniatura.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxMiniatura.TabIndex = 7;
            this.pbxMiniatura.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Nombre:";
            // 
            // lblIlustrador
            // 
            this.lblIlustrador.AutoSize = true;
            this.lblIlustrador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIlustrador.Location = new System.Drawing.Point(95, 112);
            this.lblIlustrador.Name = "lblIlustrador";
            this.lblIlustrador.Size = new System.Drawing.Size(0, 20);
            this.lblIlustrador.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Autor(es):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 117);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Ilustrador:";
            // 
            // lblNombreJuego
            // 
            this.lblNombreJuego.AutoSize = true;
            this.lblNombreJuego.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreJuego.Location = new System.Drawing.Point(95, 32);
            this.lblNombreJuego.Name = "lblNombreJuego";
            this.lblNombreJuego.Size = new System.Drawing.Size(0, 20);
            this.lblNombreJuego.TabIndex = 4;
            // 
            // pnlTree
            // 
            this.pnlTree.Controls.Add(this.tvArbol);
            this.pnlTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTree.Location = new System.Drawing.Point(0, 79);
            this.pnlTree.Name = "pnlTree";
            this.pnlTree.Size = new System.Drawing.Size(207, 375);
            this.pnlTree.TabIndex = 4;
            // 
            // tvArbol
            // 
            this.tvArbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvArbol.ImageIndex = 0;
            this.tvArbol.ImageList = this.imglArbol;
            this.tvArbol.Location = new System.Drawing.Point(0, 0);
            this.tvArbol.Name = "tvArbol";
            treeNode2.Name = "Nodo0";
            treeNode2.Tag = "A";
            treeNode2.Text = "Autores";
            this.tvArbol.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvArbol.SelectedImageIndex = 0;
            this.tvArbol.Size = new System.Drawing.Size(207, 375);
            this.tvArbol.TabIndex = 0;
            this.tvArbol.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvArbol_NodeMouseClick);
            // 
            // imglArbol
            // 
            this.imglArbol.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imglArbol.ImageSize = new System.Drawing.Size(30, 30);
            this.imglArbol.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imglIconos
            // 
            this.imglIconos.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglIconos.ImageStream")));
            this.imglIconos.TransparentColor = System.Drawing.Color.Transparent;
            this.imglIconos.Images.SetKeyName(0, "autor.jpg");
            // 
            // pnlListView
            // 
            this.pnlListView.Controls.Add(this.lvContenido);
            this.pnlListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlListView.Location = new System.Drawing.Point(207, 79);
            this.pnlListView.Name = "pnlListView";
            this.pnlListView.Size = new System.Drawing.Size(248, 375);
            this.pnlListView.TabIndex = 5;
            // 
            // lvContenido
            // 
            this.lvContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvContenido.HideSelection = false;
            this.lvContenido.LargeImageList = this.imglGrandes;
            this.lvContenido.Location = new System.Drawing.Point(0, 0);
            this.lvContenido.MultiSelect = false;
            this.lvContenido.Name = "lvContenido";
            this.lvContenido.Size = new System.Drawing.Size(248, 375);
            this.lvContenido.SmallImageList = this.imglIconos;
            this.lvContenido.TabIndex = 0;
            this.lvContenido.UseCompatibleStateImageBehavior = false;
            this.lvContenido.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LvContenido_ItemSelectionChanged);
            // 
            // imglGrandes
            // 
            this.imglGrandes.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imglGrandes.ImageSize = new System.Drawing.Size(90, 60);
            this.imglGrandes.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 501);
            this.Controls.Add(this.pnlListView);
            this.Controls.Add(this.pnlTree);
            this.Controls.Add(this.pnlDetalles);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.pnlInformacion);
            this.Name = "Form1";
            this.Text = "Explorador De Colecciones";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlInformacion.ResumeLayout(false);
            this.pnlInformacion.PerformLayout();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.pnlDetalles.ResumeLayout(false);
            this.pnlDetalles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMiniatura)).EndInit();
            this.pnlTree.ResumeLayout(false);
            this.pnlListView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInformacion;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Panel pnlDetalles;
        private System.Windows.Forms.Panel pnlTree;
        private System.Windows.Forms.Label lblElementosEncontrados;
        private System.Windows.Forms.Label lblElementosEncontradosTitulo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tvArbol;
        private System.Windows.Forms.Panel pnlListView;
        private System.Windows.Forms.ListView lvContenido;
        private System.Windows.Forms.ImageList imglIconos;
        private System.Windows.Forms.ImageList imglGrandes;
        private System.Windows.Forms.PictureBox pbxMiniatura;
        private System.Windows.Forms.Label lblIlustrador;
        private System.Windows.Forms.Label lblNombreJuego;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnActualizarColeccion;
        private System.Windows.Forms.ImageList imglArbol;
        private System.Windows.Forms.ListBox lstbxAutor;
        private System.Windows.Forms.ListBox lstbxJugadas;
    }
}

