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
    
    public void Inactive_Enter() {  debugText.Text = CurrentState.ToString(); } 
    public void Inactive_Tick() {}
    public void Inactive_Exit() {}
    
    public void Active_Enter() { float counter ; debugText.Text = CurrentState.ToString() ; }

    public void Active_Tick()
    {
        if ((int)counter <= 1) //seconds
        {
            counter = counter + Time.DeltaTime;
            if ((int)counter == 1) { CurrentState = ItemState.Inactive; }
        }
    }
    public void Active_Exit() {counter = 0;}

    public Item () : base() {}  // blank constructor

    
    public override void OnAddedToEntity() // not being run
    {
        //animator = Entity.AddComponent<SpriteAnimator>();
        //var texture = Entity.Scene.Content.LoadTexture("Images\\player_items");
        //var sprites = Sprite.SpritesFromAtlasGap(texture, 16, 16);
        //PUT SAPRITE HERE
        
        debugText = Entity.AddComponent<TextComponent>(new TextComponent());

        InitialState = ItemState.Inactive;

    }

    public void useItem()   // THIS DOESNT WORK, COME BACK LATER AND TOTALLY FIX
    {
        CurrentState = ItemState.Active;
    }
    
    
   
}