namespace KodiLuncher.Forms.Settings.Nodes
{
    partial class NodeGeneral
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_kodiDelay = new System.Windows.Forms.TextBox();
            this.m_focusInterval = new System.Windows.Forms.TextBox();
            this.m_kodiDelayEnable = new System.Windows.Forms.CheckBox();
            this.m_focusIntervalEnable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Autostart kodi delay ( 0 disable ):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kodi focus interval ( 0 disable ):";
            // 
            // m_kodiDelay
            // 
            this.m_kodiDelay.Location = new System.Drawing.Point(180, 10);
            this.m_kodiDelay.Name = "m_kodiDelay";
            this.m_kodiDelay.Size = new System.Drawing.Size(100, 20);
            this.m_kodiDelay.TabIndex = 2;
            this.m_kodiDelay.TextChanged += new System.EventHandler(this.m_kodiDelay_TextChanged);
            // 
            // m_focusInterval
            // 
            this.m_focusInterval.Location = new System.Drawing.Point(180, 36);
            this.m_focusInterval.Name = "m_focusInterval";
            this.m_focusInterval.Size = new System.Drawing.Size(100, 20);
            this.m_focusInterval.TabIndex = 3;
            this.m_focusInterval.TextChanged += new System.EventHandler(this.m_focusInterval_TextChanged);
            // 
            // m_kodiDelayEnable
            // 
            this.m_kodiDelayEnable.AutoSize = true;
            this.m_kodiDelayEnable.Location = new System.Drawing.Point(286, 13);
            this.m_kodiDelayEnable.Name = "m_kodiDelayEnable";
            this.m_kodiDelayEnable.Size = new System.Drawing.Size(15, 14);
            this.m_kodiDelayEnable.TabIndex = 4;
            this.m_kodiDelayEnable.UseVisualStyleBackColor = true;
            this.m_kodiDelayEnable.CheckedChanged += new System.EventHandler(this.m_kodiDelayEnable_CheckedChanged);
            // 
            // m_focusIntervalEnable
            // 
            this.m_focusIntervalEnable.AutoSize = true;
            this.m_focusIntervalEnable.Location = new System.Drawing.Point(286, 39);
            this.m_focusIntervalEnable.Name = "m_focusIntervalEnable";
            this.m_focusIntervalEnable.Size = new System.Drawing.Size(15, 14);
            this.m_focusIntervalEnable.TabIndex = 5;
            this.m_focusIntervalEnable.UseVisualStyleBackColor = true;
            this.m_focusIntervalEnable.CheckedChanged += new System.EventHandler(this.m_focusIntervalEnable_CheckedChanged);
            // 
            // NodeGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_focusIntervalEnable);
            this.Controls.Add(this.m_kodiDelayEnable);
            this.Controls.Add(this.m_focusInterval);
            this.Controls.Add(this.m_kodiDelay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NodeGeneral";
            this.Size = new System.Drawing.Size(375, 259);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_kodiDelay;
        private System.Windows.Forms.TextBox m_focusInterval;
        private System.Windows.Forms.CheckBox m_kodiDelayEnable;
        private System.Windows.Forms.CheckBox m_focusIntervalEnable;
    }
}
