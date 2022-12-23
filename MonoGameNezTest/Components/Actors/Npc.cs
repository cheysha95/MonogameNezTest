﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.AI.FSM;


namespace MonoGameNezTest;

public class Npc : Actor
{
    public Npc() { }

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        //--------------------------
        InitialState = ActorState.Idle;
        //TODO give different sprite, 
        
        var texture = Entity.Scene.Content.LoadTexture("Images\\npcs_red");
        var sprites = Sprite.SpritesFromAtlasGap(texture, 16, 16); // made special method for accounting for atlases with gaps, Sprite.cs
        
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
        
        facingDirection = Direction.Down;


    }
    
    public override void Update()
    {
        base.Update();
        //-------------------------
            
           
            
            
    }


    
    
    
    
    
}