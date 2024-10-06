// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using Humanizer;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Audio;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Spinru.UI;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Spinru.Objects.Drawables
{
    public partial class DrawableSpinruHitObject : DrawableHitObject<SpinruHitObject>
    {
        private const double time_preempt = 700;
        private const double time_fadein = 400;

        public override bool HandlePositionalInput => true;
        private double Rotation = 0;
        private double Rotation_2 = 0;
        public DrawableSpinruHitObject(SpinruHitObject hitObject)
            : base(hitObject)
        {
            Size = new Vector2(80);

            Origin = Anchor.Centre;
            Rotation = Math.Atan2(hitObject.Y - 192, hitObject.X - 256);
            Rotation_2 = (Rotation / Math.PI) * 180;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            AddInternal(new Circle
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.White,
            });
        }

        public override IEnumerable<HitSampleInfo> GetSamples() => new[]
        {
            new HitSampleInfo(HitSampleInfo.HIT_NORMAL)
        };

        protected static double ADiff(double angle1, double angle2)
        {
            double diff = angle2 - angle1;

            // 将差值规范到 [-π, π] 范围内
            while (diff > 180)
            {
                diff -= 360;
            }
            while (diff < -180)
            {
                diff += 360;
            }

            return Math.Abs(diff);
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (timeOffset >= 0)
            {
                var tot = 40.0;
                if (ADiff(SpinruPlayfield.Rotation_G, -Rotation_2) < tot || ADiff(SpinruPlayfield.Rotation_G, -Rotation_2 - 180.0) < tot)
                {
                    ApplyMaxResult();
                }
                else
                {
                    if (timeOffset >= 40)
                        ApplyMinResult();
                }
            }
        }

        protected override double InitialLifetimeOffset => time_preempt;

        // protected override void UpdateInitialTransforms() => this.FadeInFromZero(time_fadein);

        protected override void Update()
        {
            do
            {
                // 根据时间计算大小和距离
                var timeoff = Clock.CurrentTime - StartTimeBindable.Value + time_preempt;
                if (timeoff < 0)
                    break;
                var rat = timeoff / time_preempt;
                if (rat < 0)
                    break;
                Size = new Vector2((float)(rat * 100));

                var tot = 40.0;
                if (ADiff(SpinruPlayfield.Rotation_G, -Rotation_2) < tot || ADiff(SpinruPlayfield.Rotation_G, -Rotation_2 - 180.0) < tot)
                {
                    Colour = Color4.Blue;
                }
                else
                {
                    Colour = Color4.Red;
                }
                if (rat < 1)
                    Alpha = MathF.Pow(MathF.Max(MathF.Min((float)rat, 1), 0), 0.3f);

                // 根据Rotation和半径384*1.75/2来更新Position
                float radius = 384.0f / 2.0f;
                var r = Rotation + SpinruPlayfield.Rotation_G / 180 * Math.PI;
                float x = (float)((Math.Cos(r) * radius * (-rat) * 1.75 + radius));
                float y = (float)((Math.Sin(r) * radius * (-rat) * 1.75 + radius));

                Position = new Vector2(x, y);


            } while (false);
            base.Update();
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                    this.ScaleTo(1.8f, 400, Easing.OutQuint).FadeOut(500, Easing.OutQuint).Expire();
                    break;

                case ArmedState.Miss:
                    const double duration = 1000;

                    this.ScaleTo(0.8f, duration, Easing.OutQuint);
                    this.MoveToOffset(new Vector2(0, 10), duration, Easing.In);
                    this.FadeColour(Color4.Red.Opacity(0.5f), duration / 2, Easing.OutQuint).Then().FadeOut(duration / 2, Easing.InQuint).Expire();
                    break;
            }
        }
    }
}
