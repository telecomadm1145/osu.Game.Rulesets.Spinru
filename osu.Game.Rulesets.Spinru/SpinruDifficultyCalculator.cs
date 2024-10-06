// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Spinru.Objects;

namespace osu.Game.Rulesets.Spinru
{
    public class SpinruDifficultyCalculator : DifficultyCalculator
    {
        public SpinruDifficultyCalculator(IRulesetInfo ruleset, IWorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        protected override DifficultyAttributes CreateDifficultyAttributes(IBeatmap beatmap, Mod[] mods, Skill[] skills, double clockRate)
        {
            double rotationDifficulty = 0;
            double currentRotation = 0;
            double lastRotation = 0;
            double lastTime = 0;
            bool firstObject = true;

            foreach (SpinruHitObject hitObject in beatmap.HitObjects)
            {
                if (firstObject)
                {
                    firstObject = false;
                    lastRotation = currentRotation;
                    lastTime = hitObject.StartTime;
                    continue;
                }

                // 如果时间相同，增加难度但不更新旋转
                if (hitObject.StartTime == lastTime)
                {
                    rotationDifficulty += 1;
                    continue;
                }

                // 计算两个可能的旋转方向
                double targetRotation = hitObject.Rotation;
                double rotationOption1 = targetRotation + Math.PI / 2;
                double rotationOption2 = targetRotation - Math.PI / 2;

                // 计算从当前旋转到两个选项的角度变化
                double diff1 = CalculateRotationDifference(currentRotation, rotationOption1);
                double diff2 = CalculateRotationDifference(currentRotation, rotationOption2);

                // 选择角度变化较小的选项
                double chosenDiff = Math.Min(Math.Abs(diff1), Math.Abs(diff2));
                double rotationSpeed = chosenDiff / (hitObject.StartTime - lastTime) * 1000;

                // 将旋转速度的平方添加到难度中
                rotationDifficulty += Math.Pow(rotationSpeed, 2);

                // 更新当前旋转和时间
                currentRotation += Math.Abs(diff1) < Math.Abs(diff2) ? diff1 : diff2;
                lastTime = hitObject.StartTime;
            }

            // 计算最终难度值
            double averageDifficulty = rotationDifficulty / Math.Max(1, beatmap.HitObjects.Count - 1);
            return new DifficultyAttributes(mods, (averageDifficulty) * clockRate);
        }

        // 辅助方法：计算两个角度之间的最小差值
        private double CalculateRotationDifference(double fromRotation, double toRotation)
        {
            double diff = toRotation - fromRotation;

            // 将差值规范到 [-π, π] 范围内
            while (diff > Math.PI) diff -= 2 * Math.PI;
            while (diff < -Math.PI) diff += 2 * Math.PI;

            return diff;
        }
        protected override IEnumerable<DifficultyHitObject> CreateDifficultyHitObjects(IBeatmap beatmap, double clockRate) => Enumerable.Empty<DifficultyHitObject>();

        protected override Skill[] CreateSkills(IBeatmap beatmap, Mod[] mods, double clockRate) => Array.Empty<Skill>();
    }
}
