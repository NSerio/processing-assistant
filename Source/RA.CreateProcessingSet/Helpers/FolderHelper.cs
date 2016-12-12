using RA.CreateProcessingSet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RA.CreateProcessingSet.Helpers
{
    public class FolderHelper
    {
        public static IEnumerable<FolderModel> GetSourcePaths(string FolderPath, 
            int? CustodianLevel, 
            DestinationEnum destination,
            bool withDescription = false)
        {
            //Get all unique sub directories within the selected source path
            var folders = new List<String>();
            folders.AddRange(Directory.GetDirectories(FolderPath.Trim(), "*", SearchOption.AllDirectories).ToList());
            var folderList = new List<FolderModel>();

            foreach (var folderPath in folders)
            {
                //Initially, set custodian folder path to the selected source path
                var custodianFolderPath = FolderPath;

                if (CustodianLevel.HasValue)
                {
                    String custodianFolderName;
                    String[] foldersInPath;
                    if (CustodianLevel.Value == 0)
                    {
                        //Split the source path on backslashes
                        foldersInPath = FolderPath.Split('\\');
                        //Custodian folder is the last folder in the source path
                        custodianFolderName = foldersInPath.Last();
                        folderList.Add(CreateModel(custodianFolderPath, custodianFolderName, withDescription, destination));
                    }
                    else
                    {
                        //Remove selected source path from the full folder path
                        char[] charToTrim = { '\\' };
                        var folderPathWithoutSourcePath = folderPath.Substring(FolderPath.Length, folderPath.Length - FolderPath.Length).Trim(charToTrim);
                        //Get subdirectories under source path
                        foldersInPath = folderPathWithoutSourcePath.Split('\\');
                        //If there are at least X number of folders, select the last folder as the custodian folder
                        if (foldersInPath.Count() >= CustodianLevel.Value)
                        {
                            //Custodian folder is the directory X number of folders down from the source path
                            custodianFolderName = foldersInPath[CustodianLevel.Value - 1];
                            //Rebuild folder path up to the custodian folder
                            var folderCounter = 0;
                            while (folderCounter < CustodianLevel.Value)
                            {
                                custodianFolderPath += "\\" + foldersInPath[folderCounter];
                                folderCounter += 1;
                            }
                            folderList.Add(CreateModel(custodianFolderPath, custodianFolderName, withDescription, destination));
                        }
                    }
                }
            }
            return folderList.GroupBy(x => x.FullPath).Select(y => y.First());
        }

        private static FolderModel CreateModel(string custodianFolderPath, 
            string custodianFolderName, 
            bool withDescription,
            DestinationEnum destination)
        {
            string description = string.Empty;
            if (withDescription)
            {
                int files = Directory.GetFiles(custodianFolderPath).Length;
                int dirs = Directory.GetDirectories(custodianFolderPath).Length;
                description = string.Format("{0} file{2}, {1} sub-folder{3}", 
                    files, 
                    dirs, 
                    files == 0 || files > 1 ? "s" : string.Empty, 
                    dirs == 0 || dirs > 1 ? "s" : string.Empty);
            }
            return new FolderModel(custodianFolderPath, custodianFolderName, destination)
            {
                Description = description
            };
        }
    }
}