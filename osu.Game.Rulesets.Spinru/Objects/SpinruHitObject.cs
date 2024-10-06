// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osuTK;

namespace osu.Game.Rulesets.Spinru.Objects
{
    public class SpinruHitObject : HitObject, IHasPosition
    {
        public override Judgement CreateJudgement() => new Judgement();

        public Vector2 Position => new Vector2(X, Y);
        public double Rotation { get; set; }

        public float X => (float)(Math.Cos(Rotation) * 384 / 2);
        public float Y => (float)(Math.Sin(Rotation) * 384 / 2);
    }
}
