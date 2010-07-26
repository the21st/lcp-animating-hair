using System;
using System.Collections.Generic;
using OpenTK;

namespace AnimatingHair.Entity.PhysicalEntity
{
    class HairParticle : SPHParticle
    {
        public float Area { get; set; }
        public Vector3 Direction;

        public Vector3 RootDirection;
        public float U;
        public float V;
        public float S;

        public float DistanceFromCamera;

        public bool IsRoot { get { return S < 0; } }

        public readonly List<HairParticle> NeighborsRoot;
        public readonly List<HairParticle> NeighborsTip;

        public HairParticle( int id, float mass )
            : base( id, mass )
        {
            NeighborsTip = new List<HairParticle>();
            NeighborsRoot = new List<HairParticle>();
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        public override void IntegrateForce( float timeStep )
        {
            if ( !IsRoot )
            {
                base.IntegrateForce( timeStep );
            }
        }

        public void ApplyAngularAcceleration( float angularAcceleration, float angularVelocity )
        {
            Vector3 pos = Vector3.Zero;
            pos.X = Position.X;
            pos.Z = Position.Z;
            Vector3 direction = Vector3.Cross( Vector3.UnitY, pos );
            Force += -Mass * angularAcceleration * direction;
            Force += Mass * pos * angularVelocity * angularVelocity;
        }

        public override void RKStep( int stepNumber )
        {
            if ( !IsRoot )
                base.RKStep( stepNumber );
        }

        #region IComparable<HairParticle> Members

        public int CompareTo( HairParticle other )
        {
            return ID.CompareTo( other.ID );
        }

        #endregion
    }
}