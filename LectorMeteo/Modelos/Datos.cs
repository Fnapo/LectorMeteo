using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LectorMeteo.Modelos
{
    /*
     * 
     * Primer nivel en la estructura de los datos útiles ...
     * Así de farragosos son los datos proporcionados por la Aemet
     * 
     */
    public record class Datos([property: JsonPropertyName("prediccion")] Prediccion Prediccion);
}
