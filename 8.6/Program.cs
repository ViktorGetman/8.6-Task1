using Microsoft.VisualBasic.FileIO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
class DirectoryCleener
{
    public static void Main()
    {
        Console.WriteLine("Укажите путь к папке");
        string folderPath = Console.ReadLine();                                                
        if (!Directory.Exists(folderPath)) 
        {
            Console.WriteLine("По указаному пути пака не существует");

        }
        else
        {
            DirectoryCleen(folderPath);
        }
    }
    public static bool DirectoryCleen(string parentDirectory)
    {   
        try 
        {
            bool parentDirectoryEmpty = FilesCleen(parentDirectory);

            string[] dirs = Directory.GetDirectories(parentDirectory);  
            for (int i = 0; i < dirs.Length; i++) 
            {
                bool chaildDirectoryEmpty = DirectoryCleen(dirs[i]);
                
                var modTime = Directory.GetLastWriteTime(dirs[i]);


                if (DateTime.Now - modTime > TimeSpan.FromMinutes(30) && chaildDirectoryEmpty)
                {
                    try
                    {
                        Directory.Delete(dirs[i], true);
                    }
                    catch (Exception Ex)
                    {
                        parentDirectoryEmpty = false;
                        Console.WriteLine($"Невозможно удалить деррикторию {dirs[i]}");
                    }
                }
                else
                {
                    parentDirectoryEmpty = false;
                }

            }
            return parentDirectoryEmpty;
        }       
        catch
        {
            Console.WriteLine($"Невозможно очистить папку {parentDirectory}");
            return false;
        }
        
    }
    public static bool FilesCleen(string parentDirectory)
    {
        bool allFilesDelete = true;
        string[] files;
        files = Directory.GetFiles(parentDirectory);
             
        for (int i = 0; i < files.Length; i++) 
        {
    
            var modTime = File.GetLastWriteTime(files[i]);


            if (DateTime.Now - modTime > TimeSpan.FromMinutes(30))
            {
                try
                {
                    File.Delete(files[i]);
                }
                catch (Exception Ex)
                {
                    Console.WriteLine($"Невозможно удалить файл {files[i]}");
                    allFilesDelete = false;
                }
            }
            else
            {
                allFilesDelete = false;
            }
        }
        return allFilesDelete;
    }
}