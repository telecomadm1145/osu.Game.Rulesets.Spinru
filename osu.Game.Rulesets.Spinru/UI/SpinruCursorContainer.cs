// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Rulesets.UI;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Spinru.UI
{
    public partial class SpinruCursorContainer : GameplayCursorContainer
    {
        // private Sprite cursorSprite;
        //private Texture cursorTexture;

        protected override Drawable CreateCursor() => new Circle
        {
            Size = new Vector2(20f),
            Scale = new Vector2(1f),
            Origin = Anchor.Centre,
            Colour = Color4.White.Opacity(0),
            //Texture = cursorTexture,
        };

        //[BackgroundDependencyLoader]
        //private void load(TextureStore textures)
        //{
        //    cursorTexture = textures.Get("character");

        //    if (cursorSprite != null)
        //        cursorSprite.Texture = cursorTexture;
        //}
    }
}
