using System.IO;
using UnityEditor;
using UnityEngine;

namespace DA.Editor
{
    public class GameMenu : EditorWindow
    {
        private const string ScreenshotPath = "Screenshots";
        
        [MenuItem("Game/Clear Player Prefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("Game/Take Screenshot")]
        public static void TakeScreenshot()
        {
            Directory.CreateDirectory(ScreenshotPath);

            int i = 0;
            while (File.Exists($"{ScreenshotPath}/{i}.png"))
            {
                i++;
            }

            ScreenCapture.CaptureScreenshot($"{ScreenshotPath}/{i}.png");
        }
    }
}