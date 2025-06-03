namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

  public abstract class MapExportFormat : IMapExportFormat
  {
    public MapExportFormat() { }
    public MapExportFormat(params string[] pathAndExtension)
    {
      if (pathAndExtension != null && pathAndExtension.Length > Map._0)
      {
        this.FileExtendsion = pathAndExtension[pathAndExtension.Length - Map._1];
      }
      if (pathAndExtension != null && pathAndExtension.Length > Map._1)
      {
        this._mod = new string[pathAndExtension.Length - Map._1];
        for (int i = 0; i < this._mod.Length; i++) this._mod[i] = pathAndExtension[i];
      }
    }

    public virtual string Name { get { return this.GetType().Name; } }
    public virtual bool IsPostPokemonStudioImport { get; set; } = false;
    public virtual bool IsEnabled { get; set; } = true;
    public virtual string FileExtendsion { get; set; } = ".txt";
    public virtual string StaticFilter { get; set; } = string.Empty;
    private string[] _mod = [];
    public virtual string ModifyTargetFolder(Map map, IPokemonStudioFolder folder)
    {
      if (this._mod != null && this._mod.Length > Map._0)
      {
        return Path.Combine(folder?.Folder ?? PokemonStudioFolder.Fallback, Path.Combine(this._mod));
      }
      else
      {
        return folder?.Folder ?? PokemonStudioFolder.Fallback;
      }
    }
    public virtual string ModifyTargetFile(Map map, IPokemonStudioFolder project, string folder, string file) { return file; }

    public abstract string Export(Map map, IPokemonStudioFolder project, string folder, string file, Func<string, Tuple<string, string>> readAsset);
  }
}