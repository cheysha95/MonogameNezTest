using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.AI.FSM;


namespace MonoGameNezTest.Components.Items;
//test class for items, starting with sword

public enum ItemState { Inactive, Active, Cooldown}





public class Item : SimpleStateMachine<ItemState>
{
    public SpriteAnimator animator;
    public CircleCollider collider;
    private TextComponent debugText;
    private float counter;
    
    public void Inactive_Enter() { debugText.Enabled = false; } 
    public void Inactive_Tick() {}
    public void Inactive_Exit() {}
    
    public void Active_Enter() { debugText.Enabled = true; float counter = 0; }

    public void Active_Tick()
    {
        
        if (counter < 1) { debugText.Text = counter.ToString(); counter =+ Time.DeltaTime; }

    }
    public void Active_Exit() {}

    public Item () : base() {}  // blank constructor

    
    public override void OnAddedToEntity() // not being run
    {
        //animator = Entity.AddComponent<SpriteAnimator>();
        //var texture = Entity.Scene.Content.LoadTexture("Images\\player_items");
        //var sprites = Sprite.SpritesFromAtlasGap(texture, 16, 16);
        //PUT SAPRITE HERE
        
        debugText = Entity.AddComponent<TextComponent>(new TextComponent());
        debugText.Text = "Hello, World!";

        InitialState = ItemState.Inactive;

    }

    public void useItem()   // THIS DOESNT WORK, COME BACK LATER AND TOTALLY FIX
    {
        CurrentState = ItemState.Active;
    }
    
    
   
}