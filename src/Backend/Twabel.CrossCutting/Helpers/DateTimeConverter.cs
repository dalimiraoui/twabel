using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Twabel.CrossCutting.Helpers
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFZ"));
        }
    }
}
/*
This C# code defines a custom JSON converter for serializing and deserializing DateTime objects.

The Read method is called during deserialization when JSON data is being read from a stream. 
It takes a Utf8JsonReader object, the type being converted (DateTime), and any additional serialization options. 
It then uses the DateTime.Parse method to convert the JSON string representation of a DateTime object into a DateTime 
instance.

The Write method is called during serialization when JSON data is being written to a stream. 
It takes a Utf8JsonWriter object, the DateTime value being serialized, and any additional serialization options. 
It converts the DateTime value to universal time using ToUniversalTime, 
formats the DateTime value as a string using the specified format ("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFZ"), 
and writes it as a string value using the WriteStringValue method of the Utf8JsonWriter. 
The format string used here represents the DateTime value in ISO 8601 format with fractional 
seconds and a trailing 'Z' to indicate that the time is in UTC.


*/