using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Qplaze.DanPie.Localization
{
    public class Localization
    {
        public static event Action LanguageChanged;

        private static Dictionary<string, string> languageDictionary;

        public static string PlayerPrefsKey { get; private set; } = "GameLanguage";
        public static string ResoursesPath { get; private set; } = "Localizations";
        public static bool IsLoaded { get => languageDictionary != null; }

        public static string GetString(string searchString, UnityEngine.Object context = null)
        {
            if (languageDictionary == null)
            {
                throw new Exception($"No language loaded! Use the {nameof(LoadLanguage)} method to load a new language.");
            }

            if (languageDictionary.ContainsKey(searchString))
            {
                return languageDictionary[searchString];
            }
            else
            {
                Debug.LogError("Unknown string: '" + searchString + "'", context);
                return "^" + searchString;
            }
        }

        public static string GetStringWithValues(string searchString, params string[] values)
        {
            return string.Format(Localization.GetString(searchString), values);
        }

        public static void LoadLanguage(SystemLanguage language)
        {
            string languageName = Enum.GetName(typeof(SystemLanguage), language);
            TextAsset languageAsset = (TextAsset)Resources.Load($"{ResoursesPath}/{languageName}");

            if (languageAsset == null)
            {
                if (language != SystemLanguage.English)
                {
                    Debug.LogWarning($"Файл с языком \"{ResoursesPath}/{languageName}\" не найден. Язык автоматически изменён на Английский.");
                    LoadLanguage(SystemLanguage.English);
                    return;
                }
                else
                {
                    throw new ArgumentException($"Файл с основным языком \"{ResoursesPath}/{languageName}\" не найден!");
                } 
            }

            languageDictionary = ConvertXMLAssetToDictionaty(languageAsset);
            LanguageChanged?.Invoke();
            SaveLanguage(language);
        }

        /// <summary>
        /// Returns the last loaded language, saved via PlayerPrefs.
        /// </summary>
        /// <returns></returns>
        public static SystemLanguage GetSavedLanguage()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey))
            {
                return (SystemLanguage)PlayerPrefs.GetInt(PlayerPrefsKey);
            }
            return Application.systemLanguage;
        }

        private static void SaveLanguage(SystemLanguage language)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey, (int)language);
        }

        private static Dictionary<string, string> ConvertXMLAssetToDictionaty(TextAsset languageAsset)
        {
            XmlDocument languageXMLFile = new XmlDocument();
            languageXMLFile.LoadXml(languageAsset.text);
            Dictionary<string, string> languageDictionary = new Dictionary<string, string>();
            XmlElement root = languageXMLFile.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("//string");

            foreach (XmlNode node in nodes)
            {
                string key = node.Attributes["name"].Value;
                if (!languageDictionary.ContainsKey(key))
                {
                    languageDictionary.Add(key, node.InnerText);
                }
                else
                {
                    Debug.LogErrorFormat("Alias '{0}' уже существует в файле {1}", key, languageAsset.name);
                }
            }

            return languageDictionary;

        }

        [RuntimeInitializeOnLoadMethod]
        private static void InitializeLocalization()
        {
            LoadLanguage(GetSavedLanguage());
        }
    } 
}