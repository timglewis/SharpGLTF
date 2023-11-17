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

        public void SetFeatureIds(int featureCount) 
        {
            _featureIds.Add(new MeshFeatureFeatureId(featureCount));
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
        
        public MeshFeatureFeatureId(int featureCount)
        {
            _featureCount = featureCount;
        }
    }

    partial class MeshPrimitive
    {
        /// <summary>
        /// Sets Cesium outline vertex indices
        /// </summary>
        /// <param name="outlines">the list of vertex indices.</param>
        /// <param name="accessorName">the name of the accessor to be created.</param>
        public void SetMeshFeatures(int attribute, IReadOnlyList<uint> featureIds, string accessorName = "Mesh Features")
        {
            Guard.NotNull(featureIds, nameof(featureIds));
            Guard.MustBeGreaterThanOrEqualTo(attribute, 0, nameof(attribute));

            var model = this.LogicalParent.LogicalParent;

            var bview = model.CreateBufferView(4 * featureIds.Count, 4, BufferMode.ARRAY_BUFFER);

            var accessor = model.CreateAccessor(accessorName);

            // create and fill data
            var dstArray = new Memory.IntegerArray(bview.Content, 0, featureIds.Count, IndexEncodingType.UNSIGNED_BYTE);
            for (int i = 0; i < featureIds.Count; ++i) { dstArray[i] = featureIds[i]; }

            accessor.SetData(bview, 0, featureIds.Count, DimensionType.SCALAR, EncodingType.UNSIGNED_BYTE, false);

            SetVertexAccessor($"_FEATURE_ID_{attribute}", accessor);

            var ext = UseExtension<MeshFeatures>();
            
            ext.SetFeatureIds(featureIds.Count);
        }
    }
}
