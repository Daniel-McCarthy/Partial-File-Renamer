using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PartialFileRenamer
{
    public class FileRenamer
    {
        public void rename(string path, string match, string replace, bool overwrite, bool internalStrings, bool subdirectories, bool extensionFilter, bool extensionFilterOut, List<string> extensionF = null, List<string> extensionFO = null)
        {
            SearchOption option = SearchOption.TopDirectoryOnly;
            string searchPattern = "*.*";

            if (subdirectories == true)
            {
                option = SearchOption.AllDirectories;
            }

            List<string> files = new List<string>();

            if (extensionFilter == true)
            {
                foreach (string extension in extensionF)
                {
                    searchPattern = "*." + extension;
                    files.AddRange(Directory.GetFiles(path, searchPattern, option));
                }

            }
            else
            {
                files.AddRange(Directory.GetFiles(path, searchPattern, option));
            }


            if (extensionFilterOut == true)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    foreach(string extension in extensionFO)
                    {
                        string listFileExtension = files[i].Substring(files[i].LastIndexOf('.') + 1);
                        if (listFileExtension == extension)
                        {
                            files.Remove(files[i]);
                        }
                    }
                }

            }

            if(internalStrings == true)
            {
                editInternalStrings(ref files, match, replace);
            }

            rename(files, match, replace, overwrite);
        }

        public List<String> rename(List<string> originalFiles, string matchString, string replaceString, bool overwriteBool)
        {

            if (originalFiles.Count > 0)
            {

                FileInfo[] fileInfos = new FileInfo[originalFiles.Count];
                FileInfo[] newFileInfos = new FileInfo[originalFiles.Count];


                
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    fileInfos[i] = new FileInfo(originalFiles[i]);
                }
                

                for (int i = 0; i < originalFiles.Count; i++)
                {
                    if (fileInfos[i].Name.Contains(matchString))
                    {
                        newFileInfos[i] = new FileInfo(Path.Combine(fileInfos[i].Directory.ToString(), fileInfos[i].Name.Replace(matchString, replaceString)));

                        if (overwriteBool)
                        {
                            if (newFileInfos[i].Exists)
                            {
                                newFileInfos[i].Delete();
                            }
                        }


                        if (!newFileInfos[i].Exists)
                        {

                            try
                            {
                                File.Move(fileInfos[i].FullName, newFileInfos[i].FullName);

                                Console.WriteLine("Success!" + originalFiles[i] + " Renamed!");
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine("Error: " + exception.Message + "File: " + originalFiles[i] + " Not Renamed!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Attempted to Rename File to Name of an Existing File! File: " + originalFiles[i] + " Not Renamed!");
                        }

                    }
                }
            }
            else
            {
                Console.WriteLine("Error! No files to rename!");
            }

            return null;
        }


        public bool editInternalStrings(ref List<string> files, string matchString, string replaceString)
        {
            foreach (string filePath in files)
            {
                byte[] fileContents;
                bool binary = false;

                FileStream file = File.Open(filePath, FileMode.Open);

                Encoding detectedEncoding;

                using (StreamReader sr = new StreamReader(file, true)) //check if streamreader can figure out encoding from BOM
                {

                    sr.ReadToEnd();

                    detectedEncoding = sr.CurrentEncoding;
                    sr.Close();
                }

                file = File.Open(filePath, FileMode.Open);
                if (detectedEncoding == Encoding.Default) //if encoding wasn't detected (or was detected to be default)
                {
                    using (StreamReader sr = new StreamReader(file, new UTF8Encoding(false, true)))
                    {
                        try
                        {
                            sr.ReadToEnd();
                            detectedEncoding = Encoding.UTF8; //did not fail check for UTF8, set to this encoding
                        }
                        catch
                        {
                            detectedEncoding = Encoding.ASCII; //default to ascii as backup.
                        }

                        sr.Close();
                    }
                }

                //file = File.Open(filePath, FileMode.Open);
                using (BinaryReader br = new BinaryReader(file))
                {
                    fileContents = br.ReadBytes((int)file.Length);

                    foreach (byte number in fileContents)
                    {
                        if (number == 0)
                        {
                            binary = true;
                        }
                    }


                    if (!binary)
                    {
                        string fileContentsString = detectedEncoding.GetString(fileContents);

                        fileContentsString = fileContentsString.Replace(matchString, replaceString);

                        fileContents = detectedEncoding.GetBytes(fileContentsString);
                    }
                    else
                    {
                        Console.WriteLine("Binary File! File contents ignored. File: " + filePath);
                    }

                }

                file.Close();

                if (!binary)
                {

                    file = File.Open(filePath, FileMode.Create);

                    using (BinaryWriter bw = new BinaryWriter(file))
                    {
                        bw.Write(fileContents);
                    }
                    file.Close();
                }
            }

            return true;
        }


        /*
 * //Original implementation. Have to detect encoding to avoid changing files. If nothing else, detect if file has been changed (besides string). If has, don't save. 
public bool editInternalStrings(ref List<string> files, string matchString, string replaceString)
{
    foreach(string filePath in files)
    {
        byte[] fileContents;

        FileStream file = File.Open(filePath, FileMode.Open);
        Console.WriteLine("File 1 Open");
        using (BinaryReader br = new BinaryReader(file))
        {
            fileContents = br.ReadBytes((int)file.Length); 
            string fileContentsString = Encoding.ASCII.GetString(fileContents);//fileContents.ToString();


            Console.WriteLine("File Size: " + file.Length);
            Console.WriteLine("File Contents: " + fileContentsString);
            fileContentsString = fileContentsString.Replace(matchString, replaceString);

            fileContents = Encoding.ASCII.GetBytes(fileContentsString);

        }
        Console.WriteLine("File 1 Closed");
        file.Close();

        file = File.Open(filePath, FileMode.Create);
        Console.WriteLine("File 2 Open");
        using (BinaryWriter bw = new BinaryWriter(file))
        {
            bw.Write(fileContents);
        }
        Console.WriteLine("File 2 Closed");
        file.Close();
    }

    return true;
}
*/

    }
}
