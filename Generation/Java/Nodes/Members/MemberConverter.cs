using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes.Members
{
    public class MemberConverter : JsonConverter<Member>
    {
        public override Member? Read(ref Utf8JsonReader reader, System.Type typeToConvert,
            JsonSerializerOptions options)
        {
            // TODO(Michael): Implement conversion to either MemberVariable or MemberMethod
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Member value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}