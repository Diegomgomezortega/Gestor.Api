namespace Gestor.API.DTOs.Response.Pais
{
    public record CountryRaw
    {
        public NameRaw? name { get; set; }
    }

    public record NameRaw
    {
        public string? common { get; set; }
    }
}
