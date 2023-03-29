using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using OptionData = TMPro.TMP_Dropdown.OptionData;

namespace Qplaze.DanPie.Localization
{
    public class LocalizationDropdownController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;

        private List<OptionData> options = new List<OptionData>();

        protected void Awake()
        {
            _dropdown.ClearOptions();
            options.Clear();
            Object[] languages = Resources.LoadAll(Localization.ResoursesPath);
            string currentLanguage = Localization.GetSavedLanguage().ToString();
            int currentLanguageValue = 0;

            for (int i = 0; i < languages.Length; i++)
            {
                if (currentLanguage == languages[i].name)
                {
                    currentLanguageValue = i;
                }
                options.Add(new OptionData(languages[i].name));
            }

            _dropdown.AddOptions(options);
            _dropdown.value = currentLanguageValue;
            _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int optionId)
        {
            Localization.LoadLanguage((SystemLanguage)Enum.Parse(typeof(SystemLanguage), _dropdown.options[optionId].text));
            Canvas.ForceUpdateCanvases();
        }
    }
}
