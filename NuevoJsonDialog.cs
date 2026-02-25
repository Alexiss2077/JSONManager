using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace JSONManager
{
    internal class NuevoJsonDialog : Form
    {
        public string    RutaCompleta => txtRuta.Text.Trim();
        public JsonNode  Nodo         { get; private set; } = new JsonObject();

        private static readonly Dictionary<string, Func<JsonNode>> _plantillas = new()
        {
            ["— En blanco (objeto) —"] = () => new JsonObject(),
            ["— En blanco (array) —"]  = () => new JsonArray(),

            ["Configuración de app"] = () => new JsonObject
            {
                ["app"]      = new JsonObject { ["nombre"] = "MiApp", ["version"] = "1.0.0", ["debug"] = false },
                ["database"] = new JsonObject { ["host"] = "localhost", ["port"] = 5432, ["name"] = "mi_bd" },
                ["logging"]  = new JsonObject { ["level"] = "info", ["archivo"] = "app.log" }
            },
            ["Catálogo de productos"] = () => new JsonObject
            {
                ["catalogo"] = new JsonArray
                {
                    new JsonObject { ["id"] = "P001", ["nombre"] = "Laptop Pro", ["precio"] = 18500.00, ["stock"] = 25, ["categoria"] = "Electrónica" },
                    new JsonObject { ["id"] = "P002", ["nombre"] = "Mouse Inalámbrico", ["precio"] = 450.00, ["stock"] = 150, ["categoria"] = "Accesorios" },
                    new JsonObject { ["id"] = "P003", ["nombre"] = "Teclado Mecánico", ["precio"] = 1200.00, ["stock"] = 80, ["categoria"] = "Accesorios" }
                }
            },
            ["Lista de usuarios"] = () => new JsonArray
            {
                new JsonObject { ["id"] = 1, ["nombre"] = "Ana García", ["email"] = "ana@email.com", ["activo"] = true, ["rol"] = "admin" },
                new JsonObject { ["id"] = 2, ["nombre"] = "Luis Martínez", ["email"] = "luis@email.com", ["activo"] = true, ["rol"] = "usuario" },
                new JsonObject { ["id"] = 3, ["nombre"] = "María López", ["email"] = "maria@email.com", ["activo"] = false, ["rol"] = "usuario" }
            },
            ["Package.json (Node)"] = () => new JsonObject
            {
                ["name"]        = "mi-proyecto",
                ["version"]     = "1.0.0",
                ["description"] = "",
                ["main"]        = "index.js",
                ["scripts"]     = new JsonObject { ["start"] = "node index.js", ["test"] = "jest" },
                ["keywords"]    = new JsonArray(),
                ["author"]      = "",
                ["license"]     = "MIT",
                ["dependencies"]= new JsonObject()
            },
        };

        private TextBox    txtRuta     = null!;
        private Button     btnExaminar = null!;
        private ComboBox   cboPlant    = null!;
        private RichTextBox txtPreview = null!;
        private Button     btnAceptar  = null!;
        private Button     btnCancelar = null!;

        public NuevoJsonDialog() { BuildUI(); }

        private void BuildUI()
        {
            this.Text = "Nuevo archivo JSON";
            this.Size = new System.Drawing.Size(660, 560);
            this.MinimumSize = new System.Drawing.Size(580, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.BackColor = System.Drawing.Color.White;

            var lblTitulo = new Label
            {
                Text = "Crear nuevo archivo JSON",
                Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(40, 100, 60),
                Dock = DockStyle.Top, Height = 42,
                Padding = new Padding(14, 12, 0, 0)
            };

            var grpRuta = new GroupBox { Text = "Ubicación del archivo", Dock = DockStyle.Top, Height = 62, Padding = new Padding(8, 4, 8, 4) };
            txtRuta = new TextBox { Dock = DockStyle.Fill, PlaceholderText = "Ej: C:\\Datos\\datos.json" };
            btnExaminar = new Button { Text = "...", Dock = DockStyle.Right, Width = 40, FlatStyle = FlatStyle.Flat };
            btnExaminar.Click += (s, e) =>
            {
                using var d = new SaveFileDialog { Title = "Guardar nuevo JSON", Filter = "Archivos JSON (*.json)|*.json", FileName = "nuevo.json" };
                if (d.ShowDialog(this) == DialogResult.OK) txtRuta.Text = d.FileName;
            };
            var pRuta = new Panel { Dock = DockStyle.Fill };
            pRuta.Controls.Add(txtRuta); pRuta.Controls.Add(btnExaminar);
            grpRuta.Controls.Add(pRuta);

            var grpPlant = new GroupBox { Text = "Plantilla", Dock = DockStyle.Top, Height = 60, Padding = new Padding(8, 4, 8, 4) };
            cboPlant = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            foreach (var k in _plantillas.Keys) cboPlant.Items.Add(k);
            cboPlant.SelectedIndex = 0;
            cboPlant.SelectedIndexChanged += (s, e) => ActualizarPreview();
            grpPlant.Controls.Add(cboPlant);

            var grpPreview = new GroupBox { Text = "Vista previa del JSON", Dock = DockStyle.Fill, Padding = new Padding(8, 4, 8, 4) };
            txtPreview = new RichTextBox
            {
                Dock = DockStyle.Fill, ReadOnly = true,
                Font = new System.Drawing.Font("Consolas", 9.5f),
                BackColor = System.Drawing.Color.FromArgb(25, 35, 28),
                ForeColor = System.Drawing.Color.FromArgb(180, 240, 200),
                ScrollBars = RichTextBoxScrollBars.Both, WordWrap = false
            };
            grpPreview.Controls.Add(txtPreview);

            btnAceptar  = new Button { Text = "Crear archivo", Width = 130, Height = 34, DialogResult = DialogResult.OK, FlatStyle = FlatStyle.Flat, BackColor = System.Drawing.Color.FromArgb(40, 100, 60), ForeColor = System.Drawing.Color.White };
            btnCancelar = new Button { Text = "Cancelar",      Width = 90,  Height = 34, DialogResult = DialogResult.Cancel, FlatStyle = FlatStyle.Flat };
            btnAceptar.FlatAppearance.BorderSize = 0;

            btnAceptar.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtRuta.Text))
                { MessageBox.Show("Elige una ruta.", "Falta información", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (!txtRuta.Text.Trim().EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                { MessageBox.Show("El archivo debe tener extensión .json", "Extensión inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                string key = cboPlant.SelectedItem?.ToString() ?? "";
                Nodo = _plantillas.ContainsKey(key) ? _plantillas[key]() : new JsonObject();
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.AcceptButton = btnAceptar; this.CancelButton = btnCancelar;

            var panelOk = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 48, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(10, 6, 10, 6) };
            panelOk.Controls.AddRange(new Control[] { btnCancelar, btnAceptar });

            this.Controls.Add(grpPreview);
            this.Controls.Add(grpPlant);
            this.Controls.Add(grpRuta);
            this.Controls.Add(lblTitulo);
            this.Controls.Add(panelOk);

            ActualizarPreview();
        }

        private void ActualizarPreview()
        {
            if (cboPlant.SelectedItem is not string key) return;
            var nodo = _plantillas[key]();
            txtPreview.Text = nodo.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
        }
    }

    // ── Diálogo para ingresar / editar un valor JSON ─────────────────────
    internal class ValorDialog : Form
    {
        public JsonNode? JsonNodeResultado { get; private set; }

        private ComboBox   cboTipo  = null!;
        private TextBox    txtValor = null!;
        private Button     btnOk    = null!;
        private Button     btnCa    = null!;

        public ValorDialog(string titulo, string clave, string? valorActual = null)
        {
            this.Text = titulo;
            this.Size = new System.Drawing.Size(420, 220);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.BackColor = System.Drawing.Color.White;

            var lblClave = new Label { Text = $"Clave: {clave}", Dock = DockStyle.Top, Height = 28, Padding = new Padding(10, 8, 0, 0), Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(40, 100, 60) };
            var lblTipo  = new Label { Text = "Tipo:", Height = 22, Location = new System.Drawing.Point(10, 54), Width = 60 };
            cboTipo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new System.Drawing.Point(75, 50), Width = 130 };
            cboTipo.Items.AddRange(new object[] { "string", "number", "boolean", "null", "object {}", "array []" });
            cboTipo.SelectedIndex = 0;

            var lblVal = new Label { Text = "Valor:", Height = 22, Location = new System.Drawing.Point(10, 90), Width = 60 };
            txtValor = new TextBox { Location = new System.Drawing.Point(75, 86), Width = 300, Text = valorActual != null ? valorActual.Trim('"') : "" };

            cboTipo.SelectedIndexChanged += (s, e) =>
            {
                bool esValor = cboTipo.SelectedIndex < 4;
                txtValor.Enabled = esValor;
                if (!esValor) txtValor.Clear();
                if (cboTipo.SelectedItem?.ToString() == "boolean") txtValor.Text = "true";
                if (cboTipo.SelectedItem?.ToString() == "null")    txtValor.Text = "null";
            };

            btnOk = new Button { Text = "Aceptar",  DialogResult = DialogResult.OK,     Location = new System.Drawing.Point(210, 130), Width = 80, FlatStyle = FlatStyle.Flat, BackColor = System.Drawing.Color.FromArgb(40, 100, 60), ForeColor = System.Drawing.Color.White };
            btnCa = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(300, 130), Width = 80, FlatStyle = FlatStyle.Flat };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += BtnOk_Click;

            this.AcceptButton = btnOk; this.CancelButton = btnCa;
            this.Controls.AddRange(new Control[] { lblClave, lblTipo, cboTipo, lblVal, txtValor, btnOk, btnCa });
        }

        private void BtnOk_Click(object? s, EventArgs e)
        {
            string tipo = cboTipo.SelectedItem?.ToString() ?? "string";
            string val  = txtValor.Text;

            JsonNodeResultado = tipo switch
            {
                "number"   => double.TryParse(val, System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture, out double d)
                                  ? JsonValue.Create(d) : JsonValue.Create(val),
                "boolean"  => bool.TryParse(val, out bool b) ? JsonValue.Create(b) : JsonValue.Create(false),
                "null"     => null,
                "object {}"=> new JsonObject(),
                "array []" => new JsonArray(),
                _          => JsonValue.Create(val)
            };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
