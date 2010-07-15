﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimatingHair
{
    class RenderingOptions
    {
        public static readonly RenderingOptions Instance = new RenderingOptions();

        public RenderingOptions()
        {
            ShowHair = true;
            DebugHair = false;
            ShowConnections = false;

            AlphaTreshold = 0.15f;

            ShowBust = true;
            ShowMetaBust = false;

            LightCruising = false;
            LightCruiseSpeed = 0.002f;
            LightDistance = 7;
            LightIntensity = 1.5f;

            ShowVoxelGrid = false;
            OnlyShowOccupiedVoxels = true;

            ShowDebugAir = false;

            Near = 1;
            Far = 30;
        }

        public bool ShowHair { get; set; }
        public bool DebugHair { get; set; }
        public bool ShowConnections { get; set; }

        public bool DirectionalOpacity { get; set; }
        public float BillboardLength { get; set; }
        public float BillboardWidth { get; set; }
        public float AlphaTreshold { get; set; }
        public float DeepOpacityMapDistance { get; set; }

        public bool ShowBust { get; set; }
        public bool ShowMetaBust { get; set; }

        public bool LightCruising { get; set; }
        public float LightCruiseSpeed { get; set; }
        public float LightDistance { get; set; }
        public float LightIntensity { get; set; }

        public bool ShowVoxelGrid { get; set; }
        public bool OnlyShowOccupiedVoxels { get; set; }

        public bool ShowDebugAir { get; set; }

        public float Near { get; set; }
        public float Far { get; set; }
        public int RenderWidth { get; set; }
        public int RenderHeight { get; set; }
        public float AspectRatio { get; set; }
    }
}