using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.AI.FSM;

namespace MonoGameNezTest
{
    internal class Npc : SimpleStateMachine<PlayerState>, IUpdatable 
    {
        Direction direction = Direction.Down;
        Vector2 moveDir = Vector2.Zero;
        float moveSpeed = 60;
        string currentAnimation = "";
        Mover mover;

        SpriteAnimator animator;
        BoxCollider Collider;

        void Walking_Enter() { }
        void Walking_Tick() { }
        void Walking_Exit() { }

        void Idle_Enter() { }
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

            InitialState = PlayerState.Walking;

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

        }

        public override void Update()
        {
            UpdateAnimation();
            move();
        } 

        void UpdateAnimation()
        {
            currentAnimation = CurrentState.ToString() + direction.ToString();

            if (animator.CurrentAnimationName != currentAnimation ) { animator.Play(currentAnimation); }

            if (moveDir.X < 0) { direction = Direction.Left; }
            else if (moveDir.X > 0) { direction = Direction.Right; }
            if (moveDir.Y < 0) { direction = Direction.Up; }
            else if (moveDir.Y > 0) { direction = Direction.Down; }

        }

        void move()
        {
            if (moveDir == Vector2.Zero) { CurrentState = PlayerState.Idle; }
            if (moveDir != Vector2.Zero) { moveDir.Normalize(); CurrentState = PlayerState.Walking; }

            var movement = moveDir * moveSpeed * Time.DeltaTime;

           mover.Move(movement, out var res); 
        }


    }
}
