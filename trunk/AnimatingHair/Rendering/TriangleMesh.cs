using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AnimatingHair.Rendering
{
    /// <summary>
    /// An object containing vertex, normal and texture data of a 3D mesh.
    /// </summary>
    class TriangleMesh
    {
        // vertex, normal and texCoord data
        public Vector3[] Vertices;
        public Vector3[] Normals;
        public Vector2[] TexCoords;

        // the polygon (only triangles) data. Is actually an indexing of the vertex data.
        public TriangleIndex[] TriangleIndices;

        #region Display List

        private readonly bool useDisplayList;
        private int displayListHandle = 0;

        #endregion Display List

        public TriangleMesh( bool useDisplayList )
        {
            this.useDisplayList = useDisplayList;
            Vertices = null;
            Normals = null;
            TexCoords = null;
            TriangleIndices = null;
        }

        /// <summary>
        /// Does not touch any state/matrices. Does call Begin/End and Vertex&Co.
        /// Creates and compiles a display list if not present yet. Requires an OpenGL context.
        /// </summary>
        public void Draw()
        {
            if ( !useDisplayList )
                drawImmediateMode();
            else
                if ( displayListHandle == 0 )
                {
                    displayListHandle = GL.GenLists( 1 );
                    GL.NewList( displayListHandle, ListMode.CompileAndExecute );
                    drawImmediateMode();
                    GL.EndList();
                }
                else
                    GL.CallList( displayListHandle );
        }

        private void drawImmediateMode()
        {
            if ( wireframe )
            {
                for ( int i = 0; i < TriangleIndices.Length; i++ )
                {
                    TriangleIndex triangle = TriangleIndices[ i ];
                    GL.Begin( BeginMode.LineLoop );
                    {
                        for ( int j = 0; j < 3; j++ )
                        {
                            GL.TexCoord2( TexCoords[ triangle[ j ].TexCoord ] );
                            GL.Normal3( Normals[ triangle[ j ].Normal ] );
                            GL.Vertex3( Vertices[ triangle[ j ].Vertex ] );
                        }
                    }
                    GL.End();
                }
            }
            else
            {
                GL.Begin( BeginMode.Triangles );
                {
                    for ( int i = 0; i < TriangleIndices.Length; i++ )
                    {
                        TriangleIndex triangle = TriangleIndices[ i ];
                        for ( int j = 0; j < 3; j++ )
                        {
                            GL.TexCoord2( TexCoords[ triangle[ j ].TexCoord ] );
                            GL.Normal3( Normals[ triangle[ j ].Normal ] );
                            GL.Vertex3( Vertices[ triangle[ j ].Vertex ] );
                        }
                    }
                }
                GL.End();
            }
        }
    }

    internal struct VertexIndex
    {
        public readonly int Vertex;
        public readonly int Normal;
        public readonly int TexCoord;

        public VertexIndex( int[] indices )
        {
            Vertex = indices[ 0 ];
            TexCoord = indices[ 1 ];
            Normal = indices[ 2 ];
        }
    }

    internal struct TriangleIndex
    {
        readonly VertexIndex[] vertexIndices;

        public VertexIndex this[ int i ]
        {
            get
            {
                return vertexIndices[ i ];
            }
        }

        public TriangleIndex( VertexIndex vi1, VertexIndex vi2, VertexIndex vi3 )
        {
            vertexIndices = new VertexIndex[ 3 ];
            vertexIndices[ 0 ] = vi1;
            vertexIndices[ 1 ] = vi2;
            vertexIndices[ 2 ] = vi3;
        }
    }
}
