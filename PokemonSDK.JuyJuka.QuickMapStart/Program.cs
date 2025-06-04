namespace PokemonSDK.JuyJuka.QuickMapStart
{
  using System;

  internal static class Program
  {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      ApplicationConfiguration.Initialize();
      Application.Run(new UI.Form1());
    }

    public static void CopyFilesRecursively(string sourcePath, string targetPath, Api.Logging.ILogger logger)
    {
      //Now Create all of the directories
      foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
      {
        Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
      }

      //Copy all the files & Replaces any files with the same name
      foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
      {
        logger.Write(Path.GetFileName(newPath));
        File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
      }
    }
  }
}
/*
 * 
      float x = 0;
      int tries = 0;
      XTEst.HSLColor hSL = new XTEst.HSLColor(100, x, x);
      string html = "";
      do
      {
        html = ColorTranslator.ToHtml(hSL.ToRGB());
        x += 0.000011f;
        //x++;
        tries++;
        if (tries % 100 == 0)
        {
          System.Console.WriteLine("TO {0} => {1}", x, html);
          System.Console.ReadLine();
        }
      } while (html != "#6abf40");


 * 
 * 
    public static void CopyFilesRecursively(string sourcePath, string targetPath, Api.Logging.ILogger logger)
    {
      List<Thread> threads = new List<Thread>();
      Program.CopyFilesRecursively(sourcePath, targetPath, logger, threads);
      foreach (Thread thread in threads) { thread.Join(); }
    }

    private static void CopyFilesRecursively(string sourcePath, string targetPath, Api.Logging.ILogger logger, List<Thread> threads)
    {
      //Now Create all of the directories
      foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
      {
        Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
      }

      //Copy all the files & Replaces any files with the same name
      foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
      {
        logger.Write(Path.GetFileName(newPath));
        Thread t = new Thread(() => File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true));
        threads.Add(t);
        t.Start();
      }
    }
 */