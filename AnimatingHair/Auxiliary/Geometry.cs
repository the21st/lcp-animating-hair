using System;
using OpenTK;

namespace AnimatingHair.Auxiliary
{
    static class Geometry
    {
        /// <summary>
        /// This function tests if the 3D point 'point' lies within an arbitrarily oriented cylinder 'cylinder'.
        /// Taken from http://www.flipcode.com/archives/Fast_Point-In-Cylinder_Test.shtml
        /// </summary>
        /// <returns>
        /// -1.0 if point is Outside the cylinder;
        /// distance squared from cylinder axis if point is inside.
        /// </returns>
        public static float PointCylinderTest( Cylinder cylinder, Vector3 point )
        {
            float dx, dy, dz;	// vector d  from line segment point 1 to point 2
            float pdx, pdy, pdz;	// vector pd from point 1 to test point
            float dot, dsq;

            dx = cylinder.Endpoint2.X - cylinder.Endpoint1.X;	// translate so cylinder.Endpoint1 is origin.  Make vector from
            dy = cylinder.Endpoint2.Y - cylinder.Endpoint1.Y;   // cylinder.Endpoint1 to cylinder.Endpoint2.  Need for this is easily eliminated
            dz = cylinder.Endpoint2.Z - cylinder.Endpoint1.Z;

            pdx = point.X - cylinder.Endpoint1.X;		// vector from cylinder.Endpoint1 to test point.
            pdy = point.Y - cylinder.Endpoint1.Y;
            pdz = point.Z - cylinder.Endpoint1.Z;

            // Dot the d and pd vectors to see if point lies behind the 
            // cylinder cap at cylinder.Endpoint1.X, cylinder.Endpoint1.Y, cylinder.Endpoint1.Z

            dot = pdx * dx + pdy * dy + pdz * dz;

            // If dot is less than zero the point is behind the cylinder.Endpoint1 cap.
            // If greater than the cylinder axis line segment length squared
            // then the point is Outside the other end cap at cylinder.Endpoint2.

            if ( dot < 0.0 || dot > cylinder.LengthSquared )
            {
                return (-1.0f);
            }
            else
            {
                // Point lies within the parallel caps, so find
                // distance squared from point to line, using the fact that sin^2 + cos^2 = 1
                // the dot = cos() * |d||pd|, and cross*cross = sin^2 * |d|^2 * |pd|^2
                // Carefull: '*' means mult for scalars and dotproduct for vectors
                // In short, where dist is pt distance to cyl axis: 
                // dist = sin( pd to d ) * |pd|
                // distsq = dsq = (1 - cos^2( pd to d)) * |pd|^2
                // dsq = ( 1 - (pd * d)^2 / (|pd|^2 * |d|^2) ) * |pd|^2
                // dsq = pd * pd - dot * dot / lengthsq
                //  where lengthsq is d*d or |d|^2 that is passed into this function 

                // distance squared to the cylinder axis:

                dsq = (pdx * pdx + pdy * pdy + pdz * pdz) - dot * dot / cylinder.LengthSquared;

                if ( dsq > cylinder.RadiusSquared )
                {
                    return -1.0f;
                }
                else
                {
                    return (float)Math.Sqrt( dsq );		// return distance squared to axis
                }
            }
        }

        /// <summary>
        /// Tests whether the point lies in the cylinder with "infinite radius"
        /// Taken from http://www.flipcode.com/archives/Fast_Point-In-Cylinder_Test.shtml
        /// </summary>
        public static bool PointInInfiniteRadiusCylinder( Cylinder cylinder, Vector3 point )
        {
            float dx, dy, dz;
            float pdx, pdy, pdz;
            float dot;

            dx = cylinder.Endpoint2.X - cylinder.Endpoint1.X;
            dy = cylinder.Endpoint2.Y - cylinder.Endpoint1.Y;
            dz = cylinder.Endpoint2.Z - cylinder.Endpoint1.Z;

            pdx = point.X - cylinder.Endpoint1.X;
            pdy = point.Y - cylinder.Endpoint1.Y;
            pdz = point.Z - cylinder.Endpoint1.Z;

            // Dot the d and pd vectors to see if point lies behind the cylinder cap at Endpoint1

            dot = pdx * dx + pdy * dy + pdz * dz;

            // If dot is less than zero the point is behind the Endpoint1 cap.
            // If greater than the cylinder axis line segment length squared
            // then the point is Outside the other end cap at Endpoint2.

            if ( dot < 0.0 || dot > cylinder.LengthSquared )
                return false;
            else
                return true;
        }

