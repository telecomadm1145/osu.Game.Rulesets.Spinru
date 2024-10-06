// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Spinru.Objects;
using osu.Game.Rulesets.Spinru.Objects.Drawables;
using osu.Game.Rulesets.Spinru.Replays;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Spinru.UI
{
    [Cached]
    public partial class DrawableSpinruRuleset : DrawableRuleset<SpinruHitObject>
    {
        public DrawableSpinruRuleset(SpinruRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        public override PlayfieldAdjustmentContainer CreatePlayfieldAdjustmentContainer() => new SpinruPlayfieldAdjustmentContainer();

        protected override Playfield CreatePlayfield() => new SpinruPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new SpinruFramedReplayInputHandler(replay);

        public override DrawableHitObject<SpinruHitObject> CreateDrawableRepresentation(SpinruHitObject h) => new DrawableSpinruHitObject(h);

        protected override PassThroughInputManager CreateInputManager() => new SpinruInputManager(Ruleset?.RulesetInfo);
    }
}
