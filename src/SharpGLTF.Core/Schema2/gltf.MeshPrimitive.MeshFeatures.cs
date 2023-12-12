using System.Collections.Generic;

namespace SharpGLTF.Schema2
{
    [System.Diagnostics.DebuggerDisplay("{_DebuggerDisplay(),nq}")]
    public partial class MeshFeatures 
    {
        #region lifecycle

        internal MeshFeatures(MeshPrimitive meshPrimitive)
        {
            _featureIds = new List<MeshFeatureFeatureId>();
        }

        #endregion

        public void SetFeatureIds(int attribute, int featureCount, int propertyTable) 
        {
            _featureIds.Add(new MeshFeatureFeatureId(attribute, featureCount, propertyTable));
        }

        internal static void _ValidateAccessor(ModelRoot model, Accessor accessor)
        {
            Guard.NotNull(accessor, nameof(accessor));
            Guard.MustShareLogicalParent(model, "this", accessor, nameof(accessor));
            Guard.IsTrue(accessor.Encoding == EncodingType.UNSIGNED_BYTE, nameof(accessor));
            Guard.IsTrue(accessor.Dimensions == DimensionType.SCALAR, nameof(accessor));
            Guard.IsFalse(accessor.Normalized, nameof(accessor));
        }

    }

	partial class MeshFeatureFeatureId
	{
        public MeshFeatureFeatureId() { }
        
        public MeshFeatureFeatureId(int attribute, int featureCount, int propertyTable)
        {
            _attribute = attribute;
            _featureCount = featureCount;
            _propertyTable = propertyTable;
        }
    }

    partial class MeshPrimitive
    {
        /// <summary>
        /// Creates a vertex accessor for Mesh Features
        /// </summary>
        public void SetMeshFeatures(int attribute, byte featureId, int propertyTable)
        {
            Guard.MustBeGreaterThanOrEqualTo(attribute, 0, nameof(attribute));

            var model = this.LogicalParent.LogicalParent;

            int vertexCount = GetVertexAccessor("POSITION").Count;

            var bview = model.CreateBufferView(4 * vertexCount, 4, BufferMode.ARRAY_BUFFER);

            var accessor = model.CreateAccessor("Mesh Features");

            // create and fill data
            var dstArray = new Memory.IntegerArray(bview.Content, 0, vertexCount, IndexEncodingType.UNSIGNED_BYTE);
            for (int i = 0; i < vertexCount; ++i) { dstArray[i] = featureId; }

            accessor.SetData(bview, 0, vertexCount, DimensionType.SCALAR, EncodingType.UNSIGNED_BYTE, false);

            SetVertexAccessor($"_FEATURE_ID_{attribute}", accessor);

            var ext = UseExtension<MeshFeatures>();
            
            ext.SetFeatureIds(attribute, 1, propertyTable);
        }
    }
}
