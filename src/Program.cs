using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.Threading;
using System.IO;

namespace AetherQuest
{
    class Program : Game
    {
        GraphicsDeviceManager graphics;

        private RenderTarget2D _colorMapRenderTarget;
        private RenderTarget2D _depthMapRenderTarget;
        private RenderTarget2D _normalMapRenderTarget;
        private RenderTarget2D _shadowMapRenderTarget;
        private Texture2D _shadowMapTexture;
        private Texture2D _colorMapTexture;
        private Texture2D _normalMapTexture;
        private Texture2D _depthMapTexture;

        private VertexPositionTexture[] _vertices;

        //keeps track of logos.
        private byte steps;

        private Effect _lightEffect1;
        private Effect _lightEffect2;
        private Effect _blackAndWhite;

        private SpriteBatch spriteBatch;

        private System.Timers.Timer timer;
        
        public Program()
        {
            this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;


            //Give make the graphics render at the Screen Height and Width.
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;

            if (!graphics.IsFullScreen)
            {
                graphics.PreferredBackBufferHeight = (int)(graphics.PreferredBackBufferHeight / 1.5);
                graphics.PreferredBackBufferWidth = (int)(graphics.PreferredBackBufferWidth / 1.5);
            }


            //The name is insignificant, the player can't see it anyway.
            Player player = new Player("Starter", -1);
            player.moveX(350);

            timer = new System.Timers.Timer();

            timer.Interval = (85 * 100);
            timer.Enabled = true;




            timer.Elapsed += timer_Elapsed;

            DungeonManager.getInstance().setCurrentDungeon(new PresetDungeon(new StarterRoom()));
            InputManager.getInstance().setCurrentPlayer(player);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            steps++;

            if(steps == 2)
            {
                timer.Interval = (6 * 45 * 1000);
            }
            
            if(steps > 2)
            {
                steps--;
                playRandomSong();
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            ResourceHandler.getInstance().initialize(Content);

            //Give the Screen Resolution to the Menu Manager
            new MenuManager(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            

            var pp = GraphicsDevice.PresentationParameters;
            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;
            SurfaceFormat format = pp.BackBufferFormat;

            _colorMapRenderTarget = new RenderTarget2D(GraphicsDevice, width, height, false, format, pp.DepthStencilFormat);
            _depthMapRenderTarget = new RenderTarget2D(GraphicsDevice, width, height, false, format, pp.DepthStencilFormat);
            _normalMapRenderTarget = new RenderTarget2D(GraphicsDevice, width, height, false, format, pp.DepthStencilFormat);
            _shadowMapRenderTarget = new RenderTarget2D(GraphicsDevice, width, height, false, format, pp.DepthStencilFormat);

            _lightEffect1 = Content.Load<Effect>("Effects/LightningShadow.mgfxo");
            _lightEffect2 = Content.Load<Effect>("Effects/LightningCombined.mgfxo");
            _blackAndWhite = Content.Load<Effect>("Effects/GrayScale.mgfxo");

            _vertices = new VertexPositionTexture[4];
            _vertices[0] = new VertexPositionTexture(new Vector3(-1, 1, 0), new Vector2(0, 0));
            _vertices[1] = new VertexPositionTexture(new Vector3(1, 1, 0), new Vector2(1, 0));
            _vertices[2] = new VertexPositionTexture(new Vector3(-1, -1, 0), new Vector2(0, 1));
            _vertices[3] = new VertexPositionTexture(new Vector3(1, -1, 0), new Vector2(1, 1));
        }

        private void playRandomSong()
        {
            String[] files = Directory.GetFiles("Songs");
            String file = files[new Random().Next(files.Length)];
            file = file.Split(new string[] { "\\" }, StringSplitOptions.None).Last();
            Song x = Content.Load<Song>("Songs\\" + file);
            MediaPlayer.Play(x);
           
        }

        protected override void Initialize()
        {
            base.Initialize();

        }

        /// <summary>
        /// Draws the Scene before adding shadow and lighting.
        /// </summary>
        void DrawScene()
        {
            try
            {
                // Set the render targets
                GraphicsDevice.SetRenderTarget(_colorMapRenderTarget);

                // Clear all render targets
                GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1, 0);

                // Draw your scene here
                if (ResourceHandler.getInstance().getBottom() == 0)
                {
                    ResourceHandler.getInstance().setBottom(graphics.GraphicsDevice.Viewport.Bounds.Bottom);
                    //InputManager.getInstance().getCurrentPlayer().setY(GraphicsDevice.Viewport.Bounds.Bottom - DungeonManager.getInstance().getCurrentDungeon().getFirstChunk().getHeight() - InputManager.getInstance().getCurrentPlayer().getHeight());
                }


                if (!DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft().checkLeftTopCollision() && !DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkRight().checkRightTopCollision())
                {
                    InputManager.getInstance().getCurrentPlayer().gravityPull();

                }
                else InputManager.getInstance().getCurrentPlayer().killAcceleration();

                Thread.Sleep(16);
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

                DungeonManager.getInstance().getCurrentDungeon().draw(spriteBatch, graphics.GraphicsDevice);
                // xyz.followPlayer(dungeon);
                //xyz.draw(spriteBatch, graphics.GraphicsDevice);

                spriteBatch.End();


                // Reset the render target
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.SetRenderTarget(_normalMapRenderTarget);

                // Clear all render targets
                GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1, 0);

                // Draw all your normal maps here

                if (!DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft().checkLeftTopCollision() && !DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkRight().checkRightTopCollision())
                {
                    InputManager.getInstance().getCurrentPlayer().gravityPull();

                }
                else InputManager.getInstance().getCurrentPlayer().killAcceleration();
                // Thread.Sleep(10);
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, _blackAndWhite);
                DungeonManager.getInstance().getCurrentDungeon().draw(spriteBatch, graphics.GraphicsDevice);
                //InputManager.getInstance().getCurrentPlayer().draw(spriteBatch, graphics.GraphicsDevice);

                spriteBatch.End();

                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.SetRenderTarget(_depthMapRenderTarget);

                // Clear all render targets
                GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1, 0);

