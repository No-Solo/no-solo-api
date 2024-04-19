using System.Diagnostics.CodeAnalysis;
using NoSolo.Abstractions.Services.Localization;

namespace NoSolo.Web.API.Localization;

[ExcludeFromCodeCoverage]
public class LocalizationService : ILocalizationService
{
    // private readonly DataBaseContext _context;
    // private readonly IMemoryCache _cache;
    // private readonly IHostEnvironment _env;
    private readonly string _defaultLocale = "en-US";

    // public LocalizationService(DataBaseContext context, IMemoryCache cache, IHostEnvironment env)
    // {
    //     _context = context;
    //     _cache = cache;
    //     _locales = Locale.locales.ToList();
    //     SetLocale(_defaultLocale);
    //     _env = env;
    // }
    
    public LocalizationService()
    {
        _locales = Locale.LOCALES.ToList();
        SetLocale(_defaultLocale);
    }

    public List<string> Locales
    {
        get => _locales;
    }

    private List<string> _locales;

    public string CurrentLocale
    {
        get => _currentLocale;
    }


    private string _currentLocale;

    public string Get(string key, string locale)
    {
        // if (locale is null)
        //     locale = _defaultLocale;
        //
        // return Get(key, _session.SelectedOrganizationId ?? Session.SystemOrgId, locale);
        
        throw new NotImplementedException();
    }

    public string Get(string key, string orgId, string locale)
    {
        // if (locale is null)
        //     locale = _defaultLocale;
        //
        // key ??= key.TrimIfNull();
        // string? value = _context.Localizations.FirstOrDefault(li =>
        //     !li.Deleted && li.CreatedInOrgId == orgId && li.Key.ToLower().Trim() == key.ToLower().Trim())?.Value;
        // if (value != null)
        //     value = SplitByValueAndLocale(value, locale);
        //
        // else
        // {
        //     value = _context.Localizations.FirstOrDefault(li =>
        //         !li.Deleted && li.CreatedInOrgId == Session.SystemOrgId &&
        //         li.Key.ToLower().Trim() == key.ToLower().Trim())?.Value;
        //     if (value != null)
        //         value = SplitByValueAndLocale(value, locale);
        // }
        //
        // if (value != null)
        //     return value;
        //
        // string unlocalizedMsg = $"Unlocalized {CurrentLocale} : {key}";
        //
        // ConsoleHelper.ShowError(unlocalizedMsg);
        //
        // return unlocalizedMsg;
        
        throw new NotImplementedException();
    }

    private string SplitByValueAndLocale(string value, string locale)
    {
        // string[] values = value.Split(';');
        // foreach (string val in values)
        //     if (val.Contains(locale))
        //         return val.Split(':')[1];
        //
        // return null;
        
        throw new NotImplementedException();
    }

    public string GetSystem(string key, string locale)
    {
        // if (locale is null)
        //     locale = _defaultLocale;
        //
        // return Get(key, Session.SystemOrgId, locale);
        
        throw new NotImplementedException();
    }

    public List<string> GetRange(List<string> keys, string locale)
    {
        // if (locale is null)
        //     locale = _defaultLocale;
        // List<string> result = new List<string>();
        //
        // foreach (string key in keys)
        //     result.Add(Get(key, locale));
        //
        // return result;
        
        throw new NotImplementedException();
    }

    private void SetLocale(string locale)
    {
        if (Locales.Contains(locale))
            _currentLocale = locale;
    }
}