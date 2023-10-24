using System;
using System.Collections.Generic;
using System.Text;

using SharpGLTF.SchemaReflection;

namespace SharpGLTF
{
    class MeshFeaturesExtension : SchemaProcessor
    {
        private static string SchemaUri => Constants.VendorExtensionPath("EXT_mesh_features", "mesh.primitive.EXT_mesh_features.schema.json");
        
        public override IEnumerable<(string, SchemaType.Context)> Process()
        {
            var ctx = SchemaProcessing.LoadSchemaContext(SchemaUri);
            ctx.IgnoredByCodeEmitter("glTF Property");
            ctx.IgnoredByCodeEmitter("glTF Child of Root Property");
            ctx.IgnoredByCodeEmitter("Texture Info");

            ctx.FindClass("Feature ID Texture in EXT_mesh_features")
                .GetField("channels")
                .RemoveDefaultValue()
                .SetItemsRange(1);

            yield return ("ext.MeshFeatures.g", ctx);
        }

        public override void PrepareTypes(CodeGen.CSharpEmitter newEmitter, SchemaType.Context ctx)
        {
            newEmitter.SetRuntimeName("EXT_mesh_features glTF Mesh Primitive extension", "MeshFeatures");
            newEmitter.SetRuntimeName("Feature ID in EXT_mesh_features", "MeshFeatureFeatureId");
            newEmitter.SetRuntimeName("Feature ID Texture in EXT_mesh_features", "MeshFeatureFeatureIdTexture");
        }
    }
}