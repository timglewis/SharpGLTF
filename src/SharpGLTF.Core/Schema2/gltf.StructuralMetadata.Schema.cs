using System;
using System.Collections.Generic;
using System.Data;

namespace SharpGLTF.Schema2
{
    public partial class StructuralMetadataClassProperty
    {
        public StructuralMetadataClassProperty() {}

        public StructuralMetadataClassProperty(ElementType type, string name, string description)
        {
            _type = type;
            _name = name;
            _description = description;
        }

        public StructuralMetadataClassProperty(ElementType type, ComponentType componentType, string name, string description)
        {
            _type = type;
            _componentType = componentType;
            _name = name;
            _description = description;
        }
    }

    public partial class StructuralMetadataClass
    {
        public StructuralMetadataClass() {
            _properties = new Dictionary<String, StructuralMetadataClassProperty>();
        }

        public StructuralMetadataClass(string name, string description)
        {
            _name = name;
            _description = description;
            _properties = new Dictionary<String, StructuralMetadataClassProperty>();
        }

        public StructuralMetadataClass WithInt8(string name, string description) => WithComponentProperty(ElementType.SCALAR, ComponentType.INT8, name, description);
        public StructuralMetadataClass WithInt8(string name, string description, sbyte defaultValue) => WithComponentProperty(ElementType.SCALAR, ComponentType.INT8, name, description, defaultValue.ToString());
        public StructuralMetadataClass WithUint8(string name, string description) => WithComponentProperty(ElementType.SCALAR, ComponentType.UINT8, name, description);
        public StructuralMetadataClass WithUint8(string name, string description, byte defaultValue) => WithComponentProperty(ElementType.SCALAR, ComponentType.UINT8, name, description, defaultValue.ToString());
        public StructuralMetadataClass WithInt16(string name, string description) => WithComponentProperty(ElementType.SCALAR, ComponentType.INT16, name, description);
        public StructuralMetadataClass WithInt16(string name, string description, short defaultValue) => WithComponentProperty(ElementType.SCALAR, ComponentType.INT16, name, description, defaultValue.ToString());
        public StructuralMetadataClass WithUint16(string name, string description) => WithComponentProperty(ElementType.SCALAR, ComponentType.UINT16, name, description);
        public StructuralMetadataClass WithUint16(string name, string description, ushort defaultValue) => WithComponentProperty(ElementType.SCALAR, ComponentType.UINT16, name, description, defaultValue.ToString());
        public StructuralMetadataClass WithInt32(string name, string description) => WithComponentProperty(ElementType.SCALAR, ComponentType.INT32, name, description);
        public StructuralMetadataClass WithInt32(string name, string description, int defaultValue) => WithComponentProperty(ElementType.SCALAR, ComponentType.INT32, name, description, defaultValue.ToString());
        public StructuralMetadataClass WithUint32(string name, string description) => WithComponentProperty(ElementType.SCALAR, ComponentType.UINT32, name, description);
        public StructuralMetadataClass WithUint32(string name, string description, uint defaultValue) => WithComponentProperty(ElementType.SCALAR, ComponentType.UINT32, name, description, defaultValue.ToString());
        public StructuralMetadataClass WithString(string name, string description) => WithProperty(ElementType.STRING, name, description);
        public StructuralMetadataClass WithString(string name, string description, string defaultValue) => WithProperty(ElementType.STRING, name, description, defaultValue);
        public StructuralMetadataClass WithBoolean(string name, string description) => WithProperty(ElementType.BOOLEAN, name, description);
        public StructuralMetadataClass WithBoolean(string name, string description, bool defaultValue) => WithProperty(ElementType.BOOLEAN, name, description, defaultValue.ToString());

        public StructuralMetadataClass WithProperty(ElementType elementType, string name, string description, string defaultValue = null)
        {
            _properties ??= new Dictionary<String, StructuralMetadataClassProperty>();

            _properties[name] = new StructuralMetadataClassProperty(elementType, name, description);

            return this;
        }

        public StructuralMetadataClass WithComponentProperty(ElementType elementType, ComponentType componentType, string name, string description, string defaultValue = null)
        {
            _properties ??= new Dictionary<String, StructuralMetadataClassProperty>();

            _properties[name] = new StructuralMetadataClassProperty(elementType, componentType, name, description);

            return this;
        }
    }

    public partial class StructuralMetadataSchema
    {
        internal StructuralMetadataSchema() {
            _classes = new Dictionary<String, StructuralMetadataClass>();
            _enums = new Dictionary<String, StructuralMetadataEnum>();
        }

        public StructuralMetadataSchema(string id, string name, string version, string description)
        {
            _id = id;
            _name = name;
            _version = version;
            _description = description;
            _classes = new Dictionary<String, StructuralMetadataClass>();
            _enums = new Dictionary<String, StructuralMetadataEnum>();
        }

        public void AddClass(string name, StructuralMetadataClass definition) 
        {
            _classes ??= new Dictionary<String, StructuralMetadataClass>();
            
            _classes.Add(name, definition);
        }
    }

}