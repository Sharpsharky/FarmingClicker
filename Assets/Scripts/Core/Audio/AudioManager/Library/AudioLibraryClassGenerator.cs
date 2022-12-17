namespace Core.Audio.AudioManager.Library
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using UnityEditor;
    using UnityEngine;

    public class AudioLibraryClassGenerator
    {
        public static void Generate(List<AudioClipEntry> entries)
        {
#if UNITY_EDITOR
            string filePath = string.Empty;
            foreach(string file in Directory.GetFiles(Application.dataPath, "*.cs",
                                                      SearchOption.AllDirectories))
            {
                if(Path.GetFileNameWithoutExtension(file) != "AudioLibraryData")
                {
                    continue;
                }

                filePath = file;
                break;
            }

            // If no such file exists already, use the save panel to get a folder in which the file will be placed.
            if(string.IsNullOrEmpty(filePath))
            {
                string directory =
                    EditorUtility.OpenFolderPanel("Choose location for AudioLibrary.cs",
                                                  Application.dataPath, "");

                // Canceled choose? Do nothing.
                if(string.IsNullOrEmpty(directory))
                {
                    return;
                }

                filePath = Path.Combine(directory, "AudioLibraryData.cs");
            }
            Debug.Log($"Generating audio library at filepath: {filePath}");
            // Write out our file
            using(var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("// This file is auto-generated. Modifications will be not saved.");
                writer.WriteLine();
                writer.WriteLine("namespace AudioLibraryData");
                writer.WriteLine("{");

                // Add usings

                writer.WriteLine($"    using {typeof(AudioClipInfo).Namespace};");
                writer.WriteLine($"    using {typeof(AudioGroup).Namespace};");

                foreach(var audioClipEntry in entries)
                {
                    var checker = new Dictionary<string, int>();

                    writer.WriteLine($"    public static class {audioClipEntry.TargetGroup}");
                    writer.WriteLine("    {");
                    foreach(var clipInfo in audioClipEntry.AudioClips.Where(clipInfo =>
                        clipInfo.Clip != null))
                    {
                        writer.WriteLine("        /// <summary>");
                        writer.WriteLine($"        /// Name of clip {clipInfo.Clip.name}");
                        writer.WriteLine("        /// </summary>");

                        string name = ToPascalCase(clipInfo.Clip.name);
                        // check to prevent doubling the var name
                        if(checker.ContainsKey(name))
                        {
                            checker[name]++;
                            name += checker[name].ToString();
                        }
                        else
                        {
                            checker.Add(name, 0);
                        }

                        writer.WriteLine($"        public static readonly AudioClipInfo {name} " +
                                         $"= new AudioClipInfo {{ ClipName = \"{clipInfo.Clip.name}\", TargetGroup = AudioGroup.{audioClipEntry.TargetGroup}}};");
                    }

                    writer.WriteLine("    }");
                    writer.WriteLine();
                }

                // End of namespace
                writer.WriteLine("}");
                writer.WriteLine();
            }

            // Refresh
            AssetDatabase.Refresh();
#endif
        }

        private static string ToPascalCase(string original)
        {
            var invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
            var whiteSpace = new Regex(@"(?<=\s)");
            var startsWithLowerCaseChar = new Regex("^[a-z]");
            var firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            var lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            var upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

            // replace white spaces with undescore, then replace all invalid chars with empty string
            var pascalCase = invalidCharsRgx
                             .Replace(whiteSpace.Replace(original, "_"), string.Empty)
                             // split by underscores
                             .Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries)
                             // set first letter to uppercase
                             .Select(w => startsWithLowerCaseChar
                                         .Replace(w, m => m.Value.ToUpper()))
                             // replace second and all following upper case letters to lower if there is no next lower (ABC -> Abc)
                             .Select(w =>
                                         firstCharFollowedByUpperCasesOnly
                                             .Replace(w, m => m.Value.ToLower()))
                             // set upper case the first lower case following a number (Ab9cd -> Ab9Cd)
                             .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                             // lower second and next upper case letters except the last if it follows by any lower (ABcDEf -> AbcDef)
                             .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

            return string.Concat(pascalCase);
        }
    }
}