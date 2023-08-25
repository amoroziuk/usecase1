using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace UseCase1.Models
{
    public class Country
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("tld")]
        public List<string> Tld { get; set; }

        [JsonProperty("cca2")]
        public string Cca2 { get; set; }

        [JsonProperty("ccn3")]
        public long Ccn3 { get; set; }

        [JsonProperty("cca3")]
        public string Cca3 { get; set; }

        [JsonProperty("cioc")]
        public string Cioc { get; set; }

        [JsonProperty("independent")]
        public bool Independent { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("unMember")]
        public bool UnMember { get; set; }

        [JsonProperty("currencies")]
        public Currencies Currencies { get; set; }

        [JsonProperty("idd")]
        public Idd Idd { get; set; }

        [JsonProperty("capital")]
        public List<string> Capital { get; set; }

        [JsonProperty("altSpellings")]
        public List<string> AltSpellings { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("subregion")]
        public string Subregion { get; set; }

        [JsonProperty("languages")]
        public Languages Languages { get; set; }

        [JsonProperty("translations")]
        public Dictionary<string, Translation> Translations { get; set; }

        [JsonProperty("latlng")]
        public List<long> Latlng { get; set; }

        [JsonProperty("landlocked")]
        public bool Landlocked { get; set; }

        [JsonProperty("borders")]
        public List<string> Borders { get; set; }

        [JsonProperty("area")]
        public long Area { get; set; }

        [JsonProperty("demonyms")]
        public Demonyms Demonyms { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("maps")]
        public Maps Maps { get; set; }

        [JsonProperty("population")]
        public long Population { get; set; }

        [JsonProperty("gini")]
        public Gini Gini { get; set; }

        [JsonProperty("fifa")]
        public string Fifa { get; set; }

        [JsonProperty("car")]
        public Car Car { get; set; }

        [JsonProperty("timezones")]
        public List<string> Timezones { get; set; }

        [JsonProperty("continents")]
        public List<string> Continents { get; set; }

        [JsonProperty("flags")]
        public Flags Flags { get; set; }

        [JsonProperty("coatOfArms")]
        public CoatOfArms CoatOfArms { get; set; }

        [JsonProperty("startOfWeek")]
        public string StartOfWeek { get; set; }

        [JsonProperty("capitalInfo")]
        public CapitalInfo CapitalInfo { get; set; }

        [JsonProperty("postalCode")]
        public PostalCode PostalCode { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class CapitalInfo
    {
        [JsonProperty("latlng")]
        public List<double> Latlng { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Car
    {
        [JsonProperty("signs")]
        public List<string> Signs { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class CoatOfArms
    {
        [JsonProperty("png")]
        public Uri Png { get; set; }

        [JsonProperty("svg")]
        public Uri Svg { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Currencies
    {
        [JsonProperty("ZAR")]
        public Zar Zar { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Zar
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Demonyms
    {
        [JsonProperty("eng")]
        public Eng Eng { get; set; }

        [JsonProperty("fra")]
        public Eng Fra { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Eng
    {
        [JsonProperty("f")]
        public string F { get; set; }

        [JsonProperty("m")]
        public string M { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Flags
    {
        [JsonProperty("png")]
        public Uri Png { get; set; }

        [JsonProperty("svg")]
        public Uri Svg { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Gini
    {
        [JsonProperty("2014")]
        public long The2014 { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Idd
    {
        [JsonProperty("root")]
        public string Root { get; set; }

        [JsonProperty("suffixes")]
        public List<string> Suffixes { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Languages
    {
        [JsonProperty("afr")]
        public string Afr { get; set; }

        [JsonProperty("eng")]
        public string Eng { get; set; }

        [JsonProperty("nbl")]
        public string Nbl { get; set; }

        [JsonProperty("nso")]
        public string Nso { get; set; }

        [JsonProperty("sot")]
        public string Sot { get; set; }

        [JsonProperty("ssw")]
        public string Ssw { get; set; }

        [JsonProperty("tsn")]
        public string Tsn { get; set; }

        [JsonProperty("tso")]
        public string Tso { get; set; }

        [JsonProperty("ven")]
        public string Ven { get; set; }

        [JsonProperty("xho")]
        public string Xho { get; set; }

        [JsonProperty("zul")]
        public string Zul { get; set; }
    }

    public partial class Maps
    {
        [JsonProperty("googleMaps")]
        public Uri GoogleMaps { get; set; }

        [JsonProperty("openStreetMaps")]
        public Uri OpenStreetMaps { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Name
    {
        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("official")]
        public string Official { get; set; }

        [JsonProperty("nativeName")]
        public Dictionary<string, Translation> NativeName { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Translation
    {
        [JsonProperty("official")]
        public string Official { get; set; }

        [JsonProperty("common")]
        public string Common { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class PostalCode
    {
        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("regex")]
        public string Regex { get; set; }
    }
}
