namespace InventoryManagement.Localization;

public class Localization
{
    private readonly Dictionary<string, Dictionary<string, string>> _localizations;

    public Localization()
    {
        _localizations = new Dictionary<string, Dictionary<string, string>>();
    }

    public void AddLocalization(string languageCode, TextEnum key, string value)
    {
        AddLocalization(languageCode, key.ToString(), value);
    }
    
    public void AddLocalization(string languageCode, string key, string value)
    {
        if (!_localizations.ContainsKey(languageCode))
        {
            _localizations[languageCode] = new Dictionary<string, string>();
        }
        
        _localizations[languageCode][key] = value;
    }

    public string GetText(string languageCode, string key)
    {
        if (_localizations.ContainsKey(languageCode) && _localizations[languageCode].ContainsKey(key))
        {
            return _localizations[languageCode][key];
        }
        
        return $"[{key}]";
    }
}