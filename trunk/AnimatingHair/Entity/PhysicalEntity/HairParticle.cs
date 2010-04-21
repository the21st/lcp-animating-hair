using System;
using System.Collections.Generic;
using OpenTK;

namespace AnimatingHair.Entity.PhysicalEntity
{
    class HairParticle : SPHParticle, IComparable<HairParticle>
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

        public readonly int ID;

        public HairParticle( int id )
        {
            NeighborsTip = new List<HairParticle>();
            NeighborsRoot = new List<HairParticle>();
            ID = id;
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

        public void ApplyAngularAcceleration( float angularAcceleration )
        {
            Vector3 pos = Position;
            pos.Y = 0;
            float r = pos.Length;
            Vector3 positionNormalized = pos / r;
            Vector3 direction = Vector3.Cross( positionNormalized, Vector3.UnitY );
            Force += -Mass * direction * angularAcceleration * r;
            Force += Mass * positionNormalized * angularAcceleration * angularAcceleration * r;
        }

        public override void RKStep( int stepNumber )
        {
            if (!IsRoot)
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