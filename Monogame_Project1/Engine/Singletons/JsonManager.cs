using System;
using System.IO;
using Monogame_Project1.Engine.JSON;
using Newtonsoft.Json;

namespace Monogame_Project1.Engine.Singletons;

public class JsonManager
{
    private static JsonManager _instance;
    public static JsonManager Instance => _instance ??= new JsonManager();
    private string _jsonDirectory;
    private string _filePath;
    private GameInfo _currentGameInfo;
        
    public GameInfo CurrentGameInfo => _currentGameInfo;
    public void SetupJson()
    {
        if (File.Exists(GetJsonDirectory() + "\\GameInfo.json")) return;
        if (_jsonDirectory == null)
        {
            _jsonDirectory = GetJsonDirectory();
            _filePath = GetFilePath("LevelInfo.json"); 
            WriteJson(new GameInfo(), _filePath);
        }
    }
    public string ReadJson(string pFileName)
    {
        string fullPath = Path.Combine(_jsonDirectory, pFileName + ".json");
        if (File.Exists(fullPath))
        {
            string jsonData = File.ReadAllText(fullPath);
            Console.WriteLine(jsonData);
            return jsonData;
        }
        Console.WriteLine("File does not exist: " + fullPath);
        return null;
    }
    public void WriteJson(GameInfo pJsonData, string pFilePath)
    {
        string jsonData = JsonConvert.SerializeObject(pJsonData, Formatting.Indented);
        File.WriteAllText(pFilePath + ".json", jsonData);
    }
    public string GetFilePath(string pFileName)
    {
        return Path.Combine(_jsonDirectory, pFileName); 
    }
    public void AddLevelCount()
    {
        _currentGameInfo.LevelCount++;
        Console.WriteLine($"Level score: {_currentGameInfo.LevelCount}");
    }
    public string GetJsonDirectory()
    {
        if (_jsonDirectory == null) 
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;  
            _jsonDirectory = Path.Combine(baseDirectory, "Json");
            if (!Directory.Exists(_jsonDirectory))
            {
                CreateJsonDirectory();
            }
        }
        return _jsonDirectory;
    }
    private void CreateJsonDirectory()
    {
        Directory.CreateDirectory(_jsonDirectory);
        Console.WriteLine("Created directory: " + _jsonDirectory);
    }
}