using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibLectorMeteo
{
    /*
     * 
     * Cuarto nivel en la estructura de los datos útiles ...
     * 
     */
    public record class Temperaturas(
        [property: JsonPropertyName("maxima")] int Maxima,
        [property: JsonPropertyName("minima")] int Minima,
        [property: JsonPropertyName("dato")] List<ParHoraTemperatura> Datos
    );
}
