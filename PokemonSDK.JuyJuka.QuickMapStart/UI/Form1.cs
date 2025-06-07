namespace PokemonSDK.JuyJuka.QuickMapStart.UI
{
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Logging;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Wait;

  public partial class Form1 : Form, ILogger, IWaiter
  {
    public virtual WorldMap WorldMap { get; protected set; } = new WorldMap();

    public Form1()
    {
      this.InitializeComponent();
      this.WorldMap.Logger = this;
      this.WorldMap.Waiter = this;
      this.textBoxHabitat.Text = this.WorldMap.Assignment?.HabitatData?.Configuration;
      Assignment? a = this.WorldMap.Assignment as Assignment;
      if (a != null)
      {
        this.numericUpDownR_to_U.Value = a.UncommenPerOneRare;
        this.numericUpDownU_to_C.Value = a.CommonPerOneUncommon;
      }
      else
      {
        this.numericUpDownR_to_U.Enabled = false;
        this.numericUpDownU_to_C.Enabled = false;
      }
      int column = Map._0;
      this.tableLayoutPanelSpecies.ColumnStyles.Clear();
      this.tableLayoutPanelSpecies.ColumnCount = Map._0;
      Habitat[] hArr = Enum.GetValues<Habitat>();
      foreach (Habitat h in hArr)
      {
        Panel p = new Panel() { Tag = h, Dock = DockStyle.Fill };
        p.Controls.Add(new TextBox() { Multiline = true, Dock = DockStyle.Fill, WordWrap = false, });
        p.Controls.Add(new Label() { Text = string.Empty + h, Dock = DockStyle.Top, Height = 40, BorderStyle=BorderStyle.Fixed3D });
        p.Controls[Map._1].TextChanged += this.tabControlSpecies_txt_TextChanged;
        this.tableLayoutPanelSpecies.ColumnCount++;
        this.tableLayoutPanelSpecies.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / hArr.Length));
        this.tableLayoutPanelSpecies.Controls.Add(p, column++, Map._0);
      }
      foreach (TableLayoutPanel grid in new TableLayoutPanel[]{
        this.tableLayoutPanel1,
        this.tableLayoutPanel3,
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

    private void tabControlSpecies_txt_TextChanged(object? sender, EventArgs e)
    {
      Control s = sender as Control;
      Map map = s?.Tag as Map;
      if (s?.Parent?.Parent != null && s.Parent.Parent.Enabled && map != null && s?.Parent?.Tag != null)
      {
        Habitat h = (Habitat)s?.Parent?.Tag;
        if (map.Specis == null) map.Specis = new Dictionary<Habitat, string[]>();
        if (!map.Specis.ContainsKey(h)) map.Specis.Add(h, new string[] { });
        map.Specis[h] = System.Linq.Enumerable.Where(s.Text.Split(Environment.NewLine), s => !string.IsNullOrWhiteSpace(s)).ToArray();
      }
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

    private bool AskYesNo(string text, string caption, bool isBad)
    {
      return DialogResult.Yes == MessageBox.Show(
        text,
        caption ?? this.Text,
        MessageBoxButtons.YesNo,
        (isBad ? MessageBoxIcon.Exclamation : MessageBoxIcon.Question)
        );
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        this.textBoxImage.Text = this.openFileDialog1.FileName;
        string folder = Path.GetDirectoryName(this.openFileDialog1.FileName);
        string name = Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName);
        if (true
          && Directory.Exists(Path.Combine(folder, name))
          && this.AskYesNo("Folder recogniced. We could set it as export folder.", "Folder recogniced.", false)
          )
        {
          this.WorldMap.Project.Folder = this.textBoxFolder.Text = Path.Combine(folder, name);
          string dexFolder = Path.Combine(folder, name, "Data", "Studio", "dex");
          string dexFallback = Path.Combine(dexFolder, "regional.json");
          string[] dexes;
          if (true
            && Directory.Exists(dexFolder)
            && ((dexes = Directory.GetFiles(dexFolder)).Length == Map._1 || Array.IndexOf(dexes, dexFallback) >= Map._0)
            && this.AskYesNo("Dex Recogniced. We could set it as dex.", "Dex recogniced.", false)
            )
          {
            this.textBoxDex.Text = Path.Combine(dexFolder, dexes.Length > Map._1 ? dexFallback : dexes[0]);
          }

        }
        if (false && true // disabled untill I have more of a usefull fallback project
          && (string.IsNullOrEmpty(this.WorldMap.Project.Folder) || this.WorldMap.Project.Folder == PokemonStudioFolder.Fallback)
          && !string.IsNullOrEmpty(this.textBoxEmpty.Text)
          && Directory.Exists(this.textBoxEmpty.Text)
          && this.AskYesNo("No Folder. We could use a fallback?", "Folder fallback.", true))
        {
          this.WorldMap.Project.Folder = this.textBoxFolder.Text = Path.Combine(folder, name);
          Directory.CreateDirectory(this.WorldMap.Project.Folder);
          this.Export(false
          , () => this.WorldMap.Logger.Write("Copying empty...")
          , () => Program.CopyFilesRecursively(this.textBoxEmpty.Text, this.textBoxFolder.Text, this.WorldMap.Logger)
          );
        }
        if (true
          && File.Exists(Path.Combine(folder, name + ".csv"))
          && this.AskYesNo("CSV file recogniced. We could set it as importable Names.", "CSV/Names recogniced.", false))
        {
          this.textBoxListOfNames.Text = Path.Combine(folder, name + ".csv");
        }
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK) this.WorldMap.Project.Folder = this.textBoxFolder.Text = this.folderBrowserDialog1.SelectedPath;
    }

    private void buttonImport_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.textBoxImage.Text))
      {
        this.button1_Click(sender, e);
        return;
      }
      if (this.RequiredFailed(this.textBoxImage, null)) return;
      if (this.RequiredFailed(this.numericUpDownMaxX, null)) return;
      if (this.RequiredFailed(this.numericUpDownMaxY, null)) return;
      if (this.RequiredFailed(this.numericUpDownSizeWidht, null)) return;
      if (this.RequiredFailed(this.numericUpDownSizeHeight, null)) return;
      if (this.RequiredFailed(this.textBoxDex, null)) return;
      this.WorldMap.SkaleImage(this.textBoxImage.Text, this.textBoxDex.Text
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
          button.Text = string.Empty + m.Id;
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
      object? m = (sender as Control)?.Tag;
      var species = (m as Map)?.Specis;
      if (m != null) TypeDescriptor.AddAttributes(m, new Attribute[] { new ReadOnlyAttribute(true) });
      this.propertyGrid1.SelectedObject = m;
      this.tableLayoutPanelSpecies.Enabled = false;
      if (species != null)
        foreach (Control p in this.tableLayoutPanelSpecies.Controls)
          foreach (Control c in p.Controls)
          {
            if (c is Label) continue;
            c.Tag = null;
            c.Text = string.Empty;
            foreach (var o in species)
              if (p.Tag != null && o.Key == ((Habitat)p.Tag))
              {
                c.Tag = m;
                c.Text = string.Empty + string.Join(Environment.NewLine, o.Value);
              }
          }
      this.tableLayoutPanelSpecies.Enabled = true;
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
      this.tabControl1.SelectedTab = this.tabPagePreview;
      this.Export(true);
    }

    private void button8_Click(object sender, EventArgs e)
    {
      if (this.RequiredFailed(sender as Control, this.WorldMap?.Maps?.Count, "Maps")) return;
      if (this.RequiredFailed(this.textBoxEmpty, null)) return;
      string folder = Path.Combine(Path.GetDirectoryName(this.textBoxImage.Text), Path.GetFileNameWithoutExtension(this.textBoxImage.Text));
      this.WorldMap.Project.Folder = this.textBoxFolder.Text = folder;
      string empty = this.textBoxEmpty.Text;
      string myFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
      if (!string.IsNullOrEmpty(empty) && !Path.IsPathRooted(empty)) empty = Path.Combine(myFolder, empty);
      if (this.RequiredFailed(sender as Control, Directory.Exists(empty) ? decimal.One : decimal.Zero, "Empty Folder on HDD")) return;
      this.Export(true
        , () => this.WorldMap.Logger.Write("Copying empty...")
        , () => Program.CopyFilesRecursively(empty, folder, this.WorldMap.Logger)
        );
    }

    private void button9_Click(object sender, EventArgs e)
    {
      if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK) this.textBoxEmpty.Text = this.folderBrowserDialog1.SelectedPath;
    }

    private void Export(bool expor, params System.Action[] todos)
    {
      this.log.Clear();
      this.tabControl1.Enabled = false;
      this.toolStripProgressBar1.Visible = true;
      this.timer1.Start();
      new Thread(() =>
      {
        if (todos != null) foreach (var todosItem in todos) if (todosItem != null) todosItem();
        if (this.WorldMap?.Assignment?.HabitatData != null) this.WorldMap.Assignment.HabitatData.Configuration = this.textBoxHabitat.Text;
        Assignment? a = this.WorldMap.Assignment as Assignment;
        if (a != null && this.numericUpDownU_to_C.Enabled)
        {
          a.CommonPerOneUncommon = this.numericUpDownU_to_C.Value;
          a.UncommenPerOneRare = this.numericUpDownR_to_U.Value;
        }
        if (expor) this.WorldMap.Expor();
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
      if (this.log.Count > 0) this.ShowADialog<string, TextBox>(
        (d, c) =>
        {
          c.Multiline = true;
          c.ReadOnly = true;
          c.ScrollBars = ScrollBars.Both;
          c.Dock = DockStyle.Fill;
          c.Text = d;
        },
        string.Join(Environment.NewLine, this.log));
    }

    private static string _12_13 = ","; // = Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;

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
              color.MinHue = float.Parse(s[i++]);
              color.MaxHue = float.Parse(s[i++]);
            }
            catch (Exception ex)
            {
              this.WorldMap.Logger.Write(ex.ToString());
            }
          });
        }
      }
    }

    private IDefinitivMapColor Next(object current)
    {
      int i = Map._0;
      if (current != null) i = this.WorldMap.DefinitivMapColors.IndexOf(current as DefinitivMapColor);
      i++;
      if (i >= this.WorldMap.DefinitivMapColors.Count) i = Map._0;
      return this.WorldMap.DefinitivMapColors[i];
    }

    private void button14_Click(object sender, EventArgs e)
    {
      this.ShowADialogAndSaveOnOkay<IDefinitivMapColor, Form1ColorControl>((d, c) =>
      {
        c.ColorDialog = this.colorDialog1;
        c.MinHue = d.MinHue;
        c.MaxHue = d.MaxHue;
        c.Text = d.Name;
        c.ReturnColor = d.Color;
      }
      ,
      (d, c) =>
      {
        d.MinHue = c.MinHue;
        d.MaxHue = c.MaxHue;
        d.Color = c.ReturnColor;
      }
      ,
      this.WorldMap.DefinitivMapColors
      );

      if (this.saveFileDialogExportColors.ShowDialog() == DialogResult.OK)
      {
        System.IO.File.WriteAllLines(this.saveFileDialogExportColors.FileName, this.WorldMap.DefinitivMapColors.ConvertAll(color => string.Format(
          "{0}{6}{1:000}{6}{2:000}{6}{3:000}{6}{4}{6}{5}"
          , color?.Name
          , color?.Color.R
          , color?.Color.G
          , color?.Color.B
          , color?.MinHue
          , color?.MaxHue
          , _12_13
          )));
      }
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      Map map = this.propertyGrid1.SelectedObject as Map;
      if (map == null) return;
      Control button = null;
      foreach (Control control in this.tableLayoutPanelMapsPreview.Controls) if (control?.Tag == map) button = control;
      map.DefinitivColor = this.Next(map.DefinitivColor);
      if (button != null)
      {
        button.BackColor = map.DefinitivColor.Color;
        button.Text = map.DefinitivColor.Name;
        this.propertyGrid1.SelectedObject = null;
        this.propertyGrid1.SelectedObject = map;
      }
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
      IDefinitivMapColor next = this.Next(this.textBoxNames.Tag);
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

    private void button16_Click(object sender, EventArgs e)
    {
      using (Control x = new Form1ColorControl())
        this.ShowADialogAndSaveOnOkay<IMapExportFormat, CheckBox>((d, c) => { c.Text = d.Name; c.Checked = d.IsEnabled; c.Height = x.Height; }, (d, c) => d.IsEnabled = c.Checked, this.WorldMap.Formats);
    }

    public virtual void ShowADialogAndSaveOnOkay<TData, TControl>(Action<TData, TControl> setup, Action<TData, TControl> onOkay, params TData[] objekts)
      where TControl : Control, new()
    {
      this.ShowADialogAndSaveOnOkay<TData, TControl>(setup, onOkay, (IEnumerable<TData>)objekts);
    }

    public virtual void ShowADialogAndSaveOnOkay<TData, TControl>(Action<TData, TControl> setup, Action<TData, TControl> onOkay, IEnumerable<TData> objekts)
      where TControl : Control, new()
    {
      Dictionary<TControl, TData> cs = new Dictionary<TControl, TData>();
      if (this.ShowADialog<TData, TControl>((d, c) =>
      {
        if (setup != null) setup(d, c);
        cs.Add(c, d);
      }, objekts)) foreach (var kvp in cs) if (onOkay != null) onOkay(kvp.Value, kvp.Key);
    }

    public virtual bool ShowADialog<TData, TControl>(Action<TData, TControl> setup, params TData[] objekts)
      where TControl : Control, new()
    {
      return this.ShowADialog<TData, TControl>(setup, (IEnumerable<TData>)objekts);
    }

    public virtual bool ShowADialog<TData, TControl>(Action<TData, TControl> setup, IEnumerable<TData> objekts)
      where TControl : Control, new()
    {
      Form form = new Form();
      form.Text = this.Text;
      form.AutoScroll = true;
      if (objekts != null)
        foreach (TData data in objekts)
        {
          TControl textBox = new TControl();
          textBox.Dock = DockStyle.Top;
          if (setup != null) setup(data, textBox);
          form.Controls.Add(textBox);
        }
      form.AcceptButton = new Button()
      {
        Text = "OK",
        Dock = DockStyle.Bottom,
        Height = 40,
        DialogResult = DialogResult.OK,
      };
      form.CancelButton = new Button()
      {
        Text = "Cancel",
        Dock = DockStyle.Bottom,
        Height = 40,
        DialogResult = DialogResult.Cancel,
      };
      form.WindowState = FormWindowState.Maximized;
      form.Controls.Add((Button)form.AcceptButton);
      form.Controls.Add((Button)form.CancelButton);
      return form.ShowDialog() == form.AcceptButton.DialogResult;
    }

    public virtual void Wait(WaitFor waitFor)
    {
      this.Invoke(() => { MessageBox.Show("Waiting for:" + waitFor, this.Text, MessageBoxButtons.OK); });
    }

    private void button17_Click(object sender, EventArgs e)
    {
      IDefinitivMapColor next = this.Next(this.textBoxOneColor.Tag);
      this.textBoxOneColor.Text = next.Name;
      this.textBoxOneColor.Tag = next;
    }

    private void button18_Click(object sender, EventArgs e)
    {
      if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK) this.textBoxOneFolder.Text = this.folderBrowserDialog1.SelectedPath; ;
    }

    private void buttonOneRun_Click(object sender, EventArgs e)
    {
      if (this.textBoxOneColor.Tag == null) this.button17_Click(sender, e);
      if (string.IsNullOrEmpty(this.textBoxOneFolder.Text)) this.textBoxOneFolder.Text = Environment.CurrentDirectory;
      if (this.RequiredFailed(this.textBoxOneColor, null)) return;
      if (this.RequiredFailed(this.textBoxOneFolder, Directory.Exists(this.textBoxOneFolder.Text) ? 1 : 0, null)) return;
      List<Map> org = new List<Map>(this.WorldMap.Maps);
      var org2 = this.WorldMap.Project;
      Map? x = null;
      IMapExportFormat[] org3 = this.WorldMap.Formats;
      TmxMapExportFormat format = new TmxMapExportFormat();
      string file = null;
      var org4 = this.WorldMap.Waiter;
      var org5 = new List<IDefinitivMapColor>(this.WorldMap.DefinitivMapColors);
      try
      {
        this.WorldMap.DefinitivMapColors.Clear();
        this.WorldMap.DefinitivMapColors.Add((IDefinitivMapColor)this.textBoxOneColor.Tag);
        this.WorldMap.Waiter = null;
        this.WorldMap.Formats = [format];
        this.WorldMap.Project = new PokemonStudioFolder() { Folder = this.textBoxOneFolder.Text };
        this.WorldMap.Maps.Clear();
        this.WorldMap.Maps.Add(x = new Map(this.WorldMap)
        {
          DefinitivColor = ((IDefinitivMapColor)this.textBoxOneColor.Tag),
          WorldMapCoordinates = new Point((int)this.numericUpDownOneX.Value, (int)this.numericUpDownOneY.Value)
        });
        this.WorldMap.Expor();
        string f2 = format.ModifyTargetFolder(x, x.World.Project);
        file = format.ModifyTargetFile(x, x.World.Project, f2, Path.Combine(f2, x.Name + format.FileExtendsion));
        this.numericUpDownOneX.Value += Map._1;
        this.numericUpDownOneY.Value += Map._1;
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Empty + ex, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        this.WorldMap.Formats = org3;
        this.WorldMap.Project = org2;
        this.WorldMap.Maps.Clear();
        this.WorldMap.Maps.AddRange(org);
        this.WorldMap.DefinitivMapColors.Clear();
        this.WorldMap.DefinitivMapColors.AddRange(org5);
      }
      try
      {
        if (x != null) System.Diagnostics.Process.Start("explorer", file);
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Empty + ex, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void buttonDex_Click(object sender, EventArgs e)
    {
      if (this.openFileDialog1.ShowDialog() == DialogResult.OK) this.textBoxDex.Text = this.openFileDialog1.FileName;
    }

    private void button10_Click(object sender, EventArgs e)
    {
      if (this.openFileDialog1.ShowDialog() == DialogResult.OK) this.textBoxHabitat.Text = this.openFileDialog1.FileName;
    }
  }
}
