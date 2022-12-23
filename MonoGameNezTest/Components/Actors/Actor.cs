using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.AI.FSM;

namespace MonoGameNezTest
{
    public enum ActorState { Idle, Walking, Attacking, KnockedBack }
    
    public class Actor : SimpleStateMachine<ActorState>, IUpdatable 
    {
        public Mover mover;
        public SpriteAnimator animator;
        public BoxCollider Collider;

        public Direction facingDirection = Direction.Down;
        public Vector2 moveDir = Vector2.Zero;
        public float moveSpeed = 30;
        public string currentAnimation;
        

        #region StateMachine Cycles
       public void Walking_Enter() {UpdateAnimation(); }
        public void Walking_Tick() { move(); } // while walking, move
        public void Walking_Exit() { }

        public  void Idle_Enter() {  UpdateAnimation(); moveDir = Vector2.Zero; } /*set animation to idle*/ 
        public void Idle_Tick() { }
        public void Idle_Exit() { }

        public void Attacking_Enter() //update anime
        { }
        public void Attacking_Tick()
        {
            if (elapsedTimeInState > 2){ CurrentState = ActorState.Idle; }
            animator.Play("debug");
     
        }
        public void Attacking_Exit() { }

        #endregion
        
        public Actor() { }

        public override void OnAddedToEntity()
        {
            //setup sprties and entity component
            //var texture = Entity.Scene.Content.LoadTexture("Images\\npcs_red");
            //var sprites = Sprite.SpritesFromAtlasGap(texture, 16, 16); // made special method for accounting for atlases with gaps, Sprite.cs
            animator = Entity.AddComponent<SpriteAnimator>();
            Collider= Entity.AddComponent<BoxCollider>();
            mover = Entity.AddComponent<Mover>();

            InitialState = ActorState.Idle;
        }

        public override void Update()
        {
            base.Update(); // allows for the state machine tick to work
            UpdateAnimation();
        } 

        public void UpdateAnimation()
        {
            if (animator.Animations.Count != 0)
            {
                currentAnimation = CurrentState.ToString() + facingDirection.ToString();
                if (animator.CurrentAnimationName != currentAnimation)
                {
                    animator.Play(currentAnimation);
                }
            }
        }
        

        public void move() 
        {
           // if (facingDirection == Direction.Left && CurrentState == ActorState.Walking) { moveDir.X = -1; }
            //if (facingDirection == Direction.Right && CurrentState == ActorState.Walking) { moveDir.X = 1; }
           // if (facingDirection == Direction.Up && CurrentState == ActorState.Walking) { moveDir.Y = -1; }
            //if (facingDirection == Direction.Down && CurrentState == ActorState.Walking) { moveDir.Y = 1; }

            if (moveDir != Vector2.Zero) { moveDir.Normalize(); CurrentState = ActorState.Walking; } // normalize
            //move
            var movement = moveDir * moveSpeed * Time.DeltaTime;
           mover.Move(movement, out var res);
        } //wrapper for Move()


    }
}
