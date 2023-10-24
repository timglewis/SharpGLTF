using System;
using System.Collections.Generic;
using System.Linq;

using SharpGLTF.Collections;

namespace SharpGLTF.Schema2
{
    [System.Diagnostics.DebuggerDisplay("{_DebuggerDisplay(),nq}")]
    public partial class MeshFeatures 
    {
        private MeshPrimitive meshPrimitive;
        
        internal MeshFeatures(MeshPrimitive meshPrimitive)
        {
            this.meshPrimitive = meshPrimitive;
        }

        public Accessor FeatureIds
        {
            get
            {
                return _featureIds.HasValue
                    ? meshPrimitive.LogicalParent.LogicalParent.LogicalAccessors[_featureIds.Value]
                    : null;
            }
            set
            {
                if (value == null) { _indices = null; return; }
                
                meshPrimitive.

                _ValidateAccessor(meshPrimitive.LogicalParent.LogicalParent, value);

                _indices = value.LogicalIndex;
            }
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

    partial class MeshPrimitive
    {
        /// <summary>
        /// Sets Cesium outline vertex indices
        /// </summary>
        /// <param name="outlines">the list of vertex indices.</param>
        /// <param name="accessorName">the name of the accessor to be created.</param>
        public void SetMeshFeatures(IReadOnlyList<ushort> featureIds, string accessorName = "Mesh Features")
        {
            Guard.NotNull(featureIds, nameof(featureIds));

            // create and fill data

            var dstData = new Byte[featureIds.Count];
            var dstArray = new Memory.IntegerArray(dstData, IndexEncodingType.UNSIGNED_BYTE);
            for (int i = 0; i < featureIds.Count; ++i) { dstArray[i] = featureIds[i]; }

            var model = this.LogicalParent.LogicalParent;

            var bview = model.UseBufferView(dstData);
            var accessor = model.CreateAccessor(accessorName);

            accessor.SetData(bview, 0, dstArray.Count, DimensionType.SCALAR, EncodingType.UNSIGNED_BYTE, false);

            SetMeshFeatures(accessor);
        }

        public void SetMeshFeatures(Accessor accessor)
        {
            if (accessor == null) { RemoveExtensions<MeshFeatures>(); return; }

            MeshFeatures._ValidateAccessor(this.LogicalParent.LogicalParent, accessor);

            var ext = UseExtension<MeshFeatures>();
            ext.Indices = accessor;
        }
    }

}
