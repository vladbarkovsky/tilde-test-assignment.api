﻿using NJsonSchema.Generation;

namespace TildeTestAssignment.Web.ApiSpecification
{
    public class SchemaNameGenerator : DefaultSchemaNameGenerator, ISchemaNameGenerator
    {
        public override string Generate(Type type)
        {
            return type.ToFriendlyString().Replace(".", "");
        }
    }
}