using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.AI.FSM;

namespace MonoGameNezTest
{
    public class Actor : SimpleStateMachine<ActorState>, IUpdatable 
    {
        public Mover mover;
        public SpriteAnimator animator;
        public BoxCollider Collider;

        public Direction direction = Direction.Down;
        public Vector2 moveDir = Vector2.Zero;
        public float moveSpeed = 30;
 

        #region StateMachine Cycles
       public void Walking_Enter() {UpdateAnimation(); }
        public void Walking_Tick() { move(); } // while walking, move
        public void Walking_Exit() { }

        public  void Idle_Enter() { UpdateAnimation(); moveDir = Vector2.Zero; } /*set animation to idle*/ 
        public void Idle_Tick() { }
        public void Idle_Exit() { }
        #endregion
        
        public Actor() { }

        public override void OnAddedToEntity()
        {
            //setup sprties and entity component
            var texture = Entity.Scene.Content.LoadTexture("Images\\npcs_red");
            var sprites = Sprite.SpritesFromAtlasGap(texture, 16, 16); // made special method for accounting for atlases with gaps, Sprite.cs
            animator = Entity.AddComponent<SpriteAnimator>();
            Collider= Entity.AddComponent<BoxCollider>();
            mover = Entity.AddComponent<Mover>();



            #region Adding Animations
            animator.AddAnimation("WalkingDown", new[] { sprites[31], sprites[32] }, fps: 2);
            animator.AddAnimation("IdleDown", new[] { sprites[31] }, fps: 2);
            //
            animator.AddAnimation("WalkingRight", new[] { sprites[25], sprites[26] }, fps: 2);
            animator.AddAnimation("IdleRight", new[] { sprites[25] }, fps: 2);
            //
            animator.AddAnimation("WalkingUp", new[] { sprites[27], sprites[28] }, fps: 2);
            animator.AddAnimation("IdleUp", new[] { sprites[27] }, fps: 2);
            //
            animator.AddAnimation("WalkingLeft", new[] { sprites[29], sprites[30] }, fps: 2);
            animator.AddAnimation("IdleLeft", new[] { sprites[30] }, fps: 2);

            #endregion

            InitialState = ActorState.Walking;
        }

        public override void Update()
        {
            base.Update(); // allows for the state machine tick to work
            UpdateAnimation();
  
            #region Placeholder Behavior\

            
            if (Entity.Position.Y >= 290)
            {
                direction = Direction.Up;
            }

            if (Entity.Position.Y < 30)
            {
                direction = Direction.Down; 
            }

            #endregion
        } 

        public void UpdateAnimation()
        {
             var currentAnimation = CurrentState.ToString() + direction.ToString();
            if (animator.CurrentAnimationName != currentAnimation ) { animator.Play(currentAnimation); }
        }
        

        public void move() 
        {
            if (direction == Direction.Left && CurrentState == ActorState.Walking) { moveDir.X = -1; }
            if (direction == Direction.Right && CurrentState == ActorState.Walking) { moveDir.X = 1; }
            if (direction == Direction.Up && CurrentState == ActorState.Walking) { moveDir.Y = -1; }
            if (direction == Direction.Down && CurrentState == ActorState.Walking) { moveDir.Y = 1; }

            if (moveDir != Vector2.Zero) { moveDir.Normalize(); CurrentState = ActorState.Walking; } // normalize
            //move
            var movement = moveDir * moveSpeed * Time.DeltaTime;
           mover.Move(movement, out var res);
        }


    }
}
