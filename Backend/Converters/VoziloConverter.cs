using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class VoziloConverter : JsonConverter<Vozilo>
{

    public override Vozilo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Deserialize the root element into a JsonElement
        var root = JsonDocument.ParseValue(ref reader).RootElement;

        // Check the "Tip" property to determine the type
        if (root.TryGetProperty("tip", out var tipProperty))
        {
            var tip = tipProperty.GetString();

            if (tip == "Motor")
            {
                // Deserialize as Motor
                var motor = JsonSerializer.Deserialize<Motor>(root.GetRawText(), options);
                if (motor == null)
                {
                    throw new InvalidOperationException("Failed to deserialize Motor.");
                }
                return motor;
            }
            else if (tip == "Automobil")
            {
                // Deserialize as Automobil
                var automobil = JsonSerializer.Deserialize<Automobil>(root.GetRawText(), options);
                if (automobil == null)
                {
                    throw new InvalidOperationException("Failed to deserialize Automobil.");
                }
                return automobil;
            }
        }

        // If Tip is not found or doesn't match, throw an exception or return a default value
        throw new InvalidOperationException("Unknown vehicle type or missing 'Tip' property.");
    }

    public override void Write(Utf8JsonWriter writer, Vozilo value, JsonSerializerOptions options)
    {
        // Serialize the object based on its type (Motor or Automobil)
        if (value is Motor motor)
        {
            JsonSerializer.Serialize(writer, motor, options);
        }
        else if (value is Automobil automobil)
        {
            JsonSerializer.Serialize(writer, automobil, options);
        }
        else
        {
            // Handle unexpected types
            throw new InvalidOperationException("Unknown vehicle type.");
        }
    }

}
