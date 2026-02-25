namespace JSONManager
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.menuStrip            = new System.Windows.Forms.MenuStrip();
            this.tsmiArchivo          = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNuevo            = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbrir            = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGuardar          = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGuardarComo      = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSep1             = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEliminar         = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSep2             = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSalir            = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditar           = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAgregarPropiedad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAgregarItem      = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEliminarNodo     = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSep3             = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExpandir         = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiContraer         = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVer              = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToggleJson       = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRefrescar        = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFormatear        = new System.Windows.Forms.ToolStripMenuItem();

            this.toolStrip               = new System.Windows.Forms.ToolStrip();
            this.btnNuevoTS              = new System.Windows.Forms.ToolStripButton();
            this.btnAbrirTS              = new System.Windows.Forms.ToolStripButton();
            this.btnGuardarTS            = new System.Windows.Forms.ToolStripButton();
            this.sep1TS                  = new System.Windows.Forms.ToolStripSeparator();
            this.btnAgregarPropTS        = new System.Windows.Forms.ToolStripButton();
            this.btnAgregarItemTS        = new System.Windows.Forms.ToolStripButton();
            this.btnEliminarNodoTS       = new System.Windows.Forms.ToolStripButton();
            this.sep2TS                  = new System.Windows.Forms.ToolStripSeparator();
            this.btnExpandirTS           = new System.Windows.Forms.ToolStripButton();
            this.btnContraerTS           = new System.Windows.Forms.ToolStripButton();
            this.sep3TS                  = new System.Windows.Forms.ToolStripSeparator();
            this.btnEliminarArchivoTS    = new System.Windows.Forms.ToolStripButton();

            this.splitMain            = new System.Windows.Forms.SplitContainer();
            this.splitLeft            = new System.Windows.Forms.SplitContainer();
            this.panelTreeHeader      = new System.Windows.Forms.Panel();
            this.lblTreeTitle         = new System.Windows.Forms.Label();
            this.treeJson             = new System.Windows.Forms.TreeView();
            this.panelJsonRaw         = new System.Windows.Forms.Panel();
            this.lblJsonRawTitle      = new System.Windows.Forms.Label();
            this.txtJsonRaw           = new System.Windows.Forms.RichTextBox();
            this.panelRight           = new System.Windows.Forms.Panel();
            this.lblPropTitle         = new System.Windows.Forms.Label();
            this.gridPropiedades      = new System.Windows.Forms.DataGridView();
            this.colPropiedad         = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo              = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValor             = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelInfo            = new System.Windows.Forms.Panel();
            this.lblNodoInfo          = new System.Windows.Forms.Label();

            this.statusStrip          = new System.Windows.Forms.StatusStrip();
            this.lblStatus            = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRuta              = new System.Windows.Forms.ToolStripStatusLabel();

            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitLeft)).BeginInit();
            this.splitLeft.SuspendLayout();
            this.panelTreeHeader.SuspendLayout();
            this.panelJsonRaw.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPropiedades)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsmiArchivo, this.tsmiEditar, this.tsmiVer });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip.TabIndex = 0;

            // tsmiArchivo
            this.tsmiArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsmiNuevo, this.tsmiAbrir, this.tsmiGuardar, this.tsmiGuardarComo,
                this.tsmiSep1, this.tsmiEliminar, this.tsmiSep2, this.tsmiSalir });
            this.tsmiArchivo.Name = "tsmiArchivo";
            this.tsmiArchivo.Text = "&Archivo";

            this.tsmiNuevo.Name = "tsmiNuevo";
            this.tsmiNuevo.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
            this.tsmiNuevo.Text = "&Nuevo JSON";
            this.tsmiNuevo.Click += new System.EventHandler(this.tsmiNuevo_Click);

            this.tsmiAbrir.Name = "tsmiAbrir";
            this.tsmiAbrir.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            this.tsmiAbrir.Text = "&Abrir JSON...";
            this.tsmiAbrir.Click += new System.EventHandler(this.tsmiAbrir_Click);

            this.tsmiGuardar.Name = "tsmiGuardar";
            this.tsmiGuardar.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
            this.tsmiGuardar.Text = "&Guardar";
            this.tsmiGuardar.Click += new System.EventHandler(this.tsmiGuardar_Click);

            this.tsmiGuardarComo.Name = "tsmiGuardarComo";
            this.tsmiGuardarComo.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S;
            this.tsmiGuardarComo.Text = "Guardar &Como...";
            this.tsmiGuardarComo.Click += new System.EventHandler(this.tsmiGuardarComo_Click);

            this.tsmiSep1.Name = "tsmiSep1";

            this.tsmiEliminar.Name = "tsmiEliminar";
            this.tsmiEliminar.Text = "&Eliminar archivo";
            this.tsmiEliminar.Click += new System.EventHandler(this.tsmiEliminar_Click);

            this.tsmiSep2.Name = "tsmiSep2";

            this.tsmiSalir.Name = "tsmiSalir";
            this.tsmiSalir.Text = "Sa&lir";
            this.tsmiSalir.Click += new System.EventHandler(this.tsmiSalir_Click);

            // tsmiEditar
            this.tsmiEditar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsmiAgregarPropiedad, this.tsmiAgregarItem, this.tsmiEliminarNodo,
                this.tsmiSep3, this.tsmiExpandir, this.tsmiContraer });
            this.tsmiEditar.Name = "tsmiEditar";
            this.tsmiEditar.Text = "&Editar";

            this.tsmiAgregarPropiedad.Name = "tsmiAgregarPropiedad";
            this.tsmiAgregarPropiedad.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert;
            this.tsmiAgregarPropiedad.Text = "&Agregar propiedad";
            this.tsmiAgregarPropiedad.Click += new System.EventHandler(this.tsmiAgregarPropiedad_Click);

            this.tsmiAgregarItem.Name = "tsmiAgregarItem";
            this.tsmiAgregarItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L;
            this.tsmiAgregarItem.Text = "Agregar í&tem a array";
            this.tsmiAgregarItem.Click += new System.EventHandler(this.tsmiAgregarItem_Click);

            this.tsmiEliminarNodo.Name = "tsmiEliminarNodo";
            this.tsmiEliminarNodo.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete;
            this.tsmiEliminarNodo.Text = "E&liminar nodo";
            this.tsmiEliminarNodo.Click += new System.EventHandler(this.tsmiEliminarNodo_Click);

            this.tsmiSep3.Name = "tsmiSep3";

            this.tsmiExpandir.Name = "tsmiExpandir";
            this.tsmiExpandir.Text = "&Expandir todo";
            this.tsmiExpandir.Click += new System.EventHandler(this.tsmiExpandir_Click);

            this.tsmiContraer.Name = "tsmiContraer";
            this.tsmiContraer.Text = "&Contraer todo";
            this.tsmiContraer.Click += new System.EventHandler(this.tsmiContraer_Click);

            // tsmiVer
            this.tsmiVer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsmiToggleJson, this.tsmiRefrescar, this.tsmiFormatear });
            this.tsmiVer.Name = "tsmiVer";
            this.tsmiVer.Text = "&Ver";

            this.tsmiToggleJson.Name = "tsmiToggleJson";
            this.tsmiToggleJson.Text = "Panel JSON &fuente";
            this.tsmiToggleJson.CheckOnClick = true;
            this.tsmiToggleJson.Checked = true;
            this.tsmiToggleJson.Click += new System.EventHandler(this.tsmiToggleJson_Click);

            this.tsmiRefrescar.Name = "tsmiRefrescar";
            this.tsmiRefrescar.Text = "&Refrescar vista JSON";
            this.tsmiRefrescar.Click += new System.EventHandler(this.tsmiRefrescar_Click);

            this.tsmiFormatear.Name = "tsmiFormatear";
            this.tsmiFormatear.Text = "&Formatear / Minificar";
            this.tsmiFormatear.Click += new System.EventHandler(this.tsmiFormatear_Click);

            // toolStrip
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.btnNuevoTS, this.btnAbrirTS, this.btnGuardarTS, this.sep1TS,
                this.btnAgregarPropTS, this.btnAgregarItemTS, this.btnEliminarNodoTS, this.sep2TS,
                this.btnExpandirTS, this.btnContraerTS, this.sep3TS, this.btnEliminarArchivoTS });
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1200, 25);
            this.toolStrip.TabIndex = 1;

            this.btnNuevoTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNuevoTS.Name = "btnNuevoTS";
            this.btnNuevoTS.Text = "Nuevo";
            this.btnNuevoTS.Click += new System.EventHandler(this.tsmiNuevo_Click);

            this.btnAbrirTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAbrirTS.Name = "btnAbrirTS";
            this.btnAbrirTS.Text = "Abrir";
            this.btnAbrirTS.Click += new System.EventHandler(this.tsmiAbrir_Click);

            this.btnGuardarTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGuardarTS.Name = "btnGuardarTS";
            this.btnGuardarTS.Text = "Guardar";
            this.btnGuardarTS.Click += new System.EventHandler(this.tsmiGuardar_Click);

            this.sep1TS.Name = "sep1TS";

            this.btnAgregarPropTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAgregarPropTS.Name = "btnAgregarPropTS";
            this.btnAgregarPropTS.Text = "+ Propiedad";
            this.btnAgregarPropTS.Click += new System.EventHandler(this.tsmiAgregarPropiedad_Click);

            this.btnAgregarItemTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAgregarItemTS.Name = "btnAgregarItemTS";
            this.btnAgregarItemTS.Text = "+ Ítem";
            this.btnAgregarItemTS.Click += new System.EventHandler(this.tsmiAgregarItem_Click);

            this.btnEliminarNodoTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEliminarNodoTS.Name = "btnEliminarNodoTS";
            this.btnEliminarNodoTS.Text = "- Nodo";
            this.btnEliminarNodoTS.Click += new System.EventHandler(this.tsmiEliminarNodo_Click);

            this.sep2TS.Name = "sep2TS";

            this.btnExpandirTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExpandirTS.Name = "btnExpandirTS";
            this.btnExpandirTS.Text = "Expandir";
            this.btnExpandirTS.Click += new System.EventHandler(this.tsmiExpandir_Click);

            this.btnContraerTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnContraerTS.Name = "btnContraerTS";
            this.btnContraerTS.Text = "Contraer";
            this.btnContraerTS.Click += new System.EventHandler(this.tsmiContraer_Click);

            this.sep3TS.Name = "sep3TS";

            this.btnEliminarArchivoTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEliminarArchivoTS.Name = "btnEliminarArchivoTS";
            this.btnEliminarArchivoTS.Text = "Eliminar archivo";
            this.btnEliminarArchivoTS.Click += new System.EventHandler(this.tsmiEliminar_Click);

            // splitMain
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 49);
            this.splitMain.Name = "splitMain";
            this.splitMain.Panel1.Controls.Add(this.splitLeft);
            this.splitMain.Panel2.Controls.Add(this.panelRight);
            this.splitMain.Size = new System.Drawing.Size(1200, 610);
            this.splitMain.SplitterDistance = 820;
            this.splitMain.TabIndex = 2;

            // splitLeft
            this.splitLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLeft.Name = "splitLeft";
            this.splitLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitLeft.Panel1.Controls.Add(this.treeJson);
            this.splitLeft.Panel1.Controls.Add(this.panelTreeHeader);
            this.splitLeft.Panel2.Controls.Add(this.panelJsonRaw);
            this.splitLeft.Size = new System.Drawing.Size(820, 610);
            this.splitLeft.SplitterDistance = 380;
            this.splitLeft.TabIndex = 0;

            // panelTreeHeader
            this.panelTreeHeader.BackColor = System.Drawing.Color.FromArgb(40, 100, 60);
            this.panelTreeHeader.Controls.Add(this.lblTreeTitle);
            this.panelTreeHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTreeHeader.Height = 28;
            this.panelTreeHeader.Name = "panelTreeHeader";

            this.lblTreeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTreeTitle.ForeColor = System.Drawing.Color.White;
            this.lblTreeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTreeTitle.Name = "lblTreeTitle";
            this.lblTreeTitle.Padding = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.lblTreeTitle.Text = "  Árbol JSON";

            // treeJson
            this.treeJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeJson.Font = new System.Drawing.Font("Consolas", 9F);
            this.treeJson.HideSelection = false;
            this.treeJson.Name = "treeJson";
            this.treeJson.TabIndex = 0;
            this.treeJson.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeJson_AfterSelect);
            this.treeJson.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeJson_NodeMouseDoubleClick);

            // panelJsonRaw
            this.panelJsonRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelJsonRaw.Controls.Add(this.txtJsonRaw);
            this.panelJsonRaw.Controls.Add(this.lblJsonRawTitle);
            this.panelJsonRaw.Name = "panelJsonRaw";

            this.lblJsonRawTitle.BackColor = System.Drawing.Color.FromArgb(40, 100, 60);
            this.lblJsonRawTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblJsonRawTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblJsonRawTitle.ForeColor = System.Drawing.Color.White;
            this.lblJsonRawTitle.Height = 28;
            this.lblJsonRawTitle.Name = "lblJsonRawTitle";
            this.lblJsonRawTitle.Padding = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.lblJsonRawTitle.Text = "  JSON Fuente";

            this.txtJsonRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJsonRaw.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtJsonRaw.Name = "txtJsonRaw";
            this.txtJsonRaw.ReadOnly = true;
            this.txtJsonRaw.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.txtJsonRaw.WordWrap = false;
            this.txtJsonRaw.BackColor = System.Drawing.Color.FromArgb(25, 35, 28);
            this.txtJsonRaw.ForeColor = System.Drawing.Color.FromArgb(200, 240, 210);

            // panelRight
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Controls.Add(this.gridPropiedades);
            this.panelRight.Controls.Add(this.panelInfo);
            this.panelRight.Controls.Add(this.lblPropTitle);
            this.panelRight.Name = "panelRight";

            this.lblPropTitle.BackColor = System.Drawing.Color.FromArgb(40, 100, 60);
            this.lblPropTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPropTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPropTitle.ForeColor = System.Drawing.Color.White;
            this.lblPropTitle.Height = 28;
            this.lblPropTitle.Name = "lblPropTitle";
            this.lblPropTitle.Padding = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.lblPropTitle.Text = "  Propiedades del nodo";

            // panelInfo
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(236, 248, 238);
            this.panelInfo.Controls.Add(this.lblNodoInfo);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInfo.Height = 80;
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Padding = new System.Windows.Forms.Padding(8);

            this.lblNodoInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNodoInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblNodoInfo.ForeColor = System.Drawing.Color.FromArgb(30, 80, 40);
            this.lblNodoInfo.Name = "lblNodoInfo";
            this.lblNodoInfo.Text = "Selecciona un nodo en el árbol JSON para ver sus propiedades.";

            // gridPropiedades
            this.gridPropiedades.AllowUserToAddRows = false;
            this.gridPropiedades.AllowUserToDeleteRows = false;
            this.gridPropiedades.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPropiedades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPropiedades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colPropiedad, this.colTipo, this.colValor });
            this.gridPropiedades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPropiedades.EnableHeadersVisualStyles = false;
            this.gridPropiedades.GridColor = System.Drawing.Color.FromArgb(180, 220, 190);
            this.gridPropiedades.Name = "gridPropiedades";
            this.gridPropiedades.RowHeadersWidth = 30;
            this.gridPropiedades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPropiedades.TabIndex = 1;
            this.gridPropiedades.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPropiedades_CellEndEdit);

            this.colPropiedad.HeaderText = "Clave";
            this.colPropiedad.Name = "colPropiedad";
            this.colPropiedad.ReadOnly = true;
            this.colPropiedad.FillWeight = 35;

            this.colTipo.HeaderText = "Tipo";
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;
            this.colTipo.FillWeight = 20;

            this.colValor.HeaderText = "Valor";
            this.colValor.Name = "colValor";
            this.colValor.FillWeight = 45;

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblStatus, this.lblRuta });
            this.statusStrip.Location = new System.Drawing.Point(0, 659);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip.TabIndex = 3;

            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "Listo";
            this.lblStatus.Spring = true;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Text = "";
            this.lblRuta.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 681);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestor de Archivos JSON";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panelTreeHeader.ResumeLayout(false);
            this.panelJsonRaw.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPropiedades)).EndInit();
            this.splitLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Controles
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiArchivo;
        private System.Windows.Forms.ToolStripMenuItem tsmiNuevo;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbrir;
        private System.Windows.Forms.ToolStripMenuItem tsmiGuardar;
        private System.Windows.Forms.ToolStripMenuItem tsmiGuardarComo;
        private System.Windows.Forms.ToolStripSeparator tsmiSep1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEliminar;
        private System.Windows.Forms.ToolStripSeparator tsmiSep2;
        private System.Windows.Forms.ToolStripMenuItem tsmiSalir;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditar;
        private System.Windows.Forms.ToolStripMenuItem tsmiAgregarPropiedad;
        private System.Windows.Forms.ToolStripMenuItem tsmiAgregarItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiEliminarNodo;
        private System.Windows.Forms.ToolStripSeparator tsmiSep3;
        private System.Windows.Forms.ToolStripMenuItem tsmiExpandir;
        private System.Windows.Forms.ToolStripMenuItem tsmiContraer;
        private System.Windows.Forms.ToolStripMenuItem tsmiVer;
        private System.Windows.Forms.ToolStripMenuItem tsmiToggleJson;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefrescar;
        private System.Windows.Forms.ToolStripMenuItem tsmiFormatear;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNuevoTS;
        private System.Windows.Forms.ToolStripButton btnAbrirTS;
        private System.Windows.Forms.ToolStripButton btnGuardarTS;
        private System.Windows.Forms.ToolStripSeparator sep1TS;
        private System.Windows.Forms.ToolStripButton btnAgregarPropTS;
        private System.Windows.Forms.ToolStripButton btnAgregarItemTS;
        private System.Windows.Forms.ToolStripButton btnEliminarNodoTS;
        private System.Windows.Forms.ToolStripSeparator sep2TS;
        private System.Windows.Forms.ToolStripButton btnExpandirTS;
        private System.Windows.Forms.ToolStripButton btnContraerTS;
        private System.Windows.Forms.ToolStripSeparator sep3TS;
        private System.Windows.Forms.ToolStripButton btnEliminarArchivoTS;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.SplitContainer splitLeft;
        private System.Windows.Forms.Panel panelTreeHeader;
        private System.Windows.Forms.Label lblTreeTitle;
        private System.Windows.Forms.TreeView treeJson;
        private System.Windows.Forms.Panel panelJsonRaw;
        private System.Windows.Forms.Label lblJsonRawTitle;
        private System.Windows.Forms.RichTextBox txtJsonRaw;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblPropTitle;
        private System.Windows.Forms.DataGridView gridPropiedades;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPropiedad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValor;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblNodoInfo;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblRuta;
    }
}
