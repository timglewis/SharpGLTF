using System;
using System.Collections.Generic;
using System.Data;

namespace SharpGLTF.Schema2
{
    [System.Diagnostics.DebuggerDisplay("{_DebuggerDisplay(),nq}")]
    public partial class StructuralMetadata 
    {
        private ModelRoot _root;

        internal StructuralMetadata(ModelRoot root)
        {
            _root = root;
            _propertyTables ??= new List<StructuralMetadataPropertyTable>();
        }

        public StructuralMetadataSchema CreateSchema(string id, string name, string version, string description)
        {
            _schema = new StructuralMetadataSchema(id, name, version, description);

            return _schema;
        }

        public StructuralMetadataPropertyTable CreatePropertyTable(string name, string @class)
        {
            _propertyTables ??= new List<StructuralMetadataPropertyTable>();
            
            var table = new StructuralMetadataPropertyTable(_root, name, @class);

            _propertyTables.Add(table);

            return table;
        }

    }

    partial class ModelRoot
    {
        public StructuralMetadataSchema CreateSchema(string id, string name, string version, string description)
        {
            return UseExtension<StructuralMetadata>().CreateSchema(id, name, version, description);
        }

        public StructuralMetadataPropertyTable CreatePropertyTable(string name, string @class)
        {
            return UseExtension<StructuralMetadata>().CreatePropertyTable(name, @class);
        }

    }
}