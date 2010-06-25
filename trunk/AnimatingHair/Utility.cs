using System;
using System.Drawing;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using AnimatingHair.Rendering;
using System.IO;
using System.Collections.Generic;

namespace AnimatingHair
{
    /// <summary>
    /// Static class providing some methods such as GLU routines, 
    /// configuration loading, saving or .obj parsing.
    /// </summary>
    static class Utility
    {
        /// <summary>
        /// Calls the gluSphere routine.
        /// </summary>
        public static void DrawSphere( float radius, int lats, int longs )
        {
            IntPtr quadric = GLU.NewQuadric();
            GLU.Sphere( quadric, radius, lats, longs );
            GLU.DeleteQuadric( quadric );
        }

        /// <summary>
        /// Calls the gluCylinder routine.
        /// </summary>
        public static void RenderCylinder( float x1, float y1, float z1, float x2, float y2, float z2, float radius, int subdivisions )
        {
            float vx = x2 - x1;
            float vy = y2 - y1;
            float vz = z2 - z1;

            IntPtr quadric = GLU.NewQuadric();

            //handle the degenerate case of z1 == z2 with an approximation
            if ( vz == 0 )
                vz = .00000001f;

            float v = (float)Math.Sqrt( vx * vx + vy * vy + vz * vz );
            float ax = 57.2957795f * (float)Math.Acos( vz / v );
            if ( vz < 0.0 )
                ax = -ax;
            float rx = -vy * vz;
            float ry = vx * vz;
            GL.PushMatrix();

            //draw the cylinder body
            GL.Translate( x1, y1, z1 );
            GL.Rotate( ax, rx, ry, 0.0 );
            GLU.Cylinder( quadric, radius, radius, v, subdivisions, 1 );

            //draw the first cap
            GLU.Disk( quadric, 0, radius, subdivisions, 1 );
            GL.Translate( 0, 0, v );

            //draw the second cap
            GLU.Disk( quadric, 0, radius, subdivisions, 1 );
            GL.PopMatrix();
        }

        /// <summary>
        /// Loads a 3D mesh from an .OBJ file.
        /// </summary>
        public static TriangleMesh LoadOBJ( string path )
        {
            TriangleMesh result = new TriangleMesh( true );

            StreamReader objFile = new StreamReader( path );

            List<Vector3> vertexList = new List<Vector3>();
            List<Vector3> normalList = new List<Vector3>();
            List<Vector2> texCoordList = new List<Vector2>();
            List<TriangleIndex> triangleIndices = new List<TriangleIndex>();

            while ( !objFile.EndOfStream )
            {
                string line = objFile.ReadLine();

                string[] lineParts = line.Split( ' ' );

                switch ( lineParts[ 0 ] )
                {
                    case "v":
                        vertexList.Add( new Vector3(
                            float.Parse( lineParts[ 1 ] ),
                            float.Parse( lineParts[ 2 ] ),
                            float.Parse( lineParts[ 3 ] ) ) );
                        break;
                    case "vn":
                        normalList.Add( new Vector3(
                            float.Parse( lineParts[ 1 ] ),
                            float.Parse( lineParts[ 2 ] ),
                            float.Parse( lineParts[ 3 ] ) ) );
                        break;
                    case "vt":
                        texCoordList.Add( new Vector2(
                            float.Parse( lineParts[ 1 ] ),
                            float.Parse( lineParts[ 2 ] ) ) );
                        break;
                    case "f":
                        string[] indices1s = lineParts[ 1 ].Split( '/' );
                        int[] indices1 = {
                                             int.Parse( indices1s[ 0 ] ) - 1,
                                             int.Parse( indices1s[ 1 ] ) - 1,
                                             int.Parse( indices1s[ 2 ] ) - 1
                                         };
                        string[] indices2s = lineParts[ 2 ].Split( '/' );
                        int[] indices2 = {
                                             int.Parse( indices2s[ 0 ] ) - 1,
                                             int.Parse( indices2s[ 1 ] ) - 1,
                                             int.Parse( indices2s[ 2 ] ) - 1
                                         };
                        string[] indices3s = lineParts[ 3 ].Split( '/' );
                        int[] indices3 = {
                                             int.Parse( indices3s[ 0 ] ) - 1,
                                             int.Parse( indices3s[ 1 ] ) - 1,
                                             int.Parse( indices3s[ 2 ] ) - 1
                                         };
                        triangleIndices.Add(
                            new TriangleIndex(
                                new VertexIndex( indices1 ),
                                new VertexIndex( indices2 ),
                                new VertexIndex( indices3 )
                                ) );
                        break;
                }
            }

            result.Vertices = vertexList.ToArray();
            result.Normals = normalList.ToArray();
            result.TexCoords = texCoordList.ToArray();
            result.TriangleIndices = triangleIndices.ToArray();

            return result;
        }

