// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Spinru.Objects.Drawables;
using osu.Game.Rulesets.UI;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Spinru.UI
{
    [Cached]
    public partial class SpinruPlayfield : Playfield
    {

        private JudgementPooler<DrawableSpinruJudgement> judgementPooler;
        private JudgementContainer<DrawableSpinruJudgement> judgementLayer;


        private readonly Container judgementAboveHitObjectLayer;

        public static float Rotation_G = 0;
        public static Container InnerLine;
        protected override GameplayCursorContainer CreateCursor() => new SpinruCursorContainer();

        public SpinruPlayfield()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChildren = [
                    new Circle(){
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new(1.75f),
                        Colour = Color4.White.Opacity(40),
                        RelativeSizeAxes = Axes.Both,
                    }
                ,

                judgementLayer = new JudgementContainer<DrawableSpinruJudgement> { RelativeSizeAxes = Axes.Both },
                HitObjectContainer,
                judgementAboveHitObjectLayer = new Container { RelativeSizeAxes = Axes.Both },
                new Box(){
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new(20f,5f),
                    Position = new((-1.75f*384) / 2.0f,0),
                    Colour = Color4.White.Opacity(80),
                    RelativeSizeAxes = Axes.None,
                },
                new Box(){
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new(20f,5f),
                    Position = new((1.75f*384) / 2.0f,0),
                    Colour = Color4.White.Opacity(80),
                    RelativeSizeAxes = Axes.None,
                },
                InnerLine = new Container(){
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Children = [
                new Box(){
                                        Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new(50f),
                                        Colour = Color4.Black.Opacity(150),
                    RelativeSizeAxes = Axes.None,
                    Rotation = 45,
                }
                        ]
                }
            ];
            var tick_v = 20;
            for (int i = 0; i < 360 / tick_v; i++)
            {
                InnerLine.Add(
                    new Container()
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Rotation = i * tick_v,
                        Child =
                    new Box()
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.CentreLeft,
                        RelativeSizeAxes = Axes.None,
                        Size = new(10f, 2f),
                        Position = new((-1.75f * 384) / 2.0f, 0),
                        Colour = Color4.White.Opacity(200),
                    }
                    }
                    );
            }
            HitObjectContainer.Anchor = Anchor.Centre;
            HitObjectContainer.Origin = Anchor.Centre;
            AddInternal(judgementPooler = new([
                HitResult.Great,
                HitResult.Ok,
                HitResult.Meh,
                HitResult.Miss,
                HitResult.LargeTickMiss,
                HitResult.IgnoreMiss,
            ]));

            NewResult += onNewResult;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
        }
        public override bool HandlePositionalInput => true;
        private float x, y;
        protected override void Update()
        {
            Rotation_G = InnerLine.Rotation = (MathF.Atan2(y, x) / MathF.PI) * 180.0f;
            // 从 HitObjectContainer读取hitobject
            //foreach (DrawableSpinruHitObject obj in HitObjectContainer.Objects)
            //{
            //    // obj.Rotation_2; // 旋转（角度）
            //}
            base.Update();
        }
        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            x = e.MousePosition.X - 192.0f;
            y = e.MousePosition.Y - 192.0f;
            return base.OnMouseMove(e);
        }
        private void onNewResult(DrawableHitObject judgedObject, JudgementResult result)
        {
            //if (!judgedObject.DisplayResult || !DisplayJudgements.Value)
            //    return;

            //var explosion = judgementPooler.Get(result.Type, doj => doj.Apply(result, judgedObject));

            //if (explosion == null)
            //    return;

            //judgementLayer.Add(explosion);

            //// the proxied content is added to judgementAboveHitObjectLayer once, on first load, and never removed from it.
            //// ensure that ordering is consistent with expectations (latest judgement should be front-most).
            //judgementAboveHitObjectLayer.ChangeChildDepth(explosion.ProxiedAboveHitObjectsContent, (float)-result.TimeAbsolute);
        }
    }
}
