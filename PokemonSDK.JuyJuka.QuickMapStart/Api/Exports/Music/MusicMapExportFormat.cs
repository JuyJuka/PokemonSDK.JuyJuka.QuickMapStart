namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Music
{
  using System;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

  public class MusicMapExportFormat : MapExportFormat
  {
    public MusicMapExportFormat() : base("Data", "Studio", "maps", ".json") { this.IsPostPokemonStudioImport = true; }

    public override string ModifyTargetFile(Map map, IPokemonStudioFolder project, string folder, string file)
    {
      if (string.IsNullOrEmpty(map?.DefinitivColor?.MusicName)) return null;
      foreach (string file2 in Directory.GetFiles(folder))
      {
        string content = File.ReadAllText(file2);
        if (true
          && content.Contains("\"tiledFilename\"")
          && content.Contains("\"" + map.Name + "\"")
        )
        {
          return file2;
        }
      }
      return null;
    }

    public override string Export(Map map, IPokemonStudioFolder project, string folder, string file, Func<string, Tuple<string, string>> readAsset)
    {
      if (string.IsNullOrEmpty(file)) return string.Empty;
      string music = map.DefinitivColor.MusicName;
      string content = File.ReadAllText(file);
      // I do not want to read the JSON ... but perhaps I should.
      const string s1 = "\"bgm\"";
      const string s2 = "}";
      const string s3 = "\"name\": \"";
      const string s4 = "\"";
      int index1 = content.IndexOf(s1);
      if (index1 < 0) return string.Empty;
      int index2 = content.Substring(index1).IndexOf(s2);
      if (index2 < 0) return string.Empty;
      index2 += index1;
      int index3 = content.Substring(index1, index2 - index1).IndexOf(s3);
      if (index3 < 0) return string.Empty;
      index3 += index1 + s3.Length;
      int index4 = content.Substring(index3).IndexOf(s4);
      if (index4 < 0) return string.Empty;
      index4 += index3;
      content = string.Empty
        + content.Substring(0, index3)
        + music
        + content.Substring(index4, content.Length - index4)
        ;
      File.WriteAllText(file, content);
      return string.Empty;
    }
  }
}