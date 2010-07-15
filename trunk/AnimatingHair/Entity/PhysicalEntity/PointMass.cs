using System;
using OpenTK;

namespace AnimatingHair.Entity.PhysicalEntity
{
    /// <summary>
    /// Represents a point with Mass, Force/Acceleration, Velocity and Position.
    /// Capable of integrating these according to newton's laws of motion.
    /// </summary>
    abstract class PointMass
    {
        public float Mass;
        public float MassInverse;
        public Vector3 Force;
        public Vector3 Acceleration;
        public Vector3 Velocity;
        public Vector3 Position;

        public PointMass( float mass )
        {
            Mass = mass;
            MassInverse = 1.0f / mass;
        }

        #region Euler integration methods

        public virtual void IntegrateForce( float timeStep )
        {
            Acceleration = Force * MassInverse;

            eulerIntegration( timeStep );
        }

        private void eulerIntegration( float timeStep )
        {
            Velocity += timeStep * Acceleration;
            Position += timeStep * Velocity;
        }

        #endregion

        #region Runge-Kutta members

        private readonly Vector3[] kVelocity = new Vector3[ 5 ];
        private readonly Vector3[] kPosition = new Vector3[ 5 ];
        private Vector3 startVelocity;
        private Vector3 startPosition;

        #endregion

        #region Runge-Kutta methods

        /// <summary>
        /// Runge-Kutta integration step. 
        /// 0 is the initialization step.
        /// 1-4 are the integration steps.
        /// 5 is the finalization step.
        /// </summary>
        /// <param name="stepNumber"></param>
        public virtual void RKStep( int stepNumber )
        {
            switch ( stepNumber )
            {
                case 0:
                    rkInit();
                    break;

                case 1:
                    rkStep1();
                    break;

                case 2:
                    rkStep2();
                    break;

                case 3:
                    rkStep3();
                    break;

                case 4:
                    rkStep4();
                    break;

                case 5:
                    rkFinalize();
                    break;
            }
        }

        private void rkInit()
        {
            startVelocity = Velocity;
            startPosition = Position;
        }

        private void rkStep1()
        {
            kVelocity[ 1 ] = Acceleration * Const.Instance.TimeStep;
            kPosition[ 1 ] = startVelocity * Const.Instance.TimeStep;

            // priprava na k2:
            Position = startPosition + kPosition[ 1 ] / 2;
            Velocity = startVelocity + kVelocity[ 1 ] / 2;
        }

        private void rkStep2()
        {
            kVelocity[ 2 ] = Acceleration * Const.Instance.TimeStep;
            kPosition[ 2 ] = (startVelocity + kVelocity[ 1 ] / 2) * Const.Instance.TimeStep;

            // priprava na k3:
            Position = startPosition + kPosition[ 2 ] / 2;
            Velocity = startVelocity + kVelocity[ 2 ] / 2;
        }

        private void rkStep3()
        {
            kVelocity[ 3 ] = Acceleration * Const.Instance.TimeStep;
            kPosition[ 3 ] = (startVelocity + kVelocity[ 2 ] / 2) * Const.Instance.TimeStep;

            // priprava na k4:
            Position = startPosition + kPosition[ 3 ];
            Velocity = startVelocity + kVelocity[ 3 ];
        }

        private void rkStep4()
        {
            kVelocity[ 4 ] = Acceleration * Const.Instance.TimeStep;
            kPosition[ 4 ] = (startVelocity + kVelocity[ 3 ]) * Const.Instance.TimeStep;
        }

        private void rkFinalize()
        {
            Velocity = startVelocity + (1.0f / 6.0f) * (kVelocity[ 1 ] + 2 * (kVelocity[ 2 ] + kVelocity[ 3 ]) + kVelocity[ 4 ]);

            // TODO: !!! trimming !!!
            if ( this is SPHParticle )
            {
                SPHParticle sphParticle = this as SPHParticle;
                float trim = 0.3f;
                float length = sphParticle.Velocity.Length;
                if ( length > trim )
                {
                    float factor = trim / length;
                    sphParticle.Velocity *= factor;
                }
            }

            Position = startPosition + (1.0f / 6.0f) * (kPosition[ 1 ] + 2 * (kPosition[ 2 ] + kPosition[ 3 ]) + kPosition[ 4 ]);
        }

        #endregion
    }
}