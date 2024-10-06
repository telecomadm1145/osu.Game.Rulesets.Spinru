// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Spinru
{
    public partial class SpinruInputManager : RulesetInputManager<SpinruAction>
    {
        public SpinruInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum SpinruAction
    {
        [Description("Left side")]
        Left,

        [Description("Right Side")]
        Right,
    }
}
