﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// Thickness类型动画构建器
    /// </summary>
    /// <param name="propertyPath">关联属性</param>
    public class DanceThicknessAnimationBuilder(string propertyPath) : DanceAnimationBuilderProperty<Thickness>(propertyPath)
    {
        /// <summary>
        /// 构建
        /// </summary>
        /// <returns>建时间线</returns>
        public override Timeline Build()
        {
            ThicknessAnimationUsingKeyFrames timeline = new();
            timeline.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(this.PropertyPath));

            foreach (var keyFrame in this.KeyFrames)
            {
                timeline.KeyFrames.Add(new EasingThicknessKeyFrame(keyFrame.Value.Value, KeyTime.FromTimeSpan(keyFrame.Key), this.Easing));
            }

            return timeline;
        }
    }
}
