using osu.Game.Beatmaps;
using osu.Game.Rulesets.Spinru.Objects;
using osu.Game.Rulesets.Replays;
using System;
using osu.Game.Rulesets.Objects;

namespace osu.Game.Rulesets.Spinru.Replays
{
    public class SpinruAutoGenerator : AutoGenerator<SpinruReplayFrame>
    {
        public new Beatmap<SpinruHitObject> Beatmap => (Beatmap<SpinruHitObject>)base.Beatmap;

        public SpinruAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
        }

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

        protected override void GenerateFrames()
        {
            Frames.Add(new SpinruReplayFrame());
            double currentRotation = 0.0;
            double accumulatedRotation = 0.0; // 添加累积旋转量

            foreach (SpinruHitObject hitObject in Beatmap.HitObjects)
            {
                var targetAngle = hitObject.Rotation;
                var judge1 = targetAngle + Math.PI / 2;
                var judge2 = targetAngle - Math.PI / 2;

                // 计算到两个判定点的最短角度差
                double diffToJudge1 = ADiff(currentRotation % (2 * Math.PI), judge1);
                double diffToJudge2 = ADiff(currentRotation % (2 * Math.PI), judge2);

                // 选择角度差绝对值最小的判定点
                double chosenDiff;
                if (Math.Abs(diffToJudge1) < Math.Abs(diffToJudge2))
                {
                    chosenDiff = diffToJudge1;
                }
                else
                {
                    chosenDiff = diffToJudge2;
                }

                // 更新累积旋转量
                accumulatedRotation += chosenDiff;

                // 更新当前旋转角度，保持连续性
                currentRotation = accumulatedRotation;

                Frames.Add(new SpinruReplayFrame
                {
                    Time = hitObject.StartTime,
                    Rotation = currentRotation,
                });
            }
        }
    }
}
