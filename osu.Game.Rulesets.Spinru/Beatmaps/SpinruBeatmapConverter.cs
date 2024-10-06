// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Spinru.Objects;
using osuTK;

namespace osu.Game.Rulesets.Spinru.Beatmaps
{
    public class SpinruBeatmapConverter : BeatmapConverter<SpinruHitObject>
    {
        public SpinruBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);
        protected static double ADiff(double angle1, double angle2)
        {
            double diff = angle2 - angle1;
            // 将差值规范到 [-π, π] 范围内
            while (diff > Math.PI)
            {
                diff -= 2 * Math.PI;
            }
            while (diff < -Math.PI)
            {
                diff += 2 * Math.PI;
            }
            return diff;
        }
        protected bool v = false;

        protected double GetRotation(IHasPosition ihp)
        {
            var targetAngle = Math.Atan2(ihp.Y - 192, ihp.X - 256);
            v = !v;
            return v ? (targetAngle / 6) + Math.PI / 2 : (targetAngle / 6) - Math.PI / 2;
        }

        protected override IEnumerable<SpinruHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            yield return new SpinruHitObject
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                Rotation = GetRotation(original as IHasPosition),
            };
            
        }
    }
}