        /// <summary>
        /// Calculates a vector perpendicular to a line, passing through a point.
        /// </summary>
        /// <param name="lineStart">One of the points defining the line</param>
        /// <param name="lineEnd">Second point defining the line</param>
        /// <param name="point">The point</param>
        /// <returns>A vector from the point, normal to the line</returns>
        public static Vector3 LineToPointNormal( Vector3 lineStart, Vector3 lineEnd, Vector3 point )
        {
            Vector3 v1 = lineEnd - lineStart;
            Vector3 v2 = point - lineStart;

            float dotV1V2 = Vector3.Dot( v1, v2 );
            float dotV1V1 = Vector3.Dot( v1, v1 );

            if ( dotV1V1 < 0.00000001 )
                return new Vector3( 0, 0, 0 );

            float a = -dotV1V2 / dotV1V1;

            Vector3 perpendicularProjection = -a * v1;

            return (point - perpendicularProjection) - lineStart;
        }

        /// <summary>
        /// Rotates a vector angle radians around a specified axis.
        /// </summary>
        /// <param name="v">The vector to be rotated</param>
        /// <param name="axis">The axis of rotation</param>
        /// <param name="angle">The angle of rotation</param>
        /// <returns>The rotated vector</returns>
        public static Vector3 RotateVectorAroundAxis( Vector3 v, Vector3 axis, float angle )
        {
            axis.Normalize();
            float sin = (float)Math.Sin( angle );
            float cos = (float)Math.Cos( angle );

            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;

            Vector3 a, b, c;
            a.X = 1 + (1 - cos) * (x * x - 1);
            a.Y = z * sin + (1 - cos) * x * y;
            a.Z = -y * sin + (1 - cos) * x * z;

            b.X = -z * sin + (1 - cos) * x * y;
            b.Y = 1 + (1 - cos) * (y * y - 1);
            b.Z = x * sin + (1 - cos) * y * z;

            c.X = y * sin + (1 - cos) * x * z;
            c.Y = -x * sin + (1 - cos) * y * z;
            c.Z = 1 + (1 - cos) * (z * z - 1);

            Vector3 result;
            result.X = Vector3.Dot( v, a );
            result.Y = Vector3.Dot( v, b );
            result.Z = Vector3.Dot( v, c );

            return result;
        }

        public static bool LineSquareIntersection( Vector3 planeNormal, float planeD, Vector3 planeV0, Vector3 planeU, Vector3 planeV, Vector3 lineStart, Vector3 lineDirection )
        {
            float tt;

            if ( !linePlaneIntersection( planeNormal, planeD, lineStart, lineDirection, out tt ) )
                return false;

            if ( tt < 0 || tt > 1 )
                return false;

            Vector3 intersectionPoint = lineStart + tt * lineDirection;

            // is the intersectionPoint inside the square?
            float uu, uv, vv, wu, wv, d;
            Vector3 w;
            uu = Vector3.Dot( planeU, planeU );
            uv = Vector3.Dot( planeU, planeV );
            vv = Vector3.Dot( planeV, planeV );
            w = intersectionPoint - planeV0;
            wu = Vector3.Dot( w, planeU );
            wv = Vector3.Dot( w, planeV );
            d = uv * uv - uu * vv;

            // get and test parametric coords
            float s, t;
            s = (uv * wv - vv * wu) / d;
            if ( s < 0.0f || s > 1.0f ) // I is outside T
                return false;
            t = (uv * wu - uu * wv) / d;
            if ( t < 0.0f || t > 1.0f ) // I is outside T
                return false;

            // I is in T
            return true;
        }

        private static bool linePlaneIntersection( Vector3 planeNormal, float planeD, Vector3 lineStart, Vector3 lineDirection, out float t )
        {
            float denominator = Vector3.Dot( planeNormal, lineDirection );
            if ( Math.Abs( denominator ) < float.Epsilon )
            {
                t = 0;
                return false;
            }

            t = -(Vector3.Dot( planeNormal, lineStart ) + planeD) / denominator;
            return true;
        }
    }
}
