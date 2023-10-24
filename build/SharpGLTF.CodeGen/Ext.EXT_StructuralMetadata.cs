using System;
using System.Collections.Generic;
using System.Text;

using SharpGLTF.SchemaReflection;

namespace SharpGLTF
{
    class StructuralMetadataExtension : SchemaProcessor
    {
        private static string RootSchemaUri => Constants.VendorExtensionPath("EXT_structural_metadata", "glTF.EXT_structural_metadata.schema.json");
        private static string ElementSchemaUri => Constants.VendorExtensionPath("EXT_structural_metadata", "EXT_structural_metadata.schema.json");
        private static string MeshPrimitiveSchemaUri => Constants.VendorExtensionPath("EXT_structural_metadata", "mesh.primitive.EXT_structural_metadata.schema.json");
        private static string SchemaSchemaUri => Constants.VendorExtensionPath("EXT_structural_metadata", "schema.schema.json");
        private static string PropertyTableSchemaUri => Constants.VendorExtensionPath("EXT_structural_metadata", "propertyTable.schema.json");
        private static string PropertyTextureSchemaUri => Constants.VendorExtensionPath("EXT_structural_metadata", "propertyTexture.schema.json");
        
        public override IEnumerable<(string, SchemaType.Context)> Process()
        {
            yield return ("ext.StructuralMetadata.g", ProcessRoot());
            yield return ("ext.StructuralMetadataElement.g", ProcessElement());
            yield return ("ext.StructuralMetadataMeshPrimitive.g", ProcessMeshPrimitive());
            yield return ("ext.StructuralMetadataSchema.g", ProcessSchema());
            yield return ("ext.StructuralMetadataPropertyTable.g", ProcessPropertyTable());
            yield return ("ext.StructuralMetadataPropertyTexture.g", ProcessPropertyTexture());
        }

        private static SchemaType.Context ProcessRoot()
        {
            var ctx = SchemaProcessing.LoadSchemaContext(RootSchemaUri);
            ctx.IgnoredByCodeEmitter("glTF Property");
            ctx.IgnoredByCodeEmitter("glTF Child of Root Property");

            return ctx;
        }

        private static SchemaType.Context ProcessElement()
        {
            var ctx = SchemaProcessing.LoadSchemaContext(ElementSchemaUri);
            ctx.IgnoredByCodeEmitter("glTF Property");
            ctx.IgnoredByCodeEmitter("glTF Child of Root Property");

            return ctx;
        }

        private static SchemaType.Context ProcessMeshPrimitive()
        {
            var ctx = SchemaProcessing.LoadSchemaContext(MeshPrimitiveSchemaUri);
            ctx.IgnoredByCodeEmitter("glTF Property");
            ctx.IgnoredByCodeEmitter("glTF Child of Root Property");

            return ctx;
        }

        private static SchemaType.Context ProcessSchema()
        {
            var ctx = SchemaProcessing.LoadSchemaContext(SchemaSchemaUri);
            ctx.IgnoredByCodeEmitter("glTF Property");
            ctx.IgnoredByCodeEmitter("glTF Child of Root Property");

            return ctx;
        }

        private static SchemaType.Context ProcessPropertyTable()
        {
            var ctx = SchemaProcessing.LoadSchemaContext(PropertyTableSchemaUri);
            ctx.IgnoredByCodeEmitter("glTF Property");
            ctx.IgnoredByCodeEmitter("glTF Child of Root Property");

            return ctx;
        }

        private static SchemaType.Context ProcessPropertyTexture()
        {
            var ctx = SchemaProcessing.LoadSchemaContext(PropertyTextureSchemaUri);
            ctx.IgnoredByCodeEmitter("glTF Property");
            ctx.IgnoredByCodeEmitter("glTF Child of Root Property");
            ctx.IgnoredByCodeEmitter("Texture Info");

            ctx.FindClass("Property Texture Property in EXT_structural_metadata")
                .GetField("channels")
                .RemoveDefaultValue()
                .SetItemsRange(1);

            return ctx;
        }

        public override void PrepareTypes(CodeGen.CSharpEmitter newEmitter, SchemaType.Context ctx)
        {
            newEmitter.SetRuntimeName("EXT_structural_metadata glTF extension", "StructuralMetadata");
            newEmitter.SetRuntimeName("EXT_structural_metadata glTF Mesh Primitive extension", "StructuralMetadataMeshPrimitive");

            newEmitter.SetRuntimeName("Schema in EXT_structural_metadata", "StructuralMetadataSchema");
            newEmitter.SetRuntimeName("Class in EXT_structural_metadata", "StructuralMetadataClass");
            newEmitter.SetRuntimeName("Enum in EXT_structural_metadata", "StructuralMetadataEnum");
            newEmitter.SetRuntimeName("Enum Value in EXT_structural_metadata", "StructuralMetadataEnumValue");
            newEmitter.SetRuntimeName("Class Property in EXT_structural_metadata", "StructuralMetadataClassProperty"); 

            newEmitter.SetRuntimeName("Property Table in EXT_structural_metadata", "StructuralMetadataPropertyTable");
            newEmitter.SetRuntimeName("Property Table Property in EXT_structural_metadata", "StructuralMetadataPropertyTableProperty");

            newEmitter.SetRuntimeName("Property Texture in EXT_structural_metadata", "StructuralMetadataPropertyTexture");
            newEmitter.SetRuntimeName("Property Texture Property in EXT_structural_metadata", "StructuralMetadataPropertyTextureProperty");

            newEmitter.SetRuntimeName("UINT16-UINT32-UINT64-UINT8", "OffsetType");
            newEmitter.SetRuntimeName("BOOLEAN-ENUM-MAT2-MAT3-MAT4-SCALAR-STRING-VEC2-VEC3-VEC4", "ElementType");
            newEmitter.SetRuntimeName("FLOAT32-FLOAT64-INT16-INT32-INT64-INT8-UINT16-UINT32-UINT64-UINT8", "ComponentType");
            newEmitter.SetRuntimeName("INT16-INT32-INT64-INT8-UINT16-UINT32-UINT64-UINT8", "IntegerType");
        }
    }
}