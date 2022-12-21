using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Tiled;

namespace MonoGameNezTest;

public enum Direction { Up, Down, Left, Right}

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

        //Debug render service
       // Core.Services.AddService<Batcher>(new Batcher(GraphicsDevice));
        //Core.Services.GetService<Batcher>().Begin();



        // set screen size hohoho this zooms
        Scene.SetDesignResolution(400, 400, Scene.SceneResolutionPolicy.ShowAllPixelPerfect); // amount of rendertarget you want to show
        Screen.SetSize(400*2,400*2); // size you want it to be, larger values than the other value will stretch


        //global tileset and sprtie sheet for player

        var _tileset = Content.LoadTexture("Images\\Oracle_TilesetA");

       

        //Map
        var map = Content.LoadTiledMap("Maps\\SimpleExtended.tmx");
        var MapEntity = testScene.CreateEntity("MapEntity");
        MapEntity.AddComponent<TiledMapRenderer>(new TiledMapRenderer(map,"trees")); // new map renderer, assigning collision layre
        MapEntity.GetComponent<TiledMapRenderer>().RenderLayer = 1;
        



        //Player
        var PlayerEntity = testScene.CreateEntity("PlayerEntity",new Vector2(10,10));
        PlayerEntity.AddComponent<Player>(new Player());

        //NPC
        var testNpc = testScene.CreateEntity("NPC", new Vector2(300,100));
        testNpc.AddComponent<Npc>(new Npc());
         
    






    }

  
}