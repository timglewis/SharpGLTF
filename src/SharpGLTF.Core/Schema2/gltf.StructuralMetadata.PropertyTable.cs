using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Net.Http;

namespace SharpGLTF.Schema2
{
    public partial class StructuralMetadataPropertyTable 
    {
        public ModelRoot _root;
        public int Index { get; set; }

        public StructuralMetadataPropertyTable()
        {
            _properties = new Dictionary<String, StructuralMetadataPropertyTableProperty>();
        }

        public StructuralMetadataPropertyTable(ModelRoot root, string name, string @class)
        {
            _root = root;
            _name = name;
            _class = @class;
            _properties = new Dictionary<String, StructuralMetadataPropertyTableProperty>();
            _count = 0;
        }

        public void CreateProperty<T>(string name, List<T> entries) where T : struct
        {
            int intBufLen = Marshal.SizeOf(typeof(T)) * entries.Count;
            byte[] byteArray = new byte[intBufLen];

            System.Buffer.BlockCopy(entries.ToArray(), 0, byteArray, 0, byteArray.Length);

            BufferView intBuf = _root.UseBufferView(byteArray);
            intBuf.Name = name + ".values";

            StructuralMetadataPropertyTableProperty property = new StructuralMetadataPropertyTableProperty();
            
            property.AddProperty(intBuf.LogicalIndex);

            _AddProperty(name, property);
        }

        public void CreateStringProperty(string name, List<string> entries)
        {
            // Create the buffer to house the offset array and the string buffer
            int stringBufLen = 0;
            for (int i = 0; i < entries.Count; ++i) 
            {
                stringBufLen += Encoding.UTF8.GetByteCount(entries[i]);
            }
            int offsetBufLen = sizeof(UInt16) * entries.Count;
            int bufLen = stringBufLen + offsetBufLen;

            Buffer buffer = _root.CreateBuffer(bufLen);

            // Create the offset and string buffer views
            BufferView offsetBuf = _root.UseBufferView(buffer, 0, offsetBufLen);
            offsetBuf.Name = name + ".offsets";
            BufferView stringBuf = _root.UseBufferView(buffer, offsetBufLen, stringBufLen);
            stringBuf.Name = name + ".values";

            // Copy the string offsets and content into the buffer
            Memory.IntegerArray offsetArray = new Memory.IntegerArray(offsetBuf.Content, 0, entries.Count, IndexEncodingType.UNSIGNED_SHORT);
            int offset = 0;
            for (int i = 0; i < entries.Count; ++i) 
            {
                offsetArray[i] = (uint)offset;
                Array.Copy(Encoding.UTF8.GetBytes(entries[i]), 0, stringBuf.Content.Array, offset + stringBuf.Content.Offset, Encoding.UTF8.GetByteCount(entries[i]));
                offset += Encoding.UTF8.GetByteCount(entries[i]);
            }

            StructuralMetadataPropertyTableProperty property = new StructuralMetadataPropertyTableProperty();
            property.AddStringProperty(stringBuf.LogicalIndex, offsetBuf.LogicalIndex, OffsetType.UINT16);

            _AddProperty(name, property);
        }

/*
        public void CreateInt32Property(string name, List<Int32> entries)
        {
            int intBufLen = sizeof(Int32) * entries.Count;

            BufferView intBuf = _root.UseBufferView(entries.SelectMany(BitConverter.GetBytes).ToArray(), 0, intBufLen);
            intBuf.Name = name + ".values";

            StructuralMetadataPropertyTableProperty property = new StructuralMetadataPropertyTableProperty();
            
            property.AddProperty(intBuf.LogicalIndex);

            _AddProperty(name, property);
        }
*/
        private void _AddProperty(string propertyKey, StructuralMetadataPropertyTableProperty property)
        {
            _properties ??= new Dictionary<String, StructuralMetadataPropertyTableProperty>();

            _properties.Add(propertyKey, property);
            _count++;
        }

    }

    public partial class StructuralMetadataPropertyTableProperty 
    {
        public StructuralMetadataPropertyTableProperty() {}

        public void AddProperty(int values)
        {
            _values = values;
        }

        public void AddStringProperty(int values, int offsets, OffsetType offsetType)
        {
            _values = values;
            _stringOffsets = offsets;
            _stringOffsetType = offsetType;
        }

        public void AddArrayProperty(int values, int offsets, OffsetType offsetType)
        {
            _values = values;
            _arrayOffsets = offsets;
            _arrayOffsetType = offsetType;
        }
    }

}