                // Draw all your depth maps here

                // Deactive the rander targets to resolve them
                GraphicsDevice.SetRenderTarget(null);

                // Gather all the textures from the Rendertargets
                _colorMapTexture = _colorMapRenderTarget;
                _normalMapTexture = _normalMapRenderTarget;
                _depthMapTexture = _depthMapRenderTarget;
            }
            catch (Exception ex)
            {
                Logger.getInstance().log(ex.ToString() + " " + ex.Message);
                spriteBatch.End();
            }
            spriteBatch.End();

        }

        /// <summary>
        /// Add shadow and lighting to the world.
        /// </summary>
        private void DrawCombinedMaps()
        {
            _lightEffect2.CurrentTechnique = _lightEffect2.Techniques["DeferredCombined"];
            _lightEffect2.Parameters["ambient"].SetValue(.45f);
            _lightEffect2.Parameters["ambientColor"].SetValue(new Vector4(0.35f, 0.35f, 0.25f, 1.0f));

            // This variable is used to boost to output of the light sources when they are combined
            // I found 4 a good value for my lights but you can also make this dynamic if you want
            _lightEffect2.Parameters["lightAmbient"].SetValue(1);

            _lightEffect2.Parameters["ColorMap"].SetValue(_colorMapTexture);
            _lightEffect2.Parameters["ShadingMap"].SetValue(_shadowMapTexture);


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            foreach (var pass in _lightEffect2.CurrentTechnique.Passes)
            {
                pass.Apply();

                spriteBatch.Draw(_colorMapTexture, Vector2.Zero, Color.White);
               
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Generate a Shadow Map.
        /// </summary>
        /// <returns></returns>
        private Texture2D GenerateShadowMap()
        {
            GraphicsDevice.SetRenderTarget(_shadowMapRenderTarget);
            
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1, 0);

            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            //Project a light onto the player.
            List<Light> lights = new List<Light>();

            Light light1 = new Light();
            light1.setPower(60);
            light1.setLightRadius(new Random().Next(289, 300));
            light1.setColor(Color.LightGoldenrodYellow);

            light1.setVector(new Vector3(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2, .56f));

            lights.Add(light1);
            lights.AddRange(Light.getLights());

            foreach(Light light in lights)
            {
                _lightEffect1.CurrentTechnique = _lightEffect1.Techniques["DeferredPointLight"];

                if(light != light1)
                { 
                    if (light.getWorldPos().X + (Light.getStartVector().X) + graphics.PreferredBackBufferWidth + light.getLightRadius() < InputManager.getInstance().getCurrentPlayer().getX()) continue;
                    if (light.getWorldPos().X + (Light.getStartVector().X) - graphics.PreferredBackBufferWidth - light.getLightRadius() > InputManager.getInstance().getCurrentPlayer().getX()) continue;
                
                    _lightEffect1.Parameters["lightPosition"].SetValue(new Vector3(light.getWorldPos().X + (Light.getStartVector().X - InputManager.getInstance().getCurrentPlayer().getX()), light.getWorldPos().Y + (Light.getStartVector().Y - InputManager.getInstance().getCurrentPlayer().getY()), light.getWorldPos().Z));
                }
                else _lightEffect1.Parameters["lightPosition"].SetValue(light.getWorldPos());

                _lightEffect1.Parameters["lightStrength"].SetValue(light.getPower());
                
                _lightEffect1.Parameters["lightColor"].SetValue(light.getColor().ToVector3());
                _lightEffect1.Parameters["lightRadius"].SetValue(light.getLightRadius());

                _lightEffect1.Parameters["screenWidth"].SetValue(GraphicsDevice.Viewport.Width);
                _lightEffect1.Parameters["screenHeight"].SetValue(GraphicsDevice.Viewport.Height);

                _lightEffect1.Parameters["NormalMap"].SetValue(_normalMapTexture);
                _lightEffect1.Parameters["DepthMap"].SetValue(_depthMapTexture);

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                foreach (var pass in _lightEffect1.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, _vertices, 0, 2);
                    
                }
                spriteBatch.End();

               
            }

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.SetRenderTarget(null);


            //_colorMapTexture = _shadowMapRenderTarget;

            return _shadowMapRenderTarget;
        }



        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (MediaPlayer.State == MediaState.Stopped)
                playRandomSong();

            InputManager.getInstance().getReleasedKeys();
            if (InputManager.getInstance().getGameReleasedKeys()[15])
            {
                if (MenuManager.getInstance().getInventoryMenu().getEnabled()) MenuManager.getInstance().getInventoryMenu().setEnabled(false);
                else
                    MenuManager.getInstance().toggleMainMenu();
            }
            if (!InputManager.getInstance().textBoxFocused() && !(InputManager.getInstance().getCurrentPlayer().getHealth().getValue() < 0))
            {



                if (InputManager.getInstance().getGameReleasedKeys()[3])
                {
                    MenuManager.getInstance().toggleInventoryMenu();

                    if (MenuManager.getInstance().getInventoryMenu().getEnabled())
                        if (DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft().GetType().Equals(typeof(SquareChestChunk)))
                        {
                            ((SquareChestChunk)DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft()).useChest();
                        }

                }

                if (InputManager.getInstance().getGameReleasedKeys()[4])
                {
                    MenuManager.getInstance().toggleJournalMenu();
                }


                if (InputManager.getInstance().getGameReleasedKeys()[2])
                {
                    if (DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft().checkLeftTopCollision())
                    {
                        InputManager.getInstance().getCurrentPlayer().moveY(-32);
                    }
                }


                if (InputManager.getInstance().getGameReleasedKeys()[6])
                {
                    InputManager.getInstance().getCurrentPlayer().attack();
                }

                if (InputManager.getInstance().getGameReleasedKeys()[7])
                {
                    InputManager.getInstance().getCurrentPlayer().shootBullet();
                }


                if (InputManager.getInstance().getGameReleasedKeys()[5])
                {
                    if (DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft().GetType().Equals(typeof(MovingChunk)))
                    {
                        MovingChunk mc = ((MovingChunk)DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft());
                        mc.setLifted(!mc.getLifted());
                    }

                    if (typeof(PortalChunk).IsAssignableFrom(DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft().GetType()))
                    {
                        PortalChunk pc = ((PortalChunk)DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft());

                        DungeonManager.getInstance().setCurrentDungeon(pc.usePortal());
                        
                        if (pc.getTeleportVector() == Vector2.Zero)
                        {
                            InputManager.getInstance().getCurrentPlayer().setX(-80);
                            InputManager.getInstance().getCurrentPlayer().setY(ResourceHandler.getInstance().getBottom() - DungeonManager.getInstance().getCurrentDungeon().getFirstChunk().getHeight() - InputManager.getInstance().getCurrentPlayer().getHeight());
                        }
                        else
                        {
                            InputManager.getInstance().getCurrentPlayer().setVector(pc.getTeleportVector());
                        }
                    }
                }

                if (InputManager.getInstance().getGamePressedKeys()[1])
                {
                    MenuManager.getInstance().getInventoryMenu().setEnabled(false);

                    InputManager.getInstance().getCurrentPlayer().moveX(3);

                    if (DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkRight().checkRightSideCollision())
                    {
                        if (DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkRight().GetType().Equals(typeof(MovingChunk)))
                        {
                            ((MovingChunk)DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkRight()).setLifted(false);
                        }
                        InputManager.getInstance().getCurrentPlayer().moveX(-3);

                    }

                }

                if (InputManager.getInstance().getGamePressedKeys()[0])
                {

                    MenuManager.getInstance().getInventoryMenu().setEnabled(false);
                    InputManager.getInstance().getCurrentPlayer().moveX(-3);
                    if (DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft().checkLeftSideCollision())
                    {
                        InputManager.getInstance().getCurrentPlayer().moveX(3);
                    }
                }
            }



            InputManager.getInstance().getGamePressedKeys();
        }


        protected override void Draw(GameTime gameTime)
        {

            DrawScene();
            _shadowMapTexture = GenerateShadowMap();

            DrawCombinedMaps();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            InputManager.getInstance().getCurrentPlayer().draw(spriteBatch, graphics.GraphicsDevice);
            if (steps == 0) spriteBatch.Draw(ResourceHandler.getInstance().getBitBlitTexture(), new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2), Color.White);
            if (steps == 1) spriteBatch.Draw(ResourceHandler.getInstance().getTeamBasyl(), new Vector2(graphics.PreferredBackBufferWidth / 2 + 100, graphics.PreferredBackBufferHeight / 2), Color.White);
            
            if (MenuManager.getInstance() != null) MenuManager.getInstance().draw(spriteBatch, graphics.GraphicsDevice);
            spriteBatch.End();

            
            base.Draw(gameTime);
        }

        public static void Main(String[] args)
        {
            try
            {
                Program jesse = new Program();
                jesse.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("There was an issue. This is likely because of outdated OpenGL drivers or lack of OpenAL Installation.");
                Console.ReadKey();

            }
        }
    }
}
