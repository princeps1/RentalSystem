using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class VoziloDodavanjeDTOConverter : JsonConverter<VoziloDodavanjeDTO>
{
    public override VoziloDodavanjeDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var document = JsonDocument.ParseValue(ref reader))
        {
            var root = document.RootElement;

            if (root.TryGetProperty("tip", out var tipProperty))
            {
                var tip = tipProperty.GetString();

                if (tip!.Equals("Motor", StringComparison.OrdinalIgnoreCase))
                {
                    var motorDTO = JsonSerializer.Deserialize<MotorDodavanjeDTO>(root.GetRawText(), options);
                    if (motorDTO == null)
                    {
                        throw new InvalidOperationException("Neuspjela deserializacija za MotorDodavanjeDTO.");
                    }
                    return motorDTO;
                }
                else if (tip.Equals("Automobil", StringComparison.OrdinalIgnoreCase))
                {
                    var automobilDTO = JsonSerializer.Deserialize<AutomobilDodavanjeDTO>(root.GetRawText(), options);
                    if (automobilDTO == null)
                    {
                        throw new InvalidOperationException("Neuspjela deserializacija za AutomobilDodavanjeDTO.");
                    }
                    return automobilDTO;
                }
            }

            throw new InvalidOperationException("Nepoznati tip ili nedostaje svojstvo 'tip'.");
        }
    }

    public override void Write(Utf8JsonWriter writer, VoziloDodavanjeDTO value, JsonSerializerOptions options)
    {
        if (value is MotorDodavanjeDTO motorDTO)
        {
            JsonSerializer.Serialize(writer, motorDTO, options);
        }
        else if (value is AutomobilDodavanjeDTO automobilDTO)
        {
            JsonSerializer.Serialize(writer, automobilDTO, options);
        }
        else
        {
            throw new InvalidOperationException("Nepoznati tip VoziloDodavanjeDTO.");
        }
    }
}
