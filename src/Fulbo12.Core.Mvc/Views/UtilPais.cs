namespace Fulbo12.Core.Mvc.Views;
public static class UtilPais
{
    public static string UrlBandera(string abreviatura)
        => string.Concat(@"https://flagcdn.com/", abreviatura, ".svg");
    public static string UrlBandera(Pais pais)
        => UrlBandera(pais.Abreviatura);
    public static string IdImagen(Pais pais)
        => string.Concat("img", pais.Nombre);
}
