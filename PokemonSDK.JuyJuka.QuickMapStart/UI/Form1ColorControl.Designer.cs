namespace PokemonSDK.JuyJuka.QuickMapStart.UI
{
  partial class Form1ColorControl
  {
    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.button1 = new Button();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.trackBar3 = new TrackBar();
      this.trackBar2 = new TrackBar();
      this.numericUpDown3 = new NumericUpDown();
      this.numericUpDown2 = new NumericUpDown();
      this.numericUpDown1 = new NumericUpDown();
      this.trackBar1 = new TrackBar();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)this.trackBar3).BeginInit();
      ((System.ComponentModel.ISupportInitialize)this.trackBar2).BeginInit();
      ((System.ComponentModel.ISupportInitialize)this.numericUpDown3).BeginInit();
      ((System.ComponentModel.ISupportInitialize)this.numericUpDown2).BeginInit();
      ((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
      ((System.ComponentModel.ISupportInitialize)this.trackBar1).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = DockStyle.Top;
      this.label1.Location = new Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(68, 30);
      this.label1.TabIndex = 0;
      this.label1.Text = "label1";
      // 
      // button1
      // 
      this.button1.Dock = DockStyle.Left;
      this.button1.Location = new Point(0, 30);
      this.button1.Name = "button1";
      this.button1.Size = new Size(131, 87);
      this.button1.TabIndex = 1;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += this.button1_Click;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
      this.tableLayoutPanel1.Controls.Add(this.trackBar3, 2, 1);
      this.tableLayoutPanel1.Controls.Add(this.trackBar2, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.numericUpDown3, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.numericUpDown2, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.numericUpDown1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.trackBar1, 0, 1);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(131, 30);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new Size(600, 87);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // trackBar3
      // 
      this.trackBar3.Dock = DockStyle.Fill;
      this.trackBar3.Location = new Point(402, 46);
      this.trackBar3.Maximum = 255;
      this.trackBar3.Name = "trackBar3";
      this.trackBar3.Size = new Size(195, 38);
      this.trackBar3.TabIndex = 5;
      this.trackBar3.ValueChanged += this._ValueChanged;
      // 
      // trackBar2
      // 
      this.trackBar2.Dock = DockStyle.Fill;
      this.trackBar2.Location = new Point(202, 46);
      this.trackBar2.Maximum = 255;
      this.trackBar2.Name = "trackBar2";
      this.trackBar2.Size = new Size(194, 38);
      this.trackBar2.TabIndex = 4;
      this.trackBar2.ValueChanged += this._ValueChanged;
      // 
      // numericUpDown3
      // 
      this.numericUpDown3.Dock = DockStyle.Fill;
      this.numericUpDown3.Location = new Point(402, 3);
      this.numericUpDown3.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
      this.numericUpDown3.Name = "numericUpDown3";
      this.numericUpDown3.Size = new Size(195, 35);
      this.numericUpDown3.TabIndex = 2;
      this.numericUpDown3.ValueChanged += this._ValueChanged;
      // 
      // numericUpDown2
      // 
      this.numericUpDown2.Dock = DockStyle.Fill;
      this.numericUpDown2.Location = new Point(202, 3);
      this.numericUpDown2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
      this.numericUpDown2.Name = "numericUpDown2";
      this.numericUpDown2.Size = new Size(194, 35);
      this.numericUpDown2.TabIndex = 1;
      this.numericUpDown2.ValueChanged += this._ValueChanged;
      // 
      // numericUpDown1
      // 
      this.numericUpDown1.Dock = DockStyle.Fill;
      this.numericUpDown1.Location = new Point(3, 3);
      this.numericUpDown1.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
      this.numericUpDown1.Name = "numericUpDown1";
      this.numericUpDown1.Size = new Size(193, 35);
      this.numericUpDown1.TabIndex = 0;
      this.numericUpDown1.ValueChanged += this._ValueChanged;
      // 
      // trackBar1
      // 
      this.trackBar1.Dock = DockStyle.Fill;
      this.trackBar1.Location = new Point(3, 46);
      this.trackBar1.Maximum = 255;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new Size(193, 38);
      this.trackBar1.TabIndex = 3;
      this.trackBar1.ValueChanged += this._ValueChanged;
      // 
      // Form1ColorControl
      // 
      this.AutoScaleDimensions = new SizeF(12F, 30F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label1);
      this.Name = "Form1ColorControl";
      this.Size = new Size(731, 117);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)this.trackBar3).EndInit();
      ((System.ComponentModel.ISupportInitialize)this.trackBar2).EndInit();
      ((System.ComponentModel.ISupportInitialize)this.numericUpDown3).EndInit();
      ((System.ComponentModel.ISupportInitialize)this.numericUpDown2).EndInit();
      ((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
      ((System.ComponentModel.ISupportInitialize)this.trackBar1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion

    private Label label1;
    private Button button1;
    private TableLayoutPanel tableLayoutPanel1;
    private NumericUpDown numericUpDown3;
    private NumericUpDown numericUpDown2;
    private NumericUpDown numericUpDown1;
    private TrackBar trackBar3;
    private TrackBar trackBar2;
    private TrackBar trackBar1;
  }
}
