using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.AI.FSM;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameNezTest
{
    public enum PlayerState { Idle, Walking, Attacking, KnockedBack }
    public class Player : SimpleStateMachine<PlayerState>, IUpdatable
    {
        Direction CurrentDirection = Direction.Down;

        Vector2 moveDir = Vector2.Zero;
        float moveSpeed = 60;

        Mover mover;
        FollowCamera followCamera;
        TiledMapMover mapMover;
 
        SpriteAnimator animator;
        CircleCollider Ccollider;
        string animation = "walkingLeft";

        void Walking_Enter(){  }
        void Walking_Tick() { }
        void Idle_Enter() { }

        public Player() { }

        public override void OnAddedToEntity() // MUST OVERRIDE, load ontent here
        {
            var texture = Entity.Scene.Content.LoadTexture("Images\\link_spriteSheet");
            var sprites = Sprite.SpritesFromAtlas(texture,16,16);

            mover = Entity.AddComponent<Mover>(new Mover()); // 
            animator = Entity.AddComponent<SpriteAnimator>(); // dont know why this doesnt need new

            Ccollider = Entity.AddComponent<CircleCollider>();
            Ccollider.Radius = 6; // circle collider = way better;

            followCamera = Entity.AddComponent<FollowCamera>(new FollowCamera(Entity));
            followCamera.FollowLerp = 0.01f;
            followCamera.MapSize = new Vector2(400,800);
            followCamera.MapLockEnabled = true;
            //-----------------------------------------------------------------------------------------------------------
            InitialState = PlayerState.Idle;
           








            //add animations
            animator.AddAnimation("walkingRight", new[] { sprites[0],sprites[1] });
            animator.AddAnimation("idleRight", new[] { sprites[0]});
            //----
            animator.AddAnimation("walkingLeft", new[] { sprites[4], sprites[5] });
            animator.AddAnimation("idleLeft", new[] { sprites[4] });
            //-----
            animator.AddAnimation("walkingUp", new[] { sprites[2], sprites[3] });
            animator.AddAnimation("idleUp", new[] { sprites[2] });
            //--------
            animator.AddAnimation("walkingDown", new[] { sprites[6], sprites[7] });
            animator.AddAnimation("idleDown", new[] { sprites[6] });
        }


         void IUpdatable.Update() // DO THIS TO HAVE UPDATE CALLED
        {           
            handleInput();
            updateAnimation();

            move();
        }

        void handleInput()
        {
            if (Input.IsKeyDown(Keys.W)) { moveDir.Y = -1 ; }
            if (Input.IsKeyReleased(Keys.W)) { moveDir.Y = 0;}
            //-------------------------------------------------------------
            if (Input.IsKeyDown(Keys.S)) { moveDir.Y = 1; }
            if (Input.IsKeyReleased(Keys.S)) { moveDir.Y = 0; }
            //----------------------------------------------------------------
            if (Input.IsKeyDown(Keys.A)) { moveDir.X = -1; }
            if (Input.IsKeyReleased(Keys.A)) { moveDir.X = 0; }
            //-------------------------------------------------------------
            if (Input.IsKeyDown(Keys.D)) { moveDir.X = 1; }
            if (Input.IsKeyReleased(Keys.D)) { moveDir.X = 0; }


        }
        void move()
        {
            if (moveDir == Vector2.Zero) { CurrentState = PlayerState.Idle; }
            if (moveDir != Vector2.Zero) { moveDir.Normalize(); CurrentState = PlayerState.Walking; } // DONT FORGET TO NORMALIZE
            var movement = moveDir * moveSpeed * Time.DeltaTime; // movement is vector2
          
            Entity.GetComponent<Mover>().Move(movement, out var res); // perfer moving th ecircle collider over map mover
            
        }
        void updateAnimation()
        {
            if (moveDir.X < 0) { animation = "walkingLeft"; }
            else if (moveDir.X > 0) { animation = "walkingRight"; }
            if (moveDir.Y < 0) { animation = "walkingUp"; }
            else if (moveDir.Y > 0) { animation = "walkingDown"; }

            // play animation if moving, pause if not
            if (moveDir != Vector2.Zero)
            {
                if (!animator.IsAnimationActive(animation)) { animator.Play(animation); }
                else { animator.UnPause(); }
            }
            else { animator.Pause(); }
        }


    }
}
