namespace PokemonSDK.JuyJuka.QuickMapStart.UI
{
  public partial class Form1ColorControl : UserControl
  {
    public Form1ColorControl() : this(string.Empty, Color.Transparent) { }
    public Form1ColorControl(string name, Color init)
    {
      this.InitializeComponent();
      this.Name = name;
      this.ReturnColor = init;
    }

    public virtual Color ReturnColor
    {
      get { return this.button1.BackColor; }
      set
      {
        this.button1.BackColor = value;
        this.numericUpDown1.Value = value.R;
        this.numericUpDown2.Value = value.G;
        this.numericUpDown3.Value = value.B;
        this._ValueChanged(null, null);
      }
    }

    public ColorDialog ColorDialog { get; set; }

    public void _ValueChanged(object sender, EventArgs e)
    {
      int r = (int)this.numericUpDown1.Value;
      int g = (int)this.numericUpDown2.Value;
      int b = (int)this.numericUpDown3.Value;
      if (sender == this.numericUpDown1) r = (int)this.numericUpDown1.Value;
      if (sender == this.trackBar1) r = (int)this.trackBar1.Value;
      if (sender == this.numericUpDown2) g = (int)this.numericUpDown2.Value;
      if (sender == this.trackBar2) g = (int)this.trackBar2.Value;
      if (sender == this.numericUpDown3) b = (int)this.numericUpDown3.Value;
      if (sender == this.trackBar3) b = (int)this.trackBar3.Value;
      this.numericUpDown1.Value = this.trackBar1.Value = r;
      this.numericUpDown2.Value = this.trackBar2.Value = g;
      this.numericUpDown3.Value = this.trackBar3.Value = b;
      this.button1.BackColor = Color.FromArgb(r, g, b);
      this.button1.Text = string.Empty + this.button1.BackColor.GetHue();
      this.label1.Text = this.Name;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.ColorDialog == null) return;
      this.ColorDialog.Color = this.ReturnColor;
      if (this.ColorDialog.ShowDialog() == DialogResult.OK) this.ReturnColor = this.ColorDialog.Color;
    }
  }
}
