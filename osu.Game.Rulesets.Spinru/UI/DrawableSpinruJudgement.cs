using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Skinning;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Spinru.UI
{
    //internal partial class SkinnableLighting : SkinnableSprite
    //{
    //    private DrawableSpinruJudgement? targetJudgement;
    //    private JudgementResult? targetResult;

    //    public SkinnableLighting()
    //        : base("lighting")
    //    {
    //    }

    //    protected override void SkinChanged(ISkinSource skin)
    //    {
    //        base.SkinChanged(skin);
    //        updateColour();
    //    }

    //    /// <summary>
    //    /// Updates the lighting colour from a given hitobject and result.
    //    /// </summary>
    //    /// <param name="targetJudgement">The <see cref="DrawableHitObject"/> that's been judged.</param>
    //    /// <param name="targetResult">The <see cref="JudgementResult"/> that <paramref name="targetJudgement"/> was judged with.</param>
    //    public void SetColourFrom(DrawableSpinruJudgement targetJudgement, JudgementResult? targetResult)
    //    {
    //        this.targetJudgement = targetJudgement;
    //        this.targetResult = targetResult;

    //        updateColour();
    //    }

    //    private void updateColour()
    //    {
    //        if (targetJudgement == null || targetResult == null)
    //            Colour = Color4.White;
    //        else
    //            Colour = targetResult.IsHit ? targetJudgement.AccentColour : Color4.Transparent;
    //    }
    //}

    internal partial class DrawableSpinruJudgement : DrawableJudgement
    {
        //internal Color4 AccentColour { get; private set; }

        //internal SkinnableLighting Lighting { get; private set; } = null!;

        //[Resolved]
        //private OsuConfigManager config { get; set; } = null!;

        //private Vector2 screenSpacePosition;

        //[BackgroundDependencyLoader]
        //private void load()
        //{
        //    AddInternal(Lighting = new SkinnableLighting
        //    {
        //        Anchor = Anchor.Centre,
        //        Origin = Anchor.Centre,
        //        Blending = BlendingParameters.Additive,
        //        Depth = float.MaxValue,
        //        Alpha = 0
        //    });
        //}

        //public override void Apply(JudgementResult result, DrawableHitObject? judgedObject)
        //{
        //    base.Apply(result, judgedObject);

        //    //if (judgedObject is not DrawableOsuHitObject osuObject)
        //    //    return;


        //    screenSpacePosition = judgedObject.ToScreenSpace(judgedObject.OriginPosition);

        //    //Scale = ;
        //}

        //protected override void PrepareForUse()
        //{
        //    base.PrepareForUse();

        //    Lighting.ResetAnimation();
        //    Lighting.SetColourFrom(this, Result);
        //    Position = Parent!.ToLocalSpace(screenSpacePosition);
        //}

        //protected override void ApplyHitAnimations()
        //{
        //    bool hitLightingEnabled = config.Get<bool>(OsuSetting.HitLighting);

        //    Lighting.Alpha = 0;

        //    if (hitLightingEnabled)
        //    {
        //        // todo: this animation changes slightly based on new/old legacy skin versions.
        //        Lighting.ScaleTo(0.8f).ScaleTo(1.2f, 600, Easing.Out);
        //        Lighting.FadeIn(200).Then().Delay(200).FadeOut(1000);

        //        // extend the lifetime to cover lighting fade
        //        LifetimeEnd = Lighting.LatestTransformEndTime;
        //    }

        //    base.ApplyHitAnimations();
        //}

        //protected override Drawable CreateDefaultJudgement(HitResult result) => new OsuJudgementPiece(result);

        //private partial class OsuJudgementPiece : DefaultJudgementPiece
        //{
        //    public OsuJudgementPiece(HitResult result)
        //        : base(result)
        //    {
        //    }

        //    public override void PlayAnimation()
        //    {
        //        if (Result != HitResult.Miss)
        //        {
        //            JudgementText
        //                .ScaleTo(new Vector2(0.8f, 1))
        //                .ScaleTo(new Vector2(1.2f, 1), 1800, Easing.OutQuint);
        //        }

        //        base.PlayAnimation();
        //    }
        //}

    }
}