        /// <summary>
        /// Creates, compiles and links a vertex shader and a fragment shader.
        /// </summary>
        public static void CreateShaders( string vs, string fs, out int program )
        {
            int statusCode, vertexObject, fragmentObject;
            string info;

            vertexObject = GL.CreateShader( ShaderType.VertexShader );
            fragmentObject = GL.CreateShader( ShaderType.FragmentShader );

            // Compile vertex shader
            GL.ShaderSource( vertexObject, vs );
            GL.CompileShader( vertexObject );
            GL.GetShaderInfoLog( vertexObject, out info );
            GL.GetShader( vertexObject, ShaderParameter.CompileStatus, out statusCode );

            if ( statusCode != 1 )
                throw new ApplicationException( info );

            // Compile fragment shader
            GL.ShaderSource( fragmentObject, fs );
            GL.CompileShader( fragmentObject );
            GL.GetShaderInfoLog( fragmentObject, out info );
            GL.GetShader( fragmentObject, ShaderParameter.CompileStatus, out statusCode );

            if ( statusCode != 1 )
                throw new ApplicationException( info );

            program = GL.CreateProgram();
            GL.AttachShader( program, fragmentObject );
            GL.AttachShader( program, vertexObject );

            GL.LinkProgram( program );
        }

        /// <summary>
        /// Creates, compiles and links a fragment shader.
        /// </summary>
        public static void CreateShaders( string fs, out int program )
        {
            int statusCode, fragmentObject;
            string info;

            fragmentObject = GL.CreateShader( ShaderType.FragmentShader );

            // Compile fragment shader
            GL.ShaderSource( fragmentObject, fs );
            GL.CompileShader( fragmentObject );
            GL.GetShaderInfoLog( fragmentObject, out info );
            GL.GetShader( fragmentObject, ShaderParameter.CompileStatus, out statusCode );

            if ( statusCode != 1 )
                throw new ApplicationException( info );

            program = GL.CreateProgram();
            GL.AttachShader( program, fragmentObject );

            GL.LinkProgram( program );
        }

        /// <summary>
        /// Links an image from the specified file to an OpenGL texture
        /// and returns the reference.
        /// </summary>
        public static int UploadTexture( string filename )
        {
            int texture = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, texture );

            Bitmap bmp;

            try
            {
                StreamReader sr = new StreamReader( filename );
                bmp = new Bitmap( filename );
            }
            catch ( ArgumentException e )
            {
                throw new FileNotFoundException( "Could not find file '" + filename + "'.", e );
            }

            System.Drawing.Imaging.BitmapData data = bmp.LockBits( new Rectangle( 0, 0, bmp.Width, bmp.Height ),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb );

            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0,
                PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0 );

            bmp.UnlockBits( data );

            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear );

            return texture;
        }

        /// <summary>
        /// Saves the simulation variables to the specified file.
        /// </summary>
        public static void SaveConfiguration( string path )
        {
            StreamWriter streamWriter = new StreamWriter( path );

            string s = Const.ToString();

            streamWriter.Write( s );

            streamWriter.Close();
        }

        /// <summary>
        /// Loads the simulation variables from the specified file.
        /// </summary>
        public static void LoadConfiguration( string path )
        {
            StreamReader streamReader = new StreamReader( path );

            Const.FromString( streamReader.ReadToEnd() );

            streamReader.Close();
        }

        /// <summary>
        /// provides DLL links to the GLU.
        /// </summary>
        static class GLU
        {
            [DllImport( "glu32", EntryPoint = "gluNewQuadric" )]
            public static extern IntPtr NewQuadric();

            [DllImport( "glu32", EntryPoint = "gluSphere" )]
            public static extern void Sphere( IntPtr quadric, double radius, int slices, int stacks );

            [DllImport( "glu32", EntryPoint = "gluCylinder" )]
            public static extern void Cylinder( IntPtr quad, double baseRadius, double topRadius, double height, int slices, int stacks );

            [DllImport( "glu32", EntryPoint = "gluDisk" )]
            public static extern void Disk( IntPtr quad, double innerRadius, double outerRadius, int slices, int loops );

            [DllImport( "glu32", EntryPoint = "gluDeleteQuadric" )]
            public static extern void DeleteQuadric( IntPtr quadric );
        }
    }
}
