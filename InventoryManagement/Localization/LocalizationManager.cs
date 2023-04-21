namespace InventoryManagement.Localization;

public static class LocalizationManager
    {
        private static readonly Localization Localization = new ();
        
        private static readonly List<Language> Languages = new ()
        {
            new Language("de", "Deutsch"),
            new Language("en", "English")
        };
        
        private static Language? _currentLanguage;

        static LocalizationManager()
        {
            var de = GetLanguageByCode("de");
            
            Localization.AddLocalization(de.Code, TextEnum.MenuTitleMain, "Hauptmenü");
            Localization.AddLocalization(de.Code, TextEnum.MenuTitleLogin, "Anmeldung");
            Localization.AddLocalization(de.Code, TextEnum.MenuTitleLanguages, "Sprachen");
            Localization.AddLocalization(de.Code, TextEnum.MenuTitleInventory, "Inventar");
            Localization.AddLocalization(de.Code, TextEnum.MenuTitleUsers, "Nutzer");
            Localization.AddLocalization(de.Code, TextEnum.MenuTitleHelp, "Hilfe");
            
            Localization.AddLocalization(de.Code, TextEnum.OptionShowInventoryItems, "Inventar anzeigen");
            Localization.AddLocalization(de.Code, TextEnum.OptionCreateInventoryItem, "Item hinzufügen");
            
            Localization.AddLocalization(de.Code, TextEnum.PromptPassword, "Passwort");
            Localization.AddLocalization(de.Code, TextEnum.PromptNewUser, "[Neuer Nutzer]");
            Localization.AddLocalization(de.Code, TextEnum.PromptNewUserName, "Nutzername");
            
            
            Localization.AddLocalization(de.Code, TextEnum.ItemName, "Name");
            Localization.AddLocalization(de.Code, TextEnum.ItemQuantity, "Menge");
            Localization.AddLocalization(de.Code, TextEnum.ItemPrice, "Preis");
            
            Localization.AddLocalization(de.Code, TextEnum.PrintHelp, "Hilfe");
            Localization.AddLocalization(de.Code, TextEnum.PrintHelpRow1, "←↑↓→ zur Navigation benutzen.");
            Localization.AddLocalization(de.Code, TextEnum.PrintHelpRow2, "Esc um zum Hauptmenü zurückzukehren.");
            Localization.AddLocalization(de.Code, TextEnum.PrintHelpRow3, "Backspace um zum übergeordneten Menü zurückzukehren.");
            Localization.AddLocalization(de.Code, TextEnum.PrintHelpRow4, "Del um den Log zu leeren.");
            Localization.AddLocalization(de.Code, TextEnum.DeleteUser, "Nutzer löschen");
            Localization.AddLocalization(de.Code, TextEnum.DeleteItem, "Item löschen");
            
            Localization.AddLocalization(de.Code, TextEnum.ErrorInvalidPassword, "Ungültiges Passwort");

            var en = GetLanguageByCode("en");

            Localization.AddLocalization(en.Code, TextEnum.MenuTitleMain, "Main");
            Localization.AddLocalization(en.Code, TextEnum.MenuTitleLogin, "Login");
            Localization.AddLocalization(en.Code, TextEnum.MenuTitleLanguages, "Languages");
            Localization.AddLocalization(en.Code, TextEnum.MenuTitleInventory, "Inventory");
            Localization.AddLocalization(en.Code, TextEnum.MenuTitleUsers, "Users");
            Localization.AddLocalization(en.Code, TextEnum.MenuTitleHelp, "Help");
            
            Localization.AddLocalization(en.Code, TextEnum.OptionShowInventoryItems, "Show inventory");
            Localization.AddLocalization(en.Code, TextEnum.OptionCreateInventoryItem, "Add item");
            
            Localization.AddLocalization(en.Code, TextEnum.PromptPassword, "Password");
            Localization.AddLocalization(en.Code, TextEnum.PromptNewUser, "[New user]");
            Localization.AddLocalization(en.Code, TextEnum.PromptNewUserName, "Username");
            
            Localization.AddLocalization(en.Code, TextEnum.ItemName, "Name");
            Localization.AddLocalization(en.Code, TextEnum.ItemQuantity, "Quantity");
            Localization.AddLocalization(en.Code, TextEnum.ItemPrice, "Price");
            
            Localization.AddLocalization(en.Code, TextEnum.PrintHelp, "Help");
            Localization.AddLocalization(en.Code, TextEnum.PrintHelpRow1, "Use ←↑↓→ for navigation.");
            Localization.AddLocalization(en.Code, TextEnum.PrintHelpRow2, "Press Esc for return to main menu.");
            Localization.AddLocalization(en.Code, TextEnum.PrintHelpRow3, "Press Backspace for return to parent menu.");
            Localization.AddLocalization(en.Code, TextEnum.PrintHelpRow4, "Press Del for clear log.");
            Localization.AddLocalization(en.Code, TextEnum.DeleteUser, "Delete user");
            Localization.AddLocalization(en.Code, TextEnum.DeleteItem, "Delete item");
            
            Localization.AddLocalization(en.Code, TextEnum.ErrorInvalidPassword, "Invalid password");
            
            SetCurrentLanguage(GetLanguageByCode("de"));
        }

        public static Language GetLanguageByCode(string code)
        {
            return Languages.Find(language => language.Code == code);
        }

        public static Language GetLanguageByName(string name)
        {
            return Languages.Find(language => language.Name == name);
        }
        
        public static void SetCurrentLanguage(Language language)
        {
            if (Languages.Contains(language))
            {
                _currentLanguage = language;
            }
        }

        public static void AddLocalization(string languageCode, TextEnum key, string value)
        {
            AddLocalization(languageCode, key.ToString(), value);
        }
        
        public static void AddLocalization(string languageCode, string key, string value)
        {
            Localization.AddLocalization(languageCode, key, value);
        }

        public static string GetText(TextEnum key)
        {
            return GetText(key.ToString());
        }
        
        public static string GetText(string key)
        {
            if (_currentLanguage == null)
            {
                return $"[{key}]";
            }
            
            return Localization.GetText(_currentLanguage.Code, key);
        }

        public static List<Language> GetAvailableLanguages()
        {
            return Languages;
        }

        public static Language GetCurrentLanguage()
        {
            return _currentLanguage;
        }
    }