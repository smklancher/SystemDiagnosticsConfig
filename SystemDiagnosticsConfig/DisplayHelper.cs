﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemDiagnosticsConfig
{
    public class DisplayHelper
    {
        BindingSource bs=new BindingSource();

        #region "Controls"
        private DataGridView dgv;

        private DataGridViewCheckBoxColumn enabledColumn = new DataGridViewCheckBoxColumn();
        private DataGridViewTextBoxColumn fileColumn = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
        private DataGridViewComboBoxColumn Level=new DataGridViewComboBoxColumn();
        private DataGridViewTextBoxColumn Location=new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn Details= new DataGridViewTextBoxColumn();

        public DataGridView DataGridView
        {
            get
            {
                return dgv;
            }
            set
            {
                dgv = value;

                ((System.ComponentModel.ISupportInitialize)(dgv)).BeginInit();

                //if not in this mode, must click twice to select row then edit/open dropdown
                dgv.EditMode = DataGridViewEditMode.EditOnEnter;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.AutoGenerateColumns = false;
                dgv.MultiSelect = false;
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

                // 
                // Enabled
                // 
                enabledColumn.DataPropertyName = "Enabled";
                enabledColumn.HeaderText = "Enabled";
                enabledColumn.Name = "Enabled";
                // 
                // 
                // Name
                // 
                nameColumn.DataPropertyName = "Name";
                nameColumn.HeaderText = "Name";
                nameColumn.Name = "Name";
                // 
                // 
                // File
                // 
                fileColumn.DataPropertyName = "ConfigFilename";
                fileColumn.HeaderText = "File";
                fileColumn.Name = "File";
                // 
                // 
                // Level
                // 
                Level.DataPropertyName = "Level";
                Level.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                Level.HeaderText = "Level";
                Level.Name = "Level";
                Level.Resizable = DataGridViewTriState.True;
                Level.SortMode = DataGridViewColumnSortMode.Automatic;
                // 
                // Location
                // 
                Location.DataPropertyName = "Location";
                Location.HeaderText = "Location";
                Location.Name = "Location";
                // 
                // Details
                // 
                Details.DataPropertyName = "Details";
                Details.HeaderText = "Details";
                Details.Name = "Details";
                Details.ReadOnly = true;


                dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dgv.Columns.Clear();
                dgv.Columns.AddRange(
                    new DataGridViewColumn[] {enabledColumn, fileColumn, nameColumn,Level,Location,Details}
                    );

                bs.DataSource = typeof(LogDefinition);
                dgv.DataSource = bs;
                
                ((System.ComponentModel.ISupportInitialize)(dgv)).EndInit();
            }
        }

        private TextBox textBox;
        public TextBox TextBox
        {
            get
            {
                return textBox;
            }
            set
            {
                textBox = value;
            }
        }

        private PropertyGrid propertyGrid;
        public PropertyGrid PropertyGrid
        {
            get
            {

                return propertyGrid;
            }
            set
            {
                propertyGrid = value;
            }
        }
        #endregion

        public LogDefinition SelectedLogDef
        {
            get
            {
                DataGridViewRow row=null;
                if (dgv.SelectedCells.Count > 0)
                {
                    row = dgv.SelectedCells[0].OwningRow;
                }
                if (dgv.SelectedRows.Count > 0)
                {
                    row = dgv.SelectedRows[0];
                }
                if (row?.DataBoundItem is LogDefinition ld)
                {
                    return ld;
                }
                return null;
            }
        }

        public void SelectedLogDefChanged()
        {
            var log = SelectedLogDef;
            if (log != null)
            {
                TextBox.Text = log.Config.SysDiagObjXml().ToString();
                propertyGrid.SelectedObject = log;
            }

        }

        private void SizeDataGridColumns()
        {
            for (int i = 0; i < dgv.Columns.Count-1; i++)
            {
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                int colw = dgv.Columns[i].Width;
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns[i].Width = colw;
            }

            dgv.Columns[dgv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        public void DisplayConfigs(ConfigCollection configs)
        {
            // Display configs in property grid
            propertyGrid.SelectedObject = configs;
            propertyGrid.ExpandAllGridItems();

            // Display log defs in datagrid
            var defs = configs.SelectMany(x => x.Definitions);
            bs.Clear();
            foreach (var l in defs)
            {
                //l.Details = l.LogFileSize==string.Empty ? l.Details : $"{l.LogFileSize}, {l.Details}";
                bs.Add(l);
            }

            // Bind dropdown to available levels per def type
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var def = (LogDefinition)row.DataBoundItem;
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)(row.Cells["Level"]);
                cell.DataSource = def.AvailableLevels.ToList();
            }

            SizeDataGridColumns();
        }

        public void OpenConfig()
        {
            var log = SelectedLogDef;
            if (log == null) return;

            Process.Start(log.Config.Filename);
        }

        public void OpenLogOrFolder()
        {
            var log = SelectedLogDef;
            if (log == null) return;


            // event log (check type, or recognize name) = open event log
            if (log.ListenerType.Contains("eventlog"))
            {
                Process.Start("eventvwr.msc");
                return;
            }

            FileInfo f = log.LogFile;

            if (f.Exists)
            {
                Process.Start(f.FullName);
            }else if (f.Directory.Exists)
            {
                Process.Start(f.Directory.FullName);
            }

            // if no criteria above is met, do nothing
        }

        public string BackupConfigs(ConfigCollection configs)
        {
            StringBuilder msg = new StringBuilder("Config backup results:\n");
            foreach(var c in configs)
            {
                string bkname = Path.GetFileNameWithoutExtension(c.Filename);
                string timestamp = DateTime.Now.ToString("s").Replace(":", ".");
                bkname = Path.Combine(Path.GetDirectoryName(c.Filename),$"{bkname}_{timestamp}.config");

                try
                {
                    File.Copy(c.Filename, bkname,true);
                    msg.AppendLine($"SUCCESS: {bkname}");
                }
                catch (Exception ex)
                {
                    msg.AppendLine($"ERROR: {bkname}");
                    msg.AppendLine($"\t{ex.Message}");
                }
            }
            return msg.ToString();
        }
    }
}