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
using MonoGameNezTest.Components.Items;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoGameNezTest;

public class Player : Actor
{
    public FollowCamera followCamera;
    public CircleCollider Collider;

    public TextComponent debugText;
    
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        moveSpeed = 80;
        var texture = Entity.Scene.Content.LoadTexture("Images\\player");
        var sprites = Sprite.SpritesFromAtlasGap(texture,16,16,cellOffset: 10); //lmao it worked perfect

        Entity.RemoveComponent<BoxCollider>();
        Entity.RemoveComponent<SpriteAnimator>(); // comback here
        animator = Entity.AddComponent<SpriteAnimator>();
        Collider = Entity.AddComponent<CircleCollider>();
        Collider.Radius = 5; // circle collider = way better;
        
        followCamera = Entity.AddComponent<FollowCamera>(new FollowCamera(Entity));
        followCamera.FollowLerp = 0.01f;
        followCamera.MapSize = new Vector2(400,800);
        followCamera.MapLockEnabled = true;
        //-------------------------------------------------------------------------
        
        
        Entity.AddComponent<Item>(new Item());
        debugText = Entity.AddComponent<TextComponent>(new TextComponent());
        debugText.Color = Color.Black;
        debugText.LocalOffset = new Vector2(0, 20);

        #region addAnimations
        animator.AddAnimation("WalkingRight", new[] { sprites[0],sprites[1] });
        animator.AddAnimation("IdleRight", new[] { sprites[0]});
        //----
        animator.AddAnimation("WalkingLeft", new[] { sprites[4], sprites[5] });
        animator.AddAnimation("IdleLeft", new[] { sprites[4] });
        //-----
        animator.AddAnimation("WalkingUp", new[] { sprites[2], sprites[3] });
        animator.AddAnimation("IdleUp", new[] { sprites[2] });
        //--------
        animator.AddAnimation("WalkingDown", new[] { sprites[6], sprites[7] });
        animator.AddAnimation("IdleDown", new[] { sprites[6] });
        animator.AddAnimation("AttackingDown", new[] { sprites[6]});
        
        
        animator.AddAnimation("debug", new[] { sprites[1], sprites[2]});
        #endregion
        
        
        
        
    }

    public override void Update()
    {
        handleInput();
        base.Update();
        
        
        if (moveDir == Vector2.Zero) {CurrentState = ActorState.Idle;}
        if (moveDir != Vector2.Zero) {CurrentState = ActorState.Walking;}
        
        
        debugText.Text = CurrentState.ToString() + facingDirection.ToString() ;
    }
    

    void handleInput()
    {
        if (Input.IsKeyDown(Keys.W)) { moveDir.Y = -1 ; facingDirection = Direction.Up; }
        if (Input.IsKeyReleased(Keys.W)) { moveDir.Y = 0; }
        //-------------------------------------------------------------
        if (Input.IsKeyDown(Keys.S)) { moveDir.Y = 1; facingDirection = Direction.Down; }
        if (Input.IsKeyReleased(Keys.S)) { moveDir.Y = 0; }
        //----------------------------------------------------------------
        if (Input.IsKeyDown(Keys.A)) { moveDir.X = -1; facingDirection = Direction.Left;}
        if (Input.IsKeyReleased(Keys.A)) { moveDir.X = 0; }
        //-------------------------------------------------------------
        if (Input.IsKeyDown(Keys.D)) { moveDir.X = 1; facingDirection = Direction.Right;}
        if (Input.IsKeyReleased(Keys.D)) { moveDir.X = 0; }
        //-----------------------------------------------------------------------------
       // if (moveDir == Vector2.Zero) {CurrentState = ActorState.Idle;} // amybe this shouldnt be here, maybe enter and exits 

        
        if (Input.IsKeyPressed(Keys.Space))
        { Entity.GetComponent<Item>().useItem(); }
        
        
     
       // if (moveDir != Vector2.Zero) {CurrentState = ActorState.Walking;}
        
        
    }

    
}
