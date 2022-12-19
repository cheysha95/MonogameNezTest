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
        base.Initialize();
        var testScene = new Scene();
        Core.Scene = testScene;

        //Map
        var MapEntity = testScene.CreateEntity("MapEntity");
        MapEntity.AddComponent<TiledMapRenderer>(new TiledMapRenderer(Content.LoadTiledMap("Maps\\SimpleMap.tmx")));
        MapEntity.GetComponent<TiledMapRenderer>().RenderLayer = 1;

        //Player
        var Player = testScene.CreateEntity("Player");
        Player.AddComponent<Player>();

        
        
 
        
        


    }

  
}