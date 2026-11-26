using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Object
    {
        public static void Destroy(Object obj) { }
        public static void DontDestroyOnLoad(Object obj) { }
    }

    public class MonoBehaviour : Object
    {
        public Object gameObject => this;

        public T GetComponent<T>() where T : class
        {
            return this as T;
        }
    }

    public static class Application
    {
        public static HashSet<string> AvailableScenes = new()
        {
            "AgoeWorld_MainMenu",
            "AgoeWorld_Lobby",
            "AgoeWorld_CharacterSelection",
            "AgoeWorld_Gameplay"
        };

        public static bool CanStreamedLevelBeLoaded(string name)
        {
            return AvailableScenes.Contains(name);
        }
    }

    public static class Debug
    {
        public static void Log(string message) =>
            Console.WriteLine("INFO: " + message);

        public static void LogError(string message) =>
            Console.WriteLine("ERROR: " + message);
    }
}

namespace UnityEngine.SceneManagement
{
    public static class SceneManager
    {
        public static string LastLoadedScene { get; private set; }

        public static void LoadScene(string name)
        {
            LastLoadedScene = name;
        }
    }
}

namespace UnityEngine.UI
{
    public class Button
    {
        public ButtonClickedEvent onClick = new();

        public class ButtonClickedEvent
        {
            private readonly List<Action> listeners = new();

            public void AddListener(Action action) =>
                listeners.Add(action);

            public void Invoke()
            {
                foreach (var action in listeners)
                    action();
            }
        }
    }
}
