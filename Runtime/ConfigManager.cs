using System.IO;
using UnityEngine;

namespace ConfigKit.Runtime
{
    public static class ConfigManager
    {
        private static readonly string _persistentPath = Path.Combine(Application.persistentDataPath, "config.json");
        private static readonly string _editorConfigPath = Path.Combine(Application.streamingAssetsPath, "config.json");
        

        public static T LoadConfig<T>() where T : BaseConfig
        {
            if (Application.isEditor)
            {
                
                
                return LoadFromEditor<T>();
            }
            else
            {
                T config = null;
                if (!File.Exists(_persistentPath))
                {
                    config = LoadFromEditor<T>();
                    SavePersistenceConfig(config);
                }
                else
                {
                    config = LoadFromPersistent<T>();
                }
                
                return config;
            }
        }

        private static T LoadFromEditor<T>() where T : BaseConfig
        {
            var json = File.ReadAllText(_editorConfigPath);
            var config = JsonUtility.FromJson<T>(json);
            return config;
        }
        
        private static T LoadFromPersistent<T>() where T : BaseConfig
        {
            var json = File.ReadAllText(_persistentPath);
            var config = JsonUtility.FromJson<T>(json);
            return config;
        }
        
        private static void SaveEditorConfig<T>(T config) where T : BaseConfig
        {
            var json = JsonUtility.ToJson(config);
            File.WriteAllText(_editorConfigPath, json);
        }
        
        private static void SavePersistenceConfig<T>(T config) where T : BaseConfig
        {
            var json = JsonUtility.ToJson(config);
            File.WriteAllText(_persistentPath, json);
        }
    }

    public class BaseConfig { }
}