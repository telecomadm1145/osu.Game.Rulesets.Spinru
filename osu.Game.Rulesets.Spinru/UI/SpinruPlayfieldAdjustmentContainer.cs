// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Spinru.UI
{
    public partial class SpinruPlayfieldAdjustmentContainer : PlayfieldAdjustmentContainer
    {
        protected override Container<Drawable> Content => content;
        private readonly ScalingContainer content;

        private const float playfield_size_adjust = 0.8f;

        /// <summary>
        /// When true, an offset is applied to allow alignment with historical storyboards displayed in the same parent space.
        /// This will shift the playfield downwards slightly.
        /// </summary>
        public bool AlignWithStoryboard
        {
            set => content.PlayfieldShift = value;
        }

        public SpinruPlayfieldAdjustmentContainer()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            // Calculated from osu!stable as 512 (default gamefield size) / 640 (default window size)
            Size = new Vector2(playfield_size_adjust);

            InternalChild = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                FillAspectRatio = 1,
                Child = content = new ScalingContainer { RelativeSizeAxes = Axes.Both }
            };
        }

        /// <summary>
        /// A <see cref="Container"/> which scales its content relative to a target width.
        /// </summary>
        private partial class ScalingContainer : Container
        {
            internal bool PlayfieldShift { get; set; }

            protected override void Update()
            {
                base.Update();

                Scale = new Vector2(Parent!.ChildSize.X / 384);
                Position = new Vector2(0, (PlayfieldShift ? 8f : 0f) * Scale.X);
                Size = Vector2.Divide(Vector2.One, Scale);
            }
        }
    }
}
