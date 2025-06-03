namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports
{
  using System;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

  public interface IMapExportFormat
  {
    string FileExtendsion { get; }
    string StaticFilter { get; }
    string ModifyTargetFolder(Map map, IPokemonStudioFolder project);
    string ModifyTargetFile(Map map, IPokemonStudioFolder project, string folder, string file);
    string Export(Map map, IPokemonStudioFolder project, string folder, string file, Func<string, Tuple<string, string>> readAsset);
  }
}