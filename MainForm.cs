using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
//using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace JSONManager
{
    public partial class MainForm : Form
    {
        // ── Estado ───────────────────────────────────────────────────────
        private JsonNode?  _raiz        = null;
        private string?    _rutaActual  = null;
        private bool       _modificado  = false;
        private bool       _esNuevo     = false;
        private bool       _actualizando = false;
        private bool       _formatoIndentado = true;

        public MainForm()
        {
            InitializeComponent();
            AplicarEstilosGrid();
            ActualizarUI();
        }

        private void AplicarEstilosGrid()
        {
            gridPropiedades.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 100, 60);
            gridPropiedades.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridPropiedades.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            gridPropiedades.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 248, 238);
            gridPropiedades.Font = new Font("Segoe UI", 9f);
        }

        // ════════════════════════════════════════════════════════════════
        //  HANDLERS DEL DESIGNER
        // ════════════════════════════════════════════════════════════════
        private void tsmiNuevo_Click(object sender, EventArgs e)              => NuevoArchivo();
        private void tsmiAbrir_Click(object sender, EventArgs e)              => AbrirArchivo();
        private void tsmiGuardar_Click(object sender, EventArgs e)            => GuardarArchivo();
        private void tsmiGuardarComo_Click(object sender, EventArgs e)        => GuardarComo();
        private void tsmiEliminar_Click(object sender, EventArgs e)           => EliminarArchivo();
        private void tsmiSalir_Click(object sender, EventArgs e)              => Application.Exit();
        private void tsmiAgregarPropiedad_Click(object sender, EventArgs e)   => AgregarPropiedad();
        private void tsmiAgregarItem_Click(object sender, EventArgs e)        => AgregarItemArray();
        private void tsmiEliminarNodo_Click(object sender, EventArgs e)       => EliminarNodo();
        private void tsmiExpandir_Click(object sender, EventArgs e)           => treeJson.ExpandAll();
        private void tsmiContraer_Click(object sender, EventArgs e)           => treeJson.CollapseAll();
        private void tsmiToggleJson_Click(object sender, EventArgs e)         => splitLeft.Panel2Collapsed = !splitLeft.Panel2Collapsed;
        private void tsmiRefrescar_Click(object sender, EventArgs e)          => RefrescarJsonRaw();
        private void tsmiFormatear_Click(object sender, EventArgs e)          => ToggleFormato();
        private void treeJson_AfterSelect(object sender, TreeViewEventArgs e) => MostrarPropiedades(e.Node);
        private void treeJson_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) => EditarNodo(e.Node);
        private void gridPropiedades_CellEndEdit(object sender, DataGridViewCellEventArgs e)     => AplicarCambioPropiedad(e);
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)                 => ConfirmarSalida(e);

        // ════════════════════════════════════════════════════════════════
        //  OPERACIONES DE ARCHIVO
        // ════════════════════════════════════════════════════════════════

        private void NuevoArchivo()
        {
            if (!ConfirmarDescartarCambios()) return;

            using var dlg = new NuevoJsonDialog();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            _rutaActual = dlg.RutaCompleta;
            _raiz       = dlg.Nodo;
            _esNuevo    = true;
            _modificado = false;

            CargarArbol();
            RefrescarJsonRaw();
            ActualizarUI();
            MostrarEstado($"Nuevo archivo: {Path.GetFileName(_rutaActual)}");
        }

        private void AbrirArchivo()
        {
            if (!ConfirmarDescartarCambios()) return;

            using var dlg = new OpenFileDialog
            {
                Title  = "Abrir archivo JSON",
                Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*"
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                string texto = File.ReadAllText(dlg.FileName, Encoding.UTF8);
                _raiz       = JsonNode.Parse(texto, nodeOptions: new JsonNodeOptions { PropertyNameCaseInsensitive = true });
                _rutaActual = dlg.FileName;
                _esNuevo    = false;
                _modificado = false;

                CargarArbol();
                RefrescarJsonRaw();
                ActualizarUI();

                int nodos = ContarNodos(_raiz);
                MostrarEstado($"Abierto: {Path.GetFileName(_rutaActual)}  —  {nodos} nodos");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo:\n{ex.Message}", "Error JSON",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarArchivo()
        {
            if (_raiz == null || _rutaActual == null) return;
            if (_esNuevo) { GuardarComo(); return; }

            try
            {
                EscribirJson(_rutaActual);
                _modificado = false;
                ActualizarTitulo();
                MostrarEstado($"Guardado: {Path.GetFileName(_rutaActual)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarComo()
        {
            if (_raiz == null) return;

            using var dlg = new SaveFileDialog
            {
                Title    = "Guardar Como",
                Filter   = "Archivos JSON (*.json)|*.json",
                FileName = _rutaActual != null ? Path.GetFileName(_rutaActual) : "nuevo.json"
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                _rutaActual = dlg.FileName;
                EscribirJson(_rutaActual);
                _modificado = false;
                _esNuevo    = false;
                ActualizarUI();
                MostrarEstado($"Guardado como: {Path.GetFileName(_rutaActual)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarArchivo()
        {
            if (_rutaActual == null) return;

            string nombre = Path.GetFileName(_rutaActual);
            var res = MessageBox.Show($"¿Eliminar '{nombre}' del disco?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (res != DialogResult.Yes) return;

            try
            {
                if (!_esNuevo && File.Exists(_rutaActual))
                    File.Delete(_rutaActual);

                _raiz = null; _rutaActual = null; _modificado = false; _esNuevo = false;
                treeJson.Nodes.Clear();
                gridPropiedades.Rows.Clear();
                txtJsonRaw.Clear();
                lblNodoInfo.Text = "Selecciona un nodo en el árbol JSON para ver sus propiedades.";
                ActualizarUI();
                MostrarEstado($"Eliminado: {nombre}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EscribirJson(string ruta)
        {
            var opts = new JsonSerializerOptions { WriteIndented = _formatoIndentado };
            string json = _raiz!.ToJsonString(opts);
            File.WriteAllText(ruta, json, new UTF8Encoding(false));
        }

        // ════════════════════════════════════════════════════════════════
        //  ÁRBOL JSON
        // ════════════════════════════════════════════════════════════════

        private void CargarArbol()
        {
            treeJson.BeginUpdate();
            treeJson.Nodes.Clear();
            if (_raiz == null) { treeJson.EndUpdate(); return; }

            var raizNode = CrearTreeNode("raíz", _raiz);
            treeJson.Nodes.Add(raizNode);
            AgregarHijosArbol(raizNode, _raiz);
            raizNode.Expand();

            treeJson.EndUpdate();
            ActualizarTitulo();
        }

        private void AgregarHijosArbol(TreeNode tn, JsonNode nodo)
        {
            if (nodo is JsonObject obj)
            {
                foreach (var prop in obj)
                {
                    var hijo = CrearTreeNode(prop.Key, prop.Value);
                    tn.Nodes.Add(hijo);
                    if (prop.Value is JsonObject or JsonArray)
                        AgregarHijosArbol(hijo, prop.Value!);
                }
            }
            else if (nodo is JsonArray arr)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    var hijo = CrearTreeNode($"[{i}]", arr[i]);
                    tn.Nodes.Add(hijo);
                    if (arr[i] is JsonObject or JsonArray)
                        AgregarHijosArbol(hijo, arr[i]!);
                }
            }
        }

        private static TreeNode CrearTreeNode(string clave, JsonNode? nodo)
        {
            string tipo  = GetTipoNodo(nodo);
            string label = BuildLabel(clave, nodo);
            var tn = new TreeNode(label) { Tag = new JsonNodeRef(clave, nodo) };

            tn.ForeColor = tipo switch
            {
                "object" => Color.FromArgb(20, 80, 160),
                "array"  => Color.FromArgb(130, 60, 10),
                "string" => Color.FromArgb(20, 120, 40),
                "number" => Color.FromArgb(160, 40, 120),
                "bool"   => Color.FromArgb(40, 120, 160),
                _        => Color.FromArgb(120, 120, 120),
            };
            return tn;
        }

        private static string BuildLabel(string clave, JsonNode? nodo)
        {
            string tipo = GetTipoNodo(nodo);
            return tipo switch
            {
                "object" => $"{clave}  {{...{(nodo as JsonObject)?.Count ?? 0} props}}",
                "array"  => $"{clave}  [...{(nodo as JsonArray)?.Count ?? 0} ítems]",
                _        => $"{clave}: {TruncateVal(nodo?.ToJsonString() ?? "null", 50)}"
            };
        }

        private static string TruncateVal(string s, int max) =>
            s.Length <= max ? s : s[..max] + "…";

        private static string GetTipoNodo(JsonNode? n) => n switch
        {
            JsonObject => "object",
            JsonArray  => "array",
            JsonValue v when v.TryGetValue<bool>(out _)   => "bool",
            JsonValue v when v.TryGetValue<double>(out _) => "number",
            JsonValue v when v.TryGetValue<string>(out _) => "string",
            null       => "null",
            _          => "value"
        };

        // ════════════════════════════════════════════════════════════════
        //  PROPIEDADES DEL NODO
        // ════════════════════════════════════════════════════════════════

        private void MostrarPropiedades(TreeNode? tn)
        {
            gridPropiedades.Rows.Clear();
            if (tn?.Tag is not JsonNodeRef ref_) { lblNodoInfo.Text = ""; return; }

            _actualizando = true;
            var nodo = ref_.Nodo;
            string tipo = GetTipoNodo(nodo);
            string ruta = BuildRutaArbol(tn);

            lblNodoInfo.Text = $"Nodo: {ref_.Clave}  |  Tipo: {tipo}\nRuta: {ruta}";

            if (nodo is JsonObject obj)
            {
                lblNodoInfo.Text += $"  |  Propiedades: {obj.Count}";
                foreach (var prop in obj)
                {
                    string t = GetTipoNodo(prop.Value);
                    string v = prop.Value is JsonObject or JsonArray
                        ? $"[{t}]"
                        : prop.Value?.ToJsonString() ?? "null";
                    gridPropiedades.Rows.Add(prop.Key, t, v);
                    EstilarFila(gridPropiedades.Rows[gridPropiedades.Rows.Count - 1], t);
                }
            }
            else if (nodo is JsonArray arr)
            {
                lblNodoInfo.Text += $"  |  Ítems: {arr.Count}";
                for (int i = 0; i < arr.Count; i++)
                {
                    string t = GetTipoNodo(arr[i]);
                    string v = arr[i] is JsonObject or JsonArray ? $"[{t}]" : arr[i]?.ToJsonString() ?? "null";
                    gridPropiedades.Rows.Add($"[{i}]", t, v);
                    EstilarFila(gridPropiedades.Rows[gridPropiedades.Rows.Count - 1], t);
                }
            }
            else
            {
                // Valor primitivo
                gridPropiedades.Rows.Add(ref_.Clave, tipo, nodo?.ToJsonString() ?? "null");
                EstilarFila(gridPropiedades.Rows[0], tipo);
            }

            _actualizando = false;
            ResaltarEnJsonRaw(ref_.Clave);
        }

        private static void EstilarFila(DataGridViewRow row, string tipo)
        {
            row.Cells[1].Style.ForeColor = tipo switch
            {
                "object" => Color.FromArgb(20, 80, 160),
                "array"  => Color.FromArgb(130, 60, 10),
                "string" => Color.FromArgb(20, 120, 40),
                "number" => Color.FromArgb(160, 40, 120),
                "bool"   => Color.FromArgb(40, 120, 160),
                _        => Color.Gray,
            };
        }

        private static string BuildRutaArbol(TreeNode tn)
        {
            var partes = new List<string>();
            var actual = tn;
            while (actual != null)
            {
                if (actual.Tag is JsonNodeRef r) partes.Insert(0, r.Clave);
                actual = actual.Parent;
            }
            return string.Join(" › ", partes);
        }

        // ════════════════════════════════════════════════════════════════
        //  EDICIÓN DE NODOS
        // ════════════════════════════════════════════════════════════════

        private void AgregarPropiedad()
        {
            if (_raiz == null) return;

            // Determinar el objeto destino
            JsonObject? destino = null;
            if (treeJson.SelectedNode?.Tag is JsonNodeRef ref_ && ref_.Nodo is JsonObject sel)
                destino = sel;
            else if (_raiz is JsonObject root)
                destino = root;

            if (destino == null)
            {
                MessageBox.Show("Selecciona un nodo de tipo object para agregar una propiedad.", "Tipo incorrecto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string? clave = Prompt("Nueva propiedad", "Nombre de la propiedad (clave):");
            if (string.IsNullOrWhiteSpace(clave)) return;

            if (destino.ContainsKey(clave))
            { MessageBox.Show($"La clave '{clave}' ya existe.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            using var dlg = new ValorDialog("Valor de la propiedad", clave);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            destino[clave] = dlg.JsonNodeResultado;
            MarcarModificado();
            CargarArbol();
            RefrescarJsonRaw();
            MostrarEstado($"Propiedad '{clave}' agregada.");
        }

        private void AgregarItemArray()
        {
            if (_raiz == null) return;

            JsonArray? arr = null;
            if (treeJson.SelectedNode?.Tag is JsonNodeRef ref_ && ref_.Nodo is JsonArray sel)
                arr = sel;
            else if (_raiz is JsonArray root)
                arr = root;

            if (arr == null)
            {
                MessageBox.Show("Selecciona un nodo de tipo array para agregar un ítem.", "Tipo incorrecto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new ValorDialog("Nuevo ítem de array", $"[{arr.Count}]");
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            arr.Add(dlg.JsonNodeResultado);
            MarcarModificado();
            CargarArbol();
            RefrescarJsonRaw();
            MostrarEstado($"Ítem [{arr.Count - 1}] agregado al array.");
        }

        private void EliminarNodo()
        {
            if (treeJson.SelectedNode?.Tag is not JsonNodeRef ref_) return;
            if (treeJson.SelectedNode.Parent == null)
            { MessageBox.Show("No se puede eliminar la raíz.", "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var res = MessageBox.Show($"¿Eliminar '{ref_.Clave}' y todo su contenido?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes) return;

            // Obtener el padre para eliminar
            var padreRef = treeJson.SelectedNode.Parent?.Tag as JsonNodeRef;
            if (padreRef?.Nodo is JsonObject obj)
            {
                obj.Remove(ref_.Clave);
            }
            else if (padreRef?.Nodo is JsonArray arr)
            {
                // Clave es "[N]"
                if (int.TryParse(ref_.Clave.Trim('[', ']'), out int idx) && idx < arr.Count)
                    arr.RemoveAt(idx);
            }

            MarcarModificado();
            CargarArbol();
            RefrescarJsonRaw();
            gridPropiedades.Rows.Clear();
            lblNodoInfo.Text = "";
            MostrarEstado("Nodo eliminado.");
        }

        private void EditarNodo(TreeNode tn)
        {
            if (tn.Tag is not JsonNodeRef ref_) return;
            if (ref_.Nodo is JsonObject or JsonArray) return;  // no editar contenedores

            string valorActual = ref_.Nodo?.ToJsonString() ?? "null";
            using var dlg = new ValorDialog($"Editar: {ref_.Clave}", ref_.Clave, valorActual);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            // Reemplazar el valor en el padre
            var padreRef = tn.Parent?.Tag as JsonNodeRef;
            if (padreRef?.Nodo is JsonObject obj)
                obj[ref_.Clave] = dlg.JsonNodeResultado;
            else if (padreRef?.Nodo is JsonArray arr)
                if (int.TryParse(ref_.Clave.Trim('[', ']'), out int idx))
                    arr[idx] = dlg.JsonNodeResultado;

            MarcarModificado();
            CargarArbol();
            RefrescarJsonRaw();
            MostrarEstado($"Valor de '{ref_.Clave}' actualizado.");
        }

        private void AplicarCambioPropiedad(DataGridViewCellEventArgs e)
        {
            if (_actualizando || e.ColumnIndex != 2) return;
            if (treeJson.SelectedNode?.Tag is not JsonNodeRef ref_) return;
            if (ref_.Nodo is not JsonObject obj) return;

            var row  = gridPropiedades.Rows[e.RowIndex];
            string clave = row.Cells[0].Value?.ToString() ?? "";
            string nuevoVal = row.Cells[2].Value?.ToString() ?? "";
            string tipo = row.Cells[1].Value?.ToString() ?? "string";

            if (tipo is "object" or "array") return;  // no editar contenedores desde el grid

            obj[clave] = ParsearValorJson(nuevoVal, tipo);
            MarcarModificado();
            // Refrescar solo la etiqueta del nodo hijo correspondiente
            foreach (TreeNode hijo in treeJson.SelectedNode!.Nodes)
            {
                if (hijo.Tag is JsonNodeRef cr && cr.Clave == clave)
                {
                    hijo.Tag  = new JsonNodeRef(clave, obj[clave]);
                    hijo.Text = BuildLabel(clave, obj[clave]);
                    break;
                }
            }
            RefrescarJsonRaw();
        }

        private static JsonNode? ParsearValorJson(string texto, string tipo)
        {
            return tipo switch
            {
                "number" => double.TryParse(texto, System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture, out double d)
                                ? JsonValue.Create(d) : JsonValue.Create(texto),
                "bool"   => bool.TryParse(texto, out bool b) ? JsonValue.Create(b) : JsonValue.Create(texto),
                "null"   => null,
                _        => JsonValue.Create(texto.Trim('"'))
            };
        }

        // ════════════════════════════════════════════════════════════════
        //  JSON RAW + SINTAXIS
        // ════════════════════════════════════════════════════════════════

        private void RefrescarJsonRaw()
        {
            if (_raiz == null) { txtJsonRaw.Clear(); return; }

            var opts = new JsonSerializerOptions { WriteIndented = _formatoIndentado };
            string json = _raiz.ToJsonString(opts);
            txtJsonRaw.Text = json;
            AplicarColorSintaxis(json);
        }

        private void AplicarColorSintaxis(string json)
        {
            txtJsonRaw.SuspendLayout();

            // Base: todo en color por defecto
            txtJsonRaw.SelectAll();
            txtJsonRaw.SelectionColor = Color.FromArgb(200, 240, 210);

            // Colorear strings (claves y valores)
            ColorearStrings(json);

            // Números
            ColorearPatronRegex(json, @"\b-?\d+(\.\d+)?([eE][+-]?\d+)?\b", Color.FromArgb(255, 180, 90));

            // true / false / null
            ColorearPalabras(json, new[] { "true", "false" }, Color.FromArgb(100, 200, 255));
            ColorearPalabras(json, new[] { "null" }, Color.FromArgb(160, 160, 160));

            // Llaves y corchetes
            ColorearChars(json, new[] { '{', '}', '[', ']' }, Color.FromArgb(255, 230, 100));

            // Dos puntos y comas
            ColorearChars(json, new[] { ':', ',' }, Color.FromArgb(160, 200, 170));

            txtJsonRaw.ResumeLayout();
        }

        private void ColorearStrings(string texto)
        {
            int pos = 0;
            while (pos < texto.Length)
            {
                int inicio = texto.IndexOf('"', pos);
                if (inicio < 0) break;
                int fin = inicio + 1;
                while (fin < texto.Length)
                {
                    if (texto[fin] == '\\') { fin += 2; continue; }
                    if (texto[fin] == '"')  { fin++; break; }
                    fin++;
                }
                // Determinar si es clave (seguida de ':') o valor
                int afterFin = fin;
                while (afterFin < texto.Length && texto[afterFin] == ' ') afterFin++;
                bool esClave = afterFin < texto.Length && texto[afterFin] == ':';

                txtJsonRaw.Select(inicio, fin - inicio);
                txtJsonRaw.SelectionColor = esClave
                    ? Color.FromArgb(130, 220, 130)   // clave verde claro
                    : Color.FromArgb(255, 140, 100);  // valor string naranja

                pos = fin;
            }
        }

        private void ColorearPatronRegex(string texto, string patron, Color color)
        {
            var matches = System.Text.RegularExpressions.Regex.Matches(texto, patron);
            foreach (System.Text.RegularExpressions.Match m in matches)
            {
                txtJsonRaw.Select(m.Index, m.Length);
                txtJsonRaw.SelectionColor = color;
            }
        }

        private void ColorearPalabras(string texto, string[] palabras, Color color)
        {
            foreach (string p in palabras)
            {
                int pos = 0;
                while ((pos = texto.IndexOf(p, pos, StringComparison.Ordinal)) >= 0)
                {
                    // Verificar que sea una palabra completa (no dentro de una cadena)
                    bool inicio = pos == 0 || !char.IsLetterOrDigit(texto[pos - 1]);
                    bool fin    = pos + p.Length >= texto.Length || !char.IsLetterOrDigit(texto[pos + p.Length]);
                    if (inicio && fin)
                    {
                        txtJsonRaw.Select(pos, p.Length);
                        txtJsonRaw.SelectionColor = color;
                    }
                    pos += p.Length;
                }
            }
        }

        private void ColorearChars(string texto, char[] chars, Color color)
        {
            for (int i = 0; i < texto.Length; i++)
            {
                if (Array.IndexOf(chars, texto[i]) >= 0)
                {
                    txtJsonRaw.Select(i, 1);
                    txtJsonRaw.SelectionColor = color;
                }
            }
        }

        private void ResaltarEnJsonRaw(string clave)
        {
            if (txtJsonRaw.Text.Length == 0) return;
            string buscar = $"\"{clave}\"";
            int idx = txtJsonRaw.Text.IndexOf(buscar, StringComparison.Ordinal);
            if (idx >= 0)
            {
                txtJsonRaw.Select(idx, buscar.Length);
                txtJsonRaw.ScrollToCaret();
            }
        }

        private void ToggleFormato()
        {
            _formatoIndentado = !_formatoIndentado;
            RefrescarJsonRaw();
            MostrarEstado(_formatoIndentado ? "Formato: indentado" : "Formato: minificado");
        }

        // ════════════════════════════════════════════════════════════════
        //  UI HELPERS
        // ════════════════════════════════════════════════════════════════

        private void MarcarModificado()
        {
            _modificado = true;
            ActualizarTitulo();
        }

        private void ActualizarTitulo()
        {
            string nombre = _rutaActual != null ? Path.GetFileName(_rutaActual) : "Sin archivo";
            string flag   = _modificado ? " *" : "";
            this.Text       = $"Gestor de Archivos JSON  —  {nombre}{flag}";
            lblRuta.Text    = _rutaActual != null ? $"  {_rutaActual}" : "";
            lblTreeTitle.Text = $"  Árbol JSON  —  {nombre}{flag}";
        }

        private void ActualizarUI()
        {
            bool hay = _raiz != null;
            tsmiGuardar.Enabled          = hay;
            tsmiGuardarComo.Enabled      = hay;
            tsmiEliminar.Enabled         = hay && _rutaActual != null;
            tsmiEditar.Enabled           = hay;
            btnGuardarTS.Enabled         = hay;
            btnAgregarPropTS.Enabled     = hay;
            btnAgregarItemTS.Enabled     = hay;
            btnEliminarNodoTS.Enabled    = hay;
            btnExpandirTS.Enabled        = hay;
            btnContraerTS.Enabled        = hay;
            btnEliminarArchivoTS.Enabled = hay && _rutaActual != null;
            ActualizarTitulo();
        }

        private void MostrarEstado(string texto) => lblStatus.Text = texto;

        private static int ContarNodos(JsonNode? n) => n switch
        {
            JsonObject obj => 1 + obj.Count + obj.Values.Sum(v => ContarNodos(v)),
            JsonArray arr  => 1 + arr.Sum(v => ContarNodos(v)),
            _              => 1
        };

        private bool ConfirmarDescartarCambios()
        {
            if (!_modificado) return true;
            var res = MessageBox.Show("Hay cambios sin guardar. ¿Descartar y continuar?",
                "Cambios pendientes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) return true;
            if (res == DialogResult.No) { GuardarArchivo(); return !_modificado; }
            return false;
        }

        private void ConfirmarSalida(FormClosingEventArgs e)
        {
            if (!_modificado) return;
            var res = MessageBox.Show("Hay cambios sin guardar. ¿Guardar antes de salir?",
                "Cambios pendientes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)        GuardarArchivo();
            else if (res == DialogResult.Cancel) e.Cancel = true;
        }

        private static string? Prompt(string titulo, string mensaje, string? valorInicial = null)
        {
            using var frm = new Form { Text = titulo, Size = new Size(420, 140),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false };
            var lbl  = new Label  { Text = mensaje, Dock = DockStyle.Top, Height = 34, Padding = new Padding(10, 10, 8, 0) };
            var txt  = new TextBox{ Dock = DockStyle.Top, Text = valorInicial ?? "" };
            var pan  = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 38, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(6, 4, 6, 4) };
            var btnOk= new Button { Text = "Aceptar",  DialogResult = DialogResult.OK,     Width = 80 };
            var btnCa= new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Width = 80 };
            frm.AcceptButton = btnOk; frm.CancelButton = btnCa;
            pan.Controls.AddRange(new Control[] { btnCa, btnOk });
            frm.Controls.AddRange(new Control[] { lbl, txt, pan });
            txt.SelectAll();
            return frm.ShowDialog() == DialogResult.OK ? txt.Text : null;
        }
    }

    // ── Referencia para el Tag del TreeNode 
    internal class JsonNodeRef
    {
        public string    Clave { get; }
        public JsonNode? Nodo  { get; }
        public JsonNodeRef(string clave, JsonNode? nodo) { Clave = clave; Nodo = nodo; }
    }
}
