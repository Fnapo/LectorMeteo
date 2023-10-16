using System.Text.Json.Serialization;

namespace LectorMeteo.Modelos
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