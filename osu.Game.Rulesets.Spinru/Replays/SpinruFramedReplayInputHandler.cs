// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Input.StateChanges;
using osu.Framework.Utils;
using osu.Game.Replays;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Spinru.Replays
{
    public class SpinruFramedReplayInputHandler : FramedReplayInputHandler<SpinruReplayFrame>
    {
        public SpinruFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(SpinruReplayFrame frame) => true;

        protected override void CollectReplayInputs(List<IInput> inputs)
        {
            var rotation = Interpolation.ValueAt(CurrentTime, (float)StartFrame.Rotation, (float)EndFrame.Rotation, StartFrame.Time, EndFrame.Time);
            if (EndFrame.Action.HasValue && CurrentTime > (EndFrame.Time - 10))
            {
                inputs.Add(new KeyboardKeyInput(osuTK.Input.Key.Z, true));
            }
            inputs.Add(new MousePositionAbsoluteInput
            {
                Position = GamefieldToScreenSpace(new osuTK.Vector2(MathF.Sin(rotation) * 100 + 192, MathF.Cos(rotation) * 100 + 192))
            });
        }
    }
}
