using System.Text.Json.Serialization;

namespace LibLectorMeteo
{
    /*
     * 
     * Tercer nivel en la estructura de los datos útiles ...
     * 
     */
    public record class Medidas(
        [property: JsonPropertyName("temperatura")] Temperaturas Temperaturas,
        [property: JsonPropertyName("fecha")] DateTime Fecha
    );
}