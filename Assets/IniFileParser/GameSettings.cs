﻿using IniParser;
using IniParser.Model;
using System.IO;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static class Meshing
    {
        public static volatile int meshingThreads = 2;
        public static volatile int queueLimit = 4;
    }

    static void DeserializeIni(string filename)
    {
        if (!File.Exists(filename))
            return;
        FileIniDataParser parser = new FileIniDataParser();
        IniData data = parser.ReadFile(filename);
        if(data.Sections.ContainsSection("Meshing"))
        {
            var meshingThreadsString = data["Meshing"]["meshingThreads"];
            if (meshingThreadsString != null)
                Meshing.meshingThreads = int.Parse(meshingThreadsString);
            var queueLimitString = data["Meshing"]["queueLimit"];
            if (queueLimitString != null)
                Meshing.queueLimit = int.Parse(queueLimitString);
        }
    }

    static void SerializeIni(string filename)
    {
        FileIniDataParser parser = new FileIniDataParser();
        IniData data = new IniData();
        if (File.Exists(filename))
        {
            data = parser.ReadFile(filename);
            File.Delete(filename);
        }

        data.Sections.AddSection("Meshing");
        data["Meshing"]["meshingThreads"] = Meshing.meshingThreads.ToString();
        data["Meshing"]["queueLimit"] = Meshing.queueLimit.ToString();

        parser.WriteFile(filename, data);
    }

    // This function is called when the MonoBehaviour will be destroyed
    public void OnDestroy()
    {
        SerializeIni("Config.ini");
    }

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        DeserializeIni("Config.ini");
    }
}