using InventoryManagement.Localization;

namespace InventoryManagementTests;

[TestClass]
public class LocalizationManagerTests
{
    [TestInitialize]
    public void Setup()
    {
        // Set the default language to German
        LocalizationManager.SetCurrentLanguage(LocalizationManager.GetLanguageByCode("de"));
    }
    
    [TestMethod]
    public void TestAddLocalization()
    {
        // Add a new localized text for a key
        LocalizationManager.AddLocalization("de", "TestKey", "TestValue");

        // Retrieve the added localized text
        var actual = LocalizationManager.GetText("TestKey");

        // Check if the added localized text is retrieved correctly
        Assert.AreEqual("TestValue", actual);
    }

    [TestMethod]
    public void TestGetLanguageByCode()
    {
        // Retrieve a language object by its code
        var actual = LocalizationManager.GetLanguageByCode("en");

        // Check if the retrieved language object has the correct code and name
        Assert.AreEqual("en", actual.Code);
        Assert.AreEqual("English", actual.Name);
    }

    [TestMethod]
    public void TestGetLanguageByName()
    {
        // Retrieve a language object by its name
        var actual = LocalizationManager.GetLanguageByName("Deutsch");

        // Check if the retrieved language object has the correct code and name
        Assert.AreEqual("de", actual.Code);
        Assert.AreEqual("Deutsch", actual.Name);
    }

    [TestMethod]
    public void TestSetCurrentLanguage()
    {
        // Set the current language to English
        LocalizationManager.SetCurrentLanguage(LocalizationManager.GetLanguageByCode("en"));

        // Retrieve the current language
        var actual = LocalizationManager.GetCurrentLanguage();

        // Check if the current language is set correctly
        Assert.AreEqual("en", actual.Code);
        Assert.AreEqual("English", actual.Name);
    }

    [TestMethod]
    public void TestGetText()
    {
        // Retrieve a localized text for a key
        var actual = LocalizationManager.GetText(TextEnum.MenuTitleMain);

        // Check if the retrieved localized text is correct for the current language
        Assert.AreEqual("Hauptmenü", actual);
    }

    [TestMethod]
    public void TestGetText_DefaultLanguage()
    {
        // Set the current language to null
        LocalizationManager.SetCurrentLanguage(null);

        // Retrieve a localized text for a key
        string actual = LocalizationManager.GetText(TextEnum.MenuTitleMain);

        // Check if the default localized text is returned when no language is selected
        Assert.AreEqual("Hauptmenü", actual);
    }

    [TestMethod]
    public void TestGetAvailableLanguages()
    {
        // Retrieve a list of all available languages
        List<Language> actual = LocalizationManager.GetAvailableLanguages();

        // Check if the list contains the expected languages
        Assert.AreEqual(2, actual.Count);
        Assert.AreEqual("de", actual[0].Code);
        Assert.AreEqual("Deutsch", actual[0].Name);
        Assert.AreEqual("en", actual[1].Code);
        Assert.AreEqual("English", actual[1].Name);
    }

    [TestMethod]
    public void TestGetCurrentLanguage()
    {
        // Retrieve the current language
        Language actual = LocalizationManager.GetCurrentLanguage();

        // Check if the current language is set to the default language
        Assert.AreEqual("de", actual.Code);
        Assert.AreEqual("Deutsch", actual.Name);
    }
    
    [TestMethod]
    public void TestGetText_InvalidKey()
    {
        // Retrieve a localized text for an invalid key
        string actual = LocalizationManager.GetText("InvalidKey");

        // Check if the default localized text is returned for an invalid key
        Assert.AreEqual("[InvalidKey]", actual);
    }
    
    [TestMethod]
    public void TestGetText_InvalidLanguage()
    {
        // Set the current language to an invalid language
        LocalizationManager.SetCurrentLanguage(new Language("fr", "French"));

        // Retrieve a localized text for a key
        string actual = LocalizationManager.GetText("MenuTitleMain");

        // Check if the default localized text is returned when the selected language is not supported
        Assert.AreEqual("Hauptmenü", actual);
    }
}