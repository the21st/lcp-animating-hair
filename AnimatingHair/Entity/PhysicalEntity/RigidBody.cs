namespace AnimatingHair.Entity.PhysicalEntity
{
    /// <summary>
    /// A rigid body rotated only along the (vertical) Y-axis.
    /// </summary>
    class RigidBody : PointMass
    {
        public float AngularAcceleration;
        public float AngularVelocity;
        public float Angle;

        public RigidBody( float mass ) : base( mass ) { }

        #region Runge-Kutta members

        private readonly float[] kAngularVelocity = new float[ 5 ];
        private readonly float[] kAngle = new float[ 5 ];
        private float startAngularVelocity;
        private float startAngle;

        #endregion

        #region Runge-Kutta methods

        public override void RKStep( int stepNumber )
        {
            base.RKStep( stepNumber );

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
            startAngularVelocity = AngularVelocity;
            startAngle = Angle;
        }

        private void rkStep1()
        {
            kAngularVelocity[ 1 ] = AngularAcceleration * Const.Instance.TimeStep;
            kAngle[ 1 ] = startAngularVelocity * Const.Instance.TimeStep;

            // priprava na k2:
            Angle = startAngle + kAngle[ 1 ] / 2;
            AngularVelocity = startAngularVelocity + kAngularVelocity[ 1 ] / 2;
        }

        private void rkStep2()
        {
            kAngularVelocity[ 2 ] = AngularAcceleration * Const.Instance.TimeStep;
            kAngle[ 2 ] = (startAngularVelocity + kAngularVelocity[ 1 ] / 2) * Const.Instance.TimeStep;

            // priprava na k3:
            Angle = startAngle + kAngle[ 2 ] / 2;
            AngularVelocity = startAngularVelocity + kAngularVelocity[ 2 ] / 2;
        }

        private void rkStep3()
        {
            kAngularVelocity[ 3 ] = AngularAcceleration * Const.Instance.TimeStep;
            kAngle[ 3 ] = (startAngularVelocity + kAngularVelocity[ 2 ] / 2) * Const.Instance.TimeStep;

            // priprava na k3:
            Angle = startAngle + kAngle[ 3 ];
            AngularVelocity = startAngularVelocity + kAngularVelocity[ 3 ];
        }

        private void rkStep4()
        {
            kAngularVelocity[ 4 ] = AngularAcceleration * Const.Instance.TimeStep;
            kAngle[ 4 ] = (startAngularVelocity + kAngularVelocity[ 3 ]) * Const.Instance.TimeStep;
        }

        private void rkFinalize()
        {
            AngularVelocity = startAngularVelocity + (1.0f / 6.0f) * (kAngularVelocity[ 1 ] + 2 * (kAngularVelocity[ 2 ] + kAngularVelocity[ 3 ]) + kAngularVelocity[ 4 ]);
            Angle = startAngle + (1.0f / 6.0f) * (kAngle[ 1 ] + 2 * (kAngle[ 2 ] + kAngle[ 3 ]) + kAngle[ 4 ]);
        }

        #endregion
    }
}
