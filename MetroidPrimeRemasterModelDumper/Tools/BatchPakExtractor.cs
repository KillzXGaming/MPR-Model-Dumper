using DKCTF;
using EvilWithin2Tool;
using RetroStudioPlugin.Files.FileData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroidPrimeRemasterModelDumper
{
    public class BatchPakExtractor
    {
        public static void ExtractModels(string pakFile)
        {
            var ctx = new AvaloniaToolbox.Core.FileContext()
            {
                FilePath = pakFile,
                FileName = Path.GetFileName(pakFile),
                Stream = File.OpenRead(pakFile),
            };

            PAK pak = new PAK() { FileInfo = ctx };
            pak.Load(ctx);

            foreach (var fileInfo in pak.files)
            {
                if (fileInfo.AssetEntry.Type == "CHPR")
                    ExtractCharacterProject(fileInfo.FileData, pak);
            }
        }

        static void ExtractCharacterProject(Stream stream, PAK pak)
        {
            CHPR chpr = new CHPR(stream);

            // Load models
            foreach (var file in pak.files)
            {
                foreach (var charInfo in chpr.CharacterInfos)
                {         
                    // sub name
                    string folder = charInfo.NamePool.GetString(chpr.CharacterInfos[0].SubCharData.SubChars[0].Name);
                    // Add pak folder name onto it
                    folder = Path.Combine(Path.GetFileNameWithoutExtension(pak.FileInfo.FilePath), folder,
                        file.AssetEntry.FileID.ToString());

                    foreach (var model in charInfo.ModelNodes)
                    {
                        if (file.FileName.Contains(model.ModelFileGuid.ToString()))
                        {
                            if (!Directory.Exists(folder))
                                Directory.CreateDirectory(folder);

                            var cmdl = new CMDL(file.FileData);
                            string modelName = charInfo.NamePool.GetString(model.Name);

                            string path = Path.Combine(folder, modelName + ".gltf");
                            CMDLExporter.Export(cmdl, path, chpr);
                        }
                    }
                }
            }
        }
    }
}
