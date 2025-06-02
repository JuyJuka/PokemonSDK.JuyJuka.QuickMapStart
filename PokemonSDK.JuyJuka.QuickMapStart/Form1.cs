namespace PokemonSDK.JuyJuka.QuickMapStart
{
  using System.Drawing;

  public partial class Form1 : Form, ILogger
  {
    public virtual WorldMap WorldMap { get; protected set; } = new WorldMap();
    public Form1()
    {
      this.InitializeComponent();
      this.WorldMap.Logger = this;
      foreach (TableLayoutPanel grid in new TableLayoutPanel[]{
        this.tableLayoutPanel1,
        this.tableLayoutPanel2,
      })
      {
        grid.ColumnStyles[0].SizeType = SizeType.AutoSize;
        for (int i = 0; i < grid.RowCount; i++)
        {
          grid.RowStyles[i].SizeType = SizeType.AutoSize;
          for (int j = 0; j < grid.ColumnCount; j++)
          {
            var control = grid.GetControlFromPosition(j, i);
            if (control == null) continue;
            control.Dock = DockStyle.Fill;
            control.Padding = new Padding(0);
            control.Margin = new Padding(0);
          }
        }
      }

      foreach (Control c in this.flowLayoutPanelZoom.Controls) c.Click += this.zoom_Click;

      this.numericUpDownMaxX.Value = this.WorldMap.Max.X;
      this.numericUpDownMaxY.Value = this.WorldMap.Max.Y;
      this.numericUpDownSizeHeight.Value = this.WorldMap.Size.Height;
      this.numericUpDownSizeWidht.Value = this.WorldMap.Size.Width;
    }

    private bool RequiredFailed(Control control, string? name)
    {
      NumericUpDown? nu = control as NumericUpDown;
      if (nu != null) return this.RequiredFailed(control, nu.Value, name);
      TextBox? txt = control as TextBox;
      if (txt != null) return this.RequiredFailed(control, txt.Text, name);
      throw new ArgumentOutOfRangeException(control?.Name);
    }

    private bool RequiredFailed(Control control, object? value, string? name)
    {
      TableLayoutPanel parent = (control?.Parent as TableLayoutPanel);
      Control label = parent == null ? null : parent.GetControlFromPosition(0, parent.GetPositionFromControl(control).Row);
      bool re = true;
      if (value == null) re = false;
      if (string.IsNullOrEmpty(string.Empty + value)) re = false;
      try { if (Convert.ToInt32(value) == 0) re = false; } catch { }
      if (!re) MessageBox.Show(name ?? label?.Text ?? "???", "Input Required:");
      return !re;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.openFileDialog1.ShowDialog() == DialogResult.OK) this.textBoxImage.Text = this.openFileDialog1.FileName;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK) this.textBoxFolder.Text = this.folderBrowserDialog1.SelectedPath;
    }

    private void buttonImport_Click(object sender, EventArgs e)
    {
      if (this.RequiredFailed(this.textBoxImage, null)) return;
      if (this.RequiredFailed(this.numericUpDownMaxX, null)) return;
      if (this.RequiredFailed(this.numericUpDownMaxY, null)) return;
      if (this.RequiredFailed(this.numericUpDownSizeWidht, null)) return;
      if (this.RequiredFailed(this.numericUpDownSizeHeight, null)) return;
      this.WorldMap.SkaleImage(this.textBoxImage.Text
        , new Point((int)this.numericUpDownMaxX.Value, (int)this.numericUpDownMaxY.Value)
        , new Size((int)this.numericUpDownSizeWidht.Value, (int)this.numericUpDownSizeHeight.Value)
        );
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      TabControl tabControl = (TabControl)sender;
      if (tabControl.SelectedTab == this.tabPagePreview) this.tabControlPreviews.SelectedIndex = 0;
      this.pictureBoxBig.Image = null;
      this.pictureBoxSmall.Image = null;
      if (tabControl.SelectedTab == this.tabPageBig) this.tabControl1_SelectedIndexChanged_Bind(this.pictureBoxBig, this.WorldMap?.BitMapExportFormat?.FullImage, false);
      if (tabControl.SelectedTab == this.tabPageSmall) this.tabControl1_SelectedIndexChanged_Bind(this.pictureBoxSmall, this.WorldMap?.BitMapExportFormat?.TinnyImage, false);
      if (tabControl.SelectedTab == this.tabPageInput) this.tabControl1_SelectedIndexChanged_Bind(this.pictureBoxInput, this.WorldMap?.BitMapExportFormat?.OriginalImage, true);

      if (tabControl.SelectedTab == this.tabPagePreviewMaps)
      {
        List<Control> controls = new List<Control>();
        foreach (Control control in this.tableLayoutPanelMapsPreview.Controls) controls.Add(control);
        foreach (Control control in controls) this.tableLayoutPanelMapsPreview.Controls.Remove(control);
        foreach (Control control in controls) control.Dispose();
        Size bSize = new Size(32, 32);
        this.tableLayoutPanelMapsPreview.SuspendLayout();
        this.tableLayoutPanelMapsPreview.ColumnStyles.Clear();
        this.tableLayoutPanelMapsPreview.RowStyles.Clear();
        this.tableLayoutPanelMapsPreview.ColumnCount = this.WorldMap.Max.X;
        this.tableLayoutPanelMapsPreview.RowCount = this.WorldMap.Max.Y;
        foreach (Map m in this.WorldMap.Maps)
        {
          Button button = new Button();
          button.Size = bSize;
          button.Text = m.Name;
          button.BackColor = m.DefinitivColor.Color;
          button.AutoSize = false;
          button.Tag = m;
          button.Click += this.tableLayoutPanelMapsPreview_button_click;
          this.tableLayoutPanelMapsPreview.Controls.Add(button, m.WorldMapCoordinates.X, m.WorldMapCoordinates.Y);
        }
        this.tableLayoutPanelMapsPreview.ResumeLayout();
      }
    }

    private void tableLayoutPanelMapsPreview_button_click(object? sender, EventArgs e)
    {
      Map m = (sender as Control)?.Tag as Map;
      if (this.propertyGrid1.SelectedObject == m) this.propertyGrid1.SelectedObject = m?.DefinitivColor;
      else this.propertyGrid1.SelectedObject = m;
    }

    private void tabControl1_SelectedIndexChanged_Bind(PictureBox? pb, Image? img, bool noZoom)
    {
      if (pb == null) return;
      pb.Image = img;
      pb.Width = img?.Width ?? 0;
      pb.Height = img?.Height ?? 0;
      int zoom;
      if (!noZoom && int.TryParse(string.Empty + this.tabControlPreviews.Tag, out zoom))
      {
        pb.Width *= zoom;
        pb.Height *= zoom;
      }
    }

    private void zoom_Click(object? sender, EventArgs e)
    {
      this.tabControlPreviews.Tag = (sender as Control)?.Tag;
    }

    private void button7_Click(object sender, EventArgs e)
    {
      if (this.RequiredFailed(sender as Control, this.WorldMap?.Maps?.Count, "Maps")) return;
      if (this.RequiredFailed(this.textBoxFolder, null)) return;
      this.Export(this.textBoxFolder.Text);
    }

    private void button8_Click(object sender, EventArgs e)
    {
      if (this.RequiredFailed(sender as Control, this.WorldMap?.Maps?.Count, "Maps")) return;
      if (this.RequiredFailed(this.textBoxEmpty, null)) return;
      string folder = Path.Combine(Path.GetDirectoryName(this.textBoxImage.Text), Path.GetFileNameWithoutExtension(this.textBoxImage.Text));
      this.textBoxFolder.Text = folder;
      string empty = this.textBoxEmpty.Text;
      if (this.RequiredFailed(sender as Control, Directory.Exists(empty) ? decimal.One : decimal.Zero, "Empty Folder on HDD")) return;
      List<System.Action> todos = new List<System.Action>();
      if (string.IsNullOrEmpty(string.Empty + (sender as Control)?.Tag) /*not delete*/ && Directory.Exists(folder))
      {
        this.RequiredFailed(this.buttonDelete, decimal.Zero, null);
        return;
      }
      else if (Directory.Exists(folder))
      {
        todos.Add(() => this.WorldMap.Logger.Write("Deleting old..."));
        todos.Add(() => Directory.Delete(folder, true));
      }
      todos.Add(() => this.WorldMap.Logger.Write("Copying empty..."));
      todos.Add(() => Program.CopyFilesRecursively(empty, folder, this.WorldMap.Logger));
      this.Export(folder, todos.ToArray());
    }

    private void button9_Click(object sender, EventArgs e)
    {
      if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK) this.textBoxEmpty.Text = this.folderBrowserDialog1.SelectedPath;
    }


    private void Export(string folder, params System.Action[] todos)
    {
      this.log.Clear();
      this.tabControl1.Enabled = false;
      this.toolStripProgressBar1.Visible = true;
      this.timer1.Start();
      new Thread(() =>
      {
        if (todos != null) foreach (var todosItem in todos) if (todosItem != null) todosItem();
        this.WorldMap.Expor(folder);
        this.Invoke(() =>
        {
          this.tabControl1.Enabled = true;
          this.toolStripProgressBar1.Visible = false;
          this.timer1.Stop();
        });
      }).Start();
    }

    private List<string> log = new List<string>();
    public virtual void Write(string message)
    {
      this.toolStripStatusLabel1.Tag = message;
      this.log.Add(message);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.toolStripStatusLabel1.Text = string.Empty + this.toolStripStatusLabel1.Tag;
      if (this.toolStripProgressBar1.Value == this.toolStripProgressBar1.Maximum)
      {
        this.toolStripProgressBar1.Value = this.toolStripProgressBar1.Minimum;
      }
      else
      {
        this.toolStripProgressBar1.Value++;
      }
    }

    private void toolStripStatusLabel1_Click(object sender, EventArgs e)
    {
      if (this.log.Count > 0)
      {
        Form form = new Form();
        TextBox textBox = new TextBox();
        textBox.Dock = DockStyle.Fill;
        textBox.Multiline = true;
        textBox.ReadOnly = true;
        textBox.ScrollBars = ScrollBars.Both;
        form.Text = this.Text;
        textBox.Text = string.Join(Environment.NewLine, this.log);
        form.Controls.Add(textBox);
        form.ShowDialog();
      }
    }

    private void button10_Click(object sender, EventArgs e)
    {
      if (this.RequiredFailed(this.textBoxFolder, null)) return;
      System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
      {
        FileName = "explorer",
        WorkingDirectory = this.textBoxFolder.Text,
        Arguments = "\"" + Path.Combine(this.textBoxFolder.Text, ((Control)sender).Text) + "\"",
      });
    }
  }
}
