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
     * Segundo nivel en la estructura de los datos útiles ...
     * 
     */
    public record class Prediccion([property: JsonPropertyName("dia")] List<Medidas> Medidas);
}
