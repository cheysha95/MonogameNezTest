using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.AI.FSM;

namespace MonoGameNezTest
{
    internal class Npc : SimpleStateMachine<ActorState>, IUpdatable 
    {
        Mover mover;
        SpriteAnimator animator;
        BoxCollider Collider;

        Direction direction = Direction.Down;
        Vector2 moveDir = Vector2.Zero;
        float moveSpeed = 60;
        string currentAnimation = "";


        void Walking_Enter() {/* set animation to walking in direction */ UpdateAnimation(); }
        void Walking_Tick() {/* move in direction */ move(); }
        void Walking_Exit() { }

        void Idle_Enter() { /*set animation to idle*/ UpdateAnimation(); }
        void Idle_Tick() { }
        void Idle_Exit() { }

        public Npc() { }

        public override void OnAddedToEntity()
        {
            var texture = Entity.Scene.Content.LoadTexture("Images\\npcs_red");
            var sprites = Sprite.SpritesFromAtlasGap(texture, 16, 16); // made special method for accounting for atlases with gaps, Sprite.cs
            animator = Entity.AddComponent<SpriteAnimator>();
            Collider= Entity.AddComponent<BoxCollider>();
            mover = Entity.AddComponent<Mover>();
         



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

            InitialState = ActorState.Walking;




        }

        public override void Update()
        {
            UpdateAnimation();
            //move();
            //UpdateState();


            base.Update(); // allows for the state machine tick to work
       
        } 

        void UpdateAnimation()
        {
            currentAnimation = CurrentState.ToString() + direction.ToString();

            if (animator.CurrentAnimationName != currentAnimation ) { animator.Play(currentAnimation); }
        }

        void UpdateState() 
        {
            if (moveDir.X > 0 || moveDir.Y > 0) { CurrentState = ActorState.Walking; } 
        }

        void move() // goal is to have moveDir be completly controlled by the state
        {
            //if (moveDir == Vector2.Zero) { CurrentState = ActorState.Idle; }

            if (direction == Direction.Left && CurrentState == ActorState.Walking) { moveDir.X = -1; }
            if (direction == Direction.Right && CurrentState == ActorState.Walking) { moveDir.X = 1; }
            if (direction == Direction.Up && CurrentState == ActorState.Walking) { moveDir.Y = -1; }
            if (direction == Direction.Down && CurrentState == ActorState.Walking) { moveDir.Y = 1; }

            if (moveDir != Vector2.Zero) { moveDir.Normalize(); CurrentState = ActorState.Walking; } // normalize
            var movement = moveDir * moveSpeed * Time.DeltaTime;
           mover.Move(movement, out var res); 
        }


    }
}
