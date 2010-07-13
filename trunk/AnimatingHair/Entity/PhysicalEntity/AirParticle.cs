namespace AnimatingHair.Entity.PhysicalEntity
{
    class AirParticle : SPHParticle
    {
        public int ID;

        public AirParticle( int id, float mass )
            : base( mass )
        {
            ID = id;
        }
    }
}
