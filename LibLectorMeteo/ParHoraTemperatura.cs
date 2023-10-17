using System.Text.Json.Serialization;

namespace LibLectorMeteo
{
    /*
     * 
     * Quinto nivel en la estructura de los datos útiles ...
     * 
     */
    public record class ParHoraTemperatura(
        [property: JsonPropertyName("value")] int Temperatura,
        [property: JsonPropertyName("hora")] int Hora
    );
}