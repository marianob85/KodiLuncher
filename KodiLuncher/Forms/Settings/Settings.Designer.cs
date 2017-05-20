namespace KodiLuncher.Forms.Settings
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_treeView = new System.Windows.Forms.TreeView();
            this.m_mainPanel = new System.Windows.Forms.Panel();
            this.m_import = new System.Windows.Forms.Button();
            this.m_export = new System.Windows.Forms.Button();
            this.m_close = new System.Windows.Forms.Button();
            this.m_save = new System.Windows.Forms.Button();
            this.m_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_treeView
            // 
            this.m_treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_treeView.Location = new System.Drawing.Point(13, 13);
            this.m_treeView.Name = "m_treeView";
            this.m_treeView.Size = new System.Drawing.Size(156, 259);
            this.m_treeView.TabIndex = 0;
            // 
            // m_mainPanel
            // 
            this.m_mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mainPanel.Location = new System.Drawing.Point(175, 13);
            this.m_mainPanel.Name = "m_mainPanel";
            this.m_mainPanel.Size = new System.Drawing.Size(375, 259);
            this.m_mainPanel.TabIndex = 1;
            // 
            // m_import
            // 
            this.m_import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_import.Location = new System.Drawing.Point(13, 283);
            this.m_import.Name = "m_import";
            this.m_import.Size = new System.Drawing.Size(75, 23);
            this.m_import.TabIndex = 2;
            this.m_import.Text = "Import";
            this.m_import.UseVisualStyleBackColor = true;
            this.m_import.Click += new System.EventHandler(this.m_import_Click);
            // 
            // m_export
            // 
            this.m_export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_export.Location = new System.Drawing.Point(94, 283);
            this.m_export.Name = "m_export";
            this.m_export.Size = new System.Drawing.Size(75, 23);
            this.m_export.TabIndex = 3;
            this.m_export.Text = "Export";
            this.m_export.UseVisualStyleBackColor = true;
            this.m_export.Click += new System.EventHandler(this.m_export_Click);
            // 
            // m_close
            // 
            this.m_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_close.Location = new System.Drawing.Point(475, 283);
            this.m_close.Name = "m_close";
            this.m_close.Size = new System.Drawing.Size(75, 23);
            this.m_close.TabIndex = 4;
            this.m_close.Text = "Close";
            this.m_close.UseVisualStyleBackColor = true;
            this.m_close.Click += new System.EventHandler(this.m_close_Click);
            // 
            // m_save
            // 
            this.m_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_save.Location = new System.Drawing.Point(394, 283);
            this.m_save.Name = "m_save";
            this.m_save.Size = new System.Drawing.Size(75, 23);
            this.m_save.TabIndex = 5;
            this.m_save.Text = "Save";
            this.m_save.UseVisualStyleBackColor = true;
            this.m_save.Click += new System.EventHandler(this.m_save_Click);
            // 
            // m_cancel
            // 
            this.m_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancel.Location = new System.Drawing.Point(313, 283);
            this.m_cancel.Name = "m_cancel";
            this.m_cancel.Size = new System.Drawing.Size(75, 23);
            this.m_cancel.TabIndex = 6;
            this.m_cancel.Text = "Cancel";
            this.m_cancel.UseVisualStyleBackColor = true;
            this.m_cancel.Click += new System.EventHandler(this.m_cancel_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 318);
            this.Controls.Add(this.m_cancel);
            this.Controls.Add(this.m_save);
            this.Controls.Add(this.m_close);
            this.Controls.Add(this.m_export);
            this.Controls.Add(this.m_import);
            this.Controls.Add(this.m_mainPanel);
            this.Controls.Add(this.m_treeView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(578, 357);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_treeView;
        private System.Windows.Forms.Panel m_mainPanel;
        private System.Windows.Forms.Button m_import;
        private System.Windows.Forms.Button m_export;
        private System.Windows.Forms.Button m_close;
        private System.Windows.Forms.Button m_save;
        private System.Windows.Forms.Button m_cancel;
    }
}