// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

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

        protected override IEnumerable<SpinruHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            yield return new SpinruHitObject
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                Position = (original as IHasPosition)?.Position ?? Vector2.Zero,
            };
            
        }
    }
}
