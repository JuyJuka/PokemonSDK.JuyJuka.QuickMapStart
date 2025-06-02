namespace PokemonSDK.JuyJuka.QuickMapStart
{
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;

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
      if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        this.textBoxImage.Text = this.openFileDialog1.FileName;
        string folder = Path.GetDirectoryName(this.openFileDialog1.FileName);
        string name = Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName);
        if (Directory.Exists(Path.Combine(folder, name)) && DialogResult.Yes == MessageBox.Show("Folder recogniced. We could set it as export folder.", "Folder recogniced.", MessageBoxButtons.YesNo))
        {
          this.textBoxFolder.Text = Path.Combine(folder, name);
        }
        if (File.Exists(Path.Combine(folder, name + ".csv")) && DialogResult.Yes == MessageBox.Show("CSV file recogniced. We could set it as importable Names.", "CSV/Names recogniced.", MessageBoxButtons.YesNo))
        {
          this.textBoxListOfNames.Text = Path.Combine(folder, name + ".csv");
        }
      }
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

      if (!string.IsNullOrEmpty(this.textBoxListOfNames.Text) && File.Exists(this.textBoxListOfNames.Text))
      {
        this.openFileDialog1.FileName = this.textBoxListOfNames.Text;
        this.toolStripButtonNamesImport_Click(this.textBoxListOfNames, null);
      }
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

      if (this.tabPagePreviewMaps.Tag != this.WorldMap.BitMapExportFormat.OriginalImage)
      {
        List<Control> controls = new List<Control>();
        foreach (Control control in this.tableLayoutPanelMapsPreview.Controls) controls.Add(control);
        foreach (Control control in controls) this.tableLayoutPanelMapsPreview.Controls.Remove(control);
        foreach (Control control in controls) control.Dispose();
        Size bSize = new Size(60, 60);
        this.tableLayoutPanelMapsPreview.SuspendLayout();
        this.tableLayoutPanelMapsPreview.ColumnStyles.Clear();
        this.tableLayoutPanelMapsPreview.RowStyles.Clear();
        this.tableLayoutPanelMapsPreview.ColumnCount = this.WorldMap.Max.X;
        this.tableLayoutPanelMapsPreview.RowCount = this.WorldMap.Max.Y;
        foreach (Map m in this.WorldMap.Maps)
        {
          Button button = new Button();
          button.Size = bSize;
          button.Text = m.DefinitivColor.ToString();
          button.BackColor = m.DefinitivColor.Color;
          button.AutoSize = false;
          button.Tag = m;
          button.Click += this.tableLayoutPanelMapsPreview_button_click;
          this.tableLayoutPanelMapsPreview.Controls.Add(button, m.WorldMapCoordinates.X, m.WorldMapCoordinates.Y);
        }
        this.tabPagePreviewMaps.Tag = this.WorldMap.BitMapExportFormat.OriginalImage;
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

    private static string _12_13 = ","; // = Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
    private void button12_Click(object sender, EventArgs e)
    {
      if (this.saveFileDialogExportColors.ShowDialog() == DialogResult.OK)
      {
        System.IO.File.WriteAllLines(this.saveFileDialogExportColors.FileName, this.WorldMap.DefinitivMapColors.ConvertAll(color => string.Format(
          "{0},{1:000},{2:000},{3:000}"
          , color?.Name
          , color?.Color.R
          , color?.Color.G
          , color?.Color.B
          , _12_13
          )));
      }
    }

    private void button13_Click(object sender, EventArgs e)
    {
      if (this.openFileDialogImportColors.ShowDialog() == DialogResult.OK)
      {
        foreach (string line in System.IO.File.ReadAllLines(this.openFileDialogImportColors.FileName))
        {
          this.WorldMap.DefinitivMapColors.ForEach(color =>
          {
            if (string.IsNullOrEmpty(color?.Name)) return;
            if (!line.StartsWith(color.Name)) return;
            try
            {
              string[] s = line.Split(_12_13);
              int i = Map._1;
              color.Color = Color.FromArgb(int.Parse(s[i++]), int.Parse(s[i++]), int.Parse(s[i++]));
            }
            catch (Exception ex)
            {
              this.WorldMap.Logger.Write(ex.ToString());
            }
          });
        }
      }
    }

    private DefinitivMapColor Next(object current)
    {
      int i = Map._0;
      if (current != null) i = this.WorldMap.DefinitivMapColors.IndexOf(current as DefinitivMapColor);
      i++;
      if (i >= this.WorldMap.DefinitivMapColors.Count) i = Map._0;
      return this.WorldMap.DefinitivMapColors[i];
    }

    private void button14_Click(object sender, EventArgs e)
    {
      Form form = new Form();
      Dictionary<Form1ColorControl, DefinitivMapColor> cs = new Dictionary<Form1ColorControl, DefinitivMapColor>();
      for (int i = this.WorldMap.DefinitivMapColors.Count - Map._1; i >= 0; i--)
      {
        DefinitivMapColor color = this.WorldMap.DefinitivMapColors[i];
        if (color == null) continue;
        Form1ColorControl c = new Form1ColorControl(color.Name, color.Color);
        c.Dock = DockStyle.Top;
        c.ColorDialog = this.colorDialog1;
        form.Controls.Add(c);
        cs.Add(c, color);
      }
      form.AcceptButton = new Button()
      {
        Text = "OK",
        Dock = DockStyle.Bottom,
        Height = 40,
        DialogResult = DialogResult.OK,
      };
      // ((Button)form.AcceptButton).Click += (s, e) => form.Close();
      form.WindowState = FormWindowState.Maximized;
      form.Controls.Add((Button)form.AcceptButton);
      if (form.ShowDialog() == DialogResult.OK)
      {
        foreach (var kvp in cs)
        {
          kvp.Value.Color = kvp.Key.ReturnColor;
        }
      }
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      Map map = this.propertyGrid1.SelectedObject as Map;
      if (map == null) return;
      Control button = null;
      foreach (Control control in this.tableLayoutPanelMapsPreview.Controls) if (control?.Tag == map) button = control;
      map.Color = this.Next(map.DefinitivColor).Color;
      if (button != null)
      {
        button.BackColor = map.DefinitivColor.Color;
        button.Text = map.DefinitivColor.Name;
        this.propertyGrid1.SelectedObject = null;
        this.propertyGrid1.SelectedObject = map;
      }
    }

    private void button15_Click(object sender, EventArgs e)
    {
      int maxdmcnl = Map._0;
      foreach (var x in this.WorldMap.DefinitivMapColors) if (x.Name.Length > maxdmcnl) maxdmcnl = x.Name.Length;
      this.log.Clear();
      this.WorldMap.DefinitivMapColors.ForEach(x => this.Write(string.Format("{0}{1}\t{2:000}x{3:000}x{4:000}\t{5}"
      , x.Name
      , new string(' ', (maxdmcnl - x.Name.Length) + 1)
      , x.Color.R
      , x.Color.G
      , x.Color.B
      , x.Color.GetHue()
      )));
      this.toolStripStatusLabel1_Click(sender, e);
    }

    private static string _NamesImEx = ",";
    private void toolStripButtonNamesImport_Click(object sender, EventArgs e)
    {
      if (false
      || (sender == this.textBoxListOfNames)
      || (this.openFileDialog1.ShowDialog() == DialogResult.OK)
      )
      {
        this.WorldMap.ContigousNames.Clear();
        foreach (string line in File.ReadAllLines(this.openFileDialog1.FileName))
        {
          if (string.IsNullOrEmpty(line)) continue;
          string[] line2 = line.Split(_NamesImEx, Map._1 + Map._1);
          if (line2.Length <= Map._1) continue;
          this.WorldMap.ContigousNames.Add(new Tuple<string, string>(line2[Map._0], line2[Map._1]));
        }
        this.textBoxNames.Tag = null;
        this.toolStripButtonNextNames_Click(sender, e);
      }
    }

    private void toolStripButtonNamesExport_Click(object sender, EventArgs e)
    {
      if (this.saveFileDialogExportColors.ShowDialog() == DialogResult.OK)
      {
        File.WriteAllLines(this.saveFileDialogExportColors.FileName, this.WorldMap.ContigousNames.ConvertAll(name => name.Item1 + _NamesImEx + name.Item2));
      }
    }

    private void toolStripButtonNextNames_Click(object sender, EventArgs e)
    {
      DefinitivMapColor next = this.Next(this.textBoxNames.Tag);
      this.textBoxNames.Enabled = false;
      this.textBoxNames.Text = null;
      this.textBoxNames.Tag = next;
      if (next == null) return;
      string re = string.Empty;
      this.WorldMap.ContigousNames.FindAll(x => x?.Item1 == next.Name).ForEach(x => re += x.Item2 + Environment.NewLine);
      this.toolStripLabelNextNames.Text = next.Name;
      this.textBoxNames.Text = re;
      this.textBoxNames.Enabled = true;
    }

    private void textBoxNames_TextChanged(object sender, EventArgs e)
    {
      if (!this.textBoxNames.Enabled) return;
      DefinitivMapColor next = this.textBoxNames.Tag as DefinitivMapColor;
      if (next == null) return;
      List<Tuple<string, string>> neu = this.WorldMap.ContigousNames.FindAll(x => x.Item1 != next.Name);
      foreach (string name in this.textBoxNames.Text.Split(Environment.NewLine))
        if (!string.IsNullOrEmpty(name))
          neu.Add(new Tuple<string, string>(next.Name, name));
      this.WorldMap.ContigousNames.Clear();
      this.WorldMap.ContigousNames.AddRange(neu);
    }

    private void buttonSetImportNames_Click(object sender, EventArgs e)
    {
      if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        this.textBoxListOfNames.Text = this.openFileDialog1.FileName;
      }
    }
  }
}
