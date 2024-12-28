using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkerType
{
    Doctor,
    Nurse
}