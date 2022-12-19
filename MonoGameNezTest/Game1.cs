using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace MonoGameNezTest;

public class Game1 : Nez.Core
{

    public Game1() : base()
    {

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        var testScene = new Scene();
        var MapEntity = testScene.CreateEntity("MapEntity");
        MapEntity.AddComponent<TiledMapRenderer>("");
        
        
        testScene.ge
        
        
        

        base.Initialize();
    }

  
}