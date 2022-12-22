using System;

namespace MonoGameNezTest
{
    public class Enemy : Actor
    {
        public Enemy(){}
         

         public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            //--------------------------
            InitialState = ActorState.Idle;

            
        }

        public override void Update()
        {
            base.Update();
            //-------------------------
            
            
            
            
        }






    }

}