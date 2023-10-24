﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using NJsonSchema.References;

using JSONSCHEMA = NJsonSchema.JsonSchema;

namespace SharpGLTF
{
    using CodeGen;
    using SchemaReflection;    

    partial class Program
    {
        #region MAIN

        static void Main(string[] args)
        {
            SchemaDownload.Syncronize(Constants.RemoteSchemaRepo, Constants.RemoteSchemaBranch, Constants.LocalRepoDirectory);

            var processors = new List<SchemaProcessor>();

            // ---------------------------------------------- Add Main Schema

            processors.Add(new MainSchemaProcessor());

            // ---------------------------------------------- Add extensions            

            // material extensions       
            processors.Add(new UnlitExtension());
            processors.Add(new IorExtension());
            processors.Add(new SheenExtension());
            processors.Add(new VolumeExtension());
            processors.Add(new SpecularExtension());
            processors.Add(new ClearCoatExtension());
            processors.Add(new IridescenceExtension());
            processors.Add(new TransmissionExtension());
            processors.Add(new EmissiveStrengthExtension());
            processors.Add(new SpecularGlossinessExtension());

            // cesium outlines
            processors.Add(new CesiumPrimitiveOutlineExtension());

            // lights
            processors.Add(new LightsPunctualExtension());

            // gpu mesh instancing
            processors.Add(new MeshGpuInstancingExtension());

            // mesh features & structural metadata
            processors.Add(new MeshFeaturesExtension());
            processors.Add(new StructuralMetadataExtension());
            
            // textures
            processors.Add(new TextureTransformExtension());
            processors.Add(new TextureDDSExtension());
            processors.Add(new TextureWebpExtension());
            processors.Add(new TextureKtx2Extension());

            processors.Add(new AgiArticulationsExtension());
            processors.Add(new AgiStkMetadataExtension());

            // ----------------------------------------------  process all files

            var processes = processors.SelectMany(item => item.Process());

            foreach (var (targetFileName, schema) in processes)
            {
                System.Console.WriteLine($"Emitting {targetFileName}...");

                SchemaProcessing.EmitCodeFromSchema(targetFileName, schema, processors);
            }
        }

        #endregion     
    }    
}
