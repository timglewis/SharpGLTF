//------------------------------------------------------------------------------------------------
// This file was manually generated as the auto-generation didn't like the source schema
//------------------------------------------------------------------------------------------------

#pragma warning disable SA1001
#pragma warning disable SA1027
#pragma warning disable SA1028
#pragma warning disable SA1121
#pragma warning disable SA1205
#pragma warning disable SA1309
#pragma warning disable SA1402
#pragma warning disable SA1505
#pragma warning disable SA1507
#pragma warning disable SA1508
#pragma warning disable SA1652

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Text.Json;

namespace SharpGLTF.Schema2
{
	using Collections;

	/// <summary>
	/// An object defining classes and enums.
	/// </summary>
	partial class StructuralMetadata : ExtraProperties
	{
		private StructuralMetadataSchema _schema;

		private String _schemaUri;

		List<StructuralMetadataPropertyTable> _propertyTables;

		List<StructuralMetadataPropertyTexture> _propertyTextures;

		List<StructuralMetadataPropertyAttribute> _propertyAttributes;
	
		protected override void SerializeProperties(Utf8JsonWriter writer)
		{
			base.SerializeProperties(writer);
			SerializeProperty(writer, "schema", _schema);
			SerializeProperty(writer, "schemaUri", _schemaUri);
			SerializeProperty(writer, "propertyTables", _propertyTables);
			SerializeProperty(writer, "propertyTextures", _propertyTextures);
			SerializeProperty(writer, "propertyAttributes", _propertyAttributes);
		}
	
		protected override void DeserializeProperty(string jsonPropertyName, ref Utf8JsonReader reader)
		{
			switch (jsonPropertyName)
			{
				case "schema": _schema = DeserializePropertyValue<StructuralMetadataSchema>(ref reader); break;
				case "schemaUri": _schemaUri = DeserializePropertyValue<String>(ref reader); break;
				case "propertyTables": DeserializePropertyList<StructuralMetadataPropertyTable>(ref reader, _propertyTables); break;
				case "propertyTextures": DeserializePropertyList<StructuralMetadataPropertyTexture>(ref reader, _propertyTextures); break;
				case "propertyAttributes": DeserializePropertyList<StructuralMetadataPropertyAttribute>(ref reader, _propertyAttributes); break;
				default: base.DeserializeProperty(jsonPropertyName,ref reader); break;
			}
		}

	}
}